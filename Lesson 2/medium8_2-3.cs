using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    static UiElementsCollection ui;
    static string msg = string.Empty;

    static void Main(string[] args)
    {
        ui = new UiElementsCollection();

        ui.Add(new TextField("Burger reciper 3000"));
        ui.Add(new TextField("Enter burger name:"));
        ui.Add(new TextInputField("", 50));
        ui.Add(new Checkbox("Salad", false));
        ui.Add(new Checkbox("Cheese", false));
        ui.Add(new Checkbox("Onion", false));
        ui.Add(new Checkbox("Beacon", false));
        ui.Add(new Checkbox("Tomato", false));
        ui.Add(new Checkbox("Pickles", false));
        ui.Add(new Checkbox("Chili Pepper", false));
        ui.Add(new Checkbox("Mushrooms", false));
        ui.Add(new Checkbox("Katchup", false));
        ui.Add(new Checkbox("Mustard", false));
        ui.Add(new Checkbox("Mayonnaise", false));
        ui.Add(new Button("Print recipe", () => msg = "New recipe saved (not really)"));

        ui.Render();
        if (msg != string.Empty)
        {
            Console.WriteLine(msg);
            msg = string.Empty;
        }
        ConsoleAction(Console.ReadKey());
    }

    static void ConsoleAction(ConsoleKeyInfo key)
    {
        if (key.Key == ConsoleKey.UpArrow)
        {
            ui.SelectionUp();
        }

        if (key.Key == ConsoleKey.DownArrow)
        {
            ui.SelectionDown();
        }

        if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar)
        {
            ui.InputEnter();
        }

        if (key.Key == ConsoleKey.Backspace)
        {
            ui.InputBackspace();
        }

        if (char.IsLetterOrDigit(key.KeyChar))
        {
            ui.InputChar(key.KeyChar);
        }

        ui.Render();

        ConsoleAction(Console.ReadKey());
    }
}

class UiElementsCollection
{
    private List<UiElement> elements = new List<UiElement>();
    private List<UiElement> selectableElements = new List<UiElement>();

    private int selectedElementIndex => elements.IndexOf(selectableElements[innerIndex]);

    private int innerIndex = 0;

    public void Render()
    {
        Console.Clear();

        for (int i = 0; i < elements.Count; i++)
        {
            if (i == selectedElementIndex)
                Console.BackgroundColor = ConsoleColor.DarkGreen;

            elements[i].Render();
            if (i == selectedElementIndex)
                Console.BackgroundColor = ConsoleColor.Black;
        }
    }

    public void Add(UiElement element)
    {
        elements.Add(element);
        if (element.IsSelectable)
            selectableElements.Add(element);
    }


    public void InputEnter()
    {
        selectableElements[innerIndex].OnEnter();
    }

    public void InputBackspace()
    {
        selectableElements[innerIndex].OnBackspace();
    }

    public void InputChar(char c)
    {
        selectableElements[innerIndex].OnCharInput(c);
    }

    public void SelectionUp()
    {
        if (innerIndex - 1 < 0)
        {
            innerIndex = selectableElements.Count - 1;
        }
        else
        {
            innerIndex--;
        }
    }

    public void SelectionDown()
    {
        if (innerIndex >= selectableElements.Count - 1)
        {
            innerIndex = 0;
        }
        else
        {
            innerIndex++;
        }
    }

}

class TextField : UiElement
{
    private string _text;

    public TextField(string text)
    {
        IsSelectable = false;
        _text = text;
    }

    public override void Render()
    {
        Console.WriteLine(_text);
    }
}

class Checkbox : UiElement
{
    private string _text;
    public bool Value { get; private set; }

    public Checkbox(string text, bool @default)
    {
        IsSelectable = true;
        _text = text;
        Value = @default;
    }

    public override void OnEnter()
    {
        Switch();
    }

    public override void Render()
    {
        Console.WriteLine("[" + ((Value) ? "x" : " ") + "]" + " " + _text);
    }

    public void Switch()
    {
        Value = !Value;
    }
}

class TextInputField : UiElement
{
    public string Text { get; private set; }
    private int _length;

    public TextInputField(string text, int maxLength)
    {
        IsSelectable = true;
        Text = text;
        _length = maxLength;
    }

    public override void Render()
    {
        string line = string.Empty;
        for (int i = 0; i < _length + 4; i++)
            line += '-';

        Console.WriteLine(line);
        Console.WriteLine(Text);
        Console.WriteLine(line);
    }

    public override void OnEnter()
    {
        TryAddChar(' ');
    }

    public override void OnBackspace()
    {
        TryRemoveChar();
    }

    public override void OnCharInput(char c)
    {
        TryAddChar(c);
    }

    public void TryAddChar(char c)
    {
        if (Text.Length < _length)
            Text += c;
    }

    public void TryRemoveChar()
    {
        if (Text.Length > 0)
            Text.Remove(Text.Length - 2);
    }
}

class Button : UiElement
{
    public string Text { get; private set; }
    private Action _onClick;

    public Button(string text, Action onClick)
    {
        IsSelectable = true;
        Text = text;

        _onClick = onClick;
    }

    public override void OnEnter()
    {
        _onClick?.Invoke();
    }

    public override void Render()
    {
        string line = string.Empty;
        for (int i = 0; i < Text.Length + 4; i++)
            line += '#';

        Console.WriteLine(line);
        Console.WriteLine("# " + Text + " #");
        Console.WriteLine(line);
    }
}

abstract class UiElement
{
    public bool IsSelectable;

    abstract public void Render();

    virtual public void OnCharInput(char c) { }
    virtual public void OnEnter() { }
    virtual public void OnBackspace() { }
}

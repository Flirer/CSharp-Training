using System;
using System.Collections.Generic;
using System.Linq;

class Bag
{
    private List<Item> Items = new List<Item>();
    public readonly int MaxWeidth;
    private int _currentWeidth;

    public int CurrentWeigth { get => _currentWeidth; }

    public Bag(int maxWidth)
    {
        if (maxWidth < 0)
            throw new InvalidOperationException();

        MaxWeidth = maxWidth;
        _currentWeidth = 0;
    }

    public void AddItem(Item item)
    {
        Item targetItem = TryToGetItem(item.Name);

        if (_currentWeidth + item.Count > MaxWeidth)
            throw new InvalidOperationException();

        if (targetItem == null)
        {
            targetItem = item;
            Items.Add(targetItem);
        }
        else
        {
            targetItem.Count += item.Count;
        }
        _currentWeidth += item.Count;
    }

    public bool IsHoldingItem(Item item)
    {
        Item targetItem = TryToGetItem(item.Name);

        if (targetItem != null)
        {
            if (targetItem.Count >= item.Count)
                return true;
        }

        return false;
    }

    public void RemoveItem(Item item)
    {
        Item targetItem = TryToGetItem(item.Name);

        if (targetItem != null)
        {
            if (targetItem.Count > item.Count)
            {
                targetItem.Count -= item.Count;
                _currentWeidth -= item.Count;
            }
            else if (targetItem.Count == item.Count)
            {
                Items.Remove(targetItem);
                _currentWeidth -= item.Count;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }

    public void MoveItemToAnotherBag(Item item, Bag bag)
    {
        if (bag.CanTakeItem(item))
        {
            RemoveItem(item);
            bag.AddItem(item);
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    public bool CanTakeItem(Item item)
    {
        if (_currentWeidth + item.Count < MaxWeidth)
            return true;
        else
            return false;
    }

    Item TryToGetItem(string name)
    {
        return Items.FirstOrDefault(i => i.Name == name);
    }
}

class Item
{
    public readonly string Name;
    private int _count;

    public int Count { get => _count; set => _count = value; }

    private Item(string name, int count)
    {
        Name = name;
        _count = count;
    }

    public static Item CreateItem(string name, int count)
    {
        if (string.IsNullOrEmpty(name))
            throw new InvalidOperationException();

        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidOperationException();

        if (count < 1)
            throw new InvalidOperationException();

        return new Item(name, count);
    }
}
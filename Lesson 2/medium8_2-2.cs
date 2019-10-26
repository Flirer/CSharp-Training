using System;
using System.Collections.Generic;
using System.Linq;


class Bank
{
    private List<Account> _accounts;
    private Stack<List<Account>> _states;

    public Bank()
    {
        _accounts = new List<Account>();
        _states = new Stack<List<Account>>();
    }

    public void AddAccount(string name, float amount)
    {
        if (_accounts.Exists((account) => account.Name == name.ToUpperInvariant()))
            throw new Exception("Alredy containt account with name " + name);

        SaveState();
        _accounts.Add(new Account(name.ToUpperInvariant(), amount));
    }

    public void MoveCredits(string nameFrom, string nameTo, float amount)
    {
        Account from = GetAccount(nameFrom);
        Account to = GetAccount(nameTo);

        SaveState();

        from.WithdrawCredits(amount);
        to.DepositCredits(amount);
    }

    public void CloseAccount(string name)
    {
        Account account = GetAccount(name);

        SaveState();

        account.CloseAccount();
    }

    private Account GetAccount(string name)
    {
        Account result = _accounts.FirstOrDefault((account) => account.Name == name);

        if (result == null)
            throw new Exception("No account with name " + name + " is presented.");

        return result;
    }

    private void SaveState()
    {
        _states.Push(new List<Account>(_accounts));
    }

    public void Undo()
    {
        _accounts = _states.Pop();
    }
}

class Account
{
    public string Name { get; private set; }
    public float Amount { get; private set; }
    public bool IsActive { get; private set; }

    public Account(string name, float amount)
    {
        Name = name;
        Amount = amount;
        IsActive = true;
    }

    public void DepositCredits(float amount)
    {
        Amount += amount;
    }

    public void WithdrawCredits(float amount)
    {
        Amount -= amount;
    }

    public void CloseAccount()
    {
        IsActive = false;
    }
}
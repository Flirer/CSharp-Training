using System;

class Wombat : Creature
{
    public int Armor;

    public override void TakeDamage(int damage)
    {
        Health -= damage - Armor;
        base.TakeDamage(damage);
    }
}

class Human : Creature
{
    public int Agility;

    public override void TakeDamage(int damage)
    {
        Health -= damage / Agility;
        base.TakeDamage(damage);
    }
}

abstract class Creature
{
    public int Health;

    virtual public void TakeDamage(int damage)
    {
        if (Health <= 0)
        {
            Console.WriteLine("Я умер");
        }
    }
}
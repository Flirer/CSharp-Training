using System;

class Wombat : Creature
{
    public int Armor;

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage - Armor);
    }
}

class Human : Creature
{
    public int Agility;

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage / Agility);
    }
}

abstract class Creature
{
    public int Health;

    virtual public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Console.WriteLine("Я умер");
        }
    }
}
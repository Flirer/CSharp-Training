using System;

class Player
{
    public string Name { get; private set; }
    public int Age { get; private set; }

    private Weapon _weapon;
    private Movement _movment;

    public Player(string name, int age, Movement moving, Weapon weapon)
    {
        _weapon = weapon;
        _movment = moving;
    }

    public void Attack()
    {
        _weapon.Attack();
    }

    public bool IsReloading()
    {
        return _weapon.IsReloading();
    }
}

class Weapon
{
    public float Cooldown { get; private set; }
    public int Damage { get; private set; }

    public Weapon(int damage, float cooldown)
    {
        Damage = damage;
        Cooldown = cooldown;
    }

    public void Attack()
    {
        //attack
    }

    public bool IsReloading()
    {
        throw new NotImplementedException();
    }
}

class Movement
{
    public float DirectionX { get; private set; }
    public float DirectionY { get; private set; }
    public float Speed { get; private set; }

    public Movement(float speed)
    {
        Speed = speed;
    }

    public void Move()
    {
        //Do move
    }
}
using System;

class Player
{
    public string Name { get; private set; }
    public int Age { get; private set; }

    private Weapon _weapon;
    private Movment _movment;

    public Player(string name, int age, Movment moving, Weapon weapon)
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
    public float WeaponCooldown { get; private set; }
    public int WeaponDamage { get; private set; }

    public Weapon(int damage, float cooldown)
    {
        WeaponDamage = damage;
        WeaponCooldown = cooldown;
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

class Movment
{
    public float MovementDirectionX { get; private set; }
    public float MovementDirectionY { get; private set; }
    public float MovementSpeed { get; private set; }

    public Movment(float speed)
    {
        MovementSpeed = speed;
    }

    public void Move()
    {
        //Do move
    }
}
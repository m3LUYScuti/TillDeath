using System;

abstract class Weapon
{
    public string WeaponName { get; set; }
    public int Damage { get; set; }

    public Weapon(string name, int damage)
    {
        WeaponName = name;
        Damage = damage;
    }

    public abstract int GiveDamage();
}

class Sword : Weapon
{
    public Sword(int damage) : base("Sword", damage) { }

    public override int GiveDamage()
    {
        return Damage;
    }
}

class Bow : Weapon
{
    public Bow(int damage) : base("Bow", damage) { }

    public override int GiveDamage()
    {
        return Damage;
    }
}

class Axe : Weapon
{
    public Axe(int damage) : base("Axe", damage) { }

    public override int GiveDamage()
    {
        return Damage;
    }
}

class Staff : Weapon
{
    public Staff(int damage) : base("Staff", damage) { }

    public override int GiveDamage()
    {
        return Damage;
    }
}

class Character
{
    public string Name { get; set; }
    public int Health { get; set; }
    public Weapon EquippedWeapon { get; set; }

    public Character(string name, Weapon weapon)
    {
        Name = name;
        Health = 100;
        EquippedWeapon = weapon ?? throw new ArgumentNullException("Weapon cannot be null!");
    }

    public void Hit(Character target)
    {
        int damage = EquippedWeapon.GiveDamage();
        target.TakeDamage(damage);
        Console.WriteLine($"{Name} hits {target.Name} for {damage}, {target.Name}'s health is down to {target.Health}");
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            Console.WriteLine($"{Name} has been defeated!");
        }
    }

    public bool IsAlive()
    {
        return Health > 0;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Random rand = new Random();

        Console.Write("Enter Character 1 Name: ");
        string char1Name = Console.ReadLine();

        Console.Write("Enter Character 2 Name: ");
        string char2Name = Console.ReadLine();

        Weapon char1Weapon = GetWeaponFromUser(char1Name, rand);
        Weapon char2Weapon = GetWeaponFromUser(char2Name, rand);

        Character character1 = new Character(char1Name, char1Weapon);
        Character character2 = new Character(char2Name, char2Weapon);

        Console.WriteLine($"Combat begins between {char1Name} and {char2Name}!");

        // Randomize starting character
        bool isChar1Turn = rand.Next(0, 2) == 0;

        while (character1.IsAlive() && character2.IsAlive())
        {
            if (isChar1Turn)
            {
                character1.Hit(character2);
            }
            else
            {
                character2.Hit(character1);
            }

            // Alternate turns
            isChar1Turn = !isChar1Turn;

            System.Threading.Thread.Sleep(1000);
        }

        if (character1.IsAlive())
        {
            Console.WriteLine($"{char1Name} is victorious!");
        }
        else
        {
            Console.WriteLine($"{char2Name} is victorious!");
        }
    }

    static Weapon GetWeaponFromUser(string characterName, Random rand)
    {
        Console.WriteLine($"{characterName}, choose your weapon by entering a number:");
        Console.WriteLine("1) Sword\n2) Bow\n3) Axe\n4) Staff");

        while (true)
        {
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    return new Sword(rand.Next(20, 46));
                case "2":
                    return new Bow(rand.Next(20, 46));
                case "3":
                    return new Axe(rand.Next(20, 46));
                case "4":
                    return new Staff(rand.Next(20, 46));
                default:
                    Console.WriteLine("Invalid choice, please enter a number between 1 and 4.");
                    break;
            }
        }
    }
}

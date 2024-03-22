using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_CSharp
{
    abstract public class Item
    {
        public string name;

        public Item(string name)
        {
            this.name = name;
        }
    }

    class HealingItem : Item
    {
        int HealAmount;
        int HealType;

        public HealingItem(string name, int healAmount, int healType) : base(name)
        {
            this.name = name;
            this.HealAmount = healAmount;
            this.HealType = healType;
        }
    }

    class Armor : Item
    {
        int Defense;

        public Armor(string name, int defense) : base(name)
        {
            this.name = name;
            this.Defense = defense;
        }
    }

    class Weapon : Item
    {
        int Attack;

        public Weapon(string name, int attack) : base(name)
        {
            this.name = name;
            this.Attack = attack;
        }
    }
}

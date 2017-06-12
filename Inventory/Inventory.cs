using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory
{
    public class Item
    {
        protected string name,
                         description;
        protected double weight;


        public Item(string name, string description, double weight)
        {
            this.name = name;
            this.description = description;
            this.weight = weight;
        }

        public string getName()
        {
            return this.name;
        }

        public string getDescription()
        {
            return this.description;
        }

        public double getWeight()
        {
            return weight;
        }

        public class Consumable : Item
        {
            protected int healHP,
                          attackBoost,
                          defenseBoost,
                          speedBoost,
                          willBoost,
                          healMP;

            public Consumable(string name, string description, double weight, int[] stats) : base(name, description, weight)
            {
                if (stats.Length == 6)
                {
                    healHP = stats[0];
                    attackBoost = stats[1];
                    defenseBoost = stats[2];
                    speedBoost = stats[3];
                    willBoost = stats[4];
                    healMP = stats[5];
                }
                else
                {
                    Console.WriteLine("There was a problem creating an item");
                    this = null;
                }
            }
        }

        public class Weapon : Item
        {
            public Weapon(string name, string description, double weight) : base(name, description, weight)
            {
                // NOTHING TO DO HERE
            }
        }

        public class Armor : Item
        {
            public Armor(string name, string description, double weight) : base(name, description, weight)
            {

            }
        }

        public class ItemList
        {
            List<Item> items;

            public ItemList()
            {
                items = new List<Item>();
            }

            public void add(Item item)
            {
                items.Add(item);
                items.Sort();
            }

            public Item getItem(string name)
            {
                for(int i = 0; i < items.Count(); i++)
                {
                    if(items.ElementAt(i).getName().CompareTo(name) == 0)
                    {
                        return items.ElementAt(i);
                    }
                }

                return null;
            }

            public void print()
            {
                for(int i = 0; i < items.Count(); i++)
                {
                    Console.WriteLine(items.ElementAt(i).getName());
                }
            }
        }
    }

    public class Backpack
    {
        List<Item.Consumable> consumables;
        List<Item.Weapon> weapons;
        List<Item.Armor> armors;
        private double totalWeight;

        public Backpack()
        {
            consumables = new List<Item.Consumable>();
            weapons = new List<Item.Weapon>();
            armors = new List<Item.Armor>();
            totalWeight = 0.0;
        }

        public void add(Item item)
        {
            if (item.GetType() == typeof(Item.Consumable))
            {
                consumables.Add((Item.Consumable)item);
                consumables.Sort();
            }
            else if (item.GetType() == typeof(Item.Weapon))
            {
                weapons.Add((Item.Weapon)item);
                weapons.Sort();
            }
            else if (item.GetType() == typeof(Item.Armor))
            {
                armors.Add((Item.Armor)item);
                armors.Sort();
            }

            totalWeight += item.getWeight();
        }

        public void useItem(string player, Item item)
        {
            // APPLY STATS TO PLAYER
            discard(item);
        }

        public void discard(Item item)
        {
            bool removed = false;

            if (item.GetType() == typeof(Item.Consumable))
            {
                removed = consumables.Remove((Item.Consumable)item);
                consumables.Sort();
            }
            else if (item.GetType() == typeof(Item.Weapon))
            {
                removed = weapons.Remove((Item.Weapon)item);
                weapons.Sort();
            }
            else if (item.GetType() == typeof(Item.Armor))
            {
                removed = armors.Remove((Item.Armor)item);
                armors.Sort();
            }
            
            if (removed)
            {
                totalWeight -= item.getWeight();
            }
        }

        public double getTotalWeight()
        {
            return totalWeight;
        }

        public void print()
        {
            string consumable;
            string weapon;
            string armor;

            consumables.Sort();
            weapons.Sort();
            armors.Sort();

            for(int i = 0, c = 0, w = 0, a = 0; i < consumables.Count + weapons.Count + armors.Count; i++)
            {
                consumable = consumables.ElementAt(c).getName();
                weapon = weapons.ElementAt(w).getName();
                armor = armors.ElementAt(a).getName();

                if(consumable.CompareTo(weapon) < 0 && consumable.CompareTo(armor) < 0)
                {
                    Console.WriteLine(consumable);
                    c++;
                }
                else if(weapon.CompareTo(consumable) < 0 && weapon.CompareTo(armor) < 0)
                {
                    Console.WriteLine(weapon);
                    w++;
                }
                else if(armor.CompareTo(consumable) < 0 && armor.CompareTo(weapon) < 0)
                {
                    Console.WriteLine(armor);
                    a++;
                }
            }
        }

        public static void Main(string[] args)
        {
            Backpack backpack = new Backpack();
            backpack.add(new Item.Consumable("Potion", "Heals 5 HP", 0.5, new int[] { 5, 0, 0, 0, 0, 0, 7, 7, 7 }));
            backpack.add(new Item.Weapon("Broad Sword", "A standard blade", 5.5));
            backpack.print();
            Console.Read();
        }
    }
}

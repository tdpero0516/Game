using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Inventory
    {
        List<int> list;

        static void Main(string[] args)
        {
            Inventory backpack = new Inventory();
            backpack.add(5);
            backpack.print();
            Console.Read();
        }

        public Inventory()
        {
            list = new List<int>();
        }

        public void add(int value)
        {
            list.Add(value);
        }

        public void print()
        {
            for(int i = 0; i < list.Count; i++)
            {
                Console.Write(list.ElementAt(i) + " ");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ZuulCS
{
    class Inventory
    {
        private List<Item> items;
        public int maxWeight;
        public int weightLeft;

        internal List<Item> Items {get => items;}
        internal int WeightLeft { get => maxWeight - items.Count; }


        public Inventory(int amount)
        {
            maxWeight = amount;
            items = new List<Item>();
        }

        public bool addItem(Item item)
        {
            int currentWeight = 0;

            if(currentWeight < maxWeight)
            {
                for (int i = items.Count - 1; i >= 0; i--)
                {
                    currentWeight++;
                }
                items.Add(item);
                return (true);
            }
            return(false);
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);

        }

        public bool takeItem(Inventory other, string key)
        {
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i].Name == key)
                {
                    if (other.addItem(items[i]))
                    {
                        items.Remove(items[i]);
                        return true;
                    }
                }
            }
            return (false);
        }
    }
}

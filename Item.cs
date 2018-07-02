using System;
using System.Collections.Generic;
using System.Text;

namespace ZuulCS
{
    class Item
    {
        public string name;
        public string description;
        private int weight;

        public Item(string name, int weight, string description)
        {
            this.name = name;
            this.weight = weight;
            this.description = description;
        }

        public string Name
        {
            get { return name; }
        }

        public string getDescription
        {
            get { return description; }
        }

        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ZuulCS
{
    class Item
    {
        public string name;
        public string description;

        public Item(string name, string description)
        {
            this.name = name;
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
    }
}

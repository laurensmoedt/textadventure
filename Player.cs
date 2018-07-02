using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
    class Player
    {
        private Room _currentRoom;
        private int health = 10;
        private Inventory inventory;
        public int maxWeight;

        internal Inventory Inventory { get => inventory; }

        public Player()
        {
            this.maxWeight = 20;
            inventory = new Inventory();
        }

        public Room CurrentRoom
        {
            get { return _currentRoom; }
            set { _currentRoom = value; }
        }

        public int damage(int amount)
        {
            health = health - amount;
            return health;
        }

        private int heal(int amount)
        {
            health = health + amount;
            return health;
        }

        public void isAlive()
        {
            if (health <= 0)
            {
                Console.WriteLine("you died");
            }
        }

        public void takeItem()
        {
            
        }

        public void dropItem()
        {
            
        }

    }
}

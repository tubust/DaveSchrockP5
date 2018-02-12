using System;
using System.Collections.Generic;
using System.Text;

namespace DaveSchrockP5
{
    class BookInventory
    {
        private Books[] inventory = new Books[8];

        public Books this[int index]
        {
            set { inventory[index] = value; }
            get { return inventory[index]; }
        }

    }
}

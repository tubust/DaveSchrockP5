using System;
using System.Collections.Generic;
using System.Text;

namespace DaveSchrockP5
{
    class EventListner
    {
        private Books book;

        public EventListner(Books b)
        {
            book = b;
            book.Changed += new ChangedEventHandler(BookChanged);
        }

        private void BookChanged(object sender, EventArgs e)
        {
            if (book.BooksOnHand < 3)
            {
                Console.WriteLine("You have {0} books in inventory. You need to reorder.", book.BooksOnHand);
            }
        }
    }
}

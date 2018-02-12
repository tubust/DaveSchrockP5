using System;
using System.Collections.Generic;
using System.Text;

public delegate void ChangedEventHandler(object sender, EventArgs e);

namespace DaveSchrockP5
{
    class Books
    {
        private string isbn, title, author, publisher;
        private double wholesaleCost, markup;
        private static int counter;
        private int booksOnHand, booksSold;
        public event ChangedEventHandler Changed;



        public Books() {counter++; }

        public Books(string i, string t, string a, string p, double w, double m,int boh)
        {
            counter++;
            isbn = i;
            title = t;
            author = a;
            publisher = p;
            wholesaleCost = w;
            markup = m;
            booksOnHand = boh;
        }

        public string Isbn
        {
            set
            {
                isbn = value;
            }
            get
            {
                return isbn;
            }
        }

        public string Title
        {
            set { title = value; }
            get { return title; }
        }

        public string Author
        {
            set { author = value; }
            get { return author; }
        }

        public string Publisher
        {
            set { publisher = value; }
            get { return publisher; }
        }

        public double WholesaleCost
        {
            set { wholesaleCost = value; }
            get { return wholesaleCost; }
        }

        public double Markup
        {
            set { markup = value; }
            get { return markup; }
        }

        public int BooksOnHand
        {
            set
            {
                booksOnHand = value;
                OnChanged(EventArgs.Empty);
            }
            get { return booksOnHand; }
        }
        public int BooksSold
        {
            get { return booksSold; }
        }

        public int Counter
        {
            get { return counter; }
        }

        public static Books operator ++(Books b)
        {
            b.booksSold++;
            return b;
        }

        public double CalculateRetail
        {
           get{return (wholesaleCost + (wholesaleCost * markup));}
        }

        public void OnChanged(EventArgs e)
        {
            Changed(this, e);
        }
    }
}

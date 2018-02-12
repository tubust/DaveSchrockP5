
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DaveSchrockP5
{
    class Program
    {
        public static readonly DateTime today = DateTime.Now;
        public static int salesCounter = 0;
        private static BinaryFormatter formatter = new BinaryFormatter();
        private static FileStream file;
        [STAThread]
        static void Main(string[] args)
        {
            //
            // The first 2 lines delete the old records file
            //
            file = new FileStream("records.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite, 8, FileOptions.DeleteOnClose);
            file.Close();
            BookInventory totalInventory = new BookInventory();
            ArrayList BookSalesList = new ArrayList(1);
            int userEntry = 0;
            totalInventory[0] = new Books("0345417623","Programming for Fun", "Bill Gates", "Ballantine Books", 4.50, .45,3);
            totalInventory[1] = new Books("0283524862","PC Antics", "Steven Jobs", "Ballantine Books", 4.50, .24,10);
            totalInventory[2] = new Books("0440211457", "Chipless in Seattle", "John Jones", "Island Books", 3.00, .29,20);
            totalInventory[3] = new Books("2423568239", "Java By The Cupp", "Kathy Cupp", "OReily", 21.00, .35,2);
            totalInventory[4] = new Books("1579549543", "CPlusSharp", "Ann Confucious", "Rodale", 16.00, .45,100);
            totalInventory[5] = new Books("6546765548", "The Notebook", "Dean Dell", "Warner", 1.00, .30,1);
            totalInventory[6] = new Books("0966862309", "Apple vs PC", "Henry Wozniak", "Black Apple Press", 10.95, .37,18);
            totalInventory[7] = new Books("8898771672", "C# For Sharp People", "Bob Knife", "McGrew", 55.00, .45,35);
            EventListner[] listner = new EventListner[8];
            for (int f = 0; f < 8; f++)
            {
                listner[f] = new EventListner(totalInventory[f]);
            }
            Console.WriteLine("Dave Schrock's Bookstore");
            Console.WriteLine("Today is {0:D}", today);
            while (userEntry != 5)
            {
                displayMenu();
                bool getGoing = false;
                while (getGoing == false)
                {
                    try
                    {
                        userEntry = Int32.Parse(Console.ReadLine());
                        while (userEntry < 1 || userEntry > 5)
                        {
                            Console.Write("Error! Please enter 1 - 5: ");
                            userEntry = Int32.Parse(Console.ReadLine());
                        }
                        getGoing = true;
                    }
                    catch (FormatException)
                    {
                        Console.Write("Error! Please enter 1 - 5: ");
                    }
                }
                switch (userEntry)
                {
                    case 1:
                        {
                            displayAll(totalInventory);
                            break;
                        }
                    case 2:
                        {
                            purchaseBook(totalInventory);
                            break;
                        }
                    case 3:
                        {
                            listSales(totalInventory, salesCounter);
                            break;
                        }
                    case 4:
                        {
                            salesSummary(totalInventory, salesCounter);
                            break;
                        }
                    case 5: break;
                }
            }
            Console.WriteLine("Thank you for shopping at Dave's Bookstore. Please come again soon.");
            Console.ReadLine();
        }

        public static void purchaseBook(BookInventory b)
        {
            bool getGoing = false;
            bool getGoing2 = false;
            bool getGoing3 = false;
            bool getGoing4 = false;
            bool getGoing5 = false;
            char choice = 'x';
            int userChoice = 0, custId = 0, booksToPurchase = 0;
            while (getGoing == false)
            {
                Console.Write("Please enter your customer number: ");
                while (getGoing3 == false)
                {
                    try
                    {
                        custId = Int32.Parse(Console.ReadLine());
                        getGoing3 = true;
                    }
                    catch (FormatException)
                    {
                        Console.Write("Please enter the correct customer number: ");
                    }
                }
                for (int v = 0; v < b[0].Counter; v++)
                {
                    Console.WriteLine("{0} - {1,-35} {2,5:C}", (v + 1), b[v].Title, b[v].CalculateRetail);
                }
                Console.Write("Please enter the book you wish to purchase: ");
                try
                {
                    userChoice = Int32.Parse(Console.ReadLine());
                    while (getGoing2 == false)
                    {
                        if (userChoice < 1 || userChoice > b[0].Counter + 1)
                        {
                            Console.Write("Error! Please enter a number between 1 - {0}", (b[0].Counter + 1));
                            userChoice = Int32.Parse(Console.ReadLine());
                        }
                        else
                        {
                            getGoing2 = true;
                        }
                    }
                    getGoing = true;
                }
                catch (FormatException)
                {
                    Console.Write("Error! Please enter a number between 1 - {0}", (b[0].Counter + 1));
                    getGoing2 = false;
                }
                Console.Write("You have chosen the title {0} for {1:C}. Is this what you wanted to purchase? Press y or n: ", b[userChoice - 1].Title, b[userChoice - 1].CalculateRetail);
                do
                {
                    try
                    {
                        choice = char.Parse(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.Write("Error please enter a y or n: ");
                    }
                    switch (choice)
                    {
                        case 'y':
                            {
                                if (!(b[userChoice - 1].BooksOnHand == 0))
                                {
                                    Console.Write("How many of this title would you like to purchase? You can purchase up to {0} books: ", b[userChoice - 1].BooksOnHand);
                                    do
                                    {
                                        try
                                        {
                                            booksToPurchase = Int32.Parse(Console.ReadLine());
                                            getGoing4 = true;
                                        }
                                        catch (Exception)
                                        {
                                            Console.Write("Error! Please enter the number of books you wish to purchase: ");
                                        }
                                    } while (getGoing4 == false);
                                }
                                else
                                { booksToPurchase = 0; }
                                if (b[userChoice - 1].BooksOnHand > 0 && booksToPurchase <= b[userChoice - 1].BooksOnHand)
                                {
                                    for (int z = 0; z < booksToPurchase; z++)
                                    {
                                        b[userChoice - 1].BooksOnHand = (b[userChoice - 1].BooksOnHand - 1);
                                        b[userChoice - 1]++;
                                        BookSales temp = new BookSales(b[userChoice - 1].Isbn, custId);
                                        try
                                        {
                                            file = new FileStream("records.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                                            file.Seek(0, SeekOrigin.End);
                                            formatter.Serialize(file, temp);
                                            salesCounter++;
                                        }
                                        catch (FileNotFoundException e)
                                        {
                                            Console.WriteLine(e);
                                        }
                                        catch (SerializationException e)
                                        {
                                            Console.WriteLine(e);
                                        }
                                        finally
                                        {
                                            if (file != null)
                                            { file.Close(); }
                                        }
                                        Console.WriteLine("Sale Successful");
                                    }
                                }
                                else
                                {
                                    if (booksToPurchase == 0)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("This book is out of inventory. Please reorder. ");
                                        Console.WriteLine("Purchase Unsuccessful");
                                        Console.WriteLine();
                                    }
                                    else if(booksToPurchase >= b[userChoice - 1].BooksOnHand)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Oops! You purchased too many. Please reorder.");
                                        Console.WriteLine("Purchase Unsuccessful");
                                        Console.WriteLine();
                                    }
                                }
                                getGoing5 = true;
                                break;
                            }
                        case 'n':
                            {
                                Console.WriteLine();
                                Console.WriteLine("No Sale Made");
                                Console.WriteLine();
                                getGoing5 = true;
                                break;
                            }
                        default:
                            {
                                Console.Write("Please choose y or n: ");
                                break;
                            }
                    }
                } while (getGoing5 == false);
            }
        }
        public static void displayAll(BookInventory b)
        {
            Console.WriteLine();
            Console.WriteLine("TITLE                         COST");
            Console.WriteLine("------------------------------------------");
            for (int x = 0; x < b[0].Counter; x++)
            {
                Console.WriteLine("{0,-35} {1,5:C}", b[x].Title, b[x].CalculateRetail);
            }
            Console.WriteLine();
        }
        public static void listSales(BookInventory b, int s)
        {
            if (s != 0)
            {
                Console.WriteLine();
                BookSales[] temp = new BookSales[s];
                try
                {
                    file = new FileStream("records.dat", FileMode.Open, FileAccess.Read);
                    file.Seek(0, SeekOrigin.Begin);
                    for (int x = 0; x < s; x++)
                    {
                        temp[x] = (BookSales)formatter.Deserialize(file);
                    }
                }
                catch (SerializationException e)
                {
                    Console.WriteLine(e);
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    if (file != null)
                        file.Close();
                }
                Console.WriteLine("{0,-30} {1}", "TITLE", "RETAIL");
                Console.WriteLine("------------------------------------------");
                for (int y = 0; y < s; y++)
                {
                    for (int z = 0; z < b[0].Counter; z++)
                    {
                        if (temp[y].Isbn == b[z].Isbn)
                        {
                            Console.WriteLine("{0,-35} {1,5:C}", b[z].Title, b[z].CalculateRetail);
                        }
                    }
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("No sales to display");
                Console.WriteLine();
            }
        }
        public static void salesSummary(BookInventory b, int s)
        {
            double retailTotal = 0, wholesaleTotal = 0, profitTotal = 0;
            if (s != 0)
            {
                Console.WriteLine();
                Console.WriteLine("RETAIL                 WHOLESALE                 PROFIT");
                Console.WriteLine("---------------------------------------------------------------------");
                BookSales[] temp = new BookSales[s];
                try
                {
                    file = new FileStream("records.dat", FileMode.Open, FileAccess.Read);
                    file.Seek(0, SeekOrigin.Begin);
                    for (int x = 0; x < s; x++)
                    {
                        temp[x] = (BookSales)formatter.Deserialize(file);
                    }
                }
                catch (SerializationException e)
                {
                    Console.WriteLine(e);
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    if (file != null)
                        file.Close();
                }
                for (int y = 0; y < s; y++)
                {
                    for (int z = 0; z < b[0].Counter; z++)
                    {
                        if (temp[y].Isbn == b[z].Isbn)
                        {
                            Console.WriteLine("{0,20:C} {1,20:C} {2,20:C}", b[z].CalculateRetail, b[z].WholesaleCost, (b[z].CalculateRetail - b[z].WholesaleCost));
                            retailTotal = retailTotal + b[z].CalculateRetail;
                            wholesaleTotal = wholesaleTotal + b[z].WholesaleCost;
                            profitTotal = profitTotal + (b[z].CalculateRetail - b[z].WholesaleCost);
                        }
                    }
                }
                Console.WriteLine();
                Console.WriteLine("TOTAL");
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine("{0,20:C} {1,20:C} {2,20:C}", retailTotal, wholesaleTotal, profitTotal);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("No sales to display");
                Console.WriteLine();
            }
         }
        public static void displayMenu()
        {
                Console.WriteLine("1 - See all book titles in inventory and their individual retail prices");
                Console.WriteLine("2 - Purchase a book");
                Console.WriteLine("3 - See a list of all books sold");
                Console.WriteLine("4 - See Sales summary");
                Console.WriteLine("5 - Exit");
                Console.WriteLine();
                Console.Write("Please enter your selection: ");
        }
    }
}
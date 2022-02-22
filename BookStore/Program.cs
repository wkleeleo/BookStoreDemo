using System;
using System.Collections.Generic;

namespace BookStore
{
    /// <summary>
    /// This is the main entry point of the project
    /// </summary>
    class Program
    {
        static int tableWidth = 100;
        static void Main(string[] args)
        {
            decimal SubTotal = 0;
            decimal Total = 0;
            decimal TotalWithGST = 0;
            decimal DeliveryFee = 0;
            decimal Gst = 0;

            //create list of genre
            List<Genre> GenreList = new List<Genre>();
            GenreList.Add(new Genre("Crime", (decimal)0.05));
            GenreList.Add(new Genre("Fantasy", (decimal)0.00));
            GenreList.Add(new Genre("Romance", (decimal)0.00));

            //create list of book
            List<Book> BookList = new List<Book>();
            BookList.Add(new Book("Unsolved murders", "Emily G.Thompson, Amber Hunt", GenreList.Find(g => g.Name == "Crime"), (decimal)10.99));
            BookList.Add(new Book("Alice in Wonderland", "Lewis Carroll", GenreList.Find(g => g.Name == "Fantasy"), (decimal)5.99));
            BookList.Add(new Book("A Little Love Story", "Roland Merullo", GenreList.Find(g => g.Name == "Romance"), (decimal)2.40));
            BookList.Add(new Book("Heresy", "S J Parris", GenreList.Find(g => g.Name == "Fantasy"), (decimal)6.80));
            BookList.Add(new Book("The Neverending Story", "Michael Ende", GenreList.Find(g => g.Name == "Fantasy"), (decimal)7.99));
            BookList.Add(new Book("Jack the Ripper", "Philip Sugden", GenreList.Find(g => g.Name == "Crime"), (decimal)16.00));
            BookList.Add(new Book("The Tolkien Years", "Greg Hildebrandt", GenreList.Find(g => g.Name == "Fantasy"), (decimal)22.90));

            //create a cart
            Cart cart = new Cart(GenreList);
            cart.AddItem(BookList.Find(b => b.Title == "Unsolved murders"), 1);
            cart.AddItem(BookList.Find(b => b.Title == "A Little Love Story"), 1);
            cart.AddItem(BookList.Find(b => b.Title == "Heresy"), 1);
            cart.AddItem(BookList.Find(b => b.Title == "Jack the Ripper"), 1);
            cart.AddItem(BookList.Find(b => b.Title == "The Tolkien Years"), 1);

            //calculate cost
            SubTotal = cart.CalculateTotal();
            Gst = cart.CalculateTax(SubTotal);
            DeliveryFee = cart.CalculateDelivery(SubTotal);

            Total = SubTotal + DeliveryFee;
            TotalWithGST = SubTotal + Gst + DeliveryFee;

            //print table
            Console.Clear();
            PrintLine();
            PrintRow("Title", "Quantity", "Unit Price", "Discount");
            PrintLine();
            foreach (KeyValuePair<Book, int> item in cart.cartItems)
            {
                decimal discountPrice = (item.Key.Price * (1 - item.Key.Genre.Discount)) * item.Value;
                string discountPriceText = (item.Key.Genre.Discount * 100).ToString() + " %";
                // do something with entry.Value or entry.Key
                PrintRow(item.Key.Title, item.Value.ToString(), item.Key.Price.ToString(), discountPriceText);
            }
            
            PrintLine();
            PrintLine();
            PrintRow("",  "Amount");
            PrintLine();
            PrintRow("Total without GST", Total.ToString());
            PrintRow("Total with GST",TotalWithGST.ToString());
            Console.ReadLine();
        }

        /// <summary>
        /// This method print table line to the console
        /// </summary>
        static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        /// <summary>
        /// This method print a table row
        /// </summary>
        /// <param name="columns"></param>
        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        /// <summary>
        /// This method align the text to centre
        /// </summary>
        /// <param name="text"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    /// <summary>
    /// The Book class contains all properties required for this book store
    /// </summary>
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public Genre Genre { get; set; }
        public decimal Price { get; set; }

        /// <summary>
        /// This constructor initializes the new Book
        /// </summary>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <param name="genre"></param>
        /// <param name="price"></param>
        public Book(string title, string author, Genre genre, decimal price)
        {
            Title = title;
            Author = author;
            Genre = genre;
            Price = price;
        }

    }
}

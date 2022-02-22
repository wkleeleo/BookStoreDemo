using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    /// <summary>
    /// This Genre class includes the name and the discount
    /// </summary>
    public class Genre
    {
        string name { get; set; }
        decimal discount { get; set; }

        /// <summary>
        /// This constructor initializes the new Genre
        /// </summary>
        /// <param name="name"></param>
        /// <param name="discount"></param>
        public Genre(string name, decimal discount)
        {
            this.name = name;
            this.discount = discount;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public decimal Discount
        {
            get { return discount; }
            set { discount = value; }
        }

    }
}

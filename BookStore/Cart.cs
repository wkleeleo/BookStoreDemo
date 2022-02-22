using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    /// <summary>
    /// The Cart class
    /// Contains all methods for calculating the total order
    /// </summary>
    public class Cart
    {
        private static decimal GST = (decimal)0.1;
        private static decimal DeliveryFee = (decimal)5.95;
        private static decimal FreeDeliveryAmount = (decimal)20.00;

        public Dictionary<Book, int> cartItems;
        public List<Genre> Genres;

        public Dictionary<Book, int> CartItems
        {
            get { return cartItems; }
            set { cartItems = value; }
        }

        /// <summary>
        /// This constructor initializes the new Cart
        /// </summary>
        /// <param name="genres"></param>
        public Cart(List<Genre> genres)
        {
            cartItems = new Dictionary<Book, int>();
            Genres = genres;
        }

        /// <summary>
        /// This method add an item to the cart
        /// </summary>
        /// <param name="book"></param>
        /// <param name="count"></param>
        public void AddItem(Book book, int count)
        {
            if(count > 0)
            {
                if (this.cartItems.ContainsKey(book))
                {
                    cartItems[book] = cartItems[book] + count;
                }
                else
                {
                    cartItems.Add(book, count);
                }
            }
            
        }

        /// <summary>
        /// This method remove an item from the cart
        /// </summary>
        /// <param name="book"></param>
        /// <param name="count"></param>
        public void RemoveItem(Book book, int count)
        {
            if (count > 0)
            {
                if (this.cartItems.ContainsKey(book))
                {
                    if (cartItems[book] > count)
                    {
                        cartItems[book] = cartItems[book] - count;
                    }
                    else
                    {
                        cartItems.Remove(book);
                    }

                }
            }
        }

        /// <summary>
        /// This method calculate the order total, applying discount if required
        /// </summary>
        /// <returns>Total price of an order</returns>
        public decimal CalculateTotal()
        {
            decimal Total = 0;
            foreach (KeyValuePair<Book, int> item in cartItems)
            {
                Book book = item.Key;
                decimal itemPrice = book.Price;
                decimal discount = book.Genre.Discount;
                int itemCount = item.Value;

                Total += (item.Key.Price * (1 - discount)) * itemCount;
                // do something with entry.Value or entry.Key
            }
            return Total;
        }

        /// <summary>
        /// This method calculate the gst of the order
        /// </summary>
        /// <param name="total"></param>
        /// <returns>GST of the total price</returns>
        public decimal CalculateTax(decimal total)
        {
            decimal Tax = total * GST;

            return Tax;
        }

        /// <summary>
        /// This method calculate the shipping cost
        /// </summary>
        /// <param name="total"></param>
        /// <returns>Shipping cost of the order</returns>
        public decimal CalculateDelivery(decimal total)
        {

            if(Decimal.Compare(FreeDeliveryAmount, total) > 0)
            {
                return DeliveryFee;
            }
            return 0;
        }

    }
}

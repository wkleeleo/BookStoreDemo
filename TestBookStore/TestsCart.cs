using NUnit.Framework;

using System.Collections.Generic;
using BookStore;

namespace TestBookStore
{
    public class TestsCart
    {
        private List<Book> _bookList;
        private List<Genre> _genreList;
        private Cart _cart;
        [SetUp]
        public void Setup()
        {
            _genreList = new List<Genre>();
            _genreList.Add(new Genre("Crime", (decimal)0.05));
            _genreList.Add(new Genre("Fantasy", (decimal)0.00));
            _genreList.Add(new Genre("Romance", (decimal)0.00));

            _bookList = new List<Book>();
            _bookList.Add(new Book("Unsolved murders", "Emily G.Thompson, Amber Hunt", _genreList.Find(g => g.Name == "Crime"), (decimal)10.99));
            _bookList.Add(new Book("Alice in Wonderland", "Lewis Carroll", _genreList.Find(g => g.Name == "Fantasy"), (decimal)5.99));
            _bookList.Add(new Book("A Little Love Story", "Roland Merullo", _genreList.Find(g => g.Name == "Romance"), (decimal)2.40));
            _bookList.Add(new Book("Heresy", "S J Parris", _genreList.Find(g => g.Name == "Fantasy"), (decimal)6.80));
            _bookList.Add(new Book("The Neverending Story", "Michael Ende", _genreList.Find(g => g.Name == "Fantasy"), (decimal)7.99));
            _bookList.Add(new Book("Jack the Ripper", "Philip Sugden", _genreList.Find(g => g.Name == "Crime"), (decimal)16.00));
            _bookList.Add(new Book("The Tolkien Years", "Greg Hildebrandt", _genreList.Find(g => g.Name == "Fantasy"), (decimal)22.90));

            _cart = new Cart(_genreList);
            
        }

        [Test]
        [TestCase(-1, 0)]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 1)]
        public void CartAddItemTest(int add, int result)
        {
            _cart.AddItem(_bookList.Find(b => b.Title == "Unsolved murders"), add);
            Assert.AreEqual(_cart.cartItems.Count, result);
        }

        [Test]
        [TestCase(2, -1, 2)]
        [TestCase(2, 1, 1)]
        [TestCase(1, 1, 0)]
        [TestCase(1, 2, 0)]
        public void CartRemoveItemTest(int addNum, int removeNum, int result)
        {
            var book = _bookList.Find(b => b.Title == "Unsolved murders");
            _cart.AddItem(book, addNum);
            _cart.RemoveItem(book, removeNum);
            //test result for book still exist in the cart
            if(result > 0)
            {
                Assert.True(_cart.cartItems[book] == result);
            }
            else
            {
                //book has been removed
                Assert.AreEqual(_cart.cartItems.Count, result);
            }
            
        }

        [Test]
        [TestCase("Unsolved murders", true)]
        [TestCase("Alice in Wonderland", false)]
        [TestCase("A Little Love Story", false)]
        [TestCase("Heresy", false)]
        [TestCase("The Neverending Story", false)]
        [TestCase("Jack the Ripper", true)]
        [TestCase("The Tolkien Years", false)]
        public void CalculateTotalDiscountTest(string bookTitle, bool expectedDiscount)
        {
            decimal total = 0;
            bool hasDiscount = false;
            Book book = _bookList.Find(b => b.Title == bookTitle);

            _cart.AddItem(book, 1);

            total = _cart.CalculateTotal();
            if(decimal.Compare(total, book.Price) != 0)
            {
                hasDiscount = true;
            }
            Assert.AreEqual( hasDiscount, expectedDiscount);
        }


        private static readonly object[] _sourceBookLists =
        {
            new object[] {new List<string> {"Unsolved murders", "A Little Love Story", "Heresy", "Jack the Ripper", "The Tolkien Years" },  (decimal)57.7405},   //case 1 Discount
            new object[] {new List<string> { "Alice in Wonderland", "A Little Love Story" }, (decimal)8.39 } //case 2
        };

        [Test]
        [TestCaseSource("_sourceBookLists")]
        public void CalculateTotalTest(List<string> list, decimal expectedTotal)
        {
            decimal total = 0;
            foreach (var item in list)
            {
                _cart.AddItem(_bookList.Find(b => b.Title == item), 1);
            }
            total = _cart.CalculateTotal();
            
            Assert.AreEqual(total, expectedTotal);
        }

        [Test]
        [TestCase(5.77405)]
        public void CalculateTaxTest(decimal expectedTotal)
        {
            decimal total = 0;
            decimal tax = 0;
            decimal deliveryFee = 0;

            _cart.AddItem(_bookList.Find(b => b.Title == "Unsolved murders"), 1);
            _cart.AddItem(_bookList.Find(b => b.Title == "A Little Love Story"), 1);
            _cart.AddItem(_bookList.Find(b => b.Title == "Heresy"), 1);
            _cart.AddItem(_bookList.Find(b => b.Title == "Jack the Ripper"), 1);
            _cart.AddItem(_bookList.Find(b => b.Title == "The Tolkien Years"), 1);

            total = _cart.CalculateTotal();
            tax = _cart.CalculateTax(total);
            deliveryFee = _cart.CalculateDelivery(total);
            Assert.AreEqual(tax, expectedTotal);
        }

        [Test]
        [TestCase("Jack the Ripper", true)]
        [TestCase("The Tolkien Years", false)]
        public void CalculateDeliveryTest(string title, bool hasDelivery)
        {
            decimal total = 0;
            decimal tax = 0;
            decimal deliveryFee = 0;

            _cart.AddItem(_bookList.Find(b => b.Title == title), 1);
            total = _cart.CalculateTotal();
            deliveryFee = _cart.CalculateDelivery(total);
            Assert.AreEqual((deliveryFee > 0), hasDelivery);
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain.Abstract;
using System.Collections.Generic;
using Domain.Entities;
using WebUI.Controllers;
using System.Linq;
using WebUI.Models;

namespace UnitTests
{
    [TestClass]
    public class Can_Paginate_Test
    {
        [TestMethod]
        public void Can_Paginate()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book{BookId = 1, Name = "Book1" },
                new Book{BookId = 2, Name = "Book2" },
                new Book{BookId = 3, Name = "Book3" },
                new Book{BookId = 4, Name = "Book4" },
                new Book{BookId = 5, Name = "Book5" },
            });

            BooksController controller = new BooksController(mock.Object);
            controller.pageSize = 3;


            BooksListViewModel result = (BooksListViewModel)controller.List(2).Model;

            List<Book> books = result.Books.ToList();
            Assert.IsTrue(books.Count == 2);
            Assert.AreEqual(books[0].Name, "Book4");
            Assert.AreEqual(books[1].Name, "Book5");
        }
    }
}

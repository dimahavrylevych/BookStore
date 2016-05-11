using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;
using WebUI.Controllers;
using Moq;
using Domain.Abstract;
using WebUI.Models;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class Can_Send_Pagination_View_Model_Test
    {
        [TestMethod]
        public void Can_Send_Pagination_View_Model()
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

            PagingInfo pagingInfo = result.PagingInfo;
            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);
            Assert.AreEqual(pagingInfo.TotalItems, 5);
            Assert.AreEqual(pagingInfo.TotalPages, 2);
        }
    }
}

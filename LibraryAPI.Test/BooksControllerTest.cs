using Library.API.Controllers;
using Library.API.Data.Models;
using Library.API.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;

namespace LibraryAPI.Test
{
    public class BooksControllerTest
    {
        BooksController _controller;
        IBookService _service;
        
        public BooksControllerTest()
        {
            _service = new BookService();
            _controller = new BooksController(_service);
        }

        [Fact]
        public void GetAllTest()
        {
            // Arrange
            // Act
            var result = _controller.Get();
            // Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as OkObjectResult;
            Assert.IsType<List<Book>>(list.Value);

            var listBooks = list.Value as List<Book>;
            Assert.Equal(5, listBooks.Count);
        }

        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200", "ab2bd817-98cd-4cf3-a80a-53ea0cd9c269")]
        public void GetBooksByIdTest(string id, string guid2)
        {
            //Arrange
            Guid validGuid = Guid.Parse(id);
            Guid invalidGuid = Guid.Parse(guid2);
            //Act
            var result = _controller.Get(validGuid);
            var notFoundResult = _controller.Get(invalidGuid);

            //Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
            Assert.IsType<OkObjectResult>(result.Result);

            var item = result.Result as OkObjectResult;
            Assert.IsType<Book>(item.Value);

            var bookItem = item.Value as Book;
            Assert.Equal(validGuid, bookItem.Id);
            Assert.Equal("Managing Oneself", bookItem.Title);
        }
    }
}

using Library.API.Controllers;
using Library.API.Data.Models;
using Library.API.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public void AddBookTest()
        {
            // Arrange
            var completeBook = new Book()
            {
                Author = "Author",
                Title = "Title",
                Description = "Description"
            };

            // Act
            var createdResponse = _controller.Post(completeBook);
            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
            var item = createdResponse as CreatedAtActionResult;

            Assert.IsType<Book>(item.Value);

            var bookItem = item.Value as Book;
            Assert.Equal(completeBook.Author, bookItem.Author);
            Assert.Equal(completeBook.Title, bookItem.Title);
            Assert.Equal(completeBook.Description, bookItem.Description);

            // Arrange
            var incompleteBook = new Book()
            {
                Author = "Author",
                Description = "Description"
            };
            // Act
            _controller.ModelState.AddModelError("Title", "Title is a mandatory field");
            var badResponse = _controller.Post(incompleteBook);
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);           
        }

        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200", "ab2bd817-98cd-4cf3-a80a-53ea0cd9c269")]
        public void RemoveBookByIdTest(string id, string guid2)
        {
            //Arrange
            Guid validGuid = Guid.Parse(id);
            Guid invalidGuid = Guid.Parse(guid2);
            //Act
            var notFoundResult = _controller.Remove(invalidGuid);

            //Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
            Assert.Equal(5, _service.GetAll().Count());

            var okresult = _controller.Remove(validGuid);

            Assert.IsType<OkResult>(okresult);
            Assert.Equal(4, _service.GetAll().Count());
        }
    }
}

using Library.API.Data.Models;
using Library.API.Data.Services;
using LibraryApp.Controllers;
using LibraryApp.Data.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LibraryApp.Test
{
    public class BooksControllerTest
    {
        /// <summary>
        /// Test retrieving all books from the service.
        /// </summary>
        [Fact]
        public void IndexUnitTest()
        {
            // Arrange 
            var moqRepo = new Mock<IBookService>();
            moqRepo.Setup(n => n.GetAll()).Returns(MockData.GetTestBookItems());
            var controller = new BooksController(moqRepo.Object);

            // Act
            var result = controller.Index();

            // Assert            
            var viewResult = Assert.IsType<ViewResult>(result);            
            var viewResultBooks = Assert.IsAssignableFrom<List<Book>>(viewResult.ViewData.Model);
            Assert.Equal(5, viewResultBooks.Count);

        }

        /// <summary>
        /// Test that details are retrieved from a specified book
        /// </summary>
        /// <param name="id">GUID of the specified book</param>
        /// <param name="incorrectGuid">Control group of an incorrect GUID which should not return anything.</param>
        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200", "ab2bd817-98cd-4cf3-a80a-53ea0cd9c123")]
        public void DetailsUnitTest(string id, string incorrectGuid)
        {
            // Arrange
            var validItemGuid = Guid.Parse(id);
            var moqRepo = new Mock<IBookService>();
            moqRepo.Setup(n => n.GetById(validItemGuid)).Returns(MockData.GetTestBookItems().FirstOrDefault(x => x.Id == validItemGuid));
            var controller = new BooksController(moqRepo.Object);

            //Act
            var result = controller.Details(validItemGuid);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewResultBooks = Assert.IsAssignableFrom<Book>(viewResult.ViewData.Model);
            Assert.Equal("Managing Oneself", viewResultBooks.Title);
            Assert.Equal("Peter Drucker", viewResultBooks.Author);
            Assert.Equal(validItemGuid, viewResultBooks.Id);

            
            //Arrange
            var invalidGuid = Guid.Parse(incorrectGuid);
            moqRepo.Setup(n => n.GetById(invalidGuid)).Returns(MockData.GetTestBookItems().FirstOrDefault(x => x.Id == invalidGuid));

            //Act
            var notFoundResult = controller.Details(invalidGuid);

            //Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        /// <summary>
        /// Test creating a new book and adding it to the service
        /// </summary>
        [Fact]
        public void CreateTest()
        {
            // Arrange
            var mockRepo = new Mock<IBookService>();
            var controller = new BooksController(mockRepo.Object);
            var newValidItem = new Book()
            {
                Author = "Author",
                Title = "Title",
                Description = "Description"
            };
            // Act
            var result = controller.Create(newValidItem);
            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Null(redirectToActionResult.ControllerName);

            // Arrange
            var newInvalidItem = new Book()
            {
                Title = "Title",
                Description = "Description"
            };

            controller.ModelState.AddModelError("Author", "The Author value is required.");

            // Act
            var resultInvalid = controller.Create(newInvalidItem);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(resultInvalid);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        /// <summary>
        /// Test deleting a book from the service and ensuring the count is updated.
        /// </summary>
        /// <param name="validGuid"></param>
        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200")]
        public void DeleteTest(string validGuid)
        {
            //arrange
            var mockRepo = new Mock<IBookService>();
            mockRepo.Setup(n => n.GetAll()).Returns(MockData.GetTestBookItems());
            var controller = new BooksController(mockRepo.Object);
            var itemGuid = new Guid(validGuid);

            //act
            var result = controller.Delete(itemGuid, null);

            //assert
            var actionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", actionResult.ActionName);
            Assert.Null(actionResult.ControllerName);

        }
    }
}

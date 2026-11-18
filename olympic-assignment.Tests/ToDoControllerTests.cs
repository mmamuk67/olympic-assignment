using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using olympic_assignment.Controllers;
using olympic_assignment.Models;

namespace olympic_assignment.Tests
{
    public class ToDoControllerTests
    {
        private List<Ticket> GetSampleTickets()
        {
            return new List<Ticket>
            {
                new Ticket { Id = 1, Name = "Setup DI", Description = "Configure repository pattern", SprintNumber = 1, PointValue = 5, Status = "todo" },
                new Ticket { Id = 2, Name = "Write Unit Tests", Description = "Add xUnit and Moq tests", SprintNumber = 1, PointValue = 8, Status = "inprogress" },
                new Ticket { Id = 3, Name = "Deploy App", Description = "Verify SQLite migrations", SprintNumber = 2, PointValue = 3, Status = "done" }
            };
        }

        [Fact]
        public void Index_ReturnsViewResult_WithAllTickets_WhenFilterIsAll()
        {
            // Arrange
            var mockRepo = new Mock<ITicketRepository>();
            mockRepo.Setup(r => r.GetAllTickets()).Returns(GetSampleTickets());
            var controller = new ToDoController(mockRepo.Object);

            // Act
            var result = controller.Index("all");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Ticket>>(viewResult.Model);
            Assert.Equal(3, model.Count);
        }

        [Fact]
        public void Index_ReturnsFilteredTickets_WhenStatusFilterProvided()
        {
            // Arrange
            var mockRepo = new Mock<ITicketRepository>();
            mockRepo.Setup(r => r.GetAllTickets()).Returns(GetSampleTickets());
            var controller = new ToDoController(mockRepo.Object);

            // Act
            var result = controller.Index("todo");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Ticket>>(viewResult.Model);
            Assert.Single(model);
            Assert.Equal("Setup DI", model[0].Name);
        }

        [Fact]
        public void Add_Get_ReturnsViewResult_WithNewTicket()
        {
            // Arrange
            var mockRepo = new Mock<ITicketRepository>();
            var controller = new ToDoController(mockRepo.Object);

            // Act
            var result = controller.Add();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<Ticket>(viewResult.Model);
        }

        [Fact]
        public void Add_Post_ValidModel_AddsTicketAndRedirectsToIndex()
        {
            // Arrange
            var mockRepo = new Mock<ITicketRepository>();
            var controller = new ToDoController(mockRepo.Object);
            var newTicket = new Ticket { Id = 4, Name = "New Task", Description = "Details", SprintNumber = 2, PointValue = 5, Status = "todo" };

            // Act
            var result = controller.Add(newTicket);

            // Assert
            mockRepo.Verify(r => r.AddTicket(newTicket), Times.Once);
            mockRepo.Verify(r => r.Save(), Times.Once);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public void Add_Post_InvalidModel_ReturnsViewWithTicket()
        {
            // Arrange
            var mockRepo = new Mock<ITicketRepository>();
            var controller = new ToDoController(mockRepo.Object);
            controller.ModelState.AddModelError("Name", "Required");
            var invalidTicket = new Ticket();

            // Act
            var result = controller.Add(invalidTicket);

            // Assert
            mockRepo.Verify(r => r.AddTicket(It.IsAny<Ticket>()), Times.Never);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(invalidTicket, viewResult.Model);
        }

        [Fact]
        public void Delete_Post_DeletesTicketAndRedirectsToIndex()
        {
            // Arrange
            var mockRepo = new Mock<ITicketRepository>();
            var controller = new ToDoController(mockRepo.Object);

            // Act
            var result = controller.Delete(1);

            // Assert
            mockRepo.Verify(r => r.DeleteTicket(1), Times.Once);
            mockRepo.Verify(r => r.Save(), Times.Once);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using SmartCityApp.Controllers;
using SmartCityApp.Data;
using SmartCityApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SmartCityApp.Tests
{
    public class ReportsControllerTestes
    {
        [Fact]
        public async Task GetReports_ReturnsOkResult_WithReports()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Reports.Add(new Report("Title1", "Description1", 1) { CreatedAt = DateTime.Now });
                context.Reports.Add(new Report("Title2", "Description2", 2) { CreatedAt = DateTime.Now });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new ReportsController(context);

                // Act
                var result = await controller.GetReports();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var reports = Assert.IsAssignableFrom<IEnumerable<Report>>(okResult.Value);
                Assert.Equal(2, reports.Count());
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using StatsPerformChallengeAPI.Controllers;
using StatsPerformChallengeAPI.Models.Interfaces;
using StatsPerformChallengeAPI.Models.TechChallenge;
using StatsPerformChallengeAPI.Tests.MockDataSources;

namespace StatsPerformChallengeAPI.Tests
{
    public class BrandsControllerTest
    {
        private readonly BrandingsController _controller;
        private readonly IProtectedDataAccess _context;

        public BrandsControllerTest()
        {
            _context = new ProtectedDataAccessMock();
            _controller = new BrandingsController(_context);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var result = _controller.GetBrandings().Result;
            // Assert
            Assert.NotNull(result);
            var resultValue = (OkObjectResult)result;
            var items = Assert.IsType<List<BrandingDefinition>>(resultValue.Value);
            Assert.Equal(2, items.Count);
        }
    }
}
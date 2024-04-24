using Microsoft.AspNetCore.Mvc;
using StatsPerformChallengeAPI.Controllers;
using StatsPerformChallengeAPI.Models.Interfaces;
using StatsPerformChallengeAPI.Models.TechChallenge;
using StatsPerformChallengeAPI.Tests.MockDataSources;

namespace StatsPerformChallengeAPI.Tests
{
    public class LeaguesControllerTest
    {
        private readonly LeaguesController _controller;
        private readonly IProtectedDataAccess _context;

        public LeaguesControllerTest()
        {
            _context = new ProtectedDataAccessMock();
            _controller = new LeaguesController(_context);
        }

        [Fact]
        public void GetLeagues_WhenCalled_ReturnsAllItems()
        {
            // Act
            var result = _controller.GetLeagues().Result;
            // Assert
            Assert.NotNull(result);
            var resultValue = (OkObjectResult)result;
            var items = Assert.IsType<List<League>>(resultValue.Value);
            Assert.Single(items);
        }

        [Fact]
        public void GetUnbrandedMatches_WhenCalled_ReturnsAllItems()
        {
            // Arrange
            var leagueId = "quidditchLeague";
            // Act
            var result = _controller.GetUnbrandedMatches(leagueId).Result;
            // Assert
            Assert.NotNull(result);
            var resultValue = (OkObjectResult)result;
            var items = Assert.IsType<List<Match>>(resultValue.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public void GetUnbrandedMatches_WhenCalledWithWrongId_Returns500()
        {
            // Arrange
            var leagueId = "premierLeague";
            // Act
            var result = _controller.GetUnbrandedMatches(leagueId).Result;
            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }
        
        [Fact]
        public void GetBrandedMatches_WhenCalled_ReturnsAllItems_Brand1()
        {
            // Arrange
            var leagueId = "quidditchLeague";
            var brandingId = "brandingDef001";
            // Act
            var result = _controller.GetBrandedMatches(leagueId, brandingId).Result;
            // Assert
            Assert.NotNull(result);
            var resultValue = (OkObjectResult)result;
            var items = Assert.IsType<List<Match>>(resultValue.Value);
            Assert.Equal(2, items.Count);
            var homeTeamFirstMatch = items.First().HomeTeam;
            Assert.NotNull(homeTeamFirstMatch);
            Assert.NotNull(homeTeamFirstMatch.Brand);
            Assert.Equal("Arrows", homeTeamFirstMatch.Brand.Name);
            Assert.Equal("AA", homeTeamFirstMatch.Brand.Abbreviation);
        }        
        [Fact]
        public void GetBrandedMatches_WhenCalled_ReturnsAllItems_Brand2()
        {
            // Arrange
            var leagueId = "quidditchLeague";
            var brandingId = "brandingDef002";
            // Act
            var result = _controller.GetBrandedMatches(leagueId, brandingId).Result;
            // Assert
            Assert.NotNull(result);
            var resultValue = (OkObjectResult)result;
            var items = Assert.IsType<List<Match>>(resultValue.Value);
            Assert.Equal(2, items.Count);
            var homeTeamFirstMatch = items.First().HomeTeam;
            Assert.NotNull(homeTeamFirstMatch);
            Assert.NotNull(homeTeamFirstMatch.Brand);
            Assert.Equal("Appleby Arrows (England)", homeTeamFirstMatch.Brand.Name);
            Assert.Equal("Apple", homeTeamFirstMatch.Brand.Abbreviation);
        }
    }
}

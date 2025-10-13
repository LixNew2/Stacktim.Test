using AutoMapper;
using BOTTON_ASPNET;
using BOTTON_ASPNET.Tests.Helpers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Controllers;

namespace StacktimApi.Test;

public class PlayersControllerTests
{
    [Fact]
    public void GetPlayers_ReturnsAllPlayers()
    {
        var _context = TestDbContextFactory.Create();
        var config_mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
        var controller = new PlayersController(_context, config_mapper.CreateMapper());
        var result = controller.getPlayers();
        TestDbContextFactory.Destroy(_context);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetPlayer_WithValidId_ReturnsPlayer()
    {
        var _context = TestDbContextFactory.Create();
        var config_mapper = new MapperConfiguration(cfg => {cfg.AddProfile<MappingProfile>();});
        var controller = new PlayersController(_context, config_mapper.CreateMapper());
        var result = controller.getPlayer(1);
        TestDbContextFactory.Destroy(_context);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetPlayer_WithInvalidId_ReturnsNotFound()
    {
        var _context = TestDbContextFactory.Create();
        var config_mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
        var controller = new PlayersController(_context, config_mapper.CreateMapper());
        var result = controller.getPlayer(4);
        TestDbContextFactory.Destroy(_context);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void CreatePlayer_WithValidData_ReturnsCreated()
    {
        var _context = TestDbContextFactory.Create();
        var config_mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
        var controller = new PlayersController(_context, config_mapper.CreateMapper());
        var result = controller.createPlayer(new CreatePlayerDto{ Pseudo = "test1", Email = "test1@gmail.com"});
        TestDbContextFactory.Destroy(_context);

        Assert.IsType<CreatedResult>(result);
    }

    [Fact]
    public void CreatePlayer_WithDuplicatePseudo_ReturnsBadRequest()
    {
        var _context = TestDbContextFactory.Create();
        var config_mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
        var controller = new PlayersController(_context, config_mapper.CreateMapper());
        var result = controller.createPlayer(new CreatePlayerDto { Pseudo = "TestPlayer1", Email = "test1@gmail.com" });
        TestDbContextFactory.Destroy(_context);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void DeletePlayer_WithValidId_ReturnsNoContent()
    {
        var _context = TestDbContextFactory.Create();
        var config_mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
        var controller = new PlayersController(_context, config_mapper.CreateMapper());
        var result = controller.deletePlayer(1);
        TestDbContextFactory.Destroy(_context);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void GetLeaderboard_ReturnsOrderedPlayers()
    {
        var _context = TestDbContextFactory.Create();
        var config_mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
        var controller = new PlayersController(_context, config_mapper.CreateMapper());
        var result = controller.getPlayerLeaderboard();
       
        var players = (((OkObjectResult)result).Value as IEnumerable<PlayerModel>)?.ToList();
        for (int i = 0; i < players.Count - 1; i++)
        {
            Assert.True(players[i].TotalScore >= players[i + 1].TotalScore);
        }

        TestDbContextFactory.Destroy(_context);
    }
}
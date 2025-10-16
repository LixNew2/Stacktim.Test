using AutoMapper;
using BOTTON_ASPNET;
using BOTTON_ASPNET.Tests.Helpers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Controllers;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Stacktim.Test.controllers;

public class TeamsControllerTests
{
    [Fact]
    public void GetTeams_ReturnsAllTeams()
    {
        var _context = TestDbContextFactory.Create();
        var config_mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
        var controller = new TeamsController(_context, config_mapper.CreateMapper());
        var result = controller.getTeams();
        TestDbContextFactory.Destroy(_context);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetTeam_WithValidId_ReturnsTeam()
    {
        var _context = TestDbContextFactory.Create();
        var config_mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
        var controller = new TeamsController(_context, config_mapper.CreateMapper());
        var result = controller.getTeam(1);
        TestDbContextFactory.Destroy(_context);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetTeam_WithInvalidId_ReturnsNotFound()
    {
        var _context = TestDbContextFactory.Create();
        var config_mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
        var controller = new TeamsController(_context, config_mapper.CreateMapper());
        var result = controller.getTeam(4);
        TestDbContextFactory.Destroy(_context);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void CreateTeam_WithValidData_ReturnsCreated()
    {
        var _context = TestDbContextFactory.Create();
        var config_mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
        var controller = new TeamsController(_context, config_mapper.CreateMapper());
        var result = controller.createTeam(new CreateTeamDto { Name = "TETA", Tag = "TET" });
        TestDbContextFactory.Destroy(_context);

        Assert.IsType<CreatedResult>(result);
    }

    [Fact]
    public void CreateTeam_WithDuplicateName_ReturnsBadRequest()
    {
        var _context = TestDbContextFactory.Create();
        var config_mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
        var controller = new TeamsController(_context, config_mapper.CreateMapper());
        var result = controller.createTeam(new CreateTeamDto { Name = "ALPHA", Tag = "TES" });
        TestDbContextFactory.Destroy(_context);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void CreateTeam_WithDuplicateTag_ReturnsBadRequest()
    {
        var _context = TestDbContextFactory.Create();
        var config_mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
        var controller = new TeamsController(_context, config_mapper.CreateMapper());
        var result = controller.createTeam(new CreateTeamDto { Name = "TEST", Tag = "ALP" });
        TestDbContextFactory.Destroy(_context);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void GetRoadster_WithValidId_ReturnsRoadster()
    {
        var _context = TestDbContextFactory.Create();
        var config_mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
        var controller = new TeamsController(_context, config_mapper.CreateMapper());
        var result = controller.getRoster(1);
        TestDbContextFactory.Destroy(_context);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetRoadster_WithInvalidId_ReturnsNotFound()
    {
        var _context = TestDbContextFactory.Create();
        var config_mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
        var controller = new TeamsController(_context, config_mapper.CreateMapper());
        var result = controller.getRoster(4);
        TestDbContextFactory.Destroy(_context);

        Assert.IsType<NotFoundResult>(result);
    }
}


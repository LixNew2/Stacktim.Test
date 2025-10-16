// Helpers/TestDbContextFactory.cs
using BOTTON_ASPNET;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BOTTON_ASPNET.Tests.Helpers
{
    public static class TestDbContextFactory
    {
        public static StacktimDbContext Create()
        {
            var options = new DbContextOptionsBuilder<StacktimDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new StacktimDbContext(options);

            context.Database.EnsureCreated();

            if (!context.Players.Any())
            {
                context.Players.AddRange(
                    new PlayerModel
                    {
                        Id = 1,
                        Pseudo = "TestPlayer1",
                        Email = "test1@example.com",
                        Rank = "Gold",
                        TotalScore = 1500,
                        RegistrationDate = new DateOnly(2023, 01, 01)
                    },
                    new PlayerModel
                    {
                        Id = 2,
                        Pseudo = "TestPlayer2",
                        Email = "test2@example.com",
                        Rank = "Silver",
                        TotalScore = 1200,
                        RegistrationDate = new DateOnly(2023, 01, 02)
                    },
                    new PlayerModel
                    {
                        Id = 3,
                        Pseudo = "TestPlayer3",
                        Email = "test3@example.com",
                        Rank = "Bronze",
                        TotalScore = 800,
                        RegistrationDate = new DateOnly(2023, 01, 03)
                    }
                );

                context.SaveChanges();
            }

            if (!context.Teams.Any())
            {
                context.Teams.AddRange(
                    new TeamModel
                    {
                        Id = 1,
                        Name = "ALPHA",
                        Tag = "ALP",
                        CreationDate = new DateOnly(2023, 01, 01)
                    },
                    new TeamModel
                    {
                        Id = 2,
                        Name = "BETA",
                        Tag = "BET",
                        CreationDate = new DateOnly(2023, 01, 01)
                    },
                    new TeamModel
                    {
                        Id = 3,
                        Name = "GAMMA",
                        Tag = "GAM",
                        CreationDate = new DateOnly(2023, 01, 01)
                    }
                );

                context.SaveChanges();
            }

            return context;
        }

        public static void Destroy(StacktimDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}

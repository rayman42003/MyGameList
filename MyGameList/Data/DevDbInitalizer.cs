using MyGameList.Models;
using System.Collections.Generic;

namespace MyGameList.Data
{
    public static class DevDbInitalizer
    {
        public static void Initalize(GameContext context) {
            context.Database.EnsureCreated();

            var games = new List<Game> {
                new Game { Title = "Kingdom Hearts 3", Description = "Final installment in Xehanort Saga" }
            };
            games.ForEach(game => context.Games.Add(game));
            context.SaveChanges();
        }
    }
}

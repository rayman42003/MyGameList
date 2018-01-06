using Microsoft.EntityFrameworkCore;
using MyGameList.Models;

namespace MyGameList.Data
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options)
            : base(options) {
        }

        public DbSet<Game> Games { get; set; }
    }
}

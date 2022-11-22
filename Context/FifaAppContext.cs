using Microsoft.EntityFrameworkCore;
using WebTest.Models.Players;
using WebTest.Models.Teams;

namespace WebTest.Context
{
    public class FifaAppContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }

        public FifaAppContext(DbContextOptions<FifaAppContext> options) : base(options)
        {

        }
    }
}

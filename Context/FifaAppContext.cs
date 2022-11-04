using Microsoft.EntityFrameworkCore;
using WebTest.Models.Players;

namespace WebTest.Context
{
    public class FifaAppContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        public FifaAppContext(DbContextOptions<FifaAppContext> options) : base(options)
        {

        }
    }
}

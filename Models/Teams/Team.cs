using WebTest.Models.Players;

namespace WebTest.Models.Teams
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Player> Players { get; set; } = new List<Player>();
    }
}

using System.ComponentModel.DataAnnotations;
using WebTest.Models.Players;
#nullable disable 

namespace WebTest.Models.Teams
{
    public class Team
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Player> Players { get; set; } = new List<Player>();
    }
}

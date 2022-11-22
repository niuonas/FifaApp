#nullable disable
using System.ComponentModel.DataAnnotations;
using WebTest.Models.Teams;

namespace WebTest.Models.Players
{
    public class Player
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Nationality { get; set; }
        public int Overall { get; set; }
        public Team Team { get; set; }
    }
}

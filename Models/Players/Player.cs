#nullable disable
using System.ComponentModel.DataAnnotations;

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
        [Required]
        public string Overall { get; set; }
    }
}

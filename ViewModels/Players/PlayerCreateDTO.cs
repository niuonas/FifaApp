#nullable disable
using WebTest;

namespace WebTest.ViewModels.Players
{
    public class PlayerCreateDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nationality { get; set; }
        public string Overall { get; set; }
    }
}

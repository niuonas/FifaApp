using WebTest.ViewModels.Players;

namespace WebTest.ViewModels.Teams
{
    public class TeamVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PlayerVM> Players { get; set; }
    }
}

using RunnerWebApp.Data.Enum;
using RunnerWebApp.Models;

namespace RunnerWebApp.ViewModels
{
    public class CreateRaceViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public string Image { get; set; }
        public RaceCategory RaceCategory { get; set; }
        public string AppUserId { get; set; }
    }
}

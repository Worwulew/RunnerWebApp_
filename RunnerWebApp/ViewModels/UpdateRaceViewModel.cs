using RunnerWebApp.Data.Enum;
using RunnerWebApp.Models;

namespace RunnerWebApp.ViewModels
{
    public class UpdateRaceViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public RaceCategory RaceCategory { get; set; }
        public string AppUserId { get; set; }
    }
}

using RunnerWebApp.Models;

namespace RunnerWebApp.Interfaces
{
    public interface IDashboardService
    {
        Task<List<Race>> GetAllUserRaces();
        Task<List<Club>> GetAllUserClubs();

    }
}

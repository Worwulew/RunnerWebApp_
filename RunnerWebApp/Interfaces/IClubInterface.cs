using RunnerWebApp.Models;

namespace RunnerWebApp.Interfaces
{
    public interface IClubInterface
    {

        Task<IEnumerable<Club>> GetAll();

        Task<Club> GetByIdAsync(int id);
        Task<Club> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Club>> GetClubByCity(string city);

        bool Add(Club club);
        bool Update(Club club);
        bool Delete(Club club);
        bool Save();

    }
}

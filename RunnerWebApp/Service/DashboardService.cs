using RunnerWebApp.Data;
using RunnerWebApp.Interfaces;
using RunnerWebApp.Models;

namespace RunnerWebApp.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Club>> GetAllUserClubs()
        {
            var curUser = _httpContextAccessor.HttpContext?.User;
            var userClubs = _context.Clubs.Where(c => c.AppUser.Id == curUser.GetUserId());
            return userClubs.ToList();
        }

        public async Task<List<Race>> GetAllUserRaces()
        {
            var curUser = _httpContextAccessor.HttpContext?.User;
            var userRaces = _context.Races.Where(r => r.AppUser.Id == curUser.GetUserId());
            return userRaces.ToList();
        }
    }
}

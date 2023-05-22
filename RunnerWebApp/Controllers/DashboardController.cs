using Microsoft.AspNetCore.Mvc;
using RunnerWebApp.Data;
using RunnerWebApp.Interfaces;
using RunnerWebApp.ViewModels;

namespace RunnerWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var userRaces = await _dashboardService.GetAllUserRaces();
            var userClubs = await _dashboardService.GetAllUserClubs();
            var dashboardViewModel = new DashboardViewModel()
            {
                Races = userRaces,
                Clubs = userClubs
            };
            return View(dashboardViewModel);
        }
    }
}

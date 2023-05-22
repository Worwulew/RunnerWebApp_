using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunnerWebApp.Data;
using RunnerWebApp.Interfaces;
using RunnerWebApp.Models;
using RunnerWebApp.Service;
using RunnerWebApp.ViewModels;

namespace RunnerWebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceInterface _raceService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RaceController(IRaceInterface raceService, IHttpContextAccessor httpContextAccessor)
        {
            _raceService = raceService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> races = await _raceService.GetAll();
            return View(races);
        }

        public async Task<IActionResult> Details(int id)
        {
            var race = await _raceService.GetByIdAsync(id);
            return View(race);
        }

        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createRaceViewModel = new CreateRaceViewModel { AppUserId = curUserId };
            return View(createRaceViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel createRaceViewModel)
        {
            if (ModelState.IsValid)
            {
                var race = new Race
                {
                    Title = createRaceViewModel.Title,
                    Description = createRaceViewModel.Description,
                    Image = createRaceViewModel.Image,
                    AppUserId = createRaceViewModel.AppUserId,
                    Address = new Address
                    {
                        Street = createRaceViewModel.Address.Street,
                        City = createRaceViewModel.Address.City,
                        State = createRaceViewModel.Address.State
                    }
                };
                _raceService.Add(race);
                return RedirectToAction("Index", "Race");
            }
            else
            {
                ModelState.AddModelError("", "Wrong data");
            }
            return View(createRaceViewModel);
        }

        public async Task<IActionResult> Update(int id)
        {
            var race = await _raceService.GetByIdAsync(id);
            if (race == null)
                return View("Error");
            var raceVM = new UpdateRaceViewModel
            {
                Title = race.Title,
                Description = race.Description,
                AddressId = race.AddressId,
                Address = race.Address,
                AppUserId = race.AppUserId,
                Image = race.Image,
                RaceCategory = race.RaceCategory
            };
            return View(raceVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateRaceViewModel raceVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Update", raceVM);
            }

            var userRace = await _raceService.GetByIdAsyncNoTracking(id);

            if (userRace != null)
            {
                var race = new Race
                {
                    Id = id,
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    AddressId = raceVM.AddressId,
                    Address = raceVM.Address,
                    Image = raceVM.Image,
                    AppUserId = raceVM.AppUserId,
                    RaceCategory = raceVM.RaceCategory
                };

                _raceService.Update(race);
                return RedirectToAction("Index");
            }
            else
            {
                return View(raceVM);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var raceDetails = await _raceService.GetByIdAsync(id);
            if (raceDetails == null) return View("Erorr");
            return View(raceDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteRace(int id)
        {
            var raceDetails = await _raceService.GetByIdAsync(id);
            if (raceDetails == null) return View("Erorr");

            _raceService.Delete(raceDetails);
            return RedirectToAction("Index", "Race");
        }
    }
}

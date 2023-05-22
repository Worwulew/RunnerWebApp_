using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunnerWebApp.Data;
using RunnerWebApp.Interfaces;
using RunnerWebApp.Models;
using RunnerWebApp.ViewModels;

namespace RunnerWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubInterface _clubService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClubController(IClubInterface clubService, IHttpContextAccessor httpContextAccessor)
        {
            _clubService = clubService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var clubs = await _clubService.GetAll();
            return View(clubs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var club = await _clubService.GetByIdAsync(id);
            return View(club);
        }

        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createClubViewModel = new CreateClubViewModel { AppUserId = curUserId };
            return View(createClubViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel createClubViewModel)
        {
            if (ModelState.IsValid)
            {
                var club = new Club
                {
                    Title = createClubViewModel.Title,
                    Description = createClubViewModel.Description,
                    Image = createClubViewModel.Image,
                    AppUserId = createClubViewModel.AppUserId,
                    Address = new Address
                    {
                        Street = createClubViewModel.Address.Street,
                        City = createClubViewModel.Address.City,
                        State = createClubViewModel.Address.State
                    }
                };
                _clubService.Add(club);
                return RedirectToAction("Index", "Club");
            }
            else
            {
                ModelState.AddModelError("", "Wrong data");
            }
            return View(createClubViewModel);
        }

        public async Task<IActionResult> Update(int id)
        {
            var club = await _clubService.GetByIdAsync(id);
            if (club == null)
                return View("Error");
            var clubVM = new UpdateClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId,
                Address = club.Address,
                Image = club.Image,
                AppUserId = club.AppUserId,
                ClubCategory = club.ClubCategory
            };
            return View(clubVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Update", clubVM);
            }

            var userClub = await _clubService.GetByIdAsyncNoTracking(id);

            if (userClub != null)
            {
                var club = new Club
                {
                    Id = id,
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    AddressId = clubVM.AddressId,
                    Address = clubVM.Address,
                    AppUserId= clubVM.AppUserId,
                    Image = clubVM.Image,

                    ClubCategory = clubVM.ClubCategory
                };

                _clubService.Update(club);
                return RedirectToAction("Index");
            }
            else
            {
                return View(clubVM);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _clubService.GetByIdAsync(id);
            if (clubDetails == null) return View("Erorr");
            return View(clubDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var clubDetails = await _clubService.GetByIdAsync(id);
            if (clubDetails == null) return View("Erorr");

            _clubService.Delete(clubDetails);
            return RedirectToAction("Index", "Club");
        }
    }
}

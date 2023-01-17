using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesServices _service;

        public MoviesController(IMoviesServices service)
        {
            _service= service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync(n => n.Cinema);
            return View(data);
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var data = await _service.GetAllAsync(n => n.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = data.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                return View("Index",filteredResult);
            }
            return View("Index",data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var details = await _service.GetMovieByIdAsync(id);
            return View(details);
        }

        public async Task<IActionResult> Create()
        {
            var data = await _service.GetNewMovieDropdowns();

            ViewBag.Cinemas = new SelectList(data.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(data.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(data.Actors, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            var data = await _service.GetNewMovieDropdowns();
            if (!ModelState.IsValid)
            {
                ViewBag.Cinemas = new SelectList(data.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(data.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(data.Actors, "Id", "FullName");
                return View(movie);
            }
            await _service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var details = await _service.GetMovieByIdAsync(id);
            if (details == null)
            {
                return View("NotFound");
            }

            var response = new NewMovieVM()
            {
                Id = details.Id,
                Name = details.Name,
                Description = details.Description,
                Price = details.Price,
                StartDate= details.StartDate,
                EndDate= details.EndDate,
                ImageURL = details.ImageURL,
                MovieCategory = details.MovieCategory,
                CinemaId = details.CinemaId,
                ProducerId = details.ProducerId,
                ActorIds = details.Actors_Movies.Select(n => n.ActorId).ToList(),
            };
            var data = await _service.GetNewMovieDropdowns();

            ViewBag.Cinemas = new SelectList(data.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(data.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(data.Actors, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,NewMovieVM movie)
        {
            if(id != movie.Id)
            {
                return View("NotFound");
            }

            var data = await _service.GetNewMovieDropdowns();
            if (!ModelState.IsValid)
            {
                ViewBag.Cinemas = new SelectList(data.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(data.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(data.Actors, "Id", "FullName");
                return View(movie);
            }
            await _service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
    }
}

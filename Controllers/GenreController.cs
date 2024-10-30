
using Microsoft.AspNetCore.Mvc;
using WritersClub.Interfaces;
using WritersClub.Models;
using WritersClub.Repository;

namespace WritersClub.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenre _genres;
        

        public GenreController(IGenre genres)
        {
            _genres = genres;
        }
        public async Task<IActionResult> Index()
        {
            var genres = await _genres.GetAllGenres();
            return View(genres);
        }
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {

            var genre = id == 0 ? new Genre() : await _genres.GetGenre(id);
            return View(genre);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                if (genre.Id == 0)
                {
                    await _genres.AddGenre(genre);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _genres.UpdateGenre(genre);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(genre);
        }
public async Task<IActionResult>Delete(int id)
        {
            var genre=await _genres.DeleteGenre(id);
            if(genre==null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using OlympicGames.Models;

namespace OlympicGames.Controllers
{
    [Route("Favorites")]
    public class FavoritesController : Controller
    {
        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var session = new OlympicSession(HttpContext.Session);
            var favorites = session.GetFavoriteCountries();
            return View(favorites);
        }

        [HttpPost("Add")]
        public IActionResult Add(Country country)
        {
            var session = new OlympicSession(HttpContext.Session);
            var favorites = session.GetFavoriteCountries();

            if (!favorites.Any(c => c.CountryId == country.CountryId))
            {
                favorites.Add(country);
                session.SetFavoriteCountries(favorites);
            }

            return RedirectToAction("Index");
        }

        [HttpPost("Clear")]
        public IActionResult Clear()
        {
            var session = new OlympicSession(HttpContext.Session);
            session.RemoveFavorites();
            return RedirectToAction("Index", "Home");
        }
    }
}
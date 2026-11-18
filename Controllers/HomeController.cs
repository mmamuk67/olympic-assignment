using Microsoft.AspNetCore.Mvc;
using OlympicGames.Models;

namespace OlympicGames.Controllers
{
    public class HomeController : Controller
    {
        private static List<Country> _countries = new List<Country>
        {
            new Country { CountryId = "at", Name = "Austria", Game = "Winter", Category = "Indoor", FlagImage = "https://flagcdn.com/at.svg" },
            new Country { CountryId = "br", Name = "Brazil", Game = "Summer", Category = "Outdoor", FlagImage = "https://flagcdn.com/br.svg" },
            new Country { CountryId = "ca", Name = "Canada", Game = "Winter", Category = "Indoor", FlagImage = "https://flagcdn.com/ca.svg" },
            new Country { CountryId = "cn", Name = "China", Game = "Summer", Category = "Indoor", FlagImage = "https://flagcdn.com/cn.svg" },
            new Country { CountryId = "cy", Name = "Cyprus", Game = "Summer", Category = "Outdoor", FlagImage = "https://flagcdn.com/cy.svg" },
            new Country { CountryId = "fi", Name = "Finland", Game = "Winter", Category = "Outdoor", FlagImage = "https://flagcdn.com/fi.svg" }
        };

        [HttpGet("{activeGame?}/{activeCategory?}")]
        public IActionResult Index(CountryViewModel model)
        {
            model.ActiveGame ??= "all";
            model.ActiveCategory ??= "all";

            var countries = _countries.AsQueryable();

            if (model.ActiveGame != "all")
            {
                countries = countries.Where(c => c.Game.ToLower() == model.ActiveGame.ToLower());
            }
            if (model.ActiveCategory != "all")
            {
                countries = countries.Where(c => c.Category.ToLower() == model.ActiveCategory.ToLower());
            }

            model.Countries = countries.OrderBy(c => c.Name).ToList();

            return View(model);
        }

        public IActionResult Details(string id)
        {
            var country = _countries.FirstOrDefault(c => c.CountryId == id);
            return View(country);
        }
    }
}
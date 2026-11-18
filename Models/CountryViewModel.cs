namespace OlympicGames.Models
{
    public class CountryViewModel
    {
        public IEnumerable<Country> Countries { get; set; } = null!;
        public string ActiveGame { get; set; } = "all";
        public string ActiveCategory { get; set; } = "all";

        public string CheckActiveGame(string game) => 
            game?.ToLower() == ActiveGame?.ToLower() ? "active" : "";

        public string CheckActiveCategory(string category) => 
            category?.ToLower() == ActiveCategory?.ToLower() ? "active" : "";
    }
}
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace OlympicGames.Models
{
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }

    public class OlympicSession
    {
        private const string FavKey = "myfavorites";
        private ISession session;

        public OlympicSession(ISession session)
        {
            this.session = session;
        }

        public void SetFavoriteCountries(List<Country> favorites)
        {
            session.SetObject(FavKey, favorites);
        }

        public List<Country> GetFavoriteCountries()
        {
            return session.GetObject<List<Country>>(FavKey) ?? new List<Country>();
        }

        public void RemoveFavorites()
        {
            session.Remove(FavKey);
        }
    }
}
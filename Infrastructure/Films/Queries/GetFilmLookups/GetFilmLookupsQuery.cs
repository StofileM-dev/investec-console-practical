using Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Films.Queries.GetFilmLookups;

public class GetFilmLookupsQuery
{
    private readonly HttpClient _http;

    public GetFilmLookupsQuery(HttpClient http)
    {
        _http = http;
    }

    public async Task<Dictionary<string, string>> GetFilmLookupAsync(IEnumerable<Person> people)
    {
        var filmLookup = new Dictionary<string, string>();

        var filmUrls = people
            .SelectMany(p => p.films)
            .Distinct()
            .ToList();

        foreach (var filmUrl in filmUrls)
        {
            var response = await _http.GetAsync(filmUrl);

            if (!response.IsSuccessStatusCode)
            {
                Console.Error.WriteLine($"Request error on {filmUrl}");
                continue;
            }

            var content = await response.Content.ReadAsStringAsync();
            var filmJson = JsonConvert.DeserializeObject<JObject>(content);

            var title = filmJson?["title"]?.ToString() ?? "Unknown";

            filmLookup[filmUrl] = title;
        }

        if (!filmLookup.Any())
        {
            throw new Exception("Film lookup do not exist found!");
        }

        return filmLookup;
    }
}
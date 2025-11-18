using Domain.Entities;
using Newtonsoft.Json;

namespace Infrastructure.People.Queries.GetAllPeople;

public class GetAllPeopleQuery
{
    private readonly HttpClient _http;

    public GetAllPeopleQuery(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Person>> GetAllPeopleAsync()
    {
        var people = new List<Person>();
        var pageLink = "https://swapi.dev/api/people";

        do
        {
            var response = await _http.GetAsync(pageLink);

            if (!response.IsSuccessStatusCode)
            {
                Console.Error.WriteLine($"Request error on {pageLink}");
                break;
            }

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result>(content);

            if (result?.results == null || !result.results.Any())
            {
                break;
            }

            people.AddRange(result.results);
            pageLink = result.next;
        } while (!string.IsNullOrEmpty(pageLink));

        if (!people.Any())
        {
            throw new Exception("Not people found!");
        }

        return people;
    }
}
using Domain.Entities;

namespace Application.Buddies.Queries.GetBuddiesWithSameFilms;

public static class GetBuddiesWithSameFilmsQuery
{
    public static List<Friend> FindBuddiesWithSameFilms(IEnumerable<Person> people, IReadOnlyDictionary<string, string> filmLookup)
    {
        var buddies = people
                .GroupBy(p => string.Join("|", p.films.OrderBy(url => url)))
                .Where(g => g.Count() > 1)
                .Select(g => new Friend
                {
                    NumberOfFilms = g.First().films.Count,
                    FilmFriends = g
                        .DistinctBy(p => p.name)
                        .Select(p => new PersonFilm
                        {
                            Person = p.name,
                            Films = p.films
                                .Where(url => filmLookup.ContainsKey(url))
                                .Select(url => filmLookup[url])
                                .ToList()
                        })
                        .ToList()
                })
                .OrderByDescending(f => f.NumberOfFilms)
                .ToList();

        if (!buddies.Any())
        {
            throw new Exception("No buddies exist!");
        }

        return buddies;
    }
}
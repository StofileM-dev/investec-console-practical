// See https://aka.ms/new-console-template for more information

using Application.Buddies.Queries.GetBuddiesWithSameFilms;
using Infrastructure.People.Queries.GetAllPeople;
using Infrastructure.Films.Queries.GetFilmLookups;

var _http = new HttpClient();

var peopleQuery = new GetAllPeopleQuery(_http);
var people = await peopleQuery.GetAllPeopleAsync();

var filmQuery = new GetFilmLookupsQuery(_http);
var filmLookup = await filmQuery.GetFilmLookupAsync(people);

var buddies = GetBuddiesWithSameFilmsQuery.FindBuddiesWithSameFilms(people, filmLookup);

foreach (var friend in buddies)
{
    Console.WriteLine($"Number of Films: {friend.NumberOfFilms}");
    Console.WriteLine("Friends:");

    foreach (var person in friend.FilmFriends)
    {
        Console.Write($"- {person.Person} -> ");
        Console.WriteLine(string.Join(", ", person.Films));
    }

    Console.WriteLine();
}
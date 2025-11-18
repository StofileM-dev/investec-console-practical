namespace Domain.Entities;

public class Friend
{
    public int NumberOfFilms { get; set; }
    public IEnumerable<PersonFilm> FilmFriends { get; set; }
}
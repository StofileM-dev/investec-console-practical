namespace Domain.Entities;

public class Result
{
    public int count { get; set; }

    public string next { get; set; }

    public string previous { get; set; }

    public List<Person> results { get; set; }
}
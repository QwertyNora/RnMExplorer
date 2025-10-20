namespace RnMExplorer.Domain;

public sealed class Person
{
    public string FirstName { get; }
    public string LastName { get; }
    public string? Species { get; }
    public string? Status { get; }
    public string? OriginName { get; }
    public int EpisodeCount { get; }

    public Person(
        string firstName,
        string lastName,
        string? species,
        string? status,
        string? originName,
        int episodeCount
    )
    {
        FirstName = firstName;
        LastName = lastName;
        Species = species;
        Status = status;
        OriginName = originName;
        EpisodeCount = episodeCount;
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName,-15} | {Species,-10} | {EpisodeCount,3} eps | {OriginName}";
    }

}
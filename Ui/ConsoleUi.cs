using RnMExplorer.Domain;

namespace RnMExplorer.Ui;

// Responsible for all console user interaction (printing menus and data).
public sealed class ConsoleUi
{
    public void PrintMenu()
    {
        Console.WriteLine("""
        === Rick & Morty Explorer ===
        1) Search by last name prefix
        2) Show top-N characters (by episode count)
        3) Group by species
        4) Show simple projection (Name + Species + Episodes)
        0) Exit
        """);
    }

    private string Truncate(string? value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return "";
        return value.Length <= maxLength ? value : value.Substring(0, maxLength - 1) + "…";
    }


    public void PrintPeople(IEnumerable<Person> people)
    {

        Console.WriteLine("First Name      Last Name       Species         Episodes   Origin");
        Console.WriteLine(new string('-', 80));

        foreach (var p in people)
        {
            var origin = Truncate(p.OriginName, 15);

            Console.WriteLine(
                $"{p.FirstName,-15} {p.LastName,-15} {p.Species,-12} {p.EpisodeCount,5}   {origin,19}"
            );
        }

        Console.WriteLine();
        Console.WriteLine($"Total: {people.Count()} people shown.");
    }

}

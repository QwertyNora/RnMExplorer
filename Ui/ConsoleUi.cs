using RnMExplorer.Domain;

namespace RnMExplorer.Ui;

// Responsible for all console user interaction (printing menus and data).
public sealed class ConsoleUi
{
    // TODO: Display a simple menu with user options.
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

    // TODO: Print a list of Person objects in a table-like format.
    // Example format:
    // Rick Sanchez        | Human      | 51 episodes
    public void PrintPeople(IEnumerable<Person> people)
    {
        // TODO: Loop through each person and print formatted data.
        // Use string interpolation for nice alignment.
    }
}

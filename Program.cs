using RnMExplorer.Domain;
using RnMExplorer.Services;
using RnMExplorer.Infrastructure;
using RnMExplorer.Ui;
using System.Linq;

var cache = new FileCache();

var service = new RnMPeopleService(cache);

var ui = new ConsoleUi();

var people = await service.GetAllAsync();
Console.WriteLine($"Loaded {people.Count} characters.");


var bySpecies = people
    .GroupBy(p => p.Species ?? "(Unknown)")
    .ToDictionary(g => g.Key, g => g.ToList());

while (true)
{
    Console.Clear();
    ui.PrintMenu();
    Console.Write("> ");
    var choice = Console.ReadLine();

    if (choice == "0")
    {
        Console.Clear();
        Console.WriteLine("Goodbye!");
        break;
    }

    switch (choice)
    {
        case "1":
            Console.Clear();
            Console.Write("Enter last name prefix: ");
            var prefix = (Console.ReadLine() ?? "").Trim();

            var filtered = people
                .Where(p => !string.IsNullOrEmpty(p.LastName) &&
                p.LastName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));

            ui.PrintPeople(filtered);
            Console.WriteLine();
            Console.WriteLine("(Press ENTER to return to menu...)");
            Console.ReadLine();
            break;

        case "2":
            Console.Clear();
            Console.Write("Enter N: ");
            var input = Console.ReadLine();
            var ok = int.TryParse(input, out var n);
            if (!ok || n <= 0) n = 5; // fallback

            var topN = people
                .OrderByDescending(p => p.EpisodeCount)
                .ThenBy(p => p.LastName)
                .Take(n);

            ui.PrintPeople(topN);
            Console.WriteLine();
            Console.WriteLine("(Press ENTER to return to menu...)");
            Console.ReadLine();
            break;

        case "3":
            Console.Clear();
            var summary = bySpecies
                .Select(kv => new { Species = kv.Key, Count = kv.Value.Count })
                .OrderByDescending(x => x.Count)
                .ThenBy(x => x.Species);

            foreach (var row in summary)
                Console.WriteLine($"{row.Species,-15} : {row.Count,3}");

            Console.WriteLine();
            Console.WriteLine("(Press ENTER to return to menu...)");
            Console.ReadLine();
            break;

        case "4":
            Console.Clear();
            var projection = people
                .Select(p => new
                {
                    Name = $"{p.FirstName} {p.LastName}".Trim(),
                    Species = p.Species ?? "(Unknown)",
                    Episodes = p.EpisodeCount
                })
                .OrderBy(x => x.Name);

            Console.WriteLine("Name                 Species        Episodes");
            Console.WriteLine(new string('-', 55));
            foreach (var x in projection)
                Console.WriteLine($"{x.Name,-20}  {x.Species,-12}  {x.Episodes,7}");

            Console.WriteLine();
            Console.WriteLine("(Press ENTER to return to menu...)");
            Console.ReadLine();
            break;

        default:
            Console.Clear();
            Console.WriteLine("Unknown option.");
            Console.WriteLine();
            Console.WriteLine("(Press ENTER to return to menu...)");
            Console.ReadLine();
            break;
    }

    Console.WriteLine();
}

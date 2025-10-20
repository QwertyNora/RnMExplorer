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
    ui.PrintMenu();
    Console.Write("> ");
    var choice = Console.ReadLine();

    if (choice == "0") break;

    switch (choice)
    {
        case "1":

            Console.Write("Enter last name prefix: ");
            var prefix = (Console.ReadLine() ?? "").Trim();

            var filtered = people
                .Where(p => !string.IsNullOrEmpty(p.LastName) &&
                p.LastName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));

            ui.PrintPeople(filtered);
            break;

        case "2":

            Console.Write("Enter N: ");
            var input = Console.ReadLine();
            var ok = int.TryParse(input, out var n);
            if (!ok || n <= 0) n = 5; // fallback

            var topN = people
                .OrderByDescending(p => p.EpisodeCount)
                .ThenBy(p => p.LastName)
                .Take(n);

            ui.PrintPeople(topN);
            break;

        case "3":

            var summary = bySpecies
                .Select(kv => new { Species = kv.Key, Count = kv.Value.Count })
                .OrderByDescending(x => x.Count)
                .ThenBy(x => x.Species);

            foreach (var row in summary)
                Console.WriteLine($"{row.Species,-15} : {row.Count,3}");

            break;

        case "4":
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

            break;

        default:
            Console.WriteLine("Unknown option.");
            break;
    }

    Console.WriteLine();
}

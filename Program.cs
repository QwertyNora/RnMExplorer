using RnMExplorer.Domain;
using RnMExplorer.Services;
using RnMExplorer.Infrastructure;
using RnMExplorer.Ui;
using System.Linq;

var cache = new FileCache();

var testPerson = new Person(
    "Rick",
    "Sanchez",
    "Human",
    "Alive",
    "Earth (C-137)",
    51
);

Console.WriteLine(testPerson);



// var service = new RnMPeopleService(cache);
// var ui = new ConsoleUi();

// Console.WriteLine("Fetching Rick and Morty characters... please wait.");

// // TODO: Call GetAllAsync() to fetch all people.
// var people = await service.GetAllAsync();

// // TODO: Build a dictionary where key = species, value = list of people
// // Use LINQ: GroupBy + ToDictionary
// // Example: Dictionary<string, List<Person>> bySpecies = ...

// while (true)
// {
//     ui.PrintMenu();
//     Console.Write("> ");
//     var choice = Console.ReadLine();

//     if (choice == "0") break;

//     switch (choice)
//     {
//         case "1":
//             // TODO: Ask for a prefix input
//             // TODO: Use LINQ .Where() to filter by last name prefix
//             // TODO: Print the filtered results
//             break;

//         case "2":
//             // TODO: Ask for N (how many top characters)
//             // TODO: Order by descending EpisodeCount
//             // TODO: Take(N) and print the results
//             break;

//         case "3":
//             // TODO: Use LINQ to group by species
//             // TODO: Select new { Species, Count }
//             // TODO: Order by descending Count
//             // TODO: Print formatted summary
//             break;

//         case "4":
//             // TODO: Use LINQ .Select() to project into new anonymous object
//             // Example: new { Name, Species, EpisodeCount }
//             // TODO: Print all results
//             break;

//         default:
//             Console.WriteLine("Unknown option.");
//             break;
//     }

//     Console.WriteLine();
// }

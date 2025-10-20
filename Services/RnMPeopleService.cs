using System.Net.Http.Headers;
using System.Text.Json;
using RnMExplorer.Domain;
using RnMExplorer.Infrastructure;

namespace RnMExplorer.Services;

public sealed class RnMPeopleService : IPeopleService
{
    private readonly HttpClient _http = new() { BaseAddress = new Uri("https://rickandmortyapi.com/") };

    private readonly FileCache _cache;

    private readonly JsonSerializerOptions _json = new(JsonSerializerDefaults.Web);

    public RnMPeopleService(FileCache cache) => _cache = cache;

    public async Task<IReadOnlyList<Person>> GetAllAsync(CancellationToken ct = default)
    {
        //URL for first page
        var url = "api/character?page=1";

        // Filename for cache
        var cacheFile = "rnm-characters-page-1.json";

        // Read from cache if possible, otherwise get
        string json;
        if (_cache.TryRead(cacheFile, out var cached) && !string.IsNullOrWhiteSpace(cached))
        {
            json = cached;
        }
        else
        {
            var response = await _http.GetAsync(url, ct);
            response.EnsureSuccessStatusCode();
            json = await response.Content.ReadAsStringAsync(ct);
            await _cache.WriteAsync(cacheFile, json, ct);
        }

        // Deserialize
        var page = JsonSerializer.Deserialize<RnMPage>(json, _json) ?? new RnMPage();

        // Map to Domainmodel Person
        var people = new List<Person>();
        foreach (var character in page.Results)
        {
            var fullName = character.Name ?? "";
            var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var first = parts.Length > 0 ? parts[0] : "";
            var last = parts.Length > 0 ? parts[^1] : "";

            var person = new Person(
                firstName: first,
                lastName: last,
                species: character.Species,
                status: character.Status,
                originName: character.Origin?.Name,
                episodeCount: character.Episode.Count
            );

            people.Add(person);
        }

        return people;
    }
}

file sealed class RnMPage
{
    public RnMInfo? Info { get; set; }
    public List<RnmCharacter> Results { get; set; } = [];
}

file sealed class RnMInfo
{
    public int Count { get; set; }
    public int Pages { get; set; }
    public string? Next { get; set; }
}

file sealed class RnmCharacter
{
    public string? Name { get; set; }
    public string? Species { get; set; }
    public string? Status { get; set; }
    public RnMOrigin? Origin { get; set; }
    public List<string> Episode { get; set; } = [];
}

file sealed class RnMOrigin
{
    public string? Name { get; set; }
}
using System.Net.Http.Headers;
using System.Text.Json;
using RnMExplorer.Domain;
using RnMExplorer.Infrastructure;

namespace RnMExplorer.Services;

// TODO: Create the internal DTO-classes that matches the API (Rick and Morty):
// - RnmPage { RnmInfo? Info; List<RnmCharacter> Results; }
// - RnmInfo { int Count; int Pages; string? Next; }
// - RnmCharacter { string? Name; string? Species; string? Status; RnmOrigin? Origin; List<string> Episode; DateTime? Created; }
// - RnmOrigin { string? Name; }

public sealed class RnMPeopleService : IPeopleService
{
    // TODO: Declare fileds for HttpClient, FileCache, JsonSerializerOptions

    private readonly HttpClient _http = new() { BaseAddress = new Uri("https://rickandmortyapi.com/") };

    private readonly FileCache _cache;

    private readonly JsonSerializerOptions _json = new(JsonSerializerDefaults.Web);

    public RnMPeopleService(FileCache cache) => _cache = cache;

    // TODO: Implement GetAllAsync()
    // 1. Get first page: "api/character?page=1"
    // 2. While != null, continue
    // 3. Cacha every page in Data/ as rnm-characters-page-{page}.json
    // 4. Deserialize JSON to RnmPage with System.Text.Json
    // 5. Map to Person-object (split Name to FirstName/LastName, count Episode.Count)
}
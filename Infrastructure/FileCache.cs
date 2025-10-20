using System.Text;

namespace RnMExplorer.Infrastructure;

// Handles saving and loading JSON files locally.
public sealed class FileCache
{
    private readonly string _dataDir;

    public FileCache()
    {
        var baseDir = AppContext.BaseDirectory;

        var dataDir = Path.Combine(baseDir, "Data");

        Directory.CreateDirectory(dataDir);

        _dataDir = dataDir;
        Console.WriteLine($"[FileCache] Using data dir: {_dataDir}");
        var info = Directory.CreateDirectory(dataDir); // idempotent; safe to call twice
        Console.WriteLine($"[FileCache] Data dir path: {info.FullName}");
        Console.WriteLine($"[FileCache] Exists? {Directory.Exists(info.FullName)}");


    }

    // TODO: WriteAsync(fileName, content)
    // Purpose: Save the given text content to a file inside the Data folder.
    // Steps:
    // 1. Combine _dataDir and fileName using Path.Combine.
    // 2. Use File.WriteAllTextAsync() to write the content.
    // 3. Use Encoding.UTF8.
    public async Task WriteAsync(string fileName, string content, CancellationToken ct = default)
    {
        var path = Path.Combine(_dataDir, fileName);

        await File.WriteAllTextAsync(path, content, Encoding.UTF8, ct);
        Console.WriteLine($"[FileCache] Wrote {content?.Length ?? 0} chars to {path}");

    }

    // TODO: TryRead(fileName, out content)
    // Purpose: Try to read the content of a file.
    // Steps:
    // 1. Combine _dataDir and fileName using Path.Combine.
    // 2. If the file exists, read it using File.ReadAllText().
    // 3. Set 'content' to the file content and return true.
    // 4. If not found, set 'content' to null and return false.
    public bool TryRead(string fileName, out string? content)
    {
        var path = Path.Combine(_dataDir, fileName);

        if (!File.Exists(path))
        {
            content = null;
            return false;
        }

        content = File.ReadAllText(path, Encoding.UTF8);
        return true;
    }
}
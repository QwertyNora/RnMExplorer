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

    }

    public async Task WriteAsync(string fileName, string content, CancellationToken ct = default)
    {
        var path = Path.Combine(_dataDir, fileName);

        await File.WriteAllTextAsync(path, content, Encoding.UTF8, ct);
        Console.WriteLine($"[FileCache] Wrote {content?.Length ?? 0} chars to {path}");

    }

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
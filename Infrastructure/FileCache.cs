using System.Text;

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

    // TODO: WriteAsync(fileName, content)
    // Purpose: Save the given text content to a file inside the Data folder.
    // Steps:
    // 1. Combine _dataDir and fileName using Path.Combine.
    // 2. Use File.WriteAllTextAsync() to write the content.
    // 3. Use Encoding.UTF8.
    public async Task WriteAsync(string fileName, string content, CancellationToken ct = default)
    {
        // TODO: Implement file writing logic here
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
        // TODO: Implement file reading logic here
        content = null;
        return false;
    }
}
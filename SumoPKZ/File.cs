namespace SumoPKZ;

public interface IFile : IDisposable, IAsyncDisposable
{
    public string Path { get; }
    public Task<byte[]> ReadHeader(CancellationToken cancellationToken);
    public Stream GetContent();
}

public sealed class File : IFile
{
    public string Path { get; }
    private readonly FileStream _fileStream;

    public void Dispose() => _fileStream.Dispose();
    public ValueTask DisposeAsync() => _fileStream.DisposeAsync();

    public async Task<byte[]> ReadHeader(CancellationToken cancellationToken)
    {
        _fileStream.Seek(0, SeekOrigin.Begin);
        var header = new byte[16];
        var bytesRead = 0;
        while (bytesRead < 16)
        {
            bytesRead += await _fileStream.ReadAsync(header, bytesRead, 16 - bytesRead, cancellationToken);
        }
        return header;
    }

    public Stream GetContent()
    {
        _fileStream.Seek(16, SeekOrigin.Begin);
        return _fileStream;
    }

    public File(string filePath)
    {
        Path = filePath;
        _fileStream = new FileStream(filePath, FileMode.Open);
    }
}
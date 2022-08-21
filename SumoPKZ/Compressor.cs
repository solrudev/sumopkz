using System.IO.Compression;
using SumoPKZ.Exceptions;

namespace SumoPKZ;

public interface ICompressor
{
    Task Compress(Operation operation, CancellationToken cancellationToken);
}

public sealed class Compressor : ICompressor
{
    public async Task Compress(Operation operation, CancellationToken cancellationToken)
    {
        byte[] header = await operation.File.ReadHeader(cancellationToken);
        if (!Utils.IsPKM(header))
        {
            throw new NotPKMException();
        }

        header[6] ^= 0x80;

        string outputPath = operation.OutputPath is null
            ? Path.ChangeExtension(operation.File.Path, "pkz")
            : Path.Combine(operation.OutputPath, Path.GetFileNameWithoutExtension(operation.File.Path) + ".pkz");

        await using var outputFileStream = new FileStream(outputPath, FileMode.Create);
        await using var zlibStream = new ZLibStream(outputFileStream, CompressionLevel.Optimal);
        await outputFileStream.WriteAsync(header, 0, header.Length, cancellationToken);
        await operation.File.GetContent().CopyToAsync(zlibStream, cancellationToken);
    }
}
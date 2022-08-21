using System.IO.Compression;
using SumoPKZ.Exceptions;

namespace SumoPKZ;

public interface IDecompressor
{
    Task Decompress(Operation operation, CancellationToken cancellationToken);
}

public sealed class Decompressor : IDecompressor
{
    public async Task Decompress(Operation operation, CancellationToken cancellationToken)
    {
        byte[] header = await operation.File.ReadHeader(cancellationToken);
        if (!Utils.IsPKM(header))
        {
            throw new NotPKMException();
        }

        header[6] ^= 0x80;

        string outputPath = operation.OutputPath is null
            ? Path.ChangeExtension(operation.File.Path, "pkm")
            : Path.Combine(operation.OutputPath, Path.GetFileNameWithoutExtension(operation.File.Path) + ".pkm");

        await using var zlibStream = new ZLibStream(operation.File.GetContent(), CompressionMode.Decompress);
        await using var outputFileStream = new FileStream(outputPath, FileMode.Create);
        await outputFileStream.WriteAsync(header, 0, header.Length, cancellationToken);
        await zlibStream.CopyToAsync(outputFileStream, cancellationToken);
    }
}
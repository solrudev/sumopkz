namespace SumoPKZ;

public interface IOperationVisitor
{
    public Task Compress(CompressOperation operation, CancellationToken cancellationToken);
    public Task Decompress(DecompressOperation operation, CancellationToken cancellationToken);
}

public sealed class OperationVisitor : IOperationVisitor
{
    private readonly ICompressor _compressor;
    private readonly IDecompressor _decompressor;

    public OperationVisitor(ICompressor compressor, IDecompressor decompressor)
    {
        _compressor = compressor;
        _decompressor = decompressor;
    }

    public Task Compress(CompressOperation operation, CancellationToken cancellationToken) =>
        _compressor.Compress(operation, cancellationToken);

    public Task Decompress(DecompressOperation operation, CancellationToken cancellationToken) =>
        _decompressor.Decompress(operation, cancellationToken);
}
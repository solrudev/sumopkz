namespace SumoPKZ;

public abstract record Operation(IFile File, string? OutputPath)
{
	public abstract Task Accept(IOperationVisitor operationVisitor, CancellationToken cancellationToken);
}

public sealed record CompressOperation(IFile File, string? OutputPath) : Operation(File, OutputPath)
{
	public override Task Accept(IOperationVisitor operationVisitor, CancellationToken cancellationToken) =>
		operationVisitor.Compress(this, cancellationToken);
}

public sealed record DecompressOperation(IFile File, string? OutputPath) : Operation(File, OutputPath)
{
	public override Task Accept(IOperationVisitor operationVisitor, CancellationToken cancellationToken) =>
		operationVisitor.Decompress(this, cancellationToken);
}
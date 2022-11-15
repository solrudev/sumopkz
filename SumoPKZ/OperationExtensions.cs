namespace SumoPKZ;

public static class OperationExtensions
{
	public static string OutputPathWithExtension(this Operation operation, string extension)
	{
		return operation.OutputPath is null
			? Path.ChangeExtension(operation.File.Path, extension)
			: Path.Combine(
				operation.OutputPath,
				Path.GetFileNameWithoutExtension(operation.File.Path) + $".{extension}"
			);
	}
}
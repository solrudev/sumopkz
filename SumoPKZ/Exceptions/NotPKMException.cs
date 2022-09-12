namespace SumoPKZ.Exceptions;

public sealed class NotPKMException : Exception
{
	public override string Message => "Provided file is not a valid PKM file.";
}
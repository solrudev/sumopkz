namespace SumoPKZ.Exceptions;

public class InvalidArgumentException : Exception
{
	public override string Message => $"Invalid argument: {_argumentName}";
	private readonly string _argumentName;

	public InvalidArgumentException(string argumentName)
	{
		_argumentName = argumentName;
	}
}
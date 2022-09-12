namespace SumoPKZ.Exceptions;

public sealed class UnrecognizedCommandException : Exception
{
	public override string Message => $"Unrecognized command: {_command}";
	private readonly string _command;

	public UnrecognizedCommandException(string command)
	{
		_command = command;
	}
}
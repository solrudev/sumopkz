namespace SumoPKZ.Exceptions;

public class ArgumentMissingException : Exception
{
    public override string Message => $"Argument missing: {_argumentName}";
    private readonly string _argumentName;

    public ArgumentMissingException(string argumentName)
    {
        _argumentName = argumentName;
    }
}
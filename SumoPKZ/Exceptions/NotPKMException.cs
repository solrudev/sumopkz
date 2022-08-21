namespace SumoPKZ.Exceptions;

public class NotPKMException : Exception
{
    public override string Message => "Provided file is not a valid PKM file.";
}
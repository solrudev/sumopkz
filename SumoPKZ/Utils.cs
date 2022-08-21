using System.Text;

namespace SumoPKZ;

public static class Utils
{
    public const string HelpText =
        """
        SumoPKZ 0.0.1
        
        Usage: sumopkz command <input file path> [output directory]
        
        Command: 
        c, compress        Compress provided PKM file
        d, decompress      Decompress provided PKZ file
        h, help            Display this help
        
        Note: output directory is optional, by default output file will be saved next to the original file with its extension changed.
        """;

    public static bool IsPKM(byte[] fileHeader)
    {
        var magicNumber = new ReadOnlySpan<byte>(fileHeader, 0, 3);
        string magicNumberString = Encoding.UTF8.GetString(magicNumber);
        return magicNumberString == "PKM";
    }
}
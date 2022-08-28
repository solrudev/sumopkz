using System.Text;

namespace SumoPKZ;

public static class Utils
{
	public const string HelpText =
		"""
        SumoPKZ 0.0.2
        
        Usage: sumopkz command <input file path> [output directory]
        
        Command: 
        c, compress        Compress provided PKM file
        d, decompress      Decompress provided PKZ file
        h, help            Display this help
        
        Note: output directory is optional, by default output file will be saved next to the original file with its extension changed.
        """;

	// magic number: "PKM "
	public static bool IsPKM(byte[] fileHeader) => fileHeader[0] == 0x50 && fileHeader[1] == 0x4B &&
	                                               fileHeader[2] == 0x4D && fileHeader[3] == 0x20;
}
using SumoPKZ;
using SumoPKZ.Exceptions;
using File = SumoPKZ.File;

if (args.Length == 0 || args[0] == "h" || args[0] == "help")
{
	Console.WriteLine(Utils.HelpText);
	return;
}

var cancellationTokenSource = new CancellationTokenSource();

AppDomain.CurrentDomain.ProcessExit += (_, _) => cancellationTokenSource.Dispose();

Console.CancelKeyPress += (_, eventArgs) =>
{
	cancellationTokenSource.Cancel();
	eventArgs.Cancel = true;
};

string command = args.FirstOrDefault() ?? throw new ArgumentMissingException("command");
string inputFile = args.ElementAtOrDefault(1) ?? throw new ArgumentMissingException("input file path");
string? outputPath = args.ElementAtOrDefault(2);

await using var file = new File(inputFile);

Operation operation = command switch
{
	"c" or "compress" => new CompressOperation(file, outputPath),
	"d" or "decompress" => new DecompressOperation(file, outputPath),
	_ => throw new UnrecognizedCommandException(command)
};

var operationVisitor = new OperationVisitor(new Compressor(), new Decompressor());
await operation.Accept(operationVisitor, cancellationTokenSource.Token);

Console.WriteLine($"{inputFile} processed successfully.");
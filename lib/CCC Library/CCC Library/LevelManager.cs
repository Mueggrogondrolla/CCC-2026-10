using CCCLibrary.Serialization;
using CCCLibrary.Serialization.Interfaces;

namespace CCCLibrary;

// TODO Rename LevelManager to something different and all Manager classes as well
public sealed class LevelManager<TInput, TOutput> : ILevelManager where TInput : IInputData, new() where TOutput : IOutputData
{
	private readonly LevelFileLocator _levelFiles;
	private readonly ILevelSolver<TInput, TOutput> _levelSolver;
	private readonly SerializationOptions _serializationOptions;


    public LevelManager(LevelFileLocator fileLocator, ILevelSolver<TInput, TOutput> levelSolver, SerializationOptions serializationOptions)
	{
		_levelFiles = fileLocator;
		_levelSolver = levelSolver;
		_serializationOptions = serializationOptions;
    }

	/// <summary>
	/// Generates the output file for the example input file.
	/// </summary>
	public void GenerateExampleOutputFile()
	{
		var inputFile = _levelFiles.GetExampleInputFile();
		var input = InputParser.Parse<TInput>(inputFile.FullName, _serializationOptions);
		var output = _levelSolver.SolveFor(input);
		var serializedOutput = OutputGenerator.Generate(output, _serializationOptions.FormatProvider);
		var outputFile = Utilities.OutputFileGenerator.GenerateMatchingOutputFile(inputFile);
		File.WriteAllText(outputFile.FullName, serializedOutput);
	}

	/// <summary>
	/// Verifies that the generated example output file matches the provided example output file exactly.
	/// </summary>
	public bool VerifyGeneratedExampleOutputFile(bool throwIfDifferent = true, bool openDiffViewIfDifferent = true)
	{
		var exampleOutputFile = _levelFiles.GetExampleOutputFile();
		var ownExampleOutputFile = _levelFiles.GetOwnExampleOutputFile();

		if (File.ReadAllText(ownExampleOutputFile.FullName).ReplaceLineEndings("") != File.ReadAllText(exampleOutputFile.FullName).ReplaceLineEndings(""))
		{
			if (openDiffViewIfDifferent)
			{
				//_ = Process.Start(@$"fc ""{exampleOutputFile.FullName}"" ""{ownExampleOutputFile.FullName}""");
			}

            return throwIfDifferent ? throw new Exception("Generated example output does not match provided example output.") : false;
        }

		return true;
	}

	/// <summary>
	/// Generates the output files for all input files.
	/// </summary>
	public void GenerateOutputFiles()
	{
		foreach (FileInfo inputFile in _levelFiles.GetInputFiles())
		{
			var input = InputParser.Parse<TInput>(inputFile.FullName, _serializationOptions);
			var output = _levelSolver.SolveFor(input);
			var serializedOutput = OutputGenerator.Generate(output, _serializationOptions.FormatProvider);
			var outputFile = Utilities.OutputFileGenerator.GenerateMatchingOutputFile(inputFile);
			File.WriteAllText(outputFile.FullName, serializedOutput);
		}
	}
}

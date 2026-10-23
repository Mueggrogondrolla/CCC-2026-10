using System.Collections.Immutable;

namespace CCCLibrary;

/// <summary>
/// Run contest levels on input files and generate their output files.
/// </summary>
/// <param name="workingDirectory">Directory where all the level files
/// (input, output, description) for this contest are located.</param>
public sealed class Contest(string workingDirectory)
{
    private readonly Dictionary<int, Func<string, string>> _levelSolvers = new();
    private readonly Dictionary<int, LevelFileLocator> _levelFiles = new();

    /// <summary>
    /// Gets the set of added levels.
    /// </summary>
    /// <returns>An immutable set of the level numbers that have been added.</returns>
    public ImmutableHashSet<int> GetAddedLevels() => _levelSolvers.Keys.ToImmutableHashSet();

    /// <summary>
    /// Adds a solver for the specified level.
    /// </summary>
    /// <param name="levelNumber">Identifier of the level.</param>
    /// <param name="solver">Transforms input file content to output file content for the level.</param>
    /// <exception cref="InvalidOperationException">A solver was already added for the level before.</exception>
    public void AddLevelSolver(int levelNumber, Func<string, string> solver)
    {
        if (!_levelSolvers.TryAdd(levelNumber, solver))
        {
            throw new InvalidOperationException($"There is already a level solver for Level {levelNumber}.");
        }

        _levelFiles.Add(levelNumber, new LevelFileLocator(workingDirectory, levelNumber));
    }

    /// <summary>
    /// Generates the output file for the example input file of the given level.
    /// </summary>
    /// <param name="levelNumber">The level to use.</param>
    public void RunSolverForExampleInputFileOfLevel(int levelNumber)
    {
        ThrowIfLevelIsNotRegistered(levelNumber);

        Console.WriteLine("Generate example output file");

        RunLevelForFile(levelNumber, _levelFiles[levelNumber].GetExampleInputFile());

        Console.WriteLine("Done");
    }

    /// <summary>
    /// Generates the output files for all input files of the given level.
    /// </summary>
    /// <param name="levelNumber">The level to use.</param>
    public void RunSolverForAllInputFilesOfLevel(int levelNumber)
    {
        ThrowIfLevelIsNotRegistered(levelNumber);

        Console.WriteLine("Generate all output files");

        foreach (var file in _levelFiles[levelNumber].GetInputFiles())
        {
            RunLevelForFile(levelNumber, file);
        }

        Console.WriteLine("Done");
    }

    /// <summary>
    /// Verifies that the generated example output file matches the provided example output file exactly.
    /// </summary>
    /// <remarks>Line endings are ignored.</remarks>
    /// <param name="levelNumber">The level to check.</param>
    /// <param name="throwIfDifferent">If <see langword="true"/>, an exception
    /// is thrown if the files do not match exactly.</param>
    /// <param name="openDiffViewIfDifferent">Unused.</param>
    /// <returns>True, if the files match exactly; otherwise false.</returns>
    /// <exception cref="Exception"><paramref name="throwIfDifferent"/>
    /// is enabled and example files do not match.</exception>
    public bool VerifyGeneratedExampleOutputFile(int levelNumber, bool throwIfDifferent = true,
        bool openDiffViewIfDifferent = true)
    {
        ThrowIfLevelIsNotRegistered(levelNumber);

        Console.Write("Verifying generated example output file ... ");

        var providedExampleOutputFile = _levelFiles[levelNumber].GetExampleOutputFile();
        var ownExampleOutputFile = _levelFiles[levelNumber].GetOwnExampleOutputFile();

        var providedExampleData = File.ReadAllText(providedExampleOutputFile.FullName).ReplaceLineEndings("");
        var ownExampleData = File.ReadAllText(ownExampleOutputFile.FullName).ReplaceLineEndings("");
        if (ownExampleData == providedExampleData)
        {
            Console.WriteLine("files do match");
            return true;
        }

        Console.WriteLine("files do not match");

        if (openDiffViewIfDifferent)
        {
            //_ = Process.Start(@$"fc ""{exampleOutputFile.FullName}"" ""{ownExampleOutputFile.FullName}""");
        }

        return throwIfDifferent
            ? throw new Exception("Generated example output does not match provided example output.")
            : false;
    }

    private void RunLevelForFile(int levelNumber, FileInfo inputFile)
    {
        Console.WriteLine($"Generating output file for '{inputFile.Name}'");

        var input = File.ReadAllText(inputFile.FullName);

        var output = _levelSolvers[levelNumber](input);

        var outputFile = Utilities.OutputFileGenerator.GenerateMatchingOutputFile(inputFile);
        File.WriteAllText(outputFile.FullName, output);
    }

    private void ThrowIfLevelIsNotRegistered(int levelNumber)
    {
        if (!_levelSolvers.ContainsKey(levelNumber))
            throw new InvalidOperationException($"No solver has been added for Level {levelNumber}.");
    }
}
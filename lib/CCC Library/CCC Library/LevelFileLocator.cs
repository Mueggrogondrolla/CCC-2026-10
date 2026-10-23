namespace CCCLibrary;

/// <summary>
/// Locates level files for a chosen level.
/// </summary>
public sealed class LevelFileLocator
{
    private readonly DirectoryInfo _workingDirectory;
    private readonly int _levelId;

    /// <summary>
    /// Creates a new locator to find level files for a chosen level within a given working directory and its subdirectories.
    /// </summary>
    /// <param name="workingDirectory">The directory containing level files.</param>
    /// <param name="levelId">The level number.</param>
    public LevelFileLocator(string workingDirectory, int levelId)
    {
        if (!Directory.Exists(workingDirectory))
        {
            throw new ArgumentException(@$"Directory ""{new DirectoryInfo(workingDirectory).FullName}"" does not exist.", nameof(workingDirectory));
        }

        _workingDirectory = new DirectoryInfo(workingDirectory);
        _levelId = levelId;
    }


    /// <summary>
    /// Gets the level info file.
    /// </summary>
    public FileInfo GetInfoFile()
    {
        return _workingDirectory.EnumerateFiles($"Level{_levelId}*.pdf", SearchOption.AllDirectories).Single();
    }

    /// <summary>
    /// Gets the example input file.
    /// </summary>
    public FileInfo GetExampleInputFile()
    {
        return _workingDirectory.EnumerateFiles($"in_level-{_levelId}*example.txt", SearchOption.AllDirectories).Single();
    }

    /// <summary>
    /// Gets the example output file.
    /// </summary>
    public FileInfo GetExampleOutputFile()
    {
        return _workingDirectory.EnumerateFiles($"out_level-{_levelId}*example.txt", SearchOption.AllDirectories).Single();
    }

    /// <summary>
    /// Gets the own example output file.
    /// </summary>
    public FileInfo GetOwnExampleOutputFile()
    {
        return _workingDirectory.EnumerateFiles($"out_generated_level-{_levelId}*example.txt", SearchOption.AllDirectories).Single();
    }

    /// <summary>
    /// Gets all input files including the example input file.
    /// </summary>
    public IEnumerable<FileInfo> GetInputFiles()
    {
        return _workingDirectory.EnumerateFiles($"in_level-{_levelId}*.txt", SearchOption.AllDirectories);
    }

    /// <summary>
    /// Gets all output files including the example output file.
    /// </summary>
    public IEnumerable<FileInfo> GetOutputFiles()
    {
        return _workingDirectory.EnumerateFiles($"out_level-{_levelId}*.txt", SearchOption.AllDirectories);
    }
}

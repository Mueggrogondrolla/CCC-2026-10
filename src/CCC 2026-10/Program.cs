using System.Collections.Immutable;
using CCCLibrary;

namespace CCC_2026_10;

internal static class Program
{
    // Directory that contains all contest files
    private const string WorkingDirectory = @"../../../../../contest-files";

    public static void Main(string[] args)
    {
        var contest = CreateContest(WorkingDirectory);
        var level = GetLevelNumberFromUser(contest.GetAddedLevels());

        RunLevel(contest, level);
        Console.WriteLine("Success");
    }

    private static Contest CreateContest(string workingDirectory)
    {
        var contest = new Contest(workingDirectory);
        var levels = new Levels();

        contest.AddLevelSolver(1, levels.SolveLevel1);
        // contest.AddLevelSolver(2, levels.SolveLevel2);
        // contest.AddLevelSolver(3, levels.SolveLevel3);
        // contest.AddLevelSolver(4, levels.SolveLevel4);
        // contest.AddLevelSolver(5, levels.SolveLevel5);
        // contest.AddLevelSolver(6, levels.SolveLevel6);
        // contest.AddLevelSolver(7, levels.SolveLevel7);

        return contest;
    }

    private static void RunLevel(Contest contest, int levelNumber)
    {
        contest.RunSolverForExampleInputFileOfLevel(levelNumber);

        if (!contest.VerifyGeneratedExampleOutputFile(levelNumber, throwIfDifferent: false,
                openDiffViewIfDifferent: false))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Warning: The generated example output file does not match the provided one.");
            Console.ResetColor();
        }

        contest.RunSolverForAllInputFilesOfLevel(levelNumber);
    }

    private static int GetLevelNumberFromUser(ImmutableHashSet<int> validLevels)
    {
        Console.Write("Select Level: ");

        int levelNumber;
        while (!int.TryParse(Console.ReadLine(), out levelNumber) || !validLevels.Contains(levelNumber))
        {
            Console.WriteLine($"Invalid level number. Valid numbers are: {string.Join(", ", validLevels)}");
            Console.Write("Select Level: ");
        }

        return levelNumber;
    }
}
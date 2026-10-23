# Overview

| Class              | Description                                                                                                                                   |
|--------------------|-----------------------------------------------------------------------------------------------------------------------------------------------|
| `Contest`          | Tell it how to solve levels, then use it to generate the output files for those levels.                                                       |
| `LevelFileLocator` | Provides easy access to all contest files within a working directory.                                                                         |
| `InputLexer`       | Splits strings by a set of separator characters. Select separators once, apply to any number of strings.<br/><br/>Intended for input parsing. |

# Features

## Creating output files

Using the `Contest` class, output files can easily be created with minimal setup required. Simply create an instance of
`Contest`, add your level solver algorithms to it and call its output file creation methods.

```csharp
// Specify the directory with the level files (input, example output, instructions)
var workingDirectory = "C:\Programming\CCC 2026-11\_Resources\\";

// Create a Contest object and add solver logic for levels  
var contest = new Contest(workingDirectory);
contest.AddLevelSolver(1, new Levels().SolveLevel1);
contest.AddLevelSolver(2, new Levels().SolveLevel2);

// Select a level and generate output files
while (true) 
{
    Console.Write("Choose level to run (e.g. 1): ")
    var levelNumber = int.Parse(Console.ReadLine());
    
    // Generate the example output file first
    contest.RunSolverForExampleInputFileOfLevel(levelNumber);
    
    // Warn if the generated example output file does not match the provided one
    var exampleOutputsAreEqual = contest.VerifyGeneratedExampleOutputFile(levelNumber, throwIfDifferent: false);    
    if (!exampleOutputsAreEqual)
    {
        Console.ForegroundColor =  ConsoleColor.Yellow;
        Console.WriteLine("Warning: Generated example output file does not match provided example output file.");
        Console.ResetColor();
    }
    
    // Generate all output files
    contest.RunSolverForAllInputFilesOfLevel(levelNumber);
}

class Levels()
{
    public string SolveLevel1(string input) { /* ... */ }    
    public string SolveLevel2(string input) { /* ... */ }
}
```

## Parsing input files

### Example from old contest

```csharp
internal class Level3Solver : ILevelSolver<Level3Input, Level3Output>
{
    public Level3Output SolveFor(Level3Input input)
    {
        var lines = InputUtils.GetInputLines(input.FileContent);
        var trips = InputUtils.ReadTrips(lines.Skip(1));
        var movementSequences = new List<MovementSequence>();
        foreach (var trip in trips)
        {
            var driver = new Driver();
            movementSequences.Add(driver.DriveFromAToB(0, trip.TargetSpaceStation.Position, trip.TimeConstraint.MaxDuration));
        }
        var resultLines = movementSequences.Select(sequence => sequence.ToOutputString());
        return new Level3Output() { Result = OutputUtils.ListToString(resultLines) };
    }
}
```

```csharp
public static class InputUtils
{
    public static IEnumerable<string> GetInputLines(string fileContent)
    {
        InputLexer lineLexer = new InputLexer(['\n', '\r']);
        return lineLexer.GetTokens(fileContent);
    }
    
    public static List<MovementSequence> ReadMovementSequences(IEnumerable<string> movementSequenceLines)
    {
        InputLexer spaceLexer = new InputLexer([' ', '\t']);
        
        var movementSequences = new List<MovementSequence>();
        foreach (var line in movementSequenceLines)
        {
            var paces = spaceLexer.GetTokens(line).Select(value => new Pace(int.Parse(value)));
            movementSequences.Add(new MovementSequence(paces));
        }
        
        return movementSequences;
    }

    public static IEnumerable<Trip> ReadTrips(IEnumerable<string> tripLines)
    {
        return tripLines
            .Select(line => line.Split(' '))
            .Select(elements => (elements[0], elements[1]))
            .Select(trip => new Trip(new Spacestation(int.Parse(trip.Item1)), new TimeContraint(int.Parse(trip.Item2))));
    }
}
```

```csharp
internal static class OutputUtils
{
    public static string ListToString(IEnumerable<string> elements)
    {
        return string.Join(Environment.NewLine, elements);
    }
}
```

### InputLexer

Here are some examples on how to use the `InputLexer` class for parsing input data.

```csharp
class Levels()
{
    // This lexer splits the input file at every space, tab and newline.
    // Empty entries will be removed.
    private readonly InputLexer Lexer { get; } = new InputLexer(new[] { ' ', '\n', '\r', '\t' }); 
    
    // Example 1: Parse input using InputLexer with a Queue<string>.
    public string SolveLevel1(string input)
    {
        var tokens = new Queue<string>(Lexer.GetTokens(input));
        
        var count = int.Parse(tokens.Dequeue());
        var prices = Enumerable.Range(0, count).Select(_ => int.Parse(tokens.Dequeue()));
        
        // When you are done with parsing you should check that no data is left over
        if (tokens.Count > 0)
            throw new SerializationException("There is left over data.");
    }
    
    // Example 2: Parse input using InputLexer with a string[].
    public string SolveLevel2(string input)
    {
        var tokens = Lexer.GetTokens(input).ToArray();
        var index = 0;
        
        var dimension1 = int.Parse(tokens[index++]);
        var dimension2 = int.Parse(tokens[index++]);
        var grid = new int[dimension1, dimension2];
        for (int row = 0; row < dimension1; row++)
        {
            for (int column = 0; column < dimension2; column++)
            {
                grid[row, column] = int.Parse(tokens[index++]);
            }
        }

        if (index < tokenArray.Length)
            throw new SerializationException("There is left over data.");
    }
    
    // Example 3: Parse input using InputLexer and an IEnumerator<string>.
    public string SolveLevel3(string input)
    {
        var tokens = Lexer.GetTokens(input);
        var tokenEnumerator = tokens.GetEnumerator();
        
        var grid = ParseIntGrid(tokenEnumerator);
        
        if (tokens.MoveNext())
            throw new SerializationException("There is left over data.");
                
        
        int[,] ParseIntGrid(IEnumerator<string> tokenEnumerator)
        {
            tokenEnumerator.MoveNext();
            var width = int.Parse(tokenEnumerator.Current);
            
            tokenEnumerator.MoveNext();
            var height = int.Parse(tokenEnumerator.Current);
            
            var grid = new int[width, height];
            for (int row = 0; row < width; row++)
            {
                for (int column = 0; column < height; column++)
                {
                    tokenEnumerator.MoveNext();
                    grid[row, column] = int.Parse(tokenEnumerator.Current);
                }
            }
        }
    }
}
```
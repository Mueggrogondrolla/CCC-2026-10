using CCCLibrary.Serialization;
using System.Reflection;

namespace CCCLibraryTests;

internal class TestUtilities
{
    internal static void ParseInputFiles(string workingDirectory, Type[] inputClasses, string inputFilePattern = "level*.in")
    {
        var inputFiles = new DirectoryInfo(workingDirectory)
            .EnumerateFiles(inputFilePattern, SearchOption.AllDirectories)
            .ToArray();

        var levelFilesGroupedByLevel = inputFiles.GroupBy(file => file.Name.SkipWhile(character => character is < '0' or > '9').First());

        foreach (var levelFilesOfOneLevel in levelFilesGroupedByLevel)
        {
            var inputClass = inputClasses.Single(@class => @class.Name.Contains($"Level_{levelFilesOfOneLevel.Key}"));

            var parseInputFileMethod = typeof(InputParser)
                .GetMethod(nameof(InputParser.Parse), BindingFlags.Public | BindingFlags.Static, new Type[] { typeof(string), typeof(SerializationOptions) })!
                .MakeGenericMethod(inputClass);

            foreach (var inputFile in levelFilesOfOneLevel)
            {
                _ = parseInputFileMethod.Invoke(null, new object?[] { inputFile.FullName, SerializationOptions.Default });
            }
        }
    }
}

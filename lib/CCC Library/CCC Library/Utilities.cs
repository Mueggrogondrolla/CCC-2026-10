namespace CCCLibrary;

internal static class Utilities
{
    /// <summary>
    /// Helper to create matching output files for input files.
    /// </summary>
    internal static class OutputFileGenerator
    {
        /// <summary>
        /// Creates the matching output file for the input file.
        /// </summary>
        /// <param name="inputFile"></param>
        /// <returns></returns>
        public static FileInfo GenerateMatchingOutputFile(FileInfo inputFile)
        {
            if (inputFile.Name.Contains("example"))
            {
                return new FileInfo(inputFile.FullName.Replace("in_level-", "out_generated_level-"));
            }
            else
            {
                return new FileInfo(inputFile.FullName.Replace("in_level-", "out_level-"));
            }
        }
    }
}

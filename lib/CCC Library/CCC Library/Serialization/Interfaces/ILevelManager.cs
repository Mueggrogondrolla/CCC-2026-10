namespace CCCLibrary.Serialization.Interfaces;

public interface ILevelManager
{
    public void GenerateExampleOutputFile();

    public bool VerifyGeneratedExampleOutputFile(bool throwIfDifferent = true, bool openDiffViewIfDifferent = true);

    public void GenerateOutputFiles();
}

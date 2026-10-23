namespace CCCLibrary.Serialization.Interfaces;

/// <summary>
/// Beschreibt Logikklassen für das Lösen von Leveln.
/// </summary>
public interface ILevelSolver<TInput, TOutput> where TInput : IInputData where TOutput : IOutputData
{
    /// <summary>
    /// Ermittelt die Lösung dieses Levels für den übergebenen Input.
    /// </summary>
    TOutput SolveFor(TInput input);
}

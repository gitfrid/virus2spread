using System.Reflection;

namespace VirusSpreadLibrary.Enum;

public static class CellState
{
    public const int PersonsHealthyOrRecoverd = 0;
    public const int PersonsInfected = 1;
    public const int PersonsInfectious = 2;
    public const int PersonsRecoverdImmuneNotInfectious = 3; // -> does this apply to reality?
    public const int Virus = 4;
    public const int EmptyCell = 5;
    public static int CurrentCellState { get; set; } = 0;
}

using VirusSpreadLibrary.Enum;

namespace VirusSpreadLibrary.Grid;

public class ColorTranslation
{
    public CellState cellState { get; set; }
    public Color cellColor { get; set; }
    public ColorTranslation(CellState CellState, Color CellColor)
    {
        cellState = CellState;
        cellColor = CellColor;
    }
}

using VirusSpreadLibrary.Enum;

namespace VirusSpreadLibrary.Grid;

public class ColorTranlation
{
    public CellState cellState { get; set; }
    public Color cellColor { get; set; }
    public ColorTranlation(CellState CellState, Color CellColor)
    {
        cellState = CellState;
        cellColor = CellColor;
    }
}

using VirusSpreadLibrary.Enum;
using Microsoft.Maui.Graphics;

namespace VirusSpreadLibrary.Grid;

public class ColorTranslation
{
    public CellState cellState { get; set; }
    public Microsoft.Maui.Graphics.Color cellColor { get; set; }
    public ColorTranslation(CellState CellState, Microsoft.Maui.Graphics.Color CellColor)
    {
        cellState = CellState;
        cellColor = CellColor;
    }
}

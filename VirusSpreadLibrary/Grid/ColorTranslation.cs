using VirusSpreadLibrary.Enum;


namespace VirusSpreadLibrary.Grid;

public class ColorTranslation
{
    public CellState CellState { get; set; }
    public Microsoft.Maui.Graphics.Color CellColor { get; set; }
    public ColorTranslation()
    {
        CellState = new CellState();
        CellColor = new Microsoft.Maui.Graphics.Color();
    }
    public ColorTranslation(CellState StateOfCell, Microsoft.Maui.Graphics.Color ColorOfCell)
    {
        CellState = StateOfCell;
        CellColor = ColorOfCell;
    }
}

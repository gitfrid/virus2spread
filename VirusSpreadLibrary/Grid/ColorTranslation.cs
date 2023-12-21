using VirusSpreadLibrary.Enum;


namespace VirusSpreadLibrary.Grid;

public class ColorTranslation
{
    public int CellState { get; set; }
    public Microsoft.Maui.Graphics.Color CellColor { get; set; }
    public ColorTranslation()
    {
        CellState = 0;
        CellColor = new Microsoft.Maui.Graphics.Color();
    }
    public ColorTranslation(int StateOfCell, Microsoft.Maui.Graphics.Color ColorOfCell)
    {
        CellState = StateOfCell;
        CellColor = ColorOfCell;
    }
}

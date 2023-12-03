
namespace VirusSpreadLibrary.Grid;

public class GridCell
{
    public Microsoft.Maui.Graphics.Color PixelColor { get; set; }
    public CellPopulation Population { get; set; }
    public GridCell(Microsoft.Maui.Graphics.Color CellColor, CellPopulation CellPopulation)
    {
        Population = new CellPopulation();
        PixelColor = new Microsoft.Maui.Graphics.Color();
    }

}

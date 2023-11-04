
namespace VirusSpreadLibrary.Grid;

public class GridCell
{
    public Color PixelColor { get; set; }
    public CellPopulation Population { get; set; }
    public GridCell(Color CellColor, CellPopulation CellPopulation)
    {
        Population = new CellPopulation();
        PixelColor = new Color();
    }

}

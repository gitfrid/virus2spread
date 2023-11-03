
namespace VirusSpreadLibrary.Grid;

public class GridCell
{
    public Color PixelColor { get; set; }
    public CellPopulation? Population { get; set; }

    public GridCell(Color Color, CellPopulation Population)
    {
        this.PixelColor = Color;
        this.Population = Population;
    }
}

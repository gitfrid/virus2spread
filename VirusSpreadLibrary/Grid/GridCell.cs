
namespace VirusSpreadLibrary.Grid;
public class GridCell
{
    private Microsoft.Maui.Graphics.Color? cellColor;
    private CellPopulation? population;

    public GridCell()
    {
        //
    }
    public GridCell(Microsoft.Maui.Graphics.Color ColorOfCell, CellPopulation PopulationOfCell)
    {
        // set Cell Population and Color!
        population = new CellPopulation();
        cellColor = new Microsoft.Maui.Graphics.Color();         
    }
    public Microsoft.Maui.Graphics.Color CellColor
    {
        get => cellColor!;
        set => cellColor = value;
    }
    public CellPopulation Population
    {
        get => population!;
        set => population = value;
    }

}

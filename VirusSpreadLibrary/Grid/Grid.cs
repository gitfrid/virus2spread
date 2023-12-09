using VirusSpreadLibrary.Creature;
using VirusSpreadLibrary.Enum;
using Microsoft.Maui.Graphics;
namespace VirusSpreadLibrary.Grid;

public class Grid
{
    private int maxX;
    private int maxY;
    public Grid()
    {
        ColorList = new ColorList();
        Cells = new GridCell[,] { };
    }
    public GridCell[,] Cells { get; set; }
    public ColorList ColorList { get; set; }
    
    public void SetNewEmptyGrid(int MaxX, int MaxY)
    {
        maxX = MaxX;
        maxY = MaxY;
        Cells = new GridCell[maxX, maxY];
        CellPopulation Population = new ();
        Microsoft.Maui.Graphics.Color Color = ColorList.GetCellColor(CellState.EmptyCell, Population);

        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                this.Cells[x, y] = new GridCell(Color, Population);
            }
        }
    }
    
    public CellPopulation AddCreatureToCell(MoveData MoveToData)
    {
        // move to new grid xy end coordinate
        //SetCellState setNewCellState = new ();
        CellPopulation Population = SetCellState.SetNewState(MoveToData,this);
        return Population;
    }

    public int ReturnMaxX()
    {
        return maxX;
    }
    public int ReturnMaxY()
    {
        return maxY;
    }
}

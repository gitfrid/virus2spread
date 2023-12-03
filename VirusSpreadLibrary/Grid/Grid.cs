using VirusSpreadLibrary.Creature;
using VirusSpreadLibrary.Enum;
using Microsoft.Maui.Graphics;

namespace VirusSpreadLibrary.Grid;

public class Grid
{
    private int maxX { get; set; }
    private int maxY { get; set; }
    public ColorList ColorList { get; set; }
    public GridCell[,] GridField { get; set; }

    public Grid()
    {
        ColorList = new ColorList();
        GridField = new GridCell[,] { };
    }

    public void SetNewEmptyGrid(int MaxX, int MaxY)
    {
        maxX = MaxX;
        maxY = MaxY;
        GridField = new GridCell[maxX, maxY];
        CellPopulation Population = new ();

        Microsoft.Maui.Graphics.Color Color = ColorList.GetCellColor(CellState.EmptyCell, Population);

        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                GridField[x, y] = new GridCell(Color, Population);
            }
        }
    }
    
    public CellPopulation AddCreatureToCell(MoveData MoveToData)
    {
        // move to new grid xy end coordinate
        SetCellState setNewCellState = new ();
        CellPopulation Population = setNewCellState.SetNewState(MoveToData, this);
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

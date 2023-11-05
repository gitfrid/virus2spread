using VirusSpreadLibrary.Creature;
using VirusSpreadLibrary.Enum;

namespace VirusSpreadLibrary.Grid;

public class Grid
{
    private int maxX { get; set; }
    private int maxY { get; set; }
    public ColorList ColorList { get; set; }

    public GridCell[,] GridField;

    public Grid()
    {        
        ColorList = new ColorList();
    }
    public void SetNewEmptyGrid(int MaxX, int MaxY)
    {
        maxX = MaxX;
        maxY = MaxY;
        GridField = new GridCell[maxX, maxY];
        CellPopulation Population = new CellPopulation();
        Color Color = ColorList.GetCellColor(CellState.EmptyCell, Population);

        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                GridField[x, y] = new GridCell(Color,Population);
            }
        }
    }
    private void RemoveCreature(MoveData MoveToData, CreatureType Creature)
    {
        //
    } 
    public CellPopulation CelAddCreature(MoveData MoveToData)
    {
        SetNewCellState setNewCellState = new SetNewCellState();
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

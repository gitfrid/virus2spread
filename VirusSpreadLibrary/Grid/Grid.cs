using VirusSpreadLibrary.Creature;
using VirusSpreadLibrary.Enum;

namespace VirusSpreadLibrary.Grid;

public class Grid
{
    private int maxX { get; set; }
    private int maxY { get; set; }

    private GridCell[,] gridField;

    
    public Grid(int MaxX, int MaxY)
    {        
        maxX = MaxX;
        maxY = MaxY;
        gridField = new GridCell[maxX,maxY];
        SetNewEmptyGrid();
    }
    private void SetNewEmptyGrid()
    {
        CellPopulation Population = new CellPopulation();
        Color Color = ColorList.GetCellColor(CellState.EmptyCell, Population);
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                gridField[x, y] = new GridCell(Color,Population);
            }
        }
    }
    private void RemoveCreature(MoveData MoveToData, CreatureType Creature)
    {
        //
    } 
    public void AddCreature(MoveData MoveToData, CreatureType Creature )
    {
        //
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

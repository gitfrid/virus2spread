
using VirusSpreadLibrary.Enum;

namespace VirusSpreadLibrary.Grid;

class Grid
{
    private int maxX { get; set; }
    private int maxY { get; set; }
    private PixelColor[,] Cells { get; set; }
    public Grid(int MaxX, int MaxY)
    {
        maxX = MaxX;
        maxY = MaxY;        
        Cells = new PixelColor[MaxX, MaxY];
        CreateEmptyGrid();
    }
    private void CreateEmptyGrid()
    {
        Color col = ColorList.GetCellColor(CellState.EmptyCell);
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                SetCellColor(x, y, col);
            }
        }
    }
    public void SetCellColor(int X, int Y, Color CellPixelColor)
    {
        Cells[X, Y] = new PixelColor(X, Y, CellPixelColor);
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

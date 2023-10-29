
namespace VirusSpreadLibrary.Grid;

class Grid
{
    private int MaxX { get; set; }
    private int MaxY { get; set; }
    private PixelColor[,] Cell { get; set; }

    public Grid(int x, int y)
    {
        MaxX = x;
        MaxY = y;
        Cell = new PixelColor[x, y];
    }
    public void SetCellColor(int x, int y, Color PixelColor)
    {
        Cell[x, y] = new PixelColor(x, y, PixelColor);
    }
    public int ReturnMaxX()
    {
        return MaxX;
    }
    public int ReturnMaxY()
    {
        return MaxY;
    }
}

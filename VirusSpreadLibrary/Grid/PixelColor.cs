namespace VirusSpreadLibrary.Grid;

class PixelColor
{
    private int XPos;
    private int YPos;
    private Color PixelCol;
    public PixelColor(int x, int y, Color pixelColor) // Construct pixel with x,y and color of creature type
    {
        XPos = x;
        YPos = y;
        PixelCol = pixelColor;
    }
}


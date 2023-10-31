namespace VirusSpreadLibrary.Grid;

class PixelColor
{
    private int xPos { get; set;}
    private int yPos { get; set; }
    private Color pixcelColor { get; set; } // the color of virus or a person or a person's status

    public PixelColor(int X, int Y, Color PixcelColor) // Construct grid pixel with x,y and a color
    {
        xPos = X;
        yPos = Y;
        pixcelColor = PixcelColor;
    }
}


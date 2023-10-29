namespace VirusSpreadLibrary.SpreadModel;

class Drawing
{
    public Drawing()
    {
    }

    public Image DrawGrid(Grid.Grid currentGrid, int iteration)
    {
        int maxRows = currentGrid.ReturnMaxY();
        int maxCols = currentGrid.ReturnMaxX();
        var image = new Image<Rgba32>(maxRows, maxCols);


        for (int y = 0; y < maxCols; y++)
        {
            for (int x = 0; x < maxCols; x++)
            {
            }
        }

        return image;
    }
}

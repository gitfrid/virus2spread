using VirusSpreadLibrary.Enum;
using VirusSpreadLibrary.Grid;

namespace VirusSpreadLibrary.SpreadModel;

public class SetStartPopulation
{
    private int maxX = 0;
    private int maxY = 0;
    //private PixelColor[,] Cells { get; set; }

    public SetStartPopulation(int MaxX, int MaxY)
    {        
        maxX = MaxX;
        maxY = MaxY;
        PopulateCreatures();
    }
    private void PopulateCreatures()
    {
        Color colPerson = ColorList.GetCellColor(CellState.PersonHealthy);
        Color colVirus = ColorList.GetCellColor(CellState.Virus);
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                //SetCellColor(x, y, col);
            }
        }
    }
}

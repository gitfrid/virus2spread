using VirusSpreadLibrary.Enum;

namespace VirusSpreadLibrary.Grid;

public static class ColorList
{
    private static List<ColorTranslation> colorList = new()
    {
        new(CellState.Virus, Color.Red),
        new(CellState.PersonHealthy, Color.Green),
        new(CellState.PersonInfected, Color.LightBlue),
        new(CellState.EmptyCell, Color.Black)
    };
    public static Color GetCellColor(CellState CellState)
    {
        Color cellColor = Color.Black;

        foreach (ColorTranslation ColModel in colorList)
        {
            if (ColModel.cellState == CellState)
            {
                cellColor = ColModel.cellColor;
            }
        }
        return cellColor;
    }
}


using VirusSpreadLibrary.Enum;

namespace VirusSpreadLibrary.Grid;

public class ColorList
{
    private Color cellColor = Color.Black;

    private List<ColorTranslation> colorList = new()
    {
        new(CellState.Virus, Color.Red),
        new(CellState.PersonHealthy, Color.Green),
        new(CellState.PersonInfected, Color.Blue),
        new(CellState.EmptyCell, Color.Black)
    };
    public Color GetCellColor(CellState CellState, CellPopulation Population)
    {
    
        foreach (ColorTranslation ColModel in colorList)
        {
            if (ColModel.cellState == CellState)
            {
                cellColor = ColModel.cellColor;
            }

            // use diffrent color shades for population density
            if ( CellState==CellState.PersonInfected | CellState == CellState.PersonHealthy | CellState == CellState.Virus)
            {
                if (Population.NumPersons > 0) 
                {
                    
                    ColorExtensions.Lighten(cellColor,(1/10)); 
                }
                else if (Population.NumViruses > 0)
                {
                    ColorExtensions.Lighten(cellColor, (1/Population.NumViruses));
                }                    
            }

        }
        return cellColor;
    }
}


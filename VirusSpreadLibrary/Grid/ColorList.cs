using VirusSpreadLibrary.Enum;

namespace VirusSpreadLibrary.Grid;

public class ColorList
{
    private Color cellColor = Color.Black;

    private List<ColorTranslation> colorList = new()
    {
        new(CellState.Virus, Color.WhiteSmoke),
        new(CellState.PersonHealthy, Color.Blue),
        new(CellState.PersonInfected, Color.DarkSalmon),
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


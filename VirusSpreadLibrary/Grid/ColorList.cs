using VirusSpreadLibrary.Enum;
using Microsoft.Maui.Graphics;
using VirusSpreadLibrary.AppProperties;

namespace VirusSpreadLibrary.Grid;

public class ColorList
{
    private Microsoft.Maui.Graphics.Color cellColor = Colors.Black;

    private readonly List<ColorTranslation> colorList =
    [
        new(CellState.Virus, Microsoft.Maui.Graphics.Color.FromArgb(AppSettings.Config.VirusColor.ToArgb().ToString("X"))),
        new(CellState.PersonHealthy, Microsoft.Maui.Graphics.Color.FromArgb(AppSettings.Config.PersonHealthyColor.ToArgb().ToString("X"))),
        new(CellState.PersonInfected, Microsoft.Maui.Graphics.Color.FromArgb(AppSettings.Config.PersonInfected.ToArgb().ToString("X"))),
        new(CellState.EmptyCell, Microsoft.Maui.Graphics.Color.FromArgb(AppSettings.Config.EmptyCellColor.ToArgb().ToString()))
    ];
    public Microsoft.Maui.Graphics.Color GetCellColor(CellState CellState, CellPopulation Population)
    {
    
        foreach (ColorTranslation ColModel in colorList)
        {
            if (ColModel.CellState == CellState)
            {
                cellColor = ColModel.CellColor;
            }

            //// use diffrent color shades for population density
            //if ( CellState==CellState.PersonInfected | CellState == CellState.PersonHealthy | CellState == CellState.Virus)
            //{
            //    if (Population.NumPersons > 0) 
            //    {
                    
            //        Lighten(cellColor,(1/10)); 
            //    }
            //    else if (Population.NumViruses > 0)
            //    {
            //        Lighten(cellColor, (1/Population.NumViruses));
            //    }                    
            //}

        }
        return cellColor;
    }

    //public static Color Lighten(Color origColor, int percent = 80)
    //{

    //    Color rgbCol = rgbColToRgb(origColor);

    //    // get remainders
    //    int rr = 255 - rgbCol.R;
    //    int gr = 255 - rgbCol.G;
    //    int br = 255 - rgbCol.B;

    //    // add a percentage of the remainder, plus original value
    //    int r = System.Convert.ToInt32(percent / (double)100 * rr) + rgbCol.R;
    //    int g = System.Convert.ToInt32(percent / (double)100 * gr) + rgbCol.G;
    //    int b = System.Convert.ToInt32(percent / (double)100 * br) + rgbCol.B;

    //    return ToImageSharpColor(System.Drawing.Color.FromArgb(r, g, b));
    //}

    //public static Color Darken(Color origColor, int percent = 80)
    //{
    //    System.Drawing.Color rgbCol = ToSystemDrawingColor(origColor);

    //    // subtract the percentage of the original value from the original value
    //    int r = rgbCol.R - System.Convert.ToInt32(percent / (double)100 * rgbCol.R);
    //    int g = rgbCol.G - System.Convert.ToInt32(percent / (double)100 * rgbCol.G);
    //    int b = rgbCol.B - System.Convert.ToInt32(percent / (double)100 * rgbCol.B);

    //    return ToImageSharpColor(System.Drawing.Color.FromArgb(r, g, b));
    //}

    //public static System.Drawing.Color MauiToSystemDrawingColor(this Color c)
    //{
    //    Argb32 converted = c.ToPixel<Argb32>();
    //    return System.Drawing.Color.FromArgb((int)converted.Argb);
    //}

}


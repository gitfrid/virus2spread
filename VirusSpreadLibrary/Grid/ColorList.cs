using VirusSpreadLibrary.Enum;
using Microsoft.Maui.Graphics;
using VirusSpreadLibrary.AppProperties;
using Microsoft.Maui.Graphics.Skia;

namespace VirusSpreadLibrary.Grid;

public class ColorList
{
    private Microsoft.Maui.Graphics.Color cellColor = Colors.Black;

    private readonly List<ColorTranslation> colorList =
    [
        new(CellState.PersonsHealthyOrRecoverd, Microsoft.Maui.Graphics.Color.FromArgb(AppSettings.Config.PersonsHealthyOrRecoverdColor.ToArgb().ToString("X"))),
        new(CellState.PersonsInfected, Microsoft.Maui.Graphics.Color.FromArgb(AppSettings.Config.PersonInfectedColor.ToArgb().ToString("X"))),
        new(CellState.PersonsInfectious, Microsoft.Maui.Graphics.Color.FromArgb(AppSettings.Config.PersonInfectiousColor.ToArgb().ToString("X"))),
        new(CellState.PersonsRecoverdImmuneNotInfectious, Microsoft.Maui.Graphics.Color.FromArgb(AppSettings.Config.PersonsRecoverdImmuneNotInfectiousColor.ToArgb().ToString("X"))),
        new(CellState.Virus, Microsoft.Maui.Graphics.Color.FromArgb(AppSettings.Config.VirusColor.ToArgb().ToString("X"))),
        new(CellState.EmptyCell, Microsoft.Maui.Graphics.Color.FromArgb(AppSettings.Config.EmptyCellColor.ToArgb().ToString("X")))
    ];
    public Microsoft.Maui.Graphics.Color GetCellColor(int CellState, int PersonPopulation, int VirusPopulation)
    {

        foreach (ColorTranslation ColModel in colorList)
        {
            if (ColModel.CellState == CellState)
            {
                cellColor = ColModel.CellColor;               
            }
        }

        if (AppSettings.Config.PopulationDensityColoring) 
        {
            // use diffrent color shades for population density
            if (CellState != Enum.CellState.EmptyCell)
                {
                    if (PersonPopulation > 1)
                    {
                        cellColor = Lighten(cellColor, (double)1 / (double)PersonPopulation);
                    }
                    else if (VirusPopulation > 1)
                    {
                        cellColor = Lighten(cellColor, ((double)1 / (double)VirusPopulation));
                    }
                }
        }        
        return cellColor;
    }

    public static Microsoft.Maui.Graphics.Color Lighten(Microsoft.Maui.Graphics.Color origColor, double percent)
    {

        var newColor = origColor.WithLuminosity(origColor.GetLuminosity()*(float)(percent));
        return newColor;

        //origColor.AddLuminosity((float)percent*(float)0.6);

        //System.Drawing.Color rgbCol = MauiToSystemDrawingColor(origColor);

        //// get remainders
        //int rr = 255 - rgbCol.R;
        //int gr = 255 - rgbCol.G;
        //int br = 255 - rgbCol.B;

        //// add a percentage of the remainder, plus original value
        //int r = System.Convert.ToInt32(percent * rr) + rgbCol.R;
        //int g = System.Convert.ToInt32(percent * gr) + rgbCol.G;
        //int b = System.Convert.ToInt32(percent * br) + rgbCol.B;

        //return Microsoft.Maui.Graphics.Color.FromRgb(r, g, b);
    }

    public static Microsoft.Maui.Graphics.Color Darken(Microsoft.Maui.Graphics.Color origColor, double percent )
    {
        System.Drawing.Color rgbCol = MauiToSystemDrawingColor(origColor);

        // subtract the percentage of the original value from the original value
        int r = rgbCol.R - System.Convert.ToInt32(percent * rgbCol.R);
        int g = rgbCol.G - System.Convert.ToInt32(percent * rgbCol.G);
        int b = rgbCol.B - System.Convert.ToInt32(percent * rgbCol.B);

        return Microsoft.Maui.Graphics.Color.FromRgb(r, g, b);
    }

    public static System.Drawing.Color MauiToSystemDrawingColor(Microsoft.Maui.Graphics.Color Col)
    {
        return System.Drawing.Color.FromArgb(Col.ToArgb());
    }
    public static Microsoft.Maui.Graphics.Color SystemDrawingColorToMaui(System.Drawing.Color Col)
    {
        return Microsoft.Maui.Graphics.Color.FromInt(Col.ToArgb());
    }


}


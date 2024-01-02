using VirusSpreadLibrary.Enum;
using Microsoft.Maui.Graphics;
using VirusSpreadLibrary.AppProperties;
using Microsoft.Maui.Graphics.Skia;

namespace VirusSpreadLibrary.Grid;



public static class CellStateExtions
{
    public static Microsoft.Maui.Graphics.Color ToTheColor(this CellState cellState)
    {
        switch (cellState)
        {
            case CellState.PersonsHealthyOrRecoverd:
                return Microsoft.Maui.Graphics.Color.FromArgb(AppSettings.Config.PersonsHealthyOrRecoverdColor.ToArgb().ToString("X"));
            case CellState.PersonsInfected:
                return Microsoft.Maui.Graphics.Color.FromArgb(AppSettings.Config.PersonInfectedColor.ToArgb().ToString("X"));
            case CellState.PersonsInfectious:
                return Microsoft.Maui.Graphics.Color.FromArgb(AppSettings.Config.PersonInfectiousColor.ToArgb().ToString("X"));
            case CellState.PersonsRecoverdImmuneNotInfectious:
                return Microsoft.Maui.Graphics.Color.FromArgb(AppSettings.Config.PersonsRecoverdImmuneNotInfectiousColor.ToArgb().ToString("X"));
            case CellState.Virus:
                return Microsoft.Maui.Graphics.Color.FromArgb(AppSettings.Config.VirusColor.ToArgb().ToString("X"));
            case CellState.EmptyCell:
                return Microsoft.Maui.Graphics.Color.FromArgb(AppSettings.Config.EmptyCellColor.ToArgb().ToString("X"));
            default:
                throw new ArgumentOutOfRangeException(nameof(cellState), cellState, null);
        }
    }
}

public class ColorList
{
    public Microsoft.Maui.Graphics.Color GetCellColor(CellState cellState, int personPopulation, int virusPopulation)
    {
        var cellColor = cellState.ToTheColor();

        if (AppSettings.Config.PopulationDensityColoring && cellState != CellState.EmptyCell)
        {
            var population = Math.Max(personPopulation, virusPopulation);
            if (population > 1)
            {
                cellColor = Lighten(cellColor, 1 / (double)population);
            }
        }

        return cellColor;
    }

    
    public static Microsoft.Maui.Graphics.Color Lighten(Microsoft.Maui.Graphics.Color origColor, double percent)
    {

        var newColor = origColor.WithLuminosity(origColor.GetLuminosity()*(float)(percent));
        return newColor;
        
    }

    public static System.Drawing.Color MauiToSystemDrawingColor(Microsoft.Maui.Graphics.Color Col)
    {
        return System.Drawing.Color.FromArgb(Col.ToArgb());
    }
    public static Microsoft.Maui.Graphics.Color SystemDrawingColorToMaui(System.Drawing.Color Col)
    {
        return Microsoft.Maui.Graphics.Color.FromInt(Col.ToArgb());
    }

    public static Microsoft.Maui.Graphics.Color Darken(Microsoft.Maui.Graphics.Color origColor, double percent)
    {
        System.Drawing.Color rgbCol = MauiToSystemDrawingColor(origColor);

        // subtract the percentage of the original value from the original value
        int r = rgbCol.R - System.Convert.ToInt32(percent * rgbCol.R);
        int g = rgbCol.G - System.Convert.ToInt32(percent * rgbCol.G);
        int b = rgbCol.B - System.Convert.ToInt32(percent * rgbCol.B);
        return Microsoft.Maui.Graphics.Color.FromRgb(r, g, b);
    }

}


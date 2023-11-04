using SixLabors.ImageSharp.PixelFormats;

namespace VirusSpreadLibrary.Grid;
public static class ColorExtensions
{
    public static System.Drawing.Color ToSystemDrawingColor(this SixLabors.ImageSharp.Color c)
    {
        Argb32 converted = c.ToPixel<Argb32>();
        return System.Drawing.Color.FromArgb((int)converted.Argb);
    }
    public static SixLabors.ImageSharp.Color ToImageSharpColor(this System.Drawing.Color c)
    {
        return SixLabors.ImageSharp.Color.FromRgba(c.R, c.G, c.B, c.A);
    }

    public static Color Lighten(Color origColor , int percent = 80)
    {

        System.Drawing.Color rgbCol = ToSystemDrawingColor(origColor);

        // get remainders
        int rr = 255 - rgbCol.R;
        int gr = 255 - rgbCol.G;
        int br = 255 - rgbCol.B;

        // add a percentage of the remainder, plus original value
        int r = System.Convert.ToInt32(percent / (double)100 * rr) + rgbCol.R;
        int g = System.Convert.ToInt32(percent / (double)100 * gr) + rgbCol.G;
        int b = System.Convert.ToInt32(percent / (double)100 * br) + rgbCol.B;

        return ToImageSharpColor(System.Drawing.Color.FromArgb(r, g, b));        
    }

    public static Color Darken(Color origColor, int percent = 80)
    {
        System.Drawing.Color rgbCol = ToSystemDrawingColor(origColor);

        // subtract the percentage of the original value from the original value
        int r = rgbCol.R - System.Convert.ToInt32(percent / (double)100 * rgbCol.R);
        int g = rgbCol.G - System.Convert.ToInt32(percent / (double)100 * rgbCol.G);
        int b = rgbCol.B - System.Convert.ToInt32(percent / (double)100 * rgbCol.B);

        return ToImageSharpColor(System.Drawing.Color.FromArgb(r, g, b));
    }
}

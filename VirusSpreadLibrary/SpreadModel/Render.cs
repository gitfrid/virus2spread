using Microsoft.Maui.Graphics;

namespace VirusSpreadLibrary.SpreadModel
{
    public class Render
    {
        private static int maxX;
        private static int maxY;
        public Grid.Grid GridField = new Grid.Grid();

        public void InitGridCanvas(ICanvas canvas, float width, float height)
        {
            canvas.FillRectangle(0, 0, width, height);
            maxX = (int)width;
            maxY = (int)height;
        }
        public static void DrawPixel(ICanvas canvas , System.Drawing.Point Coordinate, Microsoft.Maui.Graphics.Color Col)
        {
            canvas.FillColor = Col.WithHue(0.6f);
            canvas.FillRectangle(Coordinate.X, Coordinate.X, 5, 5);            
        }
        public static void RandomLines(Random rand, ICanvas canvas, float width, float height, int lines = 100)
        {
            canvas.FillColor = Colors.Black;
            canvas.FillRectangle(0, 0, width, height);

            canvas.StrokeColor = Colors.White.WithAlpha(.1f);
            canvas.StrokeSize = 6;
            for (int i = 0; i < lines; i++)
            {
                int a = rand.Next(254);
                byte r = (byte)a;
                a = rand.Next(254);
                byte b = (byte)a;
                a = rand.Next(254);
                byte g = (byte)a;

                Microsoft.Maui.Graphics.Color dColor = new Microsoft.Maui.Graphics.Color();
                //dColor = Microsoft.Maui.Graphics.Color.FromRgb(152, 130, 210);
                dColor = Microsoft.Maui.Graphics.Color.FromRgb(r, b, g);
                //Microsoft.Maui.Graphics.Color sColor = Microsoft.Maui.Graphics.Color.FromRgb(152, 130, 210);
                //Colors.White.WithAlpha(.5f)
                canvas.FillColor = dColor.WithHue(0.6f);

                
                float x1 = (float)rand.NextDouble() * width;
                float x2 = (float)rand.NextDouble() * width;
                float y1 = (float)rand.NextDouble() * height;
                float y2 = (float)rand.NextDouble() * height;

                //RectangleF rect = new(
                //    x: x1,
                //    y: y1,
                //    width: 10,
                //    height: 10);
                //canvas.FillRectangle(rect);

                canvas.FillRectangle(x1, y1, 5, 5);
                canvas.FillRectangle(x2, y2, 5, 5);
                //canvas.DrawRectangle(x2, y2, 10, 10);
                //canvas.DrawLine(x1, y1, x2, y2);
            }
        }


    }
}

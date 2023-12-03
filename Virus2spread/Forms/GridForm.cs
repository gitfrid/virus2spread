using System.Diagnostics;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;
using VirusSpreadLibrary.SpreadModel;
using VirusSpreadLibrary.AppProperties;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using AnimatedGif;
using System.Text;

namespace Virus2spread.Forms
{
    public partial class GridForm : Form
    {

        readonly Stopwatch Watch = Stopwatch.StartNew();
        private Simulation simulation = new Simulation();

        private int maxX;
        private int maxY;

        private float CoordinateFactX;
        private float CoordinateFactY;
        private float RectangleX;
        private float RectangleY;
        private float indx = 0;


        public GridForm(Simulation Simulation, int MaxX, int MaxY)
        {
            InitializeComponent();
            simulation = Simulation;
            maxX = MaxX;
            maxY = MaxY;
            RecalcFormSize(skglControl1.Width, skglControl1.Height, out CoordinateFactX, out CoordinateFactY, out RectangleX, out RectangleY);
            if (AppSettings.Config.GridFormTimer < 1)
            {
                timer1.Interval = 1;
            }
            else
            {
                timer1.Interval = AppSettings.Config.GridFormTimer;
            }
        }

        private void UpdateBenchmarkMessage()
        {
            Text = $"virus2spread [{skglControl1.Width}x{skglControl1.Height}] " +
                $"in {Watch.Elapsed.TotalMilliseconds} ms " +
                $"({1 / Watch.Elapsed.TotalSeconds:N1} Hz)";
            Watch.Restart();
        }

        private void skglControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
        {
            ICanvas canvas = new SkiaCanvas() { Canvas = e.Surface.Canvas };
            simulation.DrawGrid(canvas, CoordinateFactX, CoordinateFactY, RectangleX, RectangleY);
            UpdateBenchmarkMessage();
            // to create animated gif
            // var surface = e.Surface;            
            // SaveGif(surface);
        }

        private void RecalcFormSize(float WidthX, float HeigthY, out float coordinateFactX, out float coordinateFactY, out float rectangleX, out float rectangleY)
        {
            // get relative factor of xy coordinate
            // and the pixel size ( skglControl1 / GridField )

            float cellWidthPx = WidthX / maxX;
            float cellHeightPx = HeigthY / maxY;

            float borderFrac = 0.1f;
            float xPad = borderFrac * cellWidthPx;
            float yPad = borderFrac * cellHeightPx;

            coordinateFactX = cellWidthPx + xPad;
            coordinateFactY = cellHeightPx + yPad;
            rectangleX = cellWidthPx - xPad * 2;
            rectangleY = cellHeightPx - yPad * 2;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            simulation.NextIteration();
            skglControl1.Invalidate();
        }

        private void skglControl1_SizeChanged(object sender, EventArgs e)
        {
            RecalcFormSize(skglControl1.Width, skglControl1.Height, out CoordinateFactX, out CoordinateFactY, out RectangleX, out RectangleY);
        }

        private void SaveGif(SKSurface GifSurface)
        {
            indx++;
            StringBuilder sb = new StringBuilder();
            sb.Append("simulation");
            sb.Append(indx.ToString());
            sb.Append(".gif");
            using (var gif = AnimatedGif.AnimatedGif.Create(sb.ToString(), 33))
            {
                System.Drawing.Bitmap img = GetBitmap(GifSurface.Snapshot());
                gif.AddFrame(img, delay: -1, quality: GifQuality.Bit8); // animation does not work
            }
        }
        public static System.Drawing.Bitmap GetBitmap(SkiaSharp.SKImage skiaImage)
        {
            return skiaImage.ToBitmap();
        }
      
    }
}

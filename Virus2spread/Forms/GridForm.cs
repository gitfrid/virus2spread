using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;
using VirusSpreadLibrary.SpreadModel;
using Size = System.Drawing.Size;

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

        public GridForm(Simulation Simulation, int MaxX, int MaxY)
        {
            InitializeComponent();
            simulation = Simulation;
            maxX = MaxX;
            maxY = MaxY;
            RecalcFormSize(skglControl1.Width, skglControl1.Height, out CoordinateFactX, out CoordinateFactY, out RectangleX, out RectangleY);
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
        }


        private void RecalcFormSize(float WidthX, float HeigthY, out float coordinateFactX, out float coordinateFactY, out float rectangleX, out float rectangleY)
        {
            // get relative factor of xy coordinate and the pixel size ( skglControl1 / GridField )

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
    }
}

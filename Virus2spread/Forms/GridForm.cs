using System.Diagnostics;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;
using VirusSpreadLibrary.SpreadModel;
using VirusSpreadLibrary.Grid;

namespace Virus2spread.Forms
{
    public partial class GridForm : Form
    {

        readonly Stopwatch Watch = Stopwatch.StartNew();
        public GridForm()
        {
            InitializeComponent();            
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
            //Render.RandomLines(Rand, canvas, skglControl1.Width, skglControl1.Height, LineCount);
            //Simulation.InitGridCanvas(canvas, Simulation.maxX, Simulation.maxY);
            Simulation.DrawGrid(canvas, skglControl1.Width, skglControl1.Height);
            UpdateBenchmarkMessage();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            Simulation.NextIterate();
            skglControl1.Invalidate();

        }
    }
}

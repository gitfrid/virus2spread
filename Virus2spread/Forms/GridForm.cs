using System.Diagnostics;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;
using VirusSpreadLibrary.SpreadModel;
using VirusSpreadLibrary.AppProperties;
using AnimatedGif;
using System.Text;
using SkiaSharp.Views.Desktop;


namespace Virus2spread.Forms
{
    public partial class GridForm : Form
    {

        readonly Stopwatch watch = Stopwatch.StartNew();

        public  readonly Simulation simulation;

        private readonly int maxX;
        private readonly int maxY;

        private float coordinateFactX;
        private float coordinateFactY;
        private float rectangleX;
        private float rectangleY;
        private float indx;


        public GridForm(Simulation ModelSimulation, int MaxX, int MaxY)
        {
            InitializeComponent();
            simulation = ModelSimulation;            
            maxX = MaxX;
            maxY = MaxY;
            RecalcFormSize(SkglControl.Width, SkglControl.Height, out coordinateFactX, out coordinateFactY, out rectangleX, out rectangleY);
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
            Text = $"virus2spread Iteration: {simulation.Iteration} [{SkglControl.Width}x{SkglControl.Height}] " +
                $"in {watch.Elapsed.TotalMilliseconds} ms " +
                $"({1 / watch.Elapsed.TotalSeconds:N1} Hz)";
            watch.Restart();
        }

        private void RecalcFormSize(float WidthX, float HeigthY, out float coordinateFactX, out float coordinateFactY, out float rectangleX, out float rectangleY)
        {
            // get relative factor of xy coordinate
            // and the pixel size ( skglControl1 / GridField )

            float cellWidthPx = WidthX / maxX;
            float cellHeightPx = HeigthY / maxY;

            float borderFrac = 0.01f;
            float xPad = borderFrac * cellWidthPx;
            float yPad = borderFrac * cellHeightPx;

            coordinateFactX = cellWidthPx + xPad;
            coordinateFactY = cellHeightPx + yPad;
            rectangleX = cellWidthPx - xPad * 2;
            rectangleY = cellHeightPx - yPad * 2;
        }

        private void Timer1_Tick_1(object sender, EventArgs e)
        {
            try
            {
                simulation.NextIteration();
            }
            catch (Exception ex)
            {
                string innerMessage = "";
                if (ex.InnerException != null)
                    innerMessage = ex.InnerException.Message;
                MessageBox.Show(ex.Message.ToString() + "\r\n" + innerMessage);
                this.Close();
            };
            SkglControl.Invalidate();
        }

        private void SkglControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs  e)
        {
            SkiaCanvas skiaCanvas = new()
            {
                Canvas = e.Surface.Canvas,
                FillColor = Colors.Navy
            };
            ((ICanvas)skiaCanvas).FillRectangle(0, 0, SkglControl.Width, SkglControl.Height);
            simulation.DrawGrid(skiaCanvas, coordinateFactX, coordinateFactY, rectangleX, rectangleY);
            UpdateBenchmarkMessage();
            // to create animated gif, but slows down iterations
            // var surface = e.Surface;            
            // SaveGif(surface);
        }

        private void SkglControl1_SizeChanged(object sender, EventArgs e)
        {
            RecalcFormSize(SkglControl.Width, SkglControl.Height, out coordinateFactX, out coordinateFactY, out rectangleX, out rectangleY);
        }

        public static Bitmap GetBitmap(SkiaSharp.SKImage skiaImage)
        {
            return skiaImage.ToBitmap();
        }

        #pragma warning disable IDE0051
        // to create animated gifs if needed, will slow down iterations
        private void SaveGif(SkiaSharp.SKSurface GifSurface)
        {
            indx++;
            StringBuilder sb = new();
            sb.Append("simulation");
            sb.Append(indx);
            sb.Append(".gif");
            using var gif = AnimatedGif.AnimatedGif.Create(sb.ToString(), 33);
            Bitmap img = GetBitmap(GifSurface.Snapshot());
            // save as gif animation does not work yet - only as normal gif
            gif.AddFrame(img, delay: -1, quality: GifQuality.Bit8); 
        }
        #pragma warning restore IDE0051

    }
}

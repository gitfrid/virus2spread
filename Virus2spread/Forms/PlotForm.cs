using ScottPlot;
using ScottPlot.Renderable;
using ScottPlot.Plottable;
using VirusSpreadLibrary.Plott;
using VirusSpreadLibrary.AppProperties;
//using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Virus2spread
{
    public partial class PlotForm : Form
    {
        private readonly PlotData plotData;

        // create a timer to generate data
        //readonly private System.Windows.Forms.Timer dataTimer = new();
        //readonly private System.Windows.Forms.Timer renderTimer = new();

        readonly FormsPlot formsPlot;

        private SignalPlot[] signalPlot = new SignalPlot[14];

        private double[][] signalData = new double[14][];

        private int nextDataIndex = 0;

        private Point HighlightedPoint;

        private readonly Crosshair crosshair;

        public string Title = "Virus2Spread Diagram: Y-14 parameter, X-Number of iterations";

        public PlotForm(PlotData PlotData)
        {
            InitializeComponent();
            plotData = PlotData;

            // Add the FormsPlot
            formsPlot = new() { Dock = DockStyle.Fill };
            splitContainer1.Panel2.Controls.Add(formsPlot);

            //register the MouseMove event handler
            crosshair = formsPlot.Plot.AddCrosshair(0, 0);
            crosshair.HorizontalLine.PositionLabelFont.Size = 16;
            crosshair.VerticalLine.PositionLabelFont.Size = 16;
            formsPlot.MouseMove += formsPlot_MouseMove;
            formsPlot.MouseEnter += formsPlot_MouseEnter;
            formsPlot_MouseLeave(null, null);
            
            Legend legend = formsPlot.Plot.Legend(enable: true, location: null);
            formsPlot.Plot.Palette = ScottPlot.Palette.Category20;

            for (int i = 0; i < 14; i++)
            {
                signalData[i] = new double[AppSettings.Config.MaxIterations];
                signalPlot[i] = formsPlot.Plot.AddSignal(signalData[i], 1, formsPlot.Plot.Palette.GetColor(i), string.Format("{0}", plotData.Legend[i].ToString()));
            }

            // set timer intervall to enque data and refresh plot
            dataTimer.Interval = 1;
            dataTimer.Start();
            renderTimer.Interval = 20;
            renderTimer.Start();

            Closed += (sender, args) =>
            {
                dataTimer?.Stop();
                renderTimer?.Stop();
            };

        }

        private void dataTimer_Tick(object sender, EventArgs e)
        {
            // dequeue doubles list from PlotQueue

            if (nextDataIndex >= signalData[0].Length)
            {
                dataTimer?.Stop();
                renderTimer?.Stop();
                MessageBox.Show("PlotForm data array isn't long enough, set a bigger value for Max Iterations");
                throw new OverflowException("PlotForm data array isn't long enough to accomodate new data");
                // in this situation the solution would be:
                //   1. clear the plot
                //   2. create a new larger array
                //   3. copy the old data into the start of the larger array
                //   4. plot the new (larger) array
                //   5. continue to update the new array
            }

            // en-queue n-max in the simulate class should be adjusted with the de-queue n-max below
            // to make shure, all values can be de-qued in time by the PlotForm
            for (int n = 0; n < 2; n++)
            {
                bool success = plotData.PlotDataQueue.TryDequeueList(out List<long> values);
                if (success)
                {
                    for (int i = 0; i < signalData.Length; i++)
                    {
                        signalData[i][nextDataIndex] = values[i];
                        signalPlot[i].MaxRenderIndex = nextDataIndex;
                    }
                    nextDataIndex += 1;
                }
                else
                {
                    break;
                }
            }
            //formsPlot.Refresh();
            Text = $"DataLogger Demo ({nextDataIndex:N0} points)";
        }

        private void renderTimer_Tick(object sender, EventArgs e)
        {
            if (CbAutoAxis.Checked)
            {
                formsPlot.Plot.AxisAuto();
            }
            formsPlot.Refresh();
        }

        private void BtnHoldStart_Click(object sender, EventArgs e)
        {
            // sart stop plotting
            if (dataTimer.Enabled || renderTimer.Enabled)
            {
                dataTimer.Enabled = false;
                renderTimer.Enabled = false;
            }
            else
            {
                dataTimer.Enabled = true;
                renderTimer.Enabled = true;
            }
        }

        private void BtnManualScale_Click(object sender, EventArgs e)
        {
            CbAutoAxis.Checked = false;
            formsPlot.Plot.SetAxisLimits(0, 50, -20, 20, 0, 1);
            formsPlot.Plot.SetAxisLimits(0, 50, -20_000, 20_000, 0, 1);
            formsPlot.Refresh();
        }
        private void BtnAutoScale_Click(object sender, EventArgs e)
        {
            formsPlot.Plot.Margins();
            formsPlot.Plot.AxisAuto();
            formsPlot.Refresh();
        }

        private void BtnAutoScaleTight_Click(object sender, EventArgs e)
        {
            formsPlot.Plot.Margins(0, 0);
            formsPlot.Refresh();
        }

        private void BtnAutoScaleX_Click(object sender, EventArgs e)
        {
            formsPlot.Plot.AxisAutoX();
            formsPlot.Refresh();
        }

        private void BtnAutoScaleY_Click(object sender, EventArgs e)
        {
            formsPlot.Plot.AxisAutoY();
            formsPlot.Refresh();
        }
        private void ChkShowLegend_CheckedChanged(object sender, EventArgs e)
        {
            formsPlot.Plot.Legend(ChkShowLegend.Checked);
            formsPlot.Refresh();
        }

        private void CbAutoAxis_CheckedChanged(object sender, EventArgs e)
        {
            if (CbAutoAxis.Checked == false)
            {
                formsPlot.Plot.AxisAuto(verticalMargin: .5);
                var oldLimits = formsPlot.Plot.GetAxisLimits();
                formsPlot.Plot.SetAxisLimits(xMax: oldLimits.XMax + 1000);
            }
        }

        private void CbCorssHair_CheckedChanged(object sender, EventArgs e)
        {
            if (CbCorssHair.Checked == false)
            {
                crosshair.IsVisible = false;
            }
            else 
            {
                crosshair.IsVisible = true;
            }
        }
        private void formsPlot_MouseLeave(object sender, MouseEventArgs e)
        {
            crosshair.IsVisible = false;
            formsPlot.Refresh();
        }
        private void formsPlot_MouseEnter(object sender, EventArgs e)
        {
            if (CbCorssHair.Checked) { crosshair.IsVisible = true; }
        }
        private void formsPlot_MouseMove(object sender, MouseEventArgs e)
        {
            (double coordinateX, double coordinateY) = formsPlot.GetMouseCoordinates();
            crosshair.X = coordinateX;
            crosshair.Y = coordinateY;
            formsPlot.Refresh(lowQuality: true, skipIfCurrentlyRendering: true);
        }
    }
}

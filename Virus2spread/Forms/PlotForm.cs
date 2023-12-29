using ScottPlot;
using ScottPlot.Renderable;
using ScottPlot.Plottable;
using VirusSpreadLibrary.Plott;
using VirusSpreadLibrary.AppProperties;

namespace Virus2spread
{
    public partial class PlotForm : Form
    {
        private readonly PlotData plotData;

        // create a timer to generate data
        //readonly private System.Windows.Forms.Timer dataTimer = new();
        //readonly private System.Windows.Forms.Timer renderTimer = new();

        readonly FormsPlot formsPlot;

        private readonly SignalPlot[] signalPlot = new SignalPlot[14];

        private readonly double[][] signalData = new double[14][];

        private int nextDataIndex = 0;

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
            formsPlot.MouseMove += FormsPlot_MouseMove;
            formsPlot.MouseEnter += FormsPlot_MouseEnter;
            FormsPlot_MouseLeave(null!, null!);

            Legend legend = formsPlot.Plot.Legend(enable: true, location: null);
            formsPlot.Plot.Palette = ScottPlot.Palette.Category20;

            for (int i = 0; i < 14; i++)
            {
                signalData[i] = new double[AppSettings.Config.MaxIterations];
                signalPlot[i] = formsPlot.Plot.AddSignal(signalData[i], 1, formsPlot.Plot.Palette.GetColor(i), string.Format("{0}", plotData.Legend[i].ToString()));
            }

            LegendListBox.Items.AddRange(plotData.Legend);
            LegendListBox.CheckOnClick = true;// <- change mode from double to single click

            // set viability of plot lines / lgeend
            for (int i = 0; i < LegendListBox.Items.Count; i++)
            {
                LegendListBox.SetItemChecked(i, AppSettings.Config.LegendVisability[i]); // -> load status from config
                signalPlot[i].IsVisible = LegendListBox.GetItemChecked(i);
            }

            // set timer intervall to enque data and refresh plot
            DataTimer.Interval = 1;
            DataTimer.Start();
            RenderTimer.Interval = 20;
            RenderTimer.Start();
            BtnHoldStart.BackColor = SystemColors.ControlLightLight;

            Closed += (sender, args) =>
            {
                DataTimer?.Stop();
                RenderTimer?.Stop();
            };
        }

        private void PlotForm_Load(object sender, EventArgs e)
        {
            // set window size
            this.MinimumSize = new Size(1280, 720);
            this.Location = AppSettings.Config.PlotForm_WindowLocation;
            this.Size = AppSettings.Config.PlotForm_WindowSize;
        }
        private void DataTimer_Tick(object sender, EventArgs e)
        {
            // dequeue doubles list from PlotQueue

            if (nextDataIndex >= signalData[0].Length)
            {
                DataTimer?.Stop();
                RenderTimer?.Stop();
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
            Text = $"virus2spread Charts ({nextDataIndex:N0} points)";
        }



        private void RenderTimer_Tick(object sender, EventArgs e)
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
            if (DataTimer.Enabled || RenderTimer.Enabled)
            {
                BtnHoldStart.BackColor = SystemColors.Control;
                DataTimer.Enabled = false;
                RenderTimer.Enabled = false;
            }
            else
            {
                BtnHoldStart.BackColor = SystemColors.ControlLightLight;
                DataTimer.Enabled = true;
                RenderTimer.Enabled = true;
            }
        }

        private void BtnManualScale_Click(object sender, EventArgs e)
        {
            CbAutoAxis.Checked = false;
            formsPlot.Plot.SetAxisLimits(0, 50, -20, 20, 0, 1);
            formsPlot.Plot.SetAxisLimits(0, 50, -20000, 20000, 0, 1);
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
        private void FormsPlot_MouseLeave(object sender, MouseEventArgs e)
        {
            crosshair.IsVisible = false;
            formsPlot.Refresh();
        }
        private void FormsPlot_MouseEnter(object? sender, EventArgs e)
        {
            if (CbCorssHair.Checked) { crosshair.IsVisible = true; }
        }
        private void FormsPlot_MouseMove(object? sender, MouseEventArgs e)
        {
            (double coordinateX, double coordinateY) = formsPlot.GetMouseCoordinates();
            crosshair.X = coordinateX;
            crosshair.Y = coordinateY;
            formsPlot.Refresh(lowQuality: false, skipIfCurrentlyRendering: true);
        }

        private void PlotForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // save current window size 
            AppSettings.Config.PlotForm_WindowLocation = this.Location;
            if (this.WindowState == FormWindowState.Normal)
            {
                AppSettings.Config.PlotForm_WindowSize = this.Size;
            }
            else
            {
                AppSettings.Config.PlotForm_WindowSize = this.RestoreBounds.Size;
            }
        }

        private void LegendListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                signalPlot[e.Index].IsVisible = true;
                AppSettings.Config.LegendVisability[e.Index] = true; // <- write viability status to config
            }
            else
            {
                signalPlot[e.Index].IsVisible = false;
                AppSettings.Config.LegendVisability[e.Index] = false;
            }
            formsPlot.Refresh();
        }
    }
}

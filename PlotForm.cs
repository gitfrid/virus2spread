
using ScottPlot;
using ScottPlot.Renderable;
using Simulator;

namespace Plotter
{
    public partial class PlotForm : Form
    {
        private readonly Simulate simulation;

        // create a timer to generate data
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        readonly ScottPlot.Plottable.DataLogger[] Logger = new ScottPlot.Plottable.DataLogger[10];

        // create a random number generator
        readonly Random Rand = new();

        public string Title => "Realtime infinite Datalogger with threadsave ConcurrentQueue access from MainForm and PlotForm - ten lines";

        public PlotForm(Simulate Simulation)
        {
            InitializeComponent();
            simulation = Simulation;

            Legend legend = formsPlot1.Plot.Legend(enable: true, location: null);
            formsPlot1.Plot.Palette = ScottPlot.Palette.Category20;

            for (int i = 0; i < 10; i++)
            {
                Logger[i] = formsPlot1.Plot.AddDataLogger(label: String.Format("line {0}", i));
                Logger[i].Color = formsPlot1.Plot.Palette.GetColor(i);
            }

            Logger[2].YAxisIndex = 0;
            var yAxis3 = formsPlot1.Plot.AddAxis(ScottPlot.Renderable.Edge.Left, 2);
            yAxis3.Ticks(true);
            yAxis3.AxisLabel.Label = "Y-Axis 0";
            formsPlot1.Plot.Title(Title);

            // set timer intervall to enque data and refresh plot
            timer.Interval = 700;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // dequeue doubles list from PlotQueue
            List<double> values;

            // en-queue n-max in the simulate class should be adjusted with the de-queue n-max below
            // to make shure, all values can be de-qued in time by the PlotForm
            for (int n = 0; n < 600; n++)
            {
                bool success = simulation.PlotDataQueue.TryDequeueList(out values);
                if (success)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Logger[i].Add(Logger[i].Count, values[i]);
                    }
                }
                else
                {
                    break;
                }
            }

            if (Logger[0].Count == Logger[0].CountOnLastRender)
                return;

            formsPlot1.Refresh();
            Text = $"DataLogger Demo ({Logger[0].Count:N0} points)";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // sart stop plotting
            if (timer.Enabled)
            {
                timer.Enabled = false;
            }
            else
            {
                timer.Enabled = true;
            }
        }
        private void btnAutoScale_Click(object sender, EventArgs e)
        {
            formsPlot1.Plot.Margins();
            formsPlot1.Plot.AxisAuto();
            formsPlot1.Refresh();
        }
        private void btnAutoScaleTight_Click(object sender, EventArgs e)
        {
            formsPlot1.Plot.Margins(0, 0);
            formsPlot1.Refresh();
        }
        private void btnManualScale_Click(object sender, EventArgs e)
        {
            formsPlot1.Plot.SetAxisLimits(0, 50, -20, 20, 0, 1);
            formsPlot1.Plot.SetAxisLimits(0, 50, -20_000, 20_000, 0, 1);
            formsPlot1.Refresh();
        }
        private void btnAutoScaleX_Click(object sender, EventArgs e)
        {
            formsPlot1.Plot.AxisAutoX();
            formsPlot1.Refresh();
        }
        private void btnAutoScaleY_Click(object sender, EventArgs e)
        {
            formsPlot1.Plot.AxisAutoY();
            formsPlot1.Refresh();
        }
    }
}

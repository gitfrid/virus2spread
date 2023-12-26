
using Simulator;

namespace Plotter
{

    // MainForm infinite enqueues list with ten random Y-doubles into a ConcurrentQueue 
    // PlotForm dequeues the lists and draws infinite ten lines on a Scottplot DataLogger diagram 
    // Is not fully real-time capable, also slowes down considerably above 100000 data points
    // .NET 8 WinForm App - MIT License

    // Uses Nuget Package ScottPlot.WinForms 4.1.69 (MIT License)
    // source : https://scottplot.net

    public partial class Main : Form
    {
        private readonly Simulate simulation  = new ();
        public Main()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            PlotForm form1 = new PlotForm(simulation);
            form1.Show();
            Timer1.Enabled = true;
            Timer1.Interval = 1;
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                // generates a list of Y values writes them to a queue,
                // reads list from queue and plot the lines by PlotForm
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
        }
    }
}

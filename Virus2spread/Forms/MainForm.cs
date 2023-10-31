
using System.ComponentModel;
using System.Configuration;
using VirusSpreadLibrary.Properties;
using VirusSpreadLibrary.SpreadModel;

namespace Virus2spread
{
    public partial class MainForm : Form
    {
        private Simulation _modelSimulation = new Simulation(Settings.Default.GridMaxX, Settings.Default.GridMaxY, Settings.Default.maxIterations);
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ConfigurationPropertyGrid.SelectedObject = Settings.Default;
            ConfigurationPropertyGrid.BrowsableAttributes = new AttributeCollection(new UserScopedSettingAttribute());
            // set window size
            this.Location = Settings.Default.Form_Config_WindowLocation;
            this.Size = Settings.Default.Form_Config_WindowSize;
        }

        private void StartSimulation_button1_Click(object sender, EventArgs e)
        {
            _modelSimulation.StartIterate();
        }

        private void ConfigurationPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            VirusSpreadLibrary.Properties.Settings.Default.Save();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // get window size 
            Settings.Default.Form_Config_WindowLocation = this.Location;
            if (this.WindowState == FormWindowState.Normal)
            {
                Settings.Default.Form_Config_WindowSize = this.Size;
            }
            else
            {
                Settings.Default.Form_Config_WindowSize = this.RestoreBounds.Size;
            }
            // Save Config settings
            Settings.Default.Save();
        }

    }
}

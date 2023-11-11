
using System.ComponentModel;
using System.Configuration;
using Virus2spread.Forms;
using VirusSpreadLibrary.Properties;
using VirusSpreadLibrary.SpreadModel;

namespace Virus2spread
{
    public partial class MainForm : Form
    {
        public Simulation modelSimulation = new Simulation();
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ConfigurationPropertyGrid.SelectedObject = Settings.Default;
            ConfigurationPropertyGrid.BrowsableAttributes = new AttributeCollection(new UserScopedSettingAttribute());

            // set window size
            this.MinimumSize = new Size(1600, 900);
            this.Location = Settings.Default.Form_Config_WindowLocation;
            this.Size = Settings.Default.Form_Config_WindowSize;
        }

        private void StartSimulation_button1_Click_1(object sender, EventArgs e)
        {
            Form fc = Application.OpenForms["GridForm"];
            if (fc is not null)
            {
                fc.Close();
            }
            GridForm myGridForm = new GridForm(modelSimulation, Settings.Default.GridMaxX, Settings.Default.GridMaxY);
            myGridForm.Show();
            this.Focus();
            modelSimulation.StartIteration();
        }

        private void ConfigurationPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            modelSimulation.StopIteration();
            Settings.Default.Save();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // save current window size 
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

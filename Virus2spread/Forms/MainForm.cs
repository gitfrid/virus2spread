
using System.ComponentModel;
using VirusSpreadLibrary.AppProperties;
using VirusSpreadLibrary.AppProperties.PropertyGridExt;
using VirusSpreadLibrary.SpreadModel;
using Virus2spread.Forms;

namespace Virus2spread;

public partial class MainForm : Form
{
    private Simulation? modelSimulation;
    public MainForm()
    {
        InitializeComponent();

        //  make property grid listen to collection properties changes
        //  using a custom editor extension in CollectionEditorExt.cs
        CollectionEditorExt.EditorFormClosed += new CollectionEditorExt.
        EditorFormClosedEventHandler(ConfigurationPropertyGrid_CollectionFormClosed);

    }
    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);

        AppSettings.Config.PersonMoveRate.PropertyChanged += SamplePropertyChangedHandler!;
        AppSettings.Config.VirusMoveRate.PropertyChanged += SamplePropertyChangedHandler!;
        // do somthing on change here ..
        // ConfigurationPropertyGrid.SelectedObject = ConfigurationPropertyGrid.SelectedObject = AppSettings.Config;
    }
    private void SamplePropertyChangedHandler(object sender, PropertyChangedEventArgs e)
    {
        eventsListBox.AddEvent(null!, nameof(DoubleSeriesClass.PropertyChanged), e);
    }
    private void MainForm_Load(object sender, EventArgs e)
    {
        AppSettings.Config.Setting.Load();
        PropertyGridSelectConfig();
        RestoreWindowPosition();
    }
    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        SaveWindowsPosition();
        AppSettings.Config.Setting.Save();
    }
    private void StartSimulation_button1_Click_1(object sender, EventArgs e)
    {


        Simulation simulation = new();
        modelSimulation = simulation;
        //modelSimulation.Initialize();
        Form? grdForm = Application.OpenForms["GridForm"];
        grdForm?.Close();
        GridForm gridForm = new(modelSimulation, AppSettings.Config.GridMaxX, AppSettings.Config.GridMaxY);
        gridForm.Show();
        this.Focus();
        modelSimulation.StartIteration();
    }
    private void ShowChart_button_Click(object sender, EventArgs e)
    {
        Form? pltForm = Application.OpenForms["PlotForm"];
        pltForm?.Close();
        if (modelSimulation is not null)
        {
            PlotForm plotForm = new(modelSimulation.PlotData);
            plotForm.Show();
        }
        this.Focus();
    }
    private void ShowPhaseChart_button_Click(object sender, EventArgs e)
    {
        Form? phaChartForm = Application.OpenForms["PhaseChartForm"];
        phaChartForm?.Close();
        if (modelSimulation is not null)
        {
            PhaseChartForm phaseChartForm = new(modelSimulation.PlotData);
            phaseChartForm.Show();
        }
        this.Focus();
    }
    private void PropertyGridSelectConfig()
    {
        ConfigurationPropertyGrid.SelectedObject = AppSettings.Config;
        //unhides properties with attribute [Browsable(false)]
        if (AppSettings.Config.ShowHelperSettings)
        {
            ConfigurationPropertyGrid.BrowsableAttributes = new AttributeCollection();
        }
        else
        {
            ConfigurationPropertyGrid.BrowsableAttributes = null;
        };
        ConfigurationPropertyGrid.Refresh();
    }      
    private void ConfigurationPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
        //modelSimulation.StopIteration();
        AppSettings.Config.Setting.Save();
    }

    private void ConfigurationPropertyGrid_CollectionFormClosed(object s, FormClosedEventArgs e)
    {
        // code to run if extended custom collection editor form is closed
        // MessageBox.Show("was closed");
        ConfigurationPropertyGrid.SelectedObject = ConfigurationPropertyGrid.SelectedObject;
        AppSettings.Config.Setting.Save();
    }
    private void LoadConfig_button2_Click(object sender, EventArgs e)
    {
        AppSettings.Config.Setting.Load(true);
        PropertyGridSelectConfig();
    }

    private void SaveConfig_button3_Click(object sender, EventArgs e)
    {
        AppSettings.Config.Setting.Save(true);
        ConfigurationPropertyGrid.Refresh();
    }
    private static bool IsVisiblePosition(Point location, Size size)
    {
        Rectangle myArea = new(location, size);
        bool intersect = false;
        foreach (System.Windows.Forms.Screen screen in System.Windows.Forms.Screen.AllScreens)
        {
            intersect |= myArea.IntersectsWith(screen.WorkingArea);
        }
        return intersect;
    }
    private void RestoreWindowPosition()
    {
        // set window position
        if (IsVisiblePosition(AppSettings.Config.Form_Config_WindowLocation, AppSettings.Config.Form_Config_WindowSize))
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = AppSettings.Config.Form_Config_WindowLocation;
            this.Size = AppSettings.Config.Form_Config_WindowSize;
            WindowState = FormWindowState.Normal;
        }
        else
        {
            this.WindowState = FormWindowState.Normal;
        }
    }
    private void SaveWindowsPosition()
    {
        // write window size to app config vars
        if (this.WindowState == FormWindowState.Normal)
        {
            AppSettings.Config.Form_Config_WindowSize = this.Size;
            AppSettings.Config.Form_Config_WindowLocation = this.Location;
        }
        else
        {
            AppSettings.Config.Form_Config_WindowSize = this.RestoreBounds.Size;
            AppSettings.Config.Form_Config_WindowLocation = this.RestoreBounds.Location;
        }
    }

}

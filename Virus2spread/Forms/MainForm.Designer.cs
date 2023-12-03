namespace Virus2spread
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tabControl = new TabControl();
            tabPage1 = new TabPage();
            splitContainer1 = new SplitContainer();
            ConfigurationPropertyGrid = new PropertyGrid();
            LoadConfig_button2 = new Button();
            eventsListBox = new VirusSpreadLibrary.AppProperties.EventsListBox();
            StartSimulation_button1 = new Button();
            tabPage2 = new TabPage();
            tabControl.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPage1);
            tabControl.Controls.Add(tabPage2);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1614, 832);
            tabControl.TabIndex = 2;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(splitContainer1);
            tabPage1.Location = new Point(8, 46);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1598, 778);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Settings";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(ConfigurationPropertyGrid);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(LoadConfig_button2);
            splitContainer1.Panel2.Controls.Add(eventsListBox);
            splitContainer1.Panel2.Controls.Add(StartSimulation_button1);
            splitContainer1.Size = new Size(1592, 772);
            splitContainer1.SplitterDistance = 1280;
            splitContainer1.TabIndex = 3;
            // 
            // ConfigurationPropertyGrid
            // 
            ConfigurationPropertyGrid.Dock = DockStyle.Fill;
            ConfigurationPropertyGrid.Location = new Point(0, 0);
            ConfigurationPropertyGrid.Name = "ConfigurationPropertyGrid";
            ConfigurationPropertyGrid.Size = new Size(1280, 772);
            ConfigurationPropertyGrid.TabIndex = 2;
            ConfigurationPropertyGrid.PropertyValueChanged += ConfigurationPropertyGrid_PropertyValueChanged;
            // 
            // LoadConfig_button2
            // 
            LoadConfig_button2.Location = new Point(28, 102);
            LoadConfig_button2.Name = "LoadConfig_button2";
            LoadConfig_button2.Size = new Size(150, 46);
            LoadConfig_button2.TabIndex = 5;
            LoadConfig_button2.Text = "Load config";
            LoadConfig_button2.UseVisualStyleBackColor = true;
            LoadConfig_button2.Click += LoadConfig_button2_Click;
            // 
            // eventsListBox
            // 
            eventsListBox.Dock = DockStyle.Bottom;
            eventsListBox.FormattingEnabled = true;
            eventsListBox.Location = new Point(0, 649);
            eventsListBox.Name = "eventsListBox";
            eventsListBox.Size = new Size(308, 123);
            eventsListBox.TabIndex = 4;
            // 
            // StartSimulation_button1
            // 
            StartSimulation_button1.Location = new Point(25, 27);
            StartSimulation_button1.Margin = new Padding(5);
            StartSimulation_button1.Name = "StartSimulation_button1";
            StartSimulation_button1.Size = new Size(153, 46);
            StartSimulation_button1.TabIndex = 3;
            StartSimulation_button1.Text = "Simulation";
            StartSimulation_button1.UseVisualStyleBackColor = true;
            StartSimulation_button1.Click += StartSimulation_button1_Click_1;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(8, 46);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1598, 778);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Rate Settings";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1614, 832);
            Controls.Add(tabControl);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "virus2spread MainForm";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            tabControl.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private SplitContainer splitContainer1;
        private PropertyGrid ConfigurationPropertyGrid;
        private Button StartSimulation_button1;
        private VirusSpreadLibrary.AppProperties.EventsListBox eventsListBox;
        private Button LoadConfig_button2;
    }
}
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
            ConfigurationPropertyGrid = new PropertyGrid();
            StartSimulation_button1 = new Button();
            SuspendLayout();
            // 
            // ConfigurationPropertyGrid
            // 
            ConfigurationPropertyGrid.Location = new Point(74, 54);
            ConfigurationPropertyGrid.Margin = new Padding(2);
            ConfigurationPropertyGrid.Name = "ConfigurationPropertyGrid";
            ConfigurationPropertyGrid.Size = new Size(792, 536);
            ConfigurationPropertyGrid.TabIndex = 0;
            ConfigurationPropertyGrid.PropertyValueChanged += ConfigurationPropertyGrid_PropertyValueChanged;
            // 
            // StartSimulation_button1
            // 
            StartSimulation_button1.Location = new Point(1031, 89);
            StartSimulation_button1.Name = "StartSimulation_button1";
            StartSimulation_button1.Size = new Size(94, 29);
            StartSimulation_button1.TabIndex = 1;
            StartSimulation_button1.Text = "Simulation";
            StartSimulation_button1.UseVisualStyleBackColor = true;
            StartSimulation_button1.Click += StartSimulation_button1_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1478, 622);
            Controls.Add(StartSimulation_button1);
            Controls.Add(ConfigurationPropertyGrid);
            Margin = new Padding(2);
            Name = "MainForm";
            Text = "MainForm";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private PropertyGrid ConfigurationPropertyGrid;
        private Button StartSimulation_button1;
    }
}
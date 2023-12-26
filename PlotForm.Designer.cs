namespace Plotter
{
    partial class PlotForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            formsPlot1 = new ScottPlot.FormsPlot();
            splitContainer1 = new SplitContainer();
            btnAutoScaleX = new Button();
            btnManualScale = new Button();
            btnAutoScaleTight = new Button();
            btnAutoScale = new Button();
            btnAutoScaleY = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(37, 34);
            button1.Name = "button1";
            button1.Size = new Size(190, 46);
            button1.TabIndex = 0;
            button1.Text = "hold / start";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // formsPlot1
            // 
            formsPlot1.Dock = DockStyle.Fill;
            formsPlot1.Location = new Point(0, 0);
            formsPlot1.Margin = new Padding(7, 6, 7, 6);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(1585, 1051);
            formsPlot1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(btnAutoScaleY);
            splitContainer1.Panel1.Controls.Add(btnAutoScaleX);
            splitContainer1.Panel1.Controls.Add(btnManualScale);
            splitContainer1.Panel1.Controls.Add(btnAutoScaleTight);
            splitContainer1.Panel1.Controls.Add(btnAutoScale);
            splitContainer1.Panel1.Controls.Add(button1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(formsPlot1);
            splitContainer1.Size = new Size(1862, 1051);
            splitContainer1.SplitterDistance = 271;
            splitContainer1.SplitterWidth = 6;
            splitContainer1.TabIndex = 2;
            // 
            // btnAutoScaleX
            // 
            btnAutoScaleX.Location = new Point(37, 393);
            btnAutoScaleX.Name = "btnAutoScaleX";
            btnAutoScaleX.Size = new Size(190, 46);
            btnAutoScaleX.TabIndex = 4;
            btnAutoScaleX.Text = "auto scale X";
            btnAutoScaleX.UseVisualStyleBackColor = true;
            btnAutoScaleX.Click += btnAutoScaleX_Click;
            // 
            // btnManualScale
            // 
            btnManualScale.Location = new Point(37, 156);
            btnManualScale.Name = "btnManualScale";
            btnManualScale.Size = new Size(190, 46);
            btnManualScale.TabIndex = 3;
            btnManualScale.Text = "manual scale";
            btnManualScale.UseVisualStyleBackColor = true;
            btnManualScale.Click += btnManualScale_Click;
            // 
            // btnAutoScaleTight
            // 
            btnAutoScaleTight.Location = new Point(37, 305);
            btnAutoScaleTight.Name = "btnAutoScaleTight";
            btnAutoScaleTight.Size = new Size(190, 46);
            btnAutoScaleTight.TabIndex = 2;
            btnAutoScaleTight.Text = "auto scale tight";
            btnAutoScaleTight.UseVisualStyleBackColor = true;
            btnAutoScaleTight.Click += btnAutoScaleTight_Click;
            // 
            // btnAutoScale
            // 
            btnAutoScale.Location = new Point(37, 224);
            btnAutoScale.Name = "btnAutoScale";
            btnAutoScale.Size = new Size(190, 46);
            btnAutoScale.TabIndex = 1;
            btnAutoScale.Text = "auto scale";
            btnAutoScale.UseVisualStyleBackColor = true;
            btnAutoScale.Click += btnAutoScale_Click;
            // 
            // btnAutoScaleY
            // 
            btnAutoScaleY.Location = new Point(37, 470);
            btnAutoScaleY.Name = "btnAutoScaleY";
            btnAutoScaleY.Size = new Size(190, 46);
            btnAutoScaleY.TabIndex = 5;
            btnAutoScaleY.Text = "auto scale Y";
            btnAutoScaleY.UseVisualStyleBackColor = true;
            btnAutoScaleY.Click += btnAutoScaleY_Click;
            // 
            // PlotForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1862, 1051);
            Controls.Add(splitContainer1);
            Name = "PlotForm";
            Text = "PlotForm";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private ScottPlot.FormsPlot formsPlot1;
        private SplitContainer splitContainer1;
        private Button btnAutoScale;
        private Button btnAutoScaleTight;
        private Button btnManualScale;
        private Button btnAutoScaleX;
        private Button btnAutoScaleY;
    }
}

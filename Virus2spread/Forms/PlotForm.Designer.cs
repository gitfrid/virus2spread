namespace Virus2spread
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
            components = new System.ComponentModel.Container();
            BtnHoldStart = new Button();
            splitContainer1 = new SplitContainer();
            CbAutoAxis = new CheckBox();
            ChkShowLegend = new CheckBox();
            BtnAutoScaleY = new Button();
            BtnAutoScaleX = new Button();
            BtnManualScale = new Button();
            BtnAutoScaleTight = new Button();
            BtnAutoScale = new Button();
            dataTimer = new System.Windows.Forms.Timer(components);
            renderTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // BtnHoldStart
            // 
            BtnHoldStart.Location = new Point(37, 34);
            BtnHoldStart.Name = "BtnHoldStart";
            BtnHoldStart.Size = new Size(190, 46);
            BtnHoldStart.TabIndex = 0;
            BtnHoldStart.Text = "hold / start";
            BtnHoldStart.UseVisualStyleBackColor = true;
            BtnHoldStart.Click += BtnHoldStart_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(CbAutoAxis);
            splitContainer1.Panel1.Controls.Add(ChkShowLegend);
            splitContainer1.Panel1.Controls.Add(BtnAutoScaleY);
            splitContainer1.Panel1.Controls.Add(BtnAutoScaleX);
            splitContainer1.Panel1.Controls.Add(BtnManualScale);
            splitContainer1.Panel1.Controls.Add(BtnAutoScaleTight);
            splitContainer1.Panel1.Controls.Add(BtnAutoScale);
            splitContainer1.Panel1.Controls.Add(BtnHoldStart);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = SystemColors.Control;
            splitContainer1.Size = new Size(1862, 1051);
            splitContainer1.SplitterDistance = 271;
            splitContainer1.SplitterWidth = 6;
            splitContainer1.TabIndex = 2;
            // 
            // CbAutoAxis
            // 
            CbAutoAxis.AutoSize = true;
            CbAutoAxis.Checked = true;
            CbAutoAxis.CheckState = CheckState.Checked;
            CbAutoAxis.Location = new Point(27, 895);
            CbAutoAxis.Margin = new Padding(6, 7, 6, 7);
            CbAutoAxis.Name = "CbAutoAxis";
            CbAutoAxis.Size = new Size(222, 36);
            CbAutoAxis.TabIndex = 11;
            CbAutoAxis.Text = "auto axis update";
            CbAutoAxis.TextAlign = ContentAlignment.TopCenter;
            CbAutoAxis.UseVisualStyleBackColor = true;
            CbAutoAxis.CheckedChanged += CbAutoAxis_CheckedChanged;
            // 
            // ChkShowLegend
            // 
            ChkShowLegend.AutoSize = true;
            ChkShowLegend.Checked = true;
            ChkShowLegend.CheckState = CheckState.Checked;
            ChkShowLegend.Location = new Point(27, 956);
            ChkShowLegend.Margin = new Padding(5);
            ChkShowLegend.Name = "ChkShowLegend";
            ChkShowLegend.Size = new Size(182, 36);
            ChkShowLegend.TabIndex = 10;
            ChkShowLegend.Text = "show legend";
            ChkShowLegend.UseVisualStyleBackColor = true;
            ChkShowLegend.CheckedChanged += ChkShowLegend_CheckedChanged;
            // 
            // BtnAutoScaleY
            // 
            BtnAutoScaleY.Location = new Point(37, 470);
            BtnAutoScaleY.Name = "BtnAutoScaleY";
            BtnAutoScaleY.Size = new Size(190, 46);
            BtnAutoScaleY.TabIndex = 5;
            BtnAutoScaleY.Text = "auto scale Y";
            BtnAutoScaleY.UseVisualStyleBackColor = true;
            BtnAutoScaleY.Click += BtnAutoScaleY_Click;
            // 
            // BtnAutoScaleX
            // 
            BtnAutoScaleX.Location = new Point(37, 385);
            BtnAutoScaleX.Name = "BtnAutoScaleX";
            BtnAutoScaleX.Size = new Size(190, 46);
            BtnAutoScaleX.TabIndex = 4;
            BtnAutoScaleX.Text = "auto scale X";
            BtnAutoScaleX.UseVisualStyleBackColor = true;
            BtnAutoScaleX.Click += BtnAutoScaleX_Click;
            // 
            // BtnManualScale
            // 
            BtnManualScale.Location = new Point(37, 156);
            BtnManualScale.Name = "BtnManualScale";
            BtnManualScale.Size = new Size(190, 46);
            BtnManualScale.TabIndex = 3;
            BtnManualScale.Text = "manual scale";
            BtnManualScale.UseVisualStyleBackColor = true;
            BtnManualScale.Click += BtnManualScale_Click;
            // 
            // BtnAutoScaleTight
            // 
            BtnAutoScaleTight.Location = new Point(37, 305);
            BtnAutoScaleTight.Name = "BtnAutoScaleTight";
            BtnAutoScaleTight.Size = new Size(190, 46);
            BtnAutoScaleTight.TabIndex = 2;
            BtnAutoScaleTight.Text = "auto scale tight";
            BtnAutoScaleTight.UseVisualStyleBackColor = true;
            BtnAutoScaleTight.Click += BtnAutoScaleTight_Click;
            // 
            // BtnAutoScale
            // 
            BtnAutoScale.Location = new Point(37, 224);
            BtnAutoScale.Name = "BtnAutoScale";
            BtnAutoScale.Size = new Size(190, 46);
            BtnAutoScale.TabIndex = 1;
            BtnAutoScale.Text = "auto scale";
            BtnAutoScale.UseVisualStyleBackColor = true;
            BtnAutoScale.Click += BtnAutoScale_Click;
            // 
            // dataTimer
            // 
            dataTimer.Enabled = true;
            dataTimer.Interval = 1;
            dataTimer.Tick += dataTimer_Tick;
            // 
            // renderTimer
            // 
            renderTimer.Enabled = true;
            renderTimer.Interval = 20;
            renderTimer.Tick += renderTimer_Tick;
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
            splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button BtnHoldStart;
        private SplitContainer splitContainer1;
        private Button BtnAutoScale;
        private Button BtnAutoScaleTight;
        private Button BtnManualScale;
        private Button BtnAutoScaleX;
        private Button BtnAutoScaleY;
        private CheckBox ChkShowLegend;
        private CheckBox CbAutoAxis;
        private System.Windows.Forms.Timer dataTimer;
        private System.Windows.Forms.Timer renderTimer;
    }
}

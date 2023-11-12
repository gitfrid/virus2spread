namespace Virus2spread.Forms
{
    partial class GridForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridForm));
            skglControl1 = new SkiaSharp.Views.Desktop.SKGLControl();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // skglControl1
            // 
            skglControl1.BackColor = Color.Black;
            skglControl1.Dock = DockStyle.Fill;
            skglControl1.Location = new Point(0, 0);
            skglControl1.Margin = new Padding(6, 8, 6, 8);
            skglControl1.Name = "skglControl1";
            skglControl1.Size = new Size(1334, 781);
            skglControl1.TabIndex = 0;
            skglControl1.VSync = true;
            skglControl1.PaintSurface += skglControl1_PaintSurface;
            skglControl1.SizeChanged += skglControl1_SizeChanged;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Tick += timer1_Tick_1;
            // 
            // GridForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1334, 781);
            Controls.Add(skglControl1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5);
            Name = "GridForm";
            Text = "GridForm";
            ResumeLayout(false);
        }

        #endregion

        private SkiaSharp.Views.Desktop.SKGLControl skglControl1;
        private System.Windows.Forms.Timer timer1;
    }
}
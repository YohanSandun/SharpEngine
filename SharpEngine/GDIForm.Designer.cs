namespace SharpEngine
{
    partial class GDIForm
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
            this.components = new System.ComponentModel.Container();
            this.tmrRenderer = new System.Windows.Forms.Timer(this.components);
            this.tmrAnimator = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tmrRenderer
            // 
            this.tmrRenderer.Enabled = true;
            this.tmrRenderer.Interval = 1;
            this.tmrRenderer.Tick += new System.EventHandler(this.tmrTimer_Tick);
            // 
            // tmrAnimator
            // 
            this.tmrAnimator.Enabled = true;
            this.tmrAnimator.Interval = 1;
            this.tmrAnimator.Tick += new System.EventHandler(this.tmrAnimator_Tick);
            // 
            // GDIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 612);
            this.DoubleBuffered = true;
            this.Name = "GDIForm";
            this.Text = "GDIForm";
            this.Load += new System.EventHandler(this.GDIForm_Load);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.GDIForm_Scroll);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GDIForm_Paint);
            this.Resize += new System.EventHandler(this.GDIForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrRenderer;
        private System.Windows.Forms.Timer tmrAnimator;
    }
}
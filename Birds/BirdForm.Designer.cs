namespace EAMS.Birds
{
    partial class BirdForm
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
        /// Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BirdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "BirdForm";
            this.Text = "BirdForm";
            this.Load += new System.EventHandler(this.BirdForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        /// <summary>
        /// Handles the Load event of the BirdForm control.
        /// </summary>
        private void BirdForm_Load(object sender, System.EventArgs e)
        {
            
        }
    }
}
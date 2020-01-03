namespace mastermind
{
    partial class FrmAbout
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
            this.BTN_CLOSE = new System.Windows.Forms.Button();
            this.TB_README = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // BTN_CLOSE
            // 
            this.BTN_CLOSE.Location = new System.Drawing.Point(371, 265);
            this.BTN_CLOSE.Name = "BTN_CLOSE";
            this.BTN_CLOSE.Size = new System.Drawing.Size(75, 23);
            this.BTN_CLOSE.TabIndex = 0;
            this.BTN_CLOSE.Text = "Close";
            this.BTN_CLOSE.UseVisualStyleBackColor = true;
            this.BTN_CLOSE.Click += new System.EventHandler(this.BTN_CLOSE_Click);
            // 
            // TB_README
            // 
            this.TB_README.Location = new System.Drawing.Point(13, 13);
            this.TB_README.Multiline = true;
            this.TB_README.Name = "TB_README";
            this.TB_README.Size = new System.Drawing.Size(788, 246);
            this.TB_README.TabIndex = 1;
            // 
            // FrmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 307);
            this.Controls.Add(this.TB_README);
            this.Controls.Add(this.BTN_CLOSE);
            this.Name = "FrmAbout";
            this.Text = "FrmAbout";
            this.Load += new System.EventHandler(this.FrmAbout_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTN_CLOSE;
        private System.Windows.Forms.TextBox TB_README;
    }
}
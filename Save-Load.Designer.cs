namespace WindowsFormsApp1
{
    partial class Save_Load
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
            this.lblSaveLoad = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSaveLoad
            // 
            this.lblSaveLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.lblSaveLoad.Location = new System.Drawing.Point(12, 30);
            this.lblSaveLoad.Name = "lblSaveLoad";
            this.lblSaveLoad.Size = new System.Drawing.Size(274, 60);
            this.lblSaveLoad.TabIndex = 0;
            this.lblSaveLoad.Text = "Loaded saved settings from config file";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(106, 114);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "Okay";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // Save_Load
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 163);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.lblSaveLoad);
            this.Name = "Save_Load";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Save_Load";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSaveLoad;
        private System.Windows.Forms.Button btnConfirm;
    }
}
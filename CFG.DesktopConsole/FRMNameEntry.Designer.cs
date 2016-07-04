namespace CFG.DesktopConsole
{
    partial class FRMNameEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRMNameEntry));
            this.lbl_AtomValueLabel = new System.Windows.Forms.Label();
            this.txt_AtomPath = new System.Windows.Forms.TextBox();
            this.cmd_Cancel = new System.Windows.Forms.Button();
            this.cmd_Build = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_AtomValueLabel
            // 
            this.lbl_AtomValueLabel.AutoSize = true;
            this.lbl_AtomValueLabel.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AtomValueLabel.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbl_AtomValueLabel.Location = new System.Drawing.Point(12, 16);
            this.lbl_AtomValueLabel.Name = "lbl_AtomValueLabel";
            this.lbl_AtomValueLabel.Size = new System.Drawing.Size(45, 18);
            this.lbl_AtomValueLabel.TabIndex = 3;
            this.lbl_AtomValueLabel.Text = "Name";
            // 
            // txt_AtomPath
            // 
            this.txt_AtomPath.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txt_AtomPath.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AtomPath.Location = new System.Drawing.Point(63, 13);
            this.txt_AtomPath.Name = "txt_AtomPath";
            this.txt_AtomPath.Size = new System.Drawing.Size(362, 26);
            this.txt_AtomPath.TabIndex = 0;
            this.txt_AtomPath.WordWrap = false;
            this.txt_AtomPath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_AtomPath_KeyPress);
            // 
            // cmd_Cancel
            // 
            this.cmd_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmd_Cancel.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_Cancel.ForeColor = System.Drawing.Color.RoyalBlue;
            this.cmd_Cancel.Location = new System.Drawing.Point(245, 53);
            this.cmd_Cancel.Name = "cmd_Cancel";
            this.cmd_Cancel.Size = new System.Drawing.Size(84, 26);
            this.cmd_Cancel.TabIndex = 5;
            this.cmd_Cancel.TabStop = false;
            this.cmd_Cancel.Text = "Cancel";
            this.cmd_Cancel.UseVisualStyleBackColor = true;
            this.cmd_Cancel.Click += new System.EventHandler(this.cmd_Cancel_Click);
            // 
            // cmd_Build
            // 
            this.cmd_Build.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmd_Build.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_Build.ForeColor = System.Drawing.Color.RoyalBlue;
            this.cmd_Build.Location = new System.Drawing.Point(341, 53);
            this.cmd_Build.Name = "cmd_Build";
            this.cmd_Build.Size = new System.Drawing.Size(84, 26);
            this.cmd_Build.TabIndex = 6;
            this.cmd_Build.TabStop = false;
            this.cmd_Build.Text = "Build";
            this.cmd_Build.UseVisualStyleBackColor = true;
            this.cmd_Build.Click += new System.EventHandler(this.cmd_Build_Click);
            // 
            // FRMNameEntry
            // 
            this.AcceptButton = this.cmd_Build;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CancelButton = this.cmd_Cancel;
            this.ClientSize = new System.Drawing.Size(437, 92);
            this.Controls.Add(this.cmd_Build);
            this.Controls.Add(this.cmd_Cancel);
            this.Controls.Add(this.txt_AtomPath);
            this.Controls.Add(this.lbl_AtomValueLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRMNameEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enter the Name";
            this.Load += new System.EventHandler(this.FRMNameEntry_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_AtomValueLabel;
        private System.Windows.Forms.TextBox txt_AtomPath;
        private System.Windows.Forms.Button cmd_Cancel;
        private System.Windows.Forms.Button cmd_Build;
    }
}
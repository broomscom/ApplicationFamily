namespace CFG.DesktopConsole
{
    partial class FRMConsole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRMConsole));
            this.ConsoleSplitContainer = new System.Windows.Forms.SplitContainer();
            this.cfg_TreeView = new System.Windows.Forms.TreeView();
            this.lbl_AtomPathLabel = new System.Windows.Forms.Label();
            this.txt_AtomPath = new System.Windows.Forms.TextBox();
            this.lbl_AtomValueLabel = new System.Windows.Forms.Label();
            this.txt_AtomValue = new System.Windows.Forms.TextBox();
            this.lbl_ReadOnlyLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ConsoleSplitContainer)).BeginInit();
            this.ConsoleSplitContainer.Panel1.SuspendLayout();
            this.ConsoleSplitContainer.Panel2.SuspendLayout();
            this.ConsoleSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConsoleSplitContainer
            // 
            this.ConsoleSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConsoleSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.ConsoleSplitContainer.Name = "ConsoleSplitContainer";
            // 
            // ConsoleSplitContainer.Panel1
            // 
            this.ConsoleSplitContainer.Panel1.Controls.Add(this.cfg_TreeView);
            // 
            // ConsoleSplitContainer.Panel2
            // 
            this.ConsoleSplitContainer.Panel2.Controls.Add(this.lbl_ReadOnlyLabel);
            this.ConsoleSplitContainer.Panel2.Controls.Add(this.txt_AtomValue);
            this.ConsoleSplitContainer.Panel2.Controls.Add(this.lbl_AtomValueLabel);
            this.ConsoleSplitContainer.Panel2.Controls.Add(this.txt_AtomPath);
            this.ConsoleSplitContainer.Panel2.Controls.Add(this.lbl_AtomPathLabel);
            this.ConsoleSplitContainer.Size = new System.Drawing.Size(649, 300);
            this.ConsoleSplitContainer.SplitterDistance = 216;
            this.ConsoleSplitContainer.TabIndex = 0;
            // 
            // cfg_TreeView
            // 
            this.cfg_TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfg_TreeView.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cfg_TreeView.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.cfg_TreeView.Location = new System.Drawing.Point(0, 0);
            this.cfg_TreeView.Name = "cfg_TreeView";
            this.cfg_TreeView.Size = new System.Drawing.Size(216, 300);
            this.cfg_TreeView.TabIndex = 0;
            // 
            // lbl_AtomPathLabel
            // 
            this.lbl_AtomPathLabel.AutoSize = true;
            this.lbl_AtomPathLabel.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AtomPathLabel.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbl_AtomPathLabel.Location = new System.Drawing.Point(13, 12);
            this.lbl_AtomPathLabel.Name = "lbl_AtomPathLabel";
            this.lbl_AtomPathLabel.Size = new System.Drawing.Size(73, 18);
            this.lbl_AtomPathLabel.TabIndex = 0;
            this.lbl_AtomPathLabel.Text = "Atom Path";
            // 
            // txt_AtomPath
            // 
            this.txt_AtomPath.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txt_AtomPath.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AtomPath.Location = new System.Drawing.Point(92, 9);
            this.txt_AtomPath.Name = "txt_AtomPath";
            this.txt_AtomPath.ReadOnly = true;
            this.txt_AtomPath.Size = new System.Drawing.Size(325, 26);
            this.txt_AtomPath.TabIndex = 1;
            this.txt_AtomPath.WordWrap = false;
            // 
            // lbl_AtomValueLabel
            // 
            this.lbl_AtomValueLabel.AutoSize = true;
            this.lbl_AtomValueLabel.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AtomValueLabel.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbl_AtomValueLabel.Location = new System.Drawing.Point(13, 57);
            this.lbl_AtomValueLabel.Name = "lbl_AtomValueLabel";
            this.lbl_AtomValueLabel.Size = new System.Drawing.Size(80, 18);
            this.lbl_AtomValueLabel.TabIndex = 2;
            this.lbl_AtomValueLabel.Text = "Atom Value";
            // 
            // txt_AtomValue
            // 
            this.txt_AtomValue.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AtomValue.Location = new System.Drawing.Point(16, 82);
            this.txt_AtomValue.Multiline = true;
            this.txt_AtomValue.Name = "txt_AtomValue";
            this.txt_AtomValue.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_AtomValue.Size = new System.Drawing.Size(401, 206);
            this.txt_AtomValue.TabIndex = 3;
            // 
            // lbl_ReadOnlyLabel
            // 
            this.lbl_ReadOnlyLabel.AutoSize = true;
            this.lbl_ReadOnlyLabel.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ReadOnlyLabel.ForeColor = System.Drawing.Color.Black;
            this.lbl_ReadOnlyLabel.Location = new System.Drawing.Point(89, 35);
            this.lbl_ReadOnlyLabel.Name = "lbl_ReadOnlyLabel";
            this.lbl_ReadOnlyLabel.Size = new System.Drawing.Size(58, 13);
            this.lbl_ReadOnlyLabel.TabIndex = 4;
            this.lbl_ReadOnlyLabel.Text = "(Read only)";
            // 
            // FRMConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(649, 300);
            this.Controls.Add(this.ConsoleSplitContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FRMConsole";
            this.Text = "Config Hub Console";
            this.ConsoleSplitContainer.Panel1.ResumeLayout(false);
            this.ConsoleSplitContainer.Panel2.ResumeLayout(false);
            this.ConsoleSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConsoleSplitContainer)).EndInit();
            this.ConsoleSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer ConsoleSplitContainer;
        private System.Windows.Forms.TreeView cfg_TreeView;
        private System.Windows.Forms.Label lbl_ReadOnlyLabel;
        private System.Windows.Forms.TextBox txt_AtomValue;
        private System.Windows.Forms.Label lbl_AtomValueLabel;
        private System.Windows.Forms.TextBox txt_AtomPath;
        private System.Windows.Forms.Label lbl_AtomPathLabel;
    }
}


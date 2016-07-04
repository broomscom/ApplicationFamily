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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRMConsole));
            this.ConsoleSplitContainer = new System.Windows.Forms.SplitContainer();
            this.trv_AtomTree = new System.Windows.Forms.TreeView();
            this.cms_ConfigTreeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmi_ConfigDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.cmi_ConfigRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.cmi_ConfigAddComponent = new System.Windows.Forms.ToolStripMenuItem();
            this.cmi_ConfigAddAtom = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_SelectToContinue = new System.Windows.Forms.Label();
            this.lbl_ReadOnlyLabel = new System.Windows.Forms.Label();
            this.txt_AtomValue = new System.Windows.Forms.TextBox();
            this.lbl_AtomValueLabel = new System.Windows.Forms.Label();
            this.txt_AtomPath = new System.Windows.Forms.TextBox();
            this.lbl_AtomPathLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ConsoleSplitContainer)).BeginInit();
            this.ConsoleSplitContainer.Panel1.SuspendLayout();
            this.ConsoleSplitContainer.Panel2.SuspendLayout();
            this.ConsoleSplitContainer.SuspendLayout();
            this.cms_ConfigTreeMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConsoleSplitContainer
            // 
            this.ConsoleSplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ConsoleSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConsoleSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.ConsoleSplitContainer.Name = "ConsoleSplitContainer";
            // 
            // ConsoleSplitContainer.Panel1
            // 
            this.ConsoleSplitContainer.Panel1.Controls.Add(this.trv_AtomTree);
            // 
            // ConsoleSplitContainer.Panel2
            // 
            this.ConsoleSplitContainer.Panel2.Controls.Add(this.lbl_SelectToContinue);
            this.ConsoleSplitContainer.Panel2.Controls.Add(this.lbl_ReadOnlyLabel);
            this.ConsoleSplitContainer.Panel2.Controls.Add(this.txt_AtomValue);
            this.ConsoleSplitContainer.Panel2.Controls.Add(this.lbl_AtomValueLabel);
            this.ConsoleSplitContainer.Panel2.Controls.Add(this.txt_AtomPath);
            this.ConsoleSplitContainer.Panel2.Controls.Add(this.lbl_AtomPathLabel);
            this.ConsoleSplitContainer.Size = new System.Drawing.Size(649, 300);
            this.ConsoleSplitContainer.SplitterDistance = 216;
            this.ConsoleSplitContainer.TabIndex = 0;
            this.ConsoleSplitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.ConsoleSplitContainer_SplitterMoved);
            this.ConsoleSplitContainer.SizeChanged += new System.EventHandler(this.ConsoleSplitContainer_SizeChanged);
            this.ConsoleSplitContainer.Resize += new System.EventHandler(this.ConsoleSplitContainer_Resize);
            // 
            // trv_AtomTree
            // 
            this.trv_AtomTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trv_AtomTree.ContextMenuStrip = this.cms_ConfigTreeMenu;
            this.trv_AtomTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trv_AtomTree.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trv_AtomTree.ForeColor = System.Drawing.Color.RoyalBlue;
            this.trv_AtomTree.FullRowSelect = true;
            this.trv_AtomTree.HideSelection = false;
            this.trv_AtomTree.Location = new System.Drawing.Point(0, 0);
            this.trv_AtomTree.Name = "trv_AtomTree";
            this.trv_AtomTree.Size = new System.Drawing.Size(214, 298);
            this.trv_AtomTree.TabIndex = 0;
            this.trv_AtomTree.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.trv_AtomTree_BeforeSelect);
            this.trv_AtomTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trv_AtomTree_AfterSelect);
            this.trv_AtomTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trv_AtomTree_NodeMouseClick);
            // 
            // cms_ConfigTreeMenu
            // 
            this.cms_ConfigTreeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmi_ConfigDelete,
            this.cmi_ConfigRefresh,
            this.cmi_ConfigAddComponent,
            this.cmi_ConfigAddAtom});
            this.cms_ConfigTreeMenu.Name = "cms_ConfigTreeMenu";
            this.cms_ConfigTreeMenu.Size = new System.Drawing.Size(169, 92);
            // 
            // cmi_ConfigDelete
            // 
            this.cmi_ConfigDelete.Name = "cmi_ConfigDelete";
            this.cmi_ConfigDelete.Size = new System.Drawing.Size(168, 22);
            this.cmi_ConfigDelete.Text = "Delete";
            this.cmi_ConfigDelete.Click += new System.EventHandler(this.cmi_ConfigDelete_Click);
            // 
            // cmi_ConfigRefresh
            // 
            this.cmi_ConfigRefresh.Name = "cmi_ConfigRefresh";
            this.cmi_ConfigRefresh.Size = new System.Drawing.Size(168, 22);
            this.cmi_ConfigRefresh.Text = "Refresh";
            this.cmi_ConfigRefresh.Click += new System.EventHandler(this.cmi_ConfigRefresh_Click);
            // 
            // cmi_ConfigAddComponent
            // 
            this.cmi_ConfigAddComponent.Name = "cmi_ConfigAddComponent";
            this.cmi_ConfigAddComponent.Size = new System.Drawing.Size(168, 22);
            this.cmi_ConfigAddComponent.Text = "Add Component";
            this.cmi_ConfigAddComponent.Click += new System.EventHandler(this.cmi_ConfigAddComponent_Click);
            // 
            // cmi_ConfigAddAtom
            // 
            this.cmi_ConfigAddAtom.Name = "cmi_ConfigAddAtom";
            this.cmi_ConfigAddAtom.Size = new System.Drawing.Size(168, 22);
            this.cmi_ConfigAddAtom.Text = "Add Config Atom";
            this.cmi_ConfigAddAtom.Click += new System.EventHandler(this.cmi_ConfigAddAtom_Click);
            // 
            // lbl_SelectToContinue
            // 
            this.lbl_SelectToContinue.AutoSize = true;
            this.lbl_SelectToContinue.Font = new System.Drawing.Font("Calibri", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SelectToContinue.ForeColor = System.Drawing.Color.Gray;
            this.lbl_SelectToContinue.Location = new System.Drawing.Point(13, 20);
            this.lbl_SelectToContinue.Name = "lbl_SelectToContinue";
            this.lbl_SelectToContinue.Size = new System.Drawing.Size(205, 26);
            this.lbl_SelectToContinue.TabIndex = 5;
            this.lbl_SelectToContinue.Text = "Select Atom to Modify";
            this.lbl_SelectToContinue.Visible = false;
            // 
            // lbl_ReadOnlyLabel
            // 
            this.lbl_ReadOnlyLabel.AutoSize = true;
            this.lbl_ReadOnlyLabel.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ReadOnlyLabel.ForeColor = System.Drawing.Color.Black;
            this.lbl_ReadOnlyLabel.Location = new System.Drawing.Point(89, 44);
            this.lbl_ReadOnlyLabel.Name = "lbl_ReadOnlyLabel";
            this.lbl_ReadOnlyLabel.Size = new System.Drawing.Size(58, 13);
            this.lbl_ReadOnlyLabel.TabIndex = 4;
            this.lbl_ReadOnlyLabel.Text = "(Read only)";
            this.lbl_ReadOnlyLabel.Visible = false;
            // 
            // txt_AtomValue
            // 
            this.txt_AtomValue.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AtomValue.Location = new System.Drawing.Point(16, 82);
            this.txt_AtomValue.Multiline = true;
            this.txt_AtomValue.Name = "txt_AtomValue";
            this.txt_AtomValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_AtomValue.Size = new System.Drawing.Size(401, 206);
            this.txt_AtomValue.TabIndex = 3;
            this.txt_AtomValue.Visible = false;
            this.txt_AtomValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_AtomValue_KeyDown);
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
            this.lbl_AtomValueLabel.Visible = false;
            // 
            // txt_AtomPath
            // 
            this.txt_AtomPath.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txt_AtomPath.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AtomPath.Location = new System.Drawing.Point(92, 16);
            this.txt_AtomPath.Name = "txt_AtomPath";
            this.txt_AtomPath.ReadOnly = true;
            this.txt_AtomPath.Size = new System.Drawing.Size(325, 26);
            this.txt_AtomPath.TabIndex = 1;
            this.txt_AtomPath.Visible = false;
            this.txt_AtomPath.WordWrap = false;
            // 
            // lbl_AtomPathLabel
            // 
            this.lbl_AtomPathLabel.AutoSize = true;
            this.lbl_AtomPathLabel.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AtomPathLabel.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbl_AtomPathLabel.Location = new System.Drawing.Point(13, 20);
            this.lbl_AtomPathLabel.Name = "lbl_AtomPathLabel";
            this.lbl_AtomPathLabel.Size = new System.Drawing.Size(73, 18);
            this.lbl_AtomPathLabel.TabIndex = 0;
            this.lbl_AtomPathLabel.Text = "Atom Path";
            this.lbl_AtomPathLabel.Visible = false;
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
            this.Load += new System.EventHandler(this.FRMConsole_Load);
            this.Resize += new System.EventHandler(this.FRMConsole_Resize);
            this.ConsoleSplitContainer.Panel1.ResumeLayout(false);
            this.ConsoleSplitContainer.Panel2.ResumeLayout(false);
            this.ConsoleSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConsoleSplitContainer)).EndInit();
            this.ConsoleSplitContainer.ResumeLayout(false);
            this.cms_ConfigTreeMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer ConsoleSplitContainer;
        private System.Windows.Forms.TreeView trv_AtomTree;
        private System.Windows.Forms.Label lbl_ReadOnlyLabel;
        private System.Windows.Forms.TextBox txt_AtomValue;
        private System.Windows.Forms.Label lbl_AtomValueLabel;
        private System.Windows.Forms.TextBox txt_AtomPath;
        private System.Windows.Forms.Label lbl_AtomPathLabel;
        private System.Windows.Forms.Label lbl_SelectToContinue;
        private System.Windows.Forms.ContextMenuStrip cms_ConfigTreeMenu;
        private System.Windows.Forms.ToolStripMenuItem cmi_ConfigDelete;
        private System.Windows.Forms.ToolStripMenuItem cmi_ConfigRefresh;
        private System.Windows.Forms.ToolStripMenuItem cmi_ConfigAddComponent;
        private System.Windows.Forms.ToolStripMenuItem cmi_ConfigAddAtom;
    }
}


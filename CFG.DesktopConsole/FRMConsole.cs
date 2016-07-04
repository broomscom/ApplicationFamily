using CFG.Docker;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFG.DesktopConsole
{
    public partial class FRMConsole : Form
    {
        #region State
        private IDocker DockerClient = new StandardConfigHubDocker();
        private bool InModified = false;
        #endregion

        #region Construction
        public FRMConsole()
        {
            // Initialize
            InitializeComponent();
        }
        private void FRMConsole_Load(object sender, EventArgs e)
        {
            // Shore up positions
            ShoreUpConsoleControls();

            // Toggle Edit View
            ToggleEditAtomView(false, string.Empty, string.Empty);

            // Setup DOCKER client
            string pingResponse = null;
            try
            {
                // Setup call using configuration
                DockerClient.Setup(ConfigurationManager.AppSettings["ServerWithPort"],
                    ConfigurationManager.AppSettings["ReadToken"],
                    ConfigurationManager.AppSettings["PublishToken"],
                    bool.Parse(ConfigurationManager.AppSettings["UseHTTPS"]));

                // Get ping response
                pingResponse = DockerClient.Ping();

                // Fail check
                if (pingResponse != "Pong")
                {
                    throw new Exception(pingResponse);
                }

                // Load tree
                RefreshConfigurationTree();
            }            
            catch (Exception err)
            {
                // Failure notification
                if (err.Message != "No components have been added yet")
                {
                    MessageBox.Show(err.Message, "Configuration Console - Issue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }
        #endregion

        #region Events
        private void FRMConsole_Resize(object sender, EventArgs e)
        {
            // Shore up positions
            ShoreUpConsoleControls();
        }
        private void ConsoleSplitContainer_Resize(object sender, EventArgs e)
        {
            // Shore up positions
            ShoreUpConsoleControls();
        }
        private void ConsoleSplitContainer_SizeChanged(object sender, EventArgs e)
        {
            // Shore up positions
            ShoreUpConsoleControls();
        }
        private void ConsoleSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            // Shore up positions
            ShoreUpConsoleControls();
        }
        private void trv_AtomTree_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            // Check modified
            if (InModified)
            {
                if (MessageBox.Show("The atom value has been modified.  Any modifications will be lost by switch to another atom.  Do you wish to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    ModifiedSwitch(false);
                }
            }
        }
        private void trv_AtomTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                // Show look values or clear
                if (e.Node.Tag.ToString().Contains("."))
                {
                    ToggleEditAtomView(true, e.Node.Tag.ToString(), DockerClient.ResolveAtomAsString(e.Node.Tag.ToString()));
                }
                else
                {
                    ToggleEditAtomView(false, string.Empty, string.Empty);
                }
            }
            catch
            {
                // Don't care
            }
        }
        private void trv_AtomTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Select node override
            trv_AtomTree.SelectedNode = e.Node;
        }
        private void txt_AtomValue_KeyDown(object sender, KeyEventArgs e)
        {
            // Ctrl + S publishes
            if (e.Control && e.KeyCode == Keys.S)
            {
                // Suppress default             
                e.SuppressKeyPress = true;

                // Save
                DockerClient.PublishConfigurationAtom(trv_AtomTree.SelectedNode.Tag.ToString(), txt_AtomValue.Text);

                // Remove modified
                ModifiedSwitch(false);
            }
            else
            {
                // Show modified
                ModifiedSwitch(true);
            }
        }
        private void cmi_ConfigDelete_Click(object sender, EventArgs e)
        {
            // Component or atom
            if (trv_AtomTree.SelectedNode != null && trv_AtomTree.SelectedNode.Tag != null)
            {
                if (trv_AtomTree.SelectedNode.Tag.ToString().Contains("."))
                {
                    try
                    {
                        // Try to delete
                        DockerClient.DeleteConfigurationAtom(trv_AtomTree.SelectedNode.Tag.ToString());
                        trv_AtomTree.Nodes.Remove(trv_AtomTree.SelectedNode);
                        ToggleEditAtomView(false, string.Empty, string.Empty);
                        ModifiedSwitch(false);
                        Application.DoEvents();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    try
                    {
                        // Try to delete
                        DockerClient.DeleteComponent(trv_AtomTree.SelectedNode.Tag.ToString());
                        trv_AtomTree.Nodes.Remove(trv_AtomTree.SelectedNode);
                        ToggleEditAtomView(false, string.Empty, string.Empty);
                        ModifiedSwitch(false);
                        Application.DoEvents();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void cmi_ConfigRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                // Refresh the tree
                RefreshConfigurationTree();
            }
            catch (Exception err)
            {
                // Failure notification
                if (err.Message != "No components have been added yet")
                {
                    MessageBox.Show(err.Message, "Configuration Console - Issue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }
        private void cmi_ConfigAddComponent_Click(object sender, EventArgs e)
        {
            // Build component
            FRMNameEntry entryForm = new FRMNameEntry("Enter Component Name");
            entryForm.ShowDialog();
            if (!entryForm.Canceled)
            {
                try
                {
                    // Register component
                    DockerClient.RegisterComponent(entryForm.EnteredName.Trim());
                    TreeNode node = new TreeNode();
                    node.Text = entryForm.EnteredName.Trim();
                    node.Tag = entryForm.EnteredName.Trim();
                    trv_AtomTree.Nodes.Add(node);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void cmi_ConfigAddAtom_Click(object sender, EventArgs e)
        {
            // Build atom
            FRMNameEntry entryForm = new FRMNameEntry("Enter Atom Name");
            entryForm.ShowDialog();
            if (!entryForm.Canceled)
            {
                try
                {
                    // Register atom
                    DockerClient.PublishConfigurationAtom(trv_AtomTree.SelectedNode.Tag.ToString() + "." + entryForm.EnteredName.Trim(), null);
                    TreeNode node = new TreeNode();
                    node.Text = entryForm.EnteredName.Trim();
                    node.Tag = trv_AtomTree.SelectedNode.Tag.ToString() + "." + entryForm.EnteredName.Trim();
                    trv_AtomTree.SelectedNode.Nodes.Add(node);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region Cores
        private void ShoreUpConsoleControls()
        {
            // Reposition controls
            txt_AtomPath.Width = this.Width - ConsoleSplitContainer.Panel1.Width - 130;
            txt_AtomValue.Width = this.Width - ConsoleSplitContainer.Panel1.Width - 55;
            txt_AtomValue.Height = this.Height - 140;

            // Do events clean
            Application.DoEvents();
        }
        private void RefreshConfigurationTree()
        {
            // Clear tree
            trv_AtomTree.Nodes.Clear();

            // Load component names
            List<string> componentNames = DockerClient.ListComponents();
            foreach (string name in componentNames)
            {
                // Build node
                TreeNode node = new TreeNode(name);
                node.Tag = name;

                // Add node
                trv_AtomTree.Nodes.Add(node);
            }

            // Load atoms
            foreach (TreeNode node in trv_AtomTree.Nodes)
            {
                RecurseFindSubAtoms(node);                
            }

            // Do events clean
            Application.DoEvents();
        }
        private void RecurseFindSubAtoms(TreeNode usingNode)
        {
            // Get these sub atoms and build nodes
            List<string> subAtoms = DockerClient.ListSubAtoms(usingNode.Tag.ToString());
            foreach (string subAtom in subAtoms)
            {
                // Build attach
                TreeNode attachNode = new TreeNode();
                attachNode.Text = subAtom;
                attachNode.Tag = usingNode.Tag.ToString() + "." + subAtom;

                // Attach
                usingNode.Nodes.Add(attachNode);

                // Go again
                try
                {
                    RecurseFindSubAtoms(attachNode);
                }
                catch
                {
                    // Don't care
                }
            }
        }
        private void ToggleEditAtomView(bool showValueModifyControls, string path, string value)
        {
            // Switch
            if (showValueModifyControls)
            {
                // Edit mode
                txt_AtomPath.Visible = true;
                txt_AtomValue.Visible = true;
                lbl_AtomPathLabel.Visible = true;
                lbl_AtomValueLabel.Visible = true;
                lbl_ReadOnlyLabel.Visible = true;
                lbl_SelectToContinue.Visible = false;
                txt_AtomPath.Text = path;
                txt_AtomValue.Text = value;
            }
            else
            {
                // No edit mode
                txt_AtomPath.Visible = false;
                txt_AtomValue.Visible = false;
                lbl_AtomPathLabel.Visible = false;
                lbl_AtomValueLabel.Visible = false;
                lbl_ReadOnlyLabel.Visible = false;                
                lbl_SelectToContinue.Visible = true;
            }

            // Do events clean
            Application.DoEvents();
        }
        private void ModifiedSwitch(bool showModified)
        {
            // Switch
            if (showModified)
            {
                lbl_AtomValueLabel.Text = "Atom Value (Modified)";
                InModified = true;
            }
            else
            {
                lbl_AtomValueLabel.Text = "Atom Value";
                InModified = false;
            }

            // Do events clean
            Application.DoEvents();
        }
        #endregion        
    }
}

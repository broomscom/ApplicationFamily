using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFG.DesktopConsole
{
    public partial class FRMNameEntry : Form
    {
        #region State
        public bool Canceled = true;
        public string EnteredName = string.Empty;
        private string BannerText = string.Empty;
        #endregion

        #region Construction
        public FRMNameEntry(string bannerText)
        {
            // Initialize
            InitializeComponent();
            BannerText = bannerText;
        }
        private void FRMNameEntry_Load(object sender, EventArgs e)
        {
            // Set banner
            this.Text = BannerText;
        }
        #endregion

        #region Events
        private void cmd_Cancel_Click(object sender, EventArgs e)
        {
            // Close
            this.Close();
        }

        private void cmd_Build_Click(object sender, EventArgs e)
        {
            // Ready and close
            if (txt_AtomPath.Text.Trim() == string.Empty)
            {
                // No value issue
                MessageBox.Show("The name cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Set and close
                EnteredName = txt_AtomPath.Text;
                Canceled = false;
                this.Close();
            }
        }

        private void txt_AtomPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Not dots
            if (e.KeyChar == '.')
            {
                e.Handled = true;
            }
        }
        #endregion
    }
}

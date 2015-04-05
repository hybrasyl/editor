using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HybrasylEditor.UI
{
    public partial class OptionsForm : Form
    {
        private Configuration _configuration;
        public Configuration Configuration
        {
            get
            {
                return _configuration;
            }
            set
            {
                _configuration = value.Clone(); // allows us to Cancel without saving changes
                
                BindData();
            }
        }

        public OptionsForm()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void BindData()
        {
            txtDarkagesInstallPath.Text = Configuration.DarkagesInstallDirectory;
            txtHybrasylWorldDirectory.Text = Configuration.HybrasylWorldDirectory;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Configuration.Save();
            Configuration.Current = Configuration;

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void btnSelectDarkagesInstallDirectory_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (txtDarkagesInstallPath.Text.Length > 0) dialog.SelectedPath = txtDarkagesInstallPath.Text;

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Configuration.DarkagesInstallDirectory = dialog.SelectedPath;
                txtDarkagesInstallPath.Text = dialog.SelectedPath;
            }
        }

        private void btnSelectHybrasylWorldDirectory_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (txtHybrasylWorldDirectory.Text.Length > 0) dialog.SelectedPath = txtHybrasylWorldDirectory.Text;

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Configuration.HybrasylWorldDirectory = dialog.SelectedPath;
                txtHybrasylWorldDirectory.Text = dialog.SelectedPath;
            }
        }
    }
}

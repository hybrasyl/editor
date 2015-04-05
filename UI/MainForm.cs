/*
 * This file is part of Project Hybrasyl.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the Affero General Public License as published by
 * the Free Software Foundation, version 3.
 *
 * This program is distributed in the hope that it will be useful, but
 * without ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE. See the Affero General Public License
 * for more details.
 *
 * You should have received a copy of the Affero General Public License along
 * with this program. If not, see <http://www.gnu.org/licenses/>.
 *
 * (C) 2015 Kyle Speck (kojasou@hybrasyl.com)
 *
 * Authors:   Kyle Speck    <kojasou@hybrasyl.com>
 *
 */

using System;
using System.Windows.Forms;

namespace HybrasylEditor.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void newItem_Click(object sender, EventArgs e)
        {
            var form = new NewItemForm();
            form.MdiParent = this;
            form.Show();
        }
        private void newMap_Click(object sender, EventArgs e)
        {
            if (!CheckRequiredOptionsForMapEditor())
                return;

            var form = new NewMapForm();
            form.MdiParent = this;
            form.Show();
        }
        private void newMonster_Click(object sender, EventArgs e)
        {

        }
        private void newNpc_Click(object sender, EventArgs e)
        {

        }
        private void newSkillSpell_Click(object sender, EventArgs e)
        {
            var form = new SkillForm();
            form.MdiParent = this;
            form.Show();
        }

        private void openItem_Click(object sender, EventArgs e)
        {

        }
        private void openMap_Click(object sender, EventArgs e)
        {
            if (!CheckRequiredOptionsForMapEditor())
                return;

            var form = new OpenMapForm();
            form.MdiParent = this;
            form.Show();
        }
        private void openMonster_Click(object sender, EventArgs e)
        {

        }
        private void openNpc_Click(object sender, EventArgs e)
        {

        }
        private void openSkillSpell_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void itemViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void skillViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new SkillIconForm(0, "skill");
            dialog.ShowDialog();
            int icon = dialog.SelectedIcon;
        }

        private void spellViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new SkillIconForm(0, "spell");
            dialog.ShowDialog();
            int icon = dialog.SelectedIcon;
        }

        private bool CheckRequiredOptionsForMapEditor()
        {
            // returns true/false to indicate if required options exist

            if (string.IsNullOrEmpty(Configuration.Current.DarkagesInstallDirectory) || 
                !System.IO.Directory.Exists(Configuration.Current.DarkagesInstallDirectory))
            {
                MessageBox.Show("Please select your Darkages Install Directory in: Tools > Options");
                return false;
            }

            if (string.IsNullOrEmpty(Configuration.Current.HybrasylWorldDirectory) ||
                !System.IO.Directory.Exists(Configuration.Current.HybrasylWorldDirectory))
            {
                MessageBox.Show("Please select your Hybrasyl World Directory in: Tools > Options");
                return false;
            }

            return true;
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OptionsForm() { Configuration = Configuration.Current };
            dialog.ShowDialog(this);
        }
    }
}

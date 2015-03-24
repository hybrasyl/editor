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

using Capricorn.Drawing;
using Capricorn.IO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HybrasylEditor.UI
{
    public partial class SignForm : Form
    {
        static Bitmap woodbk;

        private string signText;

        public string SignText
        {
            get { return SignText; }
        }

        static SignForm()
        {
            var epf = EPFImage.FromArchive("woodbk.epf", DATArchive.Legend);
            var pal = Palette256.FromArchive("legend.pal", DATArchive.Legend);
            woodbk = DAGraphics.RenderImage(epf[0], pal);
        }
        public SignForm(string text)
        {
            InitializeComponent();
            signPanel.BackgroundImage = woodbk;
            signTextBox.Text = text;
        }

        private void signTextBox_TextChanged(object sender, EventArgs e)
        {
            signText = signTextBox.Text;
            signTextLabel.Text = signTextBox.Text;
        }
    }
}

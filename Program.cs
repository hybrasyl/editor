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

using HybrasylEditor.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace HybrasylEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        public static string CreatePath(Environment.SpecialFolder folder, string format, params object[] args)
        {
            string baseDirectory = Environment.GetFolderPath(folder) + @"\Hybrasyl\Editor";
            string[] parts = new string[] { baseDirectory, string.Format(format, args) };
            string full = string.Join(@"\", parts);
            Directory.CreateDirectory(Path.GetDirectoryName(full));
            return full;
        }
    }
}

using Capricorn.Drawing;
using Capricorn.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HybrasylEditor.UI
{
    public partial class SkillForm : Form
    {
        private string _fileName;
        private Skill _skill;
        private bool _modified;

        public string FileName
        {
            get { return _fileName; }
            private set
            {
                _fileName = value;
                string shortFileName = Path.GetFileName(value);
                Text = string.Format("{0} - Skill Editor", shortFileName);
                saveToolStripMenuItem.Text = string.Format("Save {0}", shortFileName);
                saveAsToolStripMenuItem.Text = string.Format("Save {0} As...", shortFileName);
            }
        }

        public SkillForm()
        {
            _skill = new Skill();

            InitializeComponent();

            txtName.DataBindings.Add("Text", _skill, "Name", false, DataSourceUpdateMode.OnPropertyChanged);

            var types = new Dictionary<string, string>()
            {
                { "skill", "Skill" },
                { "spell", "Spell" },
                { "assail", "Assail" },
            };
            cmbType.DataSource = new BindingSource(types, null);
            cmbType.DisplayMember = "Value";
            cmbType.ValueMember = "Key";
            cmbType.DataBindings.Add("SelectedValue", _skill, "Type", false, DataSourceUpdateMode.OnPropertyChanged);

            var books = new Dictionary<string, string>()
            {
                { "primary_skills", "Primary Skills" },
                { "primary_spells", "Primary Spells" },
                { "secondary_skills", "Secondary Skills" },
                { "secondary_spells", "Secondary Spells" },
                { "utility_skills", "Utility Skills" },
                { "utility_spells", "Utility Spells" },
            };
            cmbBook.DataSource = new BindingSource(books, null);
            cmbBook.DisplayMember = "Value";
            cmbBook.ValueMember = "Key";
            cmbBook.DataBindings.Add("SelectedValue", _skill, "BookType", false, DataSourceUpdateMode.OnPropertyChanged);

            txtStatusAdd.DataBindings.Add("Text", _skill.Status, "Add", false, DataSourceUpdateMode.OnPropertyChanged);
            txtStatusRemove.DataBindings.Add("Text", _skill.Status, "Remove", false, DataSourceUpdateMode.OnPropertyChanged);

            _modified = false;
        }
        public SkillForm(string filename)
            : this()
        {
            FileName = filename;

            _modified = false;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FileName))
            {
                saveAsToolStripMenuItem_Click(sender, e);
                return;
            }
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            if (string.IsNullOrWhiteSpace(_fileName))
            {
                dialog.FileName = Regex.Replace(_skill.Name, "[^a-zA-Z0-9]", string.Empty);
            }
            else
            {
                dialog.InitialDirectory = Path.GetDirectoryName(_fileName);
                dialog.FileName = Path.GetFileName(_fileName);
            }
            dialog.Filter = "YAML document|*.yml";
            dialog.ShowDialog();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            _modified = true;
        }
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _modified = true;
        }
        private void imgIcon_Click(object sender, EventArgs e)
        {
            SkillIconForm dialog;
            switch (cmbBook.SelectedIndex)
            {
                case 0:
                case 2:
                case 4:
                    dialog = new SkillIconForm(0, "skill");
                    break;
                default:
                    dialog = new SkillIconForm(0, "spell");
                    break;
            }
            dialog.ShowDialog();
            if (dialog.SelectedIcon != _skill.Icon)
            {
                _modified = true;
                _skill.Icon = dialog.SelectedIcon;
                UpdateIcon();
            }
        }
        private void cmbBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            _modified = true;
            UpdateIcon();
        }

        private void SkillForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_modified)
            {
                switch (MessageBox.Show("Do you want to save changes?", "Skill Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        // TODO: save
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void UpdateIcon()
        {
            string epfFilename;
            switch (cmbBook.SelectedIndex)
            {
                case 0:
                case 2:
                case 4:
                    epfFilename = "skill001.epf";
                    break;
                default:
                    epfFilename = "spell001.epf";
                    break;
            }
            var _epfImage = EPFImage.FromArchive(epfFilename, DATArchive.Setoa);
            var _palette = Palette256.FromArchive("gui06.pal", DATArchive.Setoa);
            var bitmap = DAGraphics.RenderImage(_epfImage[_skill.Icon], _palette);
            imgIcon.Image = bitmap;
        }

        private void SkillForm_Load(object sender, EventArgs e)
        {

        }
    }
}

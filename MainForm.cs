using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAMS
{
    /// <summary>
    /// Main form for the application.
    /// </summary>
    public partial class MainForm : Form
    {
        private Animal currentAnimal;
        private CheckBox chkListAll;
        private ComboBox cmbCategory;
        private ComboBox cmbSpecies;
        private ListBox lstAllAnimals;
        private Button btnCreate;
        private TextBox txtOutput;
        private Button btnAbout;

        public MainForm()
        {
            InitializeComponent();
            this.Text = "EcoPark Animal Management System";
            this.Size = new Size(600, 400);

            chkListAll = new CheckBox { Text = "List all animals", Location = new Point(10, 10) };
            chkListAll.CheckedChanged += ChkListAll_CheckedChanged;
            Controls.Add(chkListAll);

            cmbCategory = new ComboBox { Location = new Point(10, 40), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbCategory.Items.Add("Mammal");
            cmbCategory.Items.Add("Reptile");
            cmbCategory.SelectedIndexChanged += CmbCategory_SelectedIndexChanged;
            Controls.Add(cmbCategory);

            cmbSpecies = new ComboBox { Location = new Point(200, 40), DropDownStyle = ComboBoxStyle.DropDownList };
            Controls.Add(cmbSpecies);

            lstAllAnimals = new ListBox { Location = new Point(10, 40), Size = new Size(300, 20), Visible = false };
            lstAllAnimals.Items.Add("Dog");
            lstAllAnimals.Items.Add("Cat");
            lstAllAnimals.Items.Add("Turtle");
            lstAllAnimals.Items.Add("Lizard");
            lstAllAnimals.SelectedIndexChanged += LstAllAnimals_SelectedIndexChanged;
            Controls.Add(lstAllAnimals);

            btnCreate = new Button { Text = "Create Animal", Location = new Point(10, 70) };
            btnCreate.Click += BtnCreate_Click;
            Controls.Add(btnCreate);

            txtOutput = new TextBox { Multiline = true, ReadOnly = true, Location = new Point(10, 100), Size = new Size(560, 180) };
            Controls.Add(txtOutput);

            btnAbout = new Button { Text = "About", Location = new Point(10, 300) };
            btnAbout.Click += BtnAbout_Click;
            Controls.Add(btnAbout);

        }

        private void ChkListAll_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = chkListAll.Checked;
            cmbCategory.Enabled = !isChecked;
            cmbSpecies.Enabled = !isChecked;
            lstAllAnimals.Visible = isChecked;
            if (isChecked)
            {
                cmbCategory.SelectedIndex = -1;
                cmbSpecies.Items.Clear();
                cmbSpecies.Text = string.Empty;
            }
            else
            {
                lstAllAnimals.SelectedIndex = -1;
            }
        }

        private void CmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSpecies.Items.Clear();
            string cat = cmbCategory.SelectedItem?.ToString();
            if (cat == "Mammal")
            {
                foreach (MammalSpecies sp in Enum.GetValues(typeof(MammalSpecies)))
                {
                    cmbSpecies.Items.Add(sp);
                }
            }
            else if (cat == "Reptile")
            {
                foreach (ReptileSpecies sp in Enum.GetValues(typeof(ReptileSpecies)))
                {
                    cmbSpecies.Items.Add(sp);
                }
            }
        }

        private void LstAllAnimals_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAllAnimals.SelectedItem != null)
            {
                string sp = lstAllAnimals.SelectedItem.ToString();
                string cat = GetCategoryFromSpecies(sp);
                cmbCategory.Text = cat;
                cmbSpecies.Text = sp;
            }
        }

        private string GetCategoryFromSpecies(string species)
        {
            if (species == "Dog" || species == "Cat") return "Mammal";
            return "Reptile";
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            string category, speciesStr;
            if (!chkListAll.Checked)
            {
                category = cmbCategory.Text;
                speciesStr = cmbSpecies.Text;
            }
            else
            {
                speciesStr = lstAllAnimals.SelectedItem?.ToString();
                if (speciesStr == null) return;
                category = GetCategoryFromSpecies(speciesStr);
            }

            if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(speciesStr))
            {
                MessageBox.Show("Please select a category and species.");
                return;
            }

            Form form = null;
            if (category == "Mammal")
            {
                MammalSpecies sp;
                if (Enum.TryParse(speciesStr, out sp))
                {
                    form = new MammalForm(sp);
                }
            }
            else if (category == "Reptile")
            {
                ReptileSpecies sp;
                if (Enum.TryParse(speciesStr, out sp))
                {
                    form = new ReptileForm(sp);
                }
            }

            if (form != null && form.ShowDialog() == DialogResult.OK)
            {
                if (category == "Mammal")
                {
                    currentAnimal = ((MammalForm)form).Animal;
                }
                else
                {
                    currentAnimal = ((ReptileForm)form).Animal;
                }
                txtOutput.Text = currentAnimal.ToString();
            }
        }

        private void BtnAbout_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.ShowDialog();
        }
    }
}
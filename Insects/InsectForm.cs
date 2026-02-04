using EAMS.Insects.Species;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAMS.Insects
{
    public partial class InsectForm : Form
    {
        private InsectSpecies _species;
        public Animal Animal { get; private set; }

        // Common + category controls 
        private TextBox txtName;
        private NumericUpDown numAge;
        private NumericUpDown numWeight;
        private ComboBox cmbGender;
        private TextBox txtImagePath;
        private Button btnLoadImage;
        private PictureBox picImage;
        private NumericUpDown numWings;
        private NumericUpDown numAntennaLength;

        // Species-specific
        private TextBox txtWingPattern;       
        private CheckBox chkCanSting;         
        private NumericUpDown numHoneyProd;   
        private CheckBox chkIsWorker;         

        private Button btnOK;
        private Button btnCancel;

        public InsectForm(InsectSpecies species)
        {
            _species = species;
            this.Text = $"Enter {_species} Data";
            this.Size = new Size(400, 500);

            // General group 
            GroupBox grpGeneral = new GroupBox { Text = "General Data", Location = new Point(10, 10), Size = new Size(360, 150) };
            Controls.Add(grpGeneral);

            Label lblName = new Label { Text = "Name:", Location = new Point(10, 20) };
            txtName = new TextBox { Location = new Point(100, 20) };
            grpGeneral.Controls.Add(lblName); grpGeneral.Controls.Add(txtName);

            Label lblAge = new Label { Text = "Age:", Location = new Point(10, 50) };
            numAge = new NumericUpDown { Location = new Point(100, 50), Maximum = 100 };
            grpGeneral.Controls.Add(lblAge); grpGeneral.Controls.Add(numAge);

            Label lblWeight = new Label { Text = "Weight:", Location = new Point(10, 80) };
            numWeight = new NumericUpDown { Location = new Point(100, 80), DecimalPlaces = 2, Maximum = 1000 };
            grpGeneral.Controls.Add(lblWeight); grpGeneral.Controls.Add(numWeight);

            Label lblGender = new Label { Text = "Gender:", Location = new Point(10, 110) };
            cmbGender = new ComboBox { Location = new Point(100, 110), DropDownStyle = ComboBoxStyle.DropDownList };
            foreach (GenderType g in Enum.GetValues(typeof(GenderType))) cmbGender.Items.Add(g);
            cmbGender.SelectedIndex = 2;
            grpGeneral.Controls.Add(lblGender); grpGeneral.Controls.Add(cmbGender);

            // Image section
            Label lblImage = new Label { Text = "Image Path:", Location = new Point(10, 170) };
            txtImagePath = new TextBox { Location = new Point(100, 170), Width = 200 };
            btnLoadImage = new Button { Text = "Load", Location = new Point(310, 170) };
            btnLoadImage.Click += BtnLoadImage_Click;
            picImage = new PictureBox { Location = new Point(10, 200), Size = new Size(100, 100), BorderStyle = BorderStyle.FixedSingle };
            Controls.Add(lblImage); Controls.Add(txtImagePath); Controls.Add(btnLoadImage); Controls.Add(picImage);

            // Category group
            GroupBox grpCategory = new GroupBox { Text = "Insect Data", Location = new Point(10, 310), Size = new Size(360, 80) };
            Controls.Add(grpCategory);

            Label lblWings = new Label { Text = "Number of Wings:", Location = new Point(10, 20) };
            numWings = new NumericUpDown { Location = new Point(120, 20), Maximum = 4 };
            grpCategory.Controls.Add(lblWings); grpCategory.Controls.Add(numWings);

            Label lblAntenna = new Label { Text = "Antenna Length (mm):", Location = new Point(10, 50) };
            numAntennaLength = new NumericUpDown { Location = new Point(150, 50), DecimalPlaces = 1, Maximum = 50 };
            grpCategory.Controls.Add(lblAntenna); grpCategory.Controls.Add(numAntennaLength);

            // Species group
            GroupBox grpSpecies = new GroupBox { Text = $"{_species} Data", Location = new Point(10, 400), Size = new Size(360, 60) };
            Controls.Add(grpSpecies);

            switch (_species)
            {
                case InsectSpecies.Butterfly:
                    Label lblPattern = new Label { Text = "Wing Pattern:", Location = new Point(10, 20) };
                    txtWingPattern = new TextBox { Location = new Point(100, 20), Width = 200 };
                    grpSpecies.Controls.Add(lblPattern); grpSpecies.Controls.Add(txtWingPattern);
                    break;
                case InsectSpecies.Bee:
                    Label lblSting = new Label { Text = "Can Sting:", Location = new Point(10, 20) };
                    chkCanSting = new CheckBox { Location = new Point(100, 20) };
                    chkCanSting.Checked = true;
                    grpSpecies.Controls.Add(lblSting); grpSpecies.Controls.Add(chkCanSting);

                    // Honey prod as extra (could be in separate line if form bigger)
                    Label lblHoney = new Label { Text = "Honey (g/day):", Location = new Point(10, 40) };
                    numHoneyProd = new NumericUpDown { Location = new Point(100, 40), DecimalPlaces = 2, Maximum = 10 };
                    grpSpecies.Controls.Add(lblHoney); grpSpecies.Controls.Add(numHoneyProd);
                    break;
                case InsectSpecies.Ant:
                    Label lblWorker = new Label { Text = "Is Worker:", Location = new Point(10, 20) };
                    chkIsWorker = new CheckBox { Location = new Point(100, 20) };
                    chkIsWorker.Checked = true;
                    grpSpecies.Controls.Add(lblWorker); grpSpecies.Controls.Add(chkIsWorker);
                    break;
            }

            btnOK = new Button { Text = "OK", Location = new Point(150, 470), DialogResult = DialogResult.OK };
            btnOK.Click += BtnOK_Click;
            Controls.Add(btnOK);

            btnCancel = new Button { Text = "Cancel", Location = new Point(250, 470), DialogResult = DialogResult.Cancel };
            Controls.Add(btnCancel);

            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }

        private void BtnLoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtImagePath.Text = ofd.FileName;
                    try { picImage.Image = Image.FromFile(ofd.FileName); }
                    catch { MessageBox.Show("Invalid image file."); }
                }
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || cmbGender.SelectedItem == null)
            {
                MessageBox.Show("Please fill required fields (Name & Gender).");
                this.DialogResult = DialogResult.None;
                return;
            }

            Insect insect = InsectFactory.CreateInsect(_species, (int)numWings.Value, (double)numAntennaLength.Value);

            insect.Name = txtName.Text;
            insect.Age = (int)numAge.Value;
            insect.Weight = (double)numWeight.Value;
            insect.Gender = (GenderType)cmbGender.SelectedItem;
            insect.ImagePath = txtImagePath.Text;

            switch (_species)
            {
                case InsectSpecies.Butterfly:
                    if (string.IsNullOrWhiteSpace(txtWingPattern.Text))
                    {
                        MessageBox.Show("Please enter wing pattern for Butterfly.");
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                    ((Butterfly)insect).WingPattern = txtWingPattern.Text;
                    break;
                case InsectSpecies.Bee:
                    ((Bee)insect).CanSting = chkCanSting.Checked;
                    ((Bee)insect).HoneyProductionPerDay = (double)numHoneyProd.Value;
                    break;
                case InsectSpecies.Ant:
                    ((Ant)insect).IsWorker = chkIsWorker.Checked;
                    break;
            }

            Animal = insect;
        }
    }
}

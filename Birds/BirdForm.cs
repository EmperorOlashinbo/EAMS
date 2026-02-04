using EAMS.Birds.species;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAMS.Birds
{
    public partial class BirdForm : Form
    {
        private BirdSpecies _species;
        public Animal Animal { get; private set; }

        // Common controls (same as Mammal/Reptile)
        private TextBox txtName;
        private NumericUpDown numAge;
        private NumericUpDown numWeight;
        private ComboBox cmbGender;
        private TextBox txtImagePath;
        private Button btnLoadImage;
        private PictureBox picImage;

        // Category controls
        private NumericUpDown numWingspan;
        private NumericUpDown numTailLength;

        // Species-specific
        private CheckBox chkIsBald;             
        private TextBox txtFeatherColor;

        private Button btnOK;
        private Button btnCancel;

        public BirdForm(BirdSpecies species)
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

            // Image
            Label lblImage = new Label { Text = "Image Path:", Location = new Point(10, 170) };
            txtImagePath = new TextBox { Location = new Point(100, 170), Width = 200 };
            btnLoadImage = new Button { Text = "Load", Location = new Point(310, 170) };
            btnLoadImage.Click += BtnLoadImage_Click;
            picImage = new PictureBox { Location = new Point(10, 200), Size = new Size(100, 100), BorderStyle = BorderStyle.FixedSingle };
            Controls.Add(lblImage); Controls.Add(txtImagePath); Controls.Add(btnLoadImage); Controls.Add(picImage);

            // Category group
            GroupBox grpCategory = new GroupBox { Text = "Bird Data", Location = new Point(10, 310), Size = new Size(360, 80) };
            Controls.Add(grpCategory);

            Label lblWingspan = new Label { Text = "Wingspan (cm):", Location = new Point(10, 20) };
            numWingspan = new NumericUpDown { Location = new Point(100, 20), DecimalPlaces = 1, Maximum = 300 };
            grpCategory.Controls.Add(lblWingspan); grpCategory.Controls.Add(numWingspan);

            Label lblTail = new Label { Text = "Tail Length (cm):", Location = new Point(10, 50) };
            numTailLength = new NumericUpDown { Location = new Point(100, 50), DecimalPlaces = 1, Maximum = 100 };
            grpCategory.Controls.Add(lblTail); grpCategory.Controls.Add(numTailLength);

            // Species group 
            GroupBox grpSpecies = new GroupBox { Text = $"{_species} Data", Location = new Point(10, 400), Size = new Size(360, 60) };
            Controls.Add(grpSpecies);

            if (_species == BirdSpecies.Eagle)
            {
                Label lblBald = new Label { Text = "Is Bald Eagle:", Location = new Point(10, 20) };
                chkIsBald = new CheckBox { Location = new Point(100, 20) };
                grpSpecies.Controls.Add(lblBald); grpSpecies.Controls.Add(chkIsBald);
            }
            else if (_species == BirdSpecies.Dove)
            {
                Label lblColor = new Label { Text = "Feather Color:", Location = new Point(10, 20) };
                txtFeatherColor = new TextBox { Location = new Point(100, 20), Width = 200 };
                grpSpecies.Controls.Add(lblColor); grpSpecies.Controls.Add(txtFeatherColor);
            }

            // Buttons
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

            Bird bird = BirdFactory.CreateBird(_species, (double)numWingspan.Value, (double)numTailLength.Value);

            bird.Name = txtName.Text;
            bird.Age = (int)numAge.Value;
            bird.Weight = (double)numWeight.Value;
            bird.Gender = (GenderType)cmbGender.SelectedItem;
            bird.ImagePath = txtImagePath.Text;

            if (_species == BirdSpecies.Eagle)
            {
                ((Eagle)bird).IsBald = chkIsBald.Checked;
            }
            else if (_species == BirdSpecies.Dove)
            {
                if (string.IsNullOrWhiteSpace(txtFeatherColor.Text))
                {
                    MessageBox.Show("Please enter feather color for Dove.");
                    this.DialogResult = DialogResult.None;
                    return;
                }
                ((Dove)bird).FeatherColor = txtFeatherColor.Text;
            }

            Animal = bird;
        }
    }
}

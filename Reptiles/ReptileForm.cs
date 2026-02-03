using EAMS.Reptiles.Species;
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
    /// Form for entering reptile data.
    /// </summary>
    public partial class ReptileForm : Form
    {
        private ReptileSpecies _species;
        public Animal Animal { get; private set; }

        // Common controls
        private TextBox txtName;
        private NumericUpDown numAge;
        private NumericUpDown numWeight;
        private ComboBox cmbGender;
        private TextBox txtImagePath;
        private Button btnLoadImage;
        private PictureBox picImage;
        private NumericUpDown numBodyLength;
        private CheckBox chkLivesInWater;
        private NumericUpDown numAggressiveness;

        // Species-specific controls
        private NumericUpDown numSpeciesNumeric1;
        private NumericUpDown numSpeciesNumeric2;
        private TextBox txtSpeciesText;
        private CheckBox chkSpeciesBool;

        private Button btnOK;
        private Button btnCancel;

        public ReptileForm(ReptileSpecies species)
        {
            _species = species;
            this.Text = $"Enter {_species} Data";
            this.Size = new Size(400, 500);

            // General group
            GroupBox grpGeneral = new GroupBox { Text = "General Data", Location = new Point(10, 10), Size = new Size(360, 150) };
            Controls.Add(grpGeneral);

            Label lblName = new Label { Text = "Name:", Location = new Point(10, 20) };
            txtName = new TextBox { Location = new Point(100, 20) };
            grpGeneral.Controls.Add(lblName);
            grpGeneral.Controls.Add(txtName);

            Label lblAge = new Label { Text = "Age:", Location = new Point(10, 50) };
            numAge = new NumericUpDown { Location = new Point(100, 50), Maximum = 100 };
            grpGeneral.Controls.Add(lblAge);
            grpGeneral.Controls.Add(numAge);

            Label lblWeight = new Label { Text = "Weight:", Location = new Point(10, 80) };
            numWeight = new NumericUpDown { Location = new Point(100, 80), DecimalPlaces = 2, Maximum = 1000 };
            grpGeneral.Controls.Add(lblWeight);
            grpGeneral.Controls.Add(numWeight);

            Label lblGender = new Label { Text = "Gender:", Location = new Point(10, 110) };
            cmbGender = new ComboBox { Location = new Point(100, 110), DropDownStyle = ComboBoxStyle.DropDownList };
            foreach (GenderType g in Enum.GetValues(typeof(GenderType)))
            {
                cmbGender.Items.Add(g);
            }
            cmbGender.SelectedIndex = 2; // Unknown
            grpGeneral.Controls.Add(lblGender);
            grpGeneral.Controls.Add(cmbGender);

            // Image
            Label lblImage = new Label { Text = "Image Path:", Location = new Point(10, 170) };
            txtImagePath = new TextBox { Location = new Point(100, 170), Width = 200 };
            btnLoadImage = new Button { Text = "Load", Location = new Point(310, 170) };
            btnLoadImage.Click += BtnLoadImage_Click;
            picImage = new PictureBox { Location = new Point(10, 200), Size = new Size(100, 100), BorderStyle = BorderStyle.FixedSingle };
            Controls.Add(lblImage);
            Controls.Add(txtImagePath);
            Controls.Add(btnLoadImage);
            Controls.Add(picImage);

            // Category group
            GroupBox grpCategory = new GroupBox { Text = "Reptile Data", Location = new Point(10, 310), Size = new Size(360, 80) };
            Controls.Add(grpCategory);

            Label lblBodyLength = new Label { Text = "Body Length:", Location = new Point(10, 20) };
            numBodyLength = new NumericUpDown { Location = new Point(100, 20), DecimalPlaces = 2, Maximum = 500 };
            grpCategory.Controls.Add(lblBodyLength);
            grpCategory.Controls.Add(numBodyLength);

            Label lblWater = new Label { Text = "Lives in Water:", Location = new Point(200, 20) };
            chkLivesInWater = new CheckBox { Location = new Point(300, 20) };
            grpCategory.Controls.Add(lblWater);
            grpCategory.Controls.Add(chkLivesInWater);

            Label lblAgg = new Label { Text = "Aggressiveness (0-10):", Location = new Point(10, 50) };
            numAggressiveness = new NumericUpDown { Location = new Point(150, 50), Maximum = 10 };
            grpCategory.Controls.Add(lblAgg);
            grpCategory.Controls.Add(numAggressiveness);

            // Species group
            GroupBox grpSpecies = new GroupBox { Text = $"{_species} Data", Location = new Point(10, 400), Size = new Size(360, 80) };
            Controls.Add(grpSpecies);

            switch (_species)
            {
                case ReptileSpecies.Turtle:
                    Label lblHardness = new Label { Text = "Shell Hardness:", Location = new Point(10, 20) };
                    numSpeciesNumeric1 = new NumericUpDown { Location = new Point(100, 20), Maximum = 10 };
                    grpSpecies.Controls.Add(lblHardness);
                    grpSpecies.Controls.Add(numSpeciesNumeric1);

                    Label lblWidth = new Label { Text = "Shell Width:", Location = new Point(10, 50) };
                    numSpeciesNumeric2 = new NumericUpDown { Location = new Point(100, 50), DecimalPlaces = 2, Maximum = 100 };
                    grpSpecies.Controls.Add(lblWidth);
                    grpSpecies.Controls.Add(numSpeciesNumeric2);
                    break;
                case ReptileSpecies.Lizard:
                    Label lblColor = new Label { Text = "Color:", Location = new Point(10, 20) };
                    txtSpeciesText = new TextBox { Location = new Point(100, 20) };
                    grpSpecies.Controls.Add(lblColor);
                    grpSpecies.Controls.Add(txtSpeciesText);

                    Label lblClimb = new Label { Text = "Can Climb:", Location = new Point(10, 50) };
                    chkSpeciesBool = new CheckBox { Location = new Point(100, 50) };
                    grpSpecies.Controls.Add(lblClimb);
                    grpSpecies.Controls.Add(chkSpeciesBool);
                    break;
                case ReptileSpecies.Snake:
                    Label lblLength = new Label { Text = "Length:", Location = new Point(10, 20) };
                    numSpeciesNumeric1 = new NumericUpDown { Location = new Point(100, 20), DecimalPlaces = 2, Maximum = 500 };
                    grpSpecies.Controls.Add(lblLength);
                    grpSpecies.Controls.Add(numSpeciesNumeric1);

                    Label lblVenomous = new Label { Text = "Is Venomous:", Location = new Point(10, 50) };
                    chkSpeciesBool = new CheckBox { Location = new Point(100, 50) };
                    grpSpecies.Controls.Add(lblVenomous);
                    grpSpecies.Controls.Add(chkSpeciesBool);
                    break;
            }

            btnOK = new Button { Text = "OK", Location = new Point(150, 490), DialogResult = DialogResult.OK };
            btnOK.Click += BtnOK_Click;
            Controls.Add(btnOK);

            btnCancel = new Button { Text = "Cancel", Location = new Point(250, 490), DialogResult = DialogResult.Cancel };
            Controls.Add(btnCancel);

            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }

        private void BtnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtImagePath.Text = ofd.FileName;
                try
                {
                    picImage.Image = Image.FromFile(ofd.FileName);
                }
                catch
                {
                    MessageBox.Show("Invalid image file.");
                }
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || cmbGender.SelectedItem == null)
            {
                MessageBox.Show("Please fill required fields.");
                this.DialogResult = DialogResult.None;
                return;
            }

            Reptile reptile = ReptileFactory.CreateReptile(_species, (double)numBodyLength.Value, chkLivesInWater.Checked, (int)numAggressiveness.Value);
            reptile.Name = txtName.Text;
            reptile.Age = (int)numAge.Value;
            reptile.Weight = (double)numWeight.Value;
            reptile.Gender = (GenderType)cmbGender.SelectedItem;
            reptile.ImagePath = txtImagePath.Text;

            switch (_species)
            {
                case ReptileSpecies.Turtle:
                    ((Turtle)reptile).ShellHardness = (int)numSpeciesNumeric1.Value;
                    ((Turtle)reptile).ShellWidth = (double)numSpeciesNumeric2.Value;
                    break;
                case ReptileSpecies.Lizard:
                    if (string.IsNullOrEmpty(txtSpeciesText.Text))
                    {
                        MessageBox.Show("Please enter color.");
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                    ((Lizard)reptile).Color = txtSpeciesText.Text;
                    ((Lizard)reptile).CanClimb = chkSpeciesBool.Checked;
                    break;
                case ReptileSpecies.Snake:
                    ((Snake)reptile).Length = (double)numSpeciesNumeric1.Value;
                    ((Snake)reptile).IsVenomous = chkSpeciesBool.Checked;
                    break;
            }

            Animal = reptile;
        }
    }
}

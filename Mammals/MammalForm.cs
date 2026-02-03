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
    /// Form for entering mammal data.
    /// </summary>
    public partial class MammalForm : Form
    {
        private MammalSpecies _species;
        public Animal Animal { get; private set; }

        // Common controls
        private TextBox txtName;
        private NumericUpDown numAge;
        private NumericUpDown numWeight;
        private ComboBox cmbGender;
        private TextBox txtImagePath;
        private Button btnLoadImage;
        private PictureBox picImage;
        private NumericUpDown numTeeth;
        private NumericUpDown numTailLength;

        // Species-specific
        private TextBox txtBreedOrColor;
        private CheckBox chkTrainedOrIndoor;

        private Button btnOK;
        private Button btnCancel;

        public MammalForm(MammalSpecies species)
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
            cmbGender.SelectedIndex = 2;
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
            GroupBox grpCategory = new GroupBox { Text = "Mammal Data", Location = new Point(10, 310), Size = new Size(360, 80) };
            Controls.Add(grpCategory);

            Label lblTeeth = new Label { Text = "Teeth:", Location = new Point(10, 20) };
            numTeeth = new NumericUpDown { Location = new Point(100, 20), Maximum = 100 };
            grpCategory.Controls.Add(lblTeeth);
            grpCategory.Controls.Add(numTeeth);

            Label lblTail = new Label { Text = "Tail Length:", Location = new Point(10, 50) };
            numTailLength = new NumericUpDown { Location = new Point(100, 50), DecimalPlaces = 2, Maximum = 200 };
            grpCategory.Controls.Add(lblTail);
            grpCategory.Controls.Add(numTailLength);

            // Species group
            GroupBox grpSpecies = new GroupBox { Text = $"{_species} Data", Location = new Point(10, 400), Size = new Size(360, 80) };
            Controls.Add(grpSpecies);

            if (_species == MammalSpecies.Dog)
            {
                Label lblBreed = new Label { Text = "Breed:", Location = new Point(10, 20) };
                txtBreedOrColor = new TextBox { Location = new Point(100, 20) };
                grpSpecies.Controls.Add(lblBreed);
                grpSpecies.Controls.Add(txtBreedOrColor);

                Label lblTrained = new Label { Text = "Trained:", Location = new Point(10, 50) };
                chkTrainedOrIndoor = new CheckBox { Location = new Point(100, 50) };
                grpSpecies.Controls.Add(lblTrained);
                grpSpecies.Controls.Add(chkTrainedOrIndoor);
            }
            else if (_species == MammalSpecies.Cat)
            {
                Label lblColor = new Label { Text = "Fur Color:", Location = new Point(10, 20) };
                txtBreedOrColor = new TextBox { Location = new Point(100, 20) };
                grpSpecies.Controls.Add(lblColor);
                grpSpecies.Controls.Add(txtBreedOrColor);

                Label lblIndoor = new Label { Text = "Indoor:", Location = new Point(10, 50) };
                chkTrainedOrIndoor = new CheckBox { Location = new Point(100, 50) };
                grpSpecies.Controls.Add(lblIndoor);
                grpSpecies.Controls.Add(chkTrainedOrIndoor);
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

            Mammal mammal = MammalFactory.CreateMammal(_species, (int)numTeeth.Value, (double)numTailLength.Value);
            mammal.Name = txtName.Text;
            mammal.Age = (int)numAge.Value;
            mammal.Weight = (double)numWeight.Value;
            mammal.Gender = (GenderType)cmbGender.SelectedItem;
            mammal.ImagePath = txtImagePath.Text;

            if (_species == MammalSpecies.Dog)
            {
                ((Dog)mammal).Breed = txtBreedOrColor.Text;
                ((Dog)mammal).IsTrained = chkTrainedOrIndoor.Checked;
            }
            else if (_species == MammalSpecies.Cat)
            {
                ((Cat)mammal).FurColor = txtBreedOrColor.Text;
                ((Cat)mammal).IsIndoor = chkTrainedOrIndoor.Checked;
            }

            Animal = mammal;
        }
    }
}

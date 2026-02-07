using EAMS.Mammals.Species;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EAMS
{
    /// <summary>
    /// Form for entering data about a mammal. It dynamically adjusts its fields based on the selected mammal species.
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

        // Species-specific controls
        private TextBox txtSpeciesText;
        private NumericUpDown numSpeciesNumeric;
        private CheckBox chkSpeciesBool;

        private Button btnOK;
        private Button btnCancel;
        /// <summary>
        /// Initializes a new instance of the MammalForm class, configuring input fields for general, image, category,
        /// and species specific mammal data.
        /// </summary>
        /// <param name="species">The mammal species to customize the form for.</param>
        public MammalForm(MammalSpecies species)
        {
            _species = species;
            this.Text = $"Enter {_species} Data";
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(420, 560);

            // General group
            GroupBox grpGeneral = new GroupBox { Text = "General Data", Location = new Point(10, 10), Size = new Size(390, 140) };
            Controls.Add(grpGeneral);

            Label lblName = new Label { Text = "Name:", Location = new Point(12, 24), AutoSize = true };
            txtName = new TextBox { Location = new Point(110, 20), Width = 260 };
            grpGeneral.Controls.Add(lblName);
            grpGeneral.Controls.Add(txtName);

            Label lblAge = new Label { Text = "Age:", Location = new Point(12, 54), AutoSize = true };
            numAge = new NumericUpDown { Location = new Point(110, 50), Maximum = 100 };
            grpGeneral.Controls.Add(lblAge);
            grpGeneral.Controls.Add(numAge);

            Label lblWeight = new Label { Text = "Weight:", Location = new Point(12, 84), AutoSize = true };
            numWeight = new NumericUpDown { Location = new Point(110, 80), DecimalPlaces = 2, Maximum = 1000 };
            grpGeneral.Controls.Add(lblWeight);
            grpGeneral.Controls.Add(numWeight);

            Label lblGender = new Label { Text = "Gender:", Location = new Point(12, 114), AutoSize = true };
            cmbGender = new ComboBox { Location = new Point(110, 110), DropDownStyle = ComboBoxStyle.DropDownList, Width = 140 };
            foreach (GenderType g in Enum.GetValues(typeof(GenderType)))
            {
                cmbGender.Items.Add(g);
            }
            if (cmbGender.Items.Count > 0) cmbGender.SelectedIndex = Math.Min(2, cmbGender.Items.Count - 1);
            grpGeneral.Controls.Add(lblGender);
            grpGeneral.Controls.Add(cmbGender);

            // Image group
            GroupBox grpImage = new GroupBox { Text = "Image", Location = new Point(10, 160), Size = new Size(390, 160) };
            Controls.Add(grpImage);

            Label lblImage = new Label { Text = "Image Path:", Location = new Point(12, 24), AutoSize = true };
            txtImagePath = new TextBox { Location = new Point(110, 20), Width = 200 };
            btnLoadImage = new Button { Text = "Load", Location = new Point(320, 18), Width = 44 };
            btnLoadImage.Click += BtnLoadImage_Click;
            picImage = new PictureBox { Location = new Point(12, 52), Size = new Size(120, 90), BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.Zoom };

            grpImage.Controls.Add(lblImage);
            grpImage.Controls.Add(txtImagePath);
            grpImage.Controls.Add(btnLoadImage);
            grpImage.Controls.Add(picImage);

            // Category group
            GroupBox grpCategory = new GroupBox { Text = "Mammal Data", Location = new Point(10, 330), Size = new Size(390, 80) };
            Controls.Add(grpCategory);

            Label lblTeeth = new Label { Text = "Teeth:", Location = new Point(12, 20), AutoSize = true };
            numTeeth = new NumericUpDown { Location = new Point(110, 18), Maximum = 100, Width = 80 };
            grpCategory.Controls.Add(lblTeeth);
            grpCategory.Controls.Add(numTeeth);

            Label lblTail = new Label { Text = "Tail Length:", Location = new Point(12, 50), AutoSize = true };
            numTailLength = new NumericUpDown { Location = new Point(110, 48), DecimalPlaces = 2, Maximum = 200, Width = 80 };
            grpCategory.Controls.Add(lblTail);
            grpCategory.Controls.Add(numTailLength);

            // Species group
            GroupBox grpSpecies = new GroupBox { Text = $"{_species} Data", Location = new Point(10, 420), Size = new Size(390, 100) };
            Controls.Add(grpSpecies);

            switch (_species)
            {
                case MammalSpecies.Dog:
                    Label lblBreed = new Label { Text = "Breed:", Location = new Point(12, 24), AutoSize = true };
                    txtSpeciesText = new TextBox { Location = new Point(110, 20), Width = 260 };
                    grpSpecies.Controls.Add(lblBreed);
                    grpSpecies.Controls.Add(txtSpeciesText);

                    Label lblTrained = new Label { Text = "Trained:", Location = new Point(12, 54), AutoSize = true };
                    chkSpeciesBool = new CheckBox { Location = new Point(110, 52) };
                    grpSpecies.Controls.Add(lblTrained);
                    grpSpecies.Controls.Add(chkSpeciesBool);
                    break;
                case MammalSpecies.Cat:
                    Label lblColor = new Label { Text = "Fur Color:", Location = new Point(12, 24), AutoSize = true };
                    txtSpeciesText = new TextBox { Location = new Point(110, 20), Width = 260 };
                    grpSpecies.Controls.Add(lblColor);
                    grpSpecies.Controls.Add(txtSpeciesText);

                    Label lblIndoor = new Label { Text = "Indoor:", Location = new Point(12, 54), AutoSize = true };
                    chkSpeciesBool = new CheckBox { Location = new Point(110, 52) };
                    grpSpecies.Controls.Add(lblIndoor);
                    grpSpecies.Controls.Add(chkSpeciesBool);
                    break;
                case MammalSpecies.Cow:
                    Label lblMilk = new Label { Text = "Milk Production:", Location = new Point(12, 24), AutoSize = true };
                    numSpeciesNumeric = new NumericUpDown { Location = new Point(150, 20), DecimalPlaces = 2, Maximum = 100, Width = 120 };
                    grpSpecies.Controls.Add(lblMilk);
                    grpSpecies.Controls.Add(numSpeciesNumeric);
                    break;
                case MammalSpecies.Horse:
                    Label lblHorseBreed = new Label { Text = "Breed:", Location = new Point(12, 24), AutoSize = true };
                    txtSpeciesText = new TextBox { Location = new Point(110, 20), Width = 260 };
                    grpSpecies.Controls.Add(lblHorseBreed);
                    grpSpecies.Controls.Add(txtSpeciesText);

                    Label lblRacing = new Label { Text = "Is Racing:", Location = new Point(12, 54), AutoSize = true };
                    chkSpeciesBool = new CheckBox { Location = new Point(110, 52) };
                    grpSpecies.Controls.Add(lblRacing);
                    grpSpecies.Controls.Add(chkSpeciesBool);
                    break;
            }

            btnOK = new Button { Text = "OK", Location = new Point(150, 530), DialogResult = DialogResult.OK };
            btnOK.Click += BtnOK_Click;
            Controls.Add(btnOK);

            btnCancel = new Button { Text = "Cancel", Location = new Point(250, 530), DialogResult = DialogResult.Cancel };
            Controls.Add(btnCancel);

            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }
        /// <summary>
        /// Handles the image loading process by opening a file dialog, updating the image path textbox, and displaying
        /// the selected image in the picture box.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void BtnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtImagePath.Text = ofd.FileName;
                try
                {
                    picImage.Image?.Dispose();
                    picImage.Image = Image.FromFile(ofd.FileName);
                }
                catch
                {
                    MessageBox.Show("Invalid image file.");
                }
            }
        }
        /// <summary>
        /// Handles the OK button click event, validates input fields, creates and populates a Mammal object based on
        /// the selected species, and assigns it to the Animal property.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
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

            switch (_species)
            {
                case MammalSpecies.Dog:
                    if (string.IsNullOrEmpty(txtSpeciesText.Text))
                    {
                        MessageBox.Show("Please enter breed.");
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                    ((Dog)mammal).Breed = txtSpeciesText.Text;
                    ((Dog)mammal).IsTrained = chkSpeciesBool != null && chkSpeciesBool.Checked;
                    break;
                case MammalSpecies.Cat:
                    if (string.IsNullOrEmpty(txtSpeciesText.Text))
                    {
                        MessageBox.Show("Please enter fur color.");
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                    ((Cat)mammal).FurColor = txtSpeciesText.Text;
                    ((Cat)mammal).IsIndoor = chkSpeciesBool != null && chkSpeciesBool.Checked;
                    break;
                case MammalSpecies.Cow:
                    ((Cow)mammal).MilkProduction = numSpeciesNumeric != null ? (double)numSpeciesNumeric.Value : 0.0;
                    break;
                case MammalSpecies.Horse:
                    if (string.IsNullOrEmpty(txtSpeciesText.Text))
                    {
                        MessageBox.Show("Please enter breed.");
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                    ((Horse)mammal).Breed = txtSpeciesText.Text;
                    ((Horse)mammal).IsRacing = chkSpeciesBool != null && chkSpeciesBool.Checked;
                    break;
            }

            Animal = mammal;
        }
    }
}
using EAMS.Reptiles.Species;
using System;
using System.Drawing;
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

        // General controls
        private TextBox txtName;
        private NumericUpDown numAge;
        private NumericUpDown numWeight;
        private ComboBox cmbGender;
        private TextBox txtImagePath;
        private Button btnLoadImage;
        private PictureBox picImage;
        private NumericUpDown numBodyLength;
        private Panel pnlLivesInWater;
        private RadioButton rdoLivesYes;
        private RadioButton rdoLivesNo;
        private NumericUpDown numAggressiveness;

        // Species specific controls (will be used based on species)
        private NumericUpDown numSpeciesNumeric1;
        private NumericUpDown numSpeciesNumeric2;
        private TextBox txtSpeciesText;
        private CheckBox chkSpeciesBool;

        private Button btnOK;
        private Button btnCancel;
        // Constructor takes the species to determine which specific fields to show
        public ReptileForm(ReptileSpecies species)
        {
            _species = species;
            this.Text = $"Enter {_species} Data";
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(420, 620);

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
            cmbGender = new ComboBox { Location = new Point(110, 110), DropDownStyle = ComboBoxStyle.DropDownList, Width = 120 };
            foreach (GenderType g in Enum.GetValues(typeof(GenderType)))
            {
                cmbGender.Items.Add(g);
            }
            if (cmbGender.Items.Count > 0) cmbGender.SelectedIndex = Math.Min(2, cmbGender.Items.Count - 1); // safe default
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
            GroupBox grpCategory = new GroupBox { Text = "Reptile Data", Location = new Point(10, 330), Size = new Size(390, 100) };
            Controls.Add(grpCategory);

            Label lblBodyLength = new Label { Text = "Body Length:", Location = new Point(12, 20), AutoSize = true };
            numBodyLength = new NumericUpDown { Location = new Point(110, 20), DecimalPlaces = 2, Maximum = 500 };
            grpCategory.Controls.Add(lblBodyLength);
            grpCategory.Controls.Add(numBodyLength);

            Label lblWater = new Label { Text = "Lives in Water:", Location = new Point(240, 20), AutoSize = true };
            // Panel to hold radio buttons (Yes / No)
            pnlLivesInWater = new Panel { Location = new Point(330, 12), Size = new Size(90, 28) };
            rdoLivesYes = new RadioButton { Text = "Yes", Location = new Point(0, 4), AutoSize = true };
            rdoLivesNo = new RadioButton { Text = "No", Location = new Point(46, 4), AutoSize = true, Checked = true };
            pnlLivesInWater.Controls.Add(rdoLivesYes);
            pnlLivesInWater.Controls.Add(rdoLivesNo);

            grpCategory.Controls.Add(lblWater);
            grpCategory.Controls.Add(pnlLivesInWater);

            Label lblAgg = new Label { Text = "Aggressiveness (0-10):", Location = new Point(12, 56), AutoSize = true };
            numAggressiveness = new NumericUpDown { Location = new Point(160, 54), Maximum = 10 };
            grpCategory.Controls.Add(lblAgg);
            grpCategory.Controls.Add(numAggressiveness);

            // Species group
            GroupBox grpSpecies = new GroupBox { Text = $"{_species} Data", Location = new Point(10, 440), Size = new Size(390, 120) };
            Controls.Add(grpSpecies);
            // Add species specific controls based on the selected species
            switch (_species)
            {
                case ReptileSpecies.Turtle:
                    Label lblHardness = new Label { Text = "Shell Hardness:", Location = new Point(12, 24), AutoSize = true };
                    numSpeciesNumeric1 = new NumericUpDown { Location = new Point(110, 20), Maximum = 10 };
                    grpSpecies.Controls.Add(lblHardness);
                    grpSpecies.Controls.Add(numSpeciesNumeric1);

                    Label lblWidth = new Label { Text = "Shell Width:", Location = new Point(12, 54), AutoSize = true };
                    numSpeciesNumeric2 = new NumericUpDown { Location = new Point(110, 50), DecimalPlaces = 2, Maximum = 100 };
                    grpSpecies.Controls.Add(lblWidth);
                    grpSpecies.Controls.Add(numSpeciesNumeric2);
                    break;
                case ReptileSpecies.Lizard:
                    Label lblColor = new Label { Text = "Color:", Location = new Point(12, 24), AutoSize = true };
                    txtSpeciesText = new TextBox { Location = new Point(110, 20), Width = 260 };
                    grpSpecies.Controls.Add(lblColor);
                    grpSpecies.Controls.Add(txtSpeciesText);

                    Label lblClimb = new Label { Text = "Can Climb:", Location = new Point(12, 54), AutoSize = true };
                    chkSpeciesBool = new CheckBox { Location = new Point(110, 50) };
                    grpSpecies.Controls.Add(lblClimb);
                    grpSpecies.Controls.Add(chkSpeciesBool);
                    break;
                case ReptileSpecies.Snake:
                    Label lblLength = new Label { Text = "Length:", Location = new Point(12, 24), AutoSize = true };
                    numSpeciesNumeric1 = new NumericUpDown { Location = new Point(110, 20), DecimalPlaces = 2, Maximum = 500 };
                    grpSpecies.Controls.Add(lblLength);
                    grpSpecies.Controls.Add(numSpeciesNumeric1);

                    Label lblVenomous = new Label { Text = "Is Venomous:", Location = new Point(12, 54), AutoSize = true };
                    chkSpeciesBool = new CheckBox { Location = new Point(110, 50) };
                    grpSpecies.Controls.Add(lblVenomous);
                    grpSpecies.Controls.Add(chkSpeciesBool);
                    break;
            }
            // OK and Cancel buttons
            btnOK = new Button { Text = "OK", Location = new Point(150, 580), DialogResult = DialogResult.OK };
            btnOK.Click += BtnOK_Click;
            Controls.Add(btnOK);

            btnCancel = new Button { Text = "Cancel", Location = new Point(250, 580), DialogResult = DialogResult.Cancel };
            Controls.Add(btnCancel);

            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }
        /// <summary>
        /// Handles the image loading process by opening a file dialog, updating the image path textbox, and displaying
        /// the selected image in the picture box.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
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
        /// Handles the OK button click event, validates input fields, creates a reptile object with specified
        /// properties, and assigns it to the Animal field.
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
            // Use radio button state for "Lives in water"
            bool livesInWater = rdoLivesYes.Checked;

            // Create the reptile object using the factory and set common properties
            Reptile reptile = ReptileFactory.CreateReptile(_species, (double)numBodyLength.Value, livesInWater, (int)numAggressiveness.Value);
            reptile.Name = txtName.Text;
            reptile.Age = (int)numAge.Value;
            reptile.Weight = (double)numWeight.Value;
            reptile.Gender = (GenderType)cmbGender.SelectedItem;
            reptile.ImagePath = txtImagePath.Text;
            // Set species specific properties based on the selected species
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
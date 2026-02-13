using EAMS.Birds.species;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EAMS.Birds
{
    public partial class BirdForm : Form
    {
        /// <summary>
        /// Stores the species information for a bird.
        /// </summary>
        private BirdSpecies _species;
        public Animal Animal { get; private set; }

        // General controls
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

        // Species specific
        private CheckBox chkIsBald;
        private TextBox txtFeatherColor;
        private NumericUpDown numBeakLength;
        private TextBox txtPlumeColor;

        private Button btnOK;
        private Button btnCancel;
        /// <summary>
        /// Initializes a new instance of the BirdForm class for entering data specific to the given bird species.
        /// </summary>
        /// <param name="species">The bird species for which the form is created.</param>
        public BirdForm(BirdSpecies species)
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
            foreach (GenderType g in Enum.GetValues(typeof(GenderType))) cmbGender.Items.Add(g);
            if (cmbGender.Items.Count > 0) cmbGender.SelectedIndex = Math.Min(2, cmbGender.Items.Count - 1);
            grpGeneral.Controls.Add(lblGender);
            grpGeneral.Controls.Add(cmbGender);

            // Image group (keeps controls inside a group to avoid overlap)
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
            GroupBox grpCategory = new GroupBox { Text = "Bird Data", Location = new Point(10, 330), Size = new Size(390, 80) };
            Controls.Add(grpCategory);

            Label lblWingspan = new Label { Text = "Wingspan (cm):", Location = new Point(12, 20), AutoSize = true };
            numWingspan = new NumericUpDown { Location = new Point(120, 18), DecimalPlaces = 1, Maximum = 300, Width = 80 };
            grpCategory.Controls.Add(lblWingspan);
            grpCategory.Controls.Add(numWingspan);

            Label lblTail = new Label { Text = "Tail Length (cm):", Location = new Point(12, 50), AutoSize = true };
            numTailLength = new NumericUpDown { Location = new Point(120, 48), DecimalPlaces = 1, Maximum = 100, Width = 80 };
            grpCategory.Controls.Add(lblTail);
            grpCategory.Controls.Add(numTailLength);

            // Species group
            GroupBox grpSpecies = new GroupBox { Text = $"{_species} Data", Location = new Point(10, 420), Size = new Size(390, 90) };
            Controls.Add(grpSpecies);

            if (_species == BirdSpecies.Eagle)
            {
                Label lblBald = new Label { Text = "Is Bald Eagle:", Location = new Point(12, 24), AutoSize = true };
                chkIsBald = new CheckBox { Location = new Point(120, 22) };
                grpSpecies.Controls.Add(lblBald);
                grpSpecies.Controls.Add(chkIsBald);
            }
            else if (_species == BirdSpecies.Dove)
            {
                Label lblColor = new Label { Text = "Feather Color:", Location = new Point(12, 24), AutoSize = true };
                txtFeatherColor = new TextBox { Location = new Point(120, 20), Width = 250 };
                grpSpecies.Controls.Add(lblColor);
                grpSpecies.Controls.Add(txtFeatherColor);
            }
            else if (_species == BirdSpecies.Falcon)
            {
                Label lblBeak = new Label { Text = "Beak Length (cm):", Location = new Point(12, 24), AutoSize = true };
                numBeakLength = new NumericUpDown { Location = new Point(140, 22), DecimalPlaces = 1, Maximum = 50, Width = 80 };
                grpSpecies.Controls.Add(lblBeak);
                grpSpecies.Controls.Add(numBeakLength);
            }
            else if (_species == BirdSpecies.Peacock)
            {
                Label lblPlume = new Label { Text = "Plume Color:", Location = new Point(12, 24), AutoSize = true };
                txtPlumeColor = new TextBox { Location = new Point(120, 20), Width = 250 };
                grpSpecies.Controls.Add(lblPlume);
                grpSpecies.Controls.Add(txtPlumeColor);
            }

            // Buttons
            btnOK = new Button { Text = "OK", Location = new Point(150, 520), DialogResult = DialogResult.OK };
            btnOK.Click += BtnOK_Click;
            Controls.Add(btnOK);

            btnCancel = new Button { Text = "Cancel", Location = new Point(250, 520), DialogResult = DialogResult.Cancel };
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
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtImagePath.Text = ofd.FileName;
                    try
                    {
                        picImage.Image?.Dispose();
                        picImage.Image = Image.FromFile(ofd.FileName);
                    }
                    catch { MessageBox.Show("Invalid image file."); }
                }
            }
        }

        /// <summary>
        /// Handles the OK button click event, validates input fields, creates a bird instance with specified
        /// properties, and assigns it to the Animal property.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || cmbGender.SelectedItem == null)
            {
                MessageBox.Show("Please fill required fields (Name & Gender).");
                this.DialogResult = DialogResult.None;
                return;
            }

            // Create bird instance using factory
            Bird bird = BirdFactory.CreateBird(_species, (double)numWingspan.Value, (double)numTailLength.Value);

            bird.Name = txtName.Text;
            bird.Age = (int)numAge.Value;
            bird.Weight = (double)numWeight.Value;
            bird.Gender = (GenderType)cmbGender.SelectedItem;
            bird.ImagePath = txtImagePath.Text;

            if (_species == BirdSpecies.Eagle)
            {
                ((Eagle)bird).IsBald = chkIsBald != null && chkIsBald.Checked;
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
            else if (_species == BirdSpecies.Falcon)
            {
                ((Falcon)bird).BeakLength = (double)numBeakLength.Value;
            }
            else if (_species == BirdSpecies.Peacock)
            {
                if (string.IsNullOrWhiteSpace(txtPlumeColor.Text))
                {
                    MessageBox.Show("Please enter plume color for Peacock.");
                    this.DialogResult = DialogResult.None;
                    return;
                }
                ((Peacock)bird).PlumeColor = txtPlumeColor.Text;
            }

            Animal = bird;
        }
    }
}
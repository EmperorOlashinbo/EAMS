using EAMS.Insects.Species;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EAMS.Insects
{
    /// <summary>
    /// Represents a Windows Form for entering and editing data for an insect, including general, category specific, and
    /// species specific information.
    /// </summary>
    public partial class InsectForm : Form
    {
        private InsectSpecies _species;
        public Animal Animal { get; private set; }

        // General controls 
        private TextBox txtName;
        private NumericUpDown numAge;
        private NumericUpDown numWeight;
        private ComboBox cmbGender;
        private TextBox txtImagePath;
        private Button btnLoadImage;
        private PictureBox picImage;
        private NumericUpDown numWings;
        private NumericUpDown numAntennaLength;

        // Species specific
        private TextBox txtWingPattern;
        private CheckBox chkCanSting;
        private NumericUpDown numHoneyProd;
        private CheckBox chkIsWorker;
        private NumericUpDown numFlightSpeed;
        private NumericUpDown numSpotCount;

        private Button btnOK;
        private Button btnCancel;
        /// <summary>
        /// Initializes a new instance of the InsectForm class, configuring input controls for general, image, category,
        /// and species specific insect data.
        /// </summary>
        /// <param name="species">The insect species to be used for species-specific controls and labeling.</param>
        public InsectForm(InsectSpecies species)
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
            GroupBox grpCategory = new GroupBox { Text = "Insect Data", Location = new Point(10, 330), Size = new Size(390, 80) };
            Controls.Add(grpCategory);

            Label lblWings = new Label { Text = "Number of Wings:", Location = new Point(12, 20), AutoSize = true };
            numWings = new NumericUpDown { Location = new Point(150, 18), Maximum = 4, Width = 80 };
            grpCategory.Controls.Add(lblWings);
            grpCategory.Controls.Add(numWings);

            Label lblAntenna = new Label { Text = "Antenna Length (mm):", Location = new Point(12, 50), AutoSize = true };
            numAntennaLength = new NumericUpDown { Location = new Point(170, 48), DecimalPlaces = 1, Maximum = 50, Width = 80 };
            grpCategory.Controls.Add(lblAntenna);
            grpCategory.Controls.Add(numAntennaLength);

            // Species group
            GroupBox grpSpecies = new GroupBox { Text = $"{_species} Data", Location = new Point(10, 420), Size = new Size(390, 100) };
            Controls.Add(grpSpecies);

            switch (_species)
            {
                case InsectSpecies.Butterfly:
                    Label lblPattern = new Label { Text = "Wing Pattern:", Location = new Point(12, 24), AutoSize = true };
                    txtWingPattern = new TextBox { Location = new Point(120, 20), Width = 250 };
                    grpSpecies.Controls.Add(lblPattern);
                    grpSpecies.Controls.Add(txtWingPattern);
                    break;
                case InsectSpecies.Bee:
                    Label lblSting = new Label { Text = "Can Sting:", Location = new Point(12, 20), AutoSize = true };
                    chkCanSting = new CheckBox { Location = new Point(120, 18) };
                    chkCanSting.Checked = true;
                    grpSpecies.Controls.Add(lblSting);
                    grpSpecies.Controls.Add(chkCanSting);

                    Label lblHoney = new Label { Text = "Honey (g/day):", Location = new Point(12, 50), AutoSize = true };
                    numHoneyProd = new NumericUpDown { Location = new Point(120, 48), DecimalPlaces = 2, Maximum = 10, Width = 120 };
                    grpSpecies.Controls.Add(lblHoney);
                    grpSpecies.Controls.Add(numHoneyProd);
                    break;
                case InsectSpecies.Ant:
                    Label lblWorker = new Label { Text = "Is Worker:", Location = new Point(12, 24), AutoSize = true };
                    chkIsWorker = new CheckBox { Location = new Point(120, 22) };
                    chkIsWorker.Checked = true;
                    grpSpecies.Controls.Add(lblWorker);
                    grpSpecies.Controls.Add(chkIsWorker);
                    break;
                case InsectSpecies.Dragonfly:
                    Label lblFlight = new Label { Text = "Flight Speed (km/h):", Location = new Point(12, 24), AutoSize = true };
                    numFlightSpeed = new NumericUpDown { Location = new Point(160, 22), DecimalPlaces = 1, Maximum = 200, Width = 100 };
                    grpSpecies.Controls.Add(lblFlight);
                    grpSpecies.Controls.Add(numFlightSpeed);
                    break;
                case InsectSpecies.Ladybug:
                    Label lblSpots = new Label { Text = "Spot Count:", Location = new Point(12, 24), AutoSize = true };
                    numSpotCount = new NumericUpDown { Location = new Point(120, 22), Maximum = 50, Width = 80 };
                    grpSpecies.Controls.Add(lblSpots);
                    grpSpecies.Controls.Add(numSpotCount);
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
        /// Handles the click event to open an image file dialog, display the selected image in a picture box, and
        /// update the image path text box.
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
        /// Handles the OK button click event, validates input fields, creates and populates an insect object based on
        /// user input, and assigns it to the Animal property.
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
                    ((Bee)insect).CanSting = chkCanSting != null && chkCanSting.Checked;
                    ((Bee)insect).HoneyProductionPerDay = numHoneyProd != null ? (double)numHoneyProd.Value : 0.0;
                    break;
                case InsectSpecies.Ant:
                    ((Ant)insect).IsWorker = chkIsWorker != null && chkIsWorker.Checked;
                    break;
                case InsectSpecies.Dragonfly:
                    ((Dragonfly)insect).FlightSpeed = numFlightSpeed != null ? (double)numFlightSpeed.Value : 0.0;
                    break;
                case InsectSpecies.Ladybug:
                    ((Ladybug)insect).SpotCount = numSpotCount != null ? (int)numSpotCount.Value : 0;
                    break;
            }

            Animal = insect;
        }
        /// <summary>
        /// Handles the form's Load event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void InsectForm_Load(object sender, EventArgs e)
        {
        }
    }
}
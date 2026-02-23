using EAMS.Birds;
using EAMS.Birds.species;
using EAMS.Insects;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EAMS
{
    /// <summary>
    /// Main form for the EcoPark Animal Management System.
    /// Handles category/species selection, animal creation/editing, and displays results.
    /// </summary>
    public partial class MainForm : Form
    {
        private Animal currentAnimal;
        private CheckBox chkListAll;
        private ListBox lstCategory;
        private ListBox lstSpecies;
        private ListBox lstAllAnimals;
        private Button btnCreate;
        private TextBox txtOutput;
        private Button btnAbout;
        private Button btnClear;
        private PictureBox picPreview;
        // General data controls
        private GroupBox grpGeneral;
        private Label lblName;
        private TextBox txtName;
        private Label lblAge;
        private TextBox txtAge;
        private Label lblWeight;
        private TextBox txtWeight;
        private Label lblGender;
        private ComboBox cmbGender;
        private Button btnAdd;
        private Button btnLoadImage;

        /// <summary>
        /// Initializes the main form, sets up the UI components, and configures event handlers for user interactions.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            this.Text = "EcoPark Animal Management System by Ibrahim";
            this.Size = new Size(760, 540);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Menu bar
            MenuStrip menu = new MenuStrip();
            ToolStripMenuItem mnuFile = new ToolStripMenuItem("File");
            ToolStripMenuItem mnuHelp = new ToolStripMenuItem("Help");
            ToolStripMenuItem mnuAbout = new ToolStripMenuItem("About");
            mnuAbout.Click += BtnAbout_Click;
            mnuHelp.DropDownItems.Add(mnuAbout);
            menu.Items.Add(mnuFile);
            menu.Items.Add(mnuHelp);
            this.MainMenuStrip = menu;
            Controls.Add(menu);

            // Create Animal label + lists
            Label lblCreate = new Label { Text = "Create Animal", Location = new Point(10, 30), AutoSize = true };
            Controls.Add(lblCreate);

            lstCategory = new ListBox { Location = new Point(10, 55), Size = new Size(120, 120) };
            lstCategory.Items.AddRange(new[] { "Bird", "Insect", "Mammal", "Reptile" });
            lstCategory.SelectedIndexChanged += LstCategory_SelectedIndexChanged;
            Controls.Add(lstCategory);

            lstSpecies = new ListBox { Location = new Point(140, 55), Size = new Size(120, 120) };
            Controls.Add(lstSpecies);

            chkListAll = new CheckBox { Text = "List all animal species", Location = new Point(10, 180), AutoSize = true };
            chkListAll.CheckedChanged += ChkListAll_CheckedChanged;
            Controls.Add(chkListAll);

            lstAllAnimals = new ListBox { Location = new Point(10, 205), Size = new Size(250, 120), Visible = false };
            lstAllAnimals.Items.AddRange(new[] { "Dog", "Cat", "Cow", "Horse", "Turtle", "Lizard", "Snake", "Eagle", "Dove", "Butterfly", "Bee", "Ant" });
            lstAllAnimals.SelectedIndexChanged += LstAllAnimals_SelectedIndexChanged;
            Controls.Add(lstAllAnimals);

            btnCreate = new Button { Text = "Create Animal", Location = new Point(140, 180), Width = 120 };
            btnCreate.Click += BtnCreate_Click;
            Controls.Add(btnCreate);

            // Image preview and load image button
            picPreview = new PictureBox
            {
                Location = new Point(410, 55),
                Size = new Size(200, 150),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle
            };
            Controls.Add(picPreview);

            btnLoadImage = new Button { Text = "Load Image", Location = new Point(410, 210), Width = 200 };
            btnLoadImage.Click += BtnLoadImage_Click;
            Controls.Add(btnLoadImage);

            // General data group
            grpGeneral = new GroupBox { Text = "General Data", Location = new Point(10, 340), Size = new Size(360, 150) };
            lblName = new Label { Text = "Name", Location = new Point(10, 20), AutoSize = true };
            grpGeneral.Controls.Add(lblName);
            txtName = new TextBox { Location = new Point(90, 18), Width = 240 };
            grpGeneral.Controls.Add(txtName);

            lblAge = new Label { Text = "Age", Location = new Point(10, 50), AutoSize = true };
            grpGeneral.Controls.Add(lblAge);
            txtAge = new TextBox { Location = new Point(90, 48), Width = 100 };
            grpGeneral.Controls.Add(txtAge);

            lblWeight = new Label { Text = "Weight", Location = new Point(10, 80), AutoSize = true };
            grpGeneral.Controls.Add(lblWeight);
            txtWeight = new TextBox { Location = new Point(90, 78), Width = 100 };
            grpGeneral.Controls.Add(txtWeight);

            lblGender = new Label { Text = "Gender", Location = new Point(10, 110), AutoSize = true };
            grpGeneral.Controls.Add(lblGender);
            cmbGender = new ComboBox { Location = new Point(90, 108), Width = 120, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbGender.Items.AddRange(new[] { "Male", "Female", "Unknown" });
            grpGeneral.Controls.Add(cmbGender);

            btnAdd = new Button { Text = "Add", Location = new Point(240, 108), Width = 90, Enabled = false };
            btnAdd.Click += BtnAdd_Click;
            grpGeneral.Controls.Add(btnAdd);
            Controls.Add(grpGeneral);

            // Output box
            GroupBox grpOutput = new GroupBox { Text = "Animal Details", Location = new Point(410, 260), Size = new Size(320, 230) };
            txtOutput = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Consolas", 10)
            };
            grpOutput.Controls.Add(txtOutput);
            Controls.Add(grpOutput);

            // Bottom buttons
            btnClear = new Button { Text = "Clear Output", Location = new Point(540, 500 - 40), Width = 190 };
            btnClear.Click += BtnClear_Click;
            Controls.Add(btnClear);

            btnAbout = new Button { Text = "About", Location = new Point(10, 500 - 40), Width = 100 };
            btnAbout.Click += BtnAbout_Click;
            Controls.Add(btnAbout);
        }
        /// <summary>
        /// Handles the image loading process by displaying an open file dialog, updating the preview image, and setting
        /// the current animal's image path.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void BtnLoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog { Filter = "Images|*.png;*.jpg;*.jpeg;*.bmp;*.gif" })
            {
                if (dlg.ShowDialog() == DialogResult.OK && File.Exists(dlg.FileName))
                {
                    try
                    {
                        picPreview.Image?.Dispose();
                        picPreview.Image = Image.FromFile(dlg.FileName);
                        if (currentAnimal != null)
                            currentAnimal.ImagePath = dlg.FileName;
                    }
                    catch { /* ignore image load errors */ }
                }
            }
        }
        /// <summary>
        /// Toggles between listing all animal species and filtering by category. When "List all animal species" is checked,
        /// the category and species lists are disabled, and the all animals list is shown. When unchecked, the category
        /// and species lists are enabled, and the all animals list is hidden.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkListAll_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = chkListAll.Checked;
            lstCategory.Enabled = !isChecked;
            lstSpecies.Enabled = !isChecked;
            lstAllAnimals.Visible = isChecked;
            if (isChecked)
            {
                lstCategory.ClearSelected();
                lstSpecies.Items.Clear();
            }
            else
            {
                lstAllAnimals.ClearSelected();
            }
        }
        /// <summary>
        /// Updates the species list based on the selected category. When a category is selected, 
        /// the corresponding species are loaded into the species list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LstCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstSpecies.Items.Clear();
            string cat = lstCategory.SelectedItem?.ToString() ?? "";
            switch (cat)
            {
                case "Mammal":
                    lstSpecies.Items.AddRange(Enum.GetNames(typeof(MammalSpecies)));
                    break;
                case "Reptile":
                    lstSpecies.Items.AddRange(Enum.GetNames(typeof(ReptileSpecies)));
                    break;
                case "Bird":
                    lstSpecies.Items.AddRange(Enum.GetNames(typeof(BirdSpecies)));
                    break;
                case "Insect":
                    lstSpecies.Items.AddRange(Enum.GetNames(typeof(InsectSpecies)));
                    break;
            }
        }
        /// <summary>
        /// When a species is selected from the "List all animal species" list, 
        /// this event handler determines the category of the selected species,
        /// and updates the category and species lists accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LstAllAnimals_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAllAnimals.SelectedItem is string sp && !string.IsNullOrEmpty(sp))
            {
                string cat = GetCategoryFromSpecies(sp);
                if (!string.IsNullOrEmpty(cat))
                {
                    lstCategory.SelectedItem = cat;
                    lstSpecies.SelectedItem = sp;
                }
            }
        }
        /// <summary>
        /// Determines the category of an animal based on its species name. 
        /// This method is used to map a selected species
        /// to its corresponding category.
        /// </summary>
        /// <param name="species">The species name of the animal.</param>
        /// <returns>The category of the animal.</returns>
        private string GetCategoryFromSpecies(string species)
        {
            if (species == "Dog" || species == "Cat" || species == "Cow" || species == "Horse")
                return "Mammal";
            if (species == "Turtle" || species == "Lizard" || species == "Snake" || species == "Crocodile")
                return "Reptile";
            if (species == "Eagle" || species == "Dove" || species == "Falcon" || species == "Peacock")
                return "Bird";
            if (species == "Butterfly" || species == "Bee" || species == "Ant" || species == "Dragonfly" || species == "Ladybug")
                return "Insect";
            return "";
        }
        /// <summary>
        /// Handles the creation of a new animal based on the selected category and species. 
        /// It opens the corresponding form for the selected species,
        /// and assigns the created animal to the currentAnimal variable.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreate_Click(object sender, EventArgs e)
        {
            string category = chkListAll.Checked ? GetCategoryFromSpecies(lstAllAnimals.SelectedItem?.ToString() ?? "") : lstCategory.SelectedItem?.ToString() ?? "";
            string speciesStr = chkListAll.Checked ? lstAllAnimals.SelectedItem?.ToString() : lstSpecies.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(speciesStr))
            {
                MessageBox.Show("Please select a category and species.", "Input required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Form form = null;
            try
            {
                if (category == "Mammal" && Enum.TryParse<MammalSpecies>(speciesStr, out var mSp))
                    form = new MammalForm(mSp);
                else if (category == "Reptile" && Enum.TryParse<ReptileSpecies>(speciesStr, out var rSp))
                    form = new ReptileForm(rSp);
                else if (category == "Bird" && Enum.TryParse<BirdSpecies>(speciesStr, out var bSp))
                    form = new BirdForm(bSp);
                else if (category == "Insect" && Enum.TryParse<InsectSpecies>(speciesStr, out var iSp))
                    form = new InsectForm(iSp);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (form != null && form.ShowDialog() == DialogResult.OK)
            {
                // Assign currentAnimal based on category
                if (category == "Mammal")
                    currentAnimal = ((MammalForm)form).Animal;
                else if (category == "Reptile")
                    currentAnimal = ((ReptileForm)form).Animal;
                else if (category == "Bird")
                    currentAnimal = ((BirdForm)form).Animal;
                else if (category == "Insect")
                    currentAnimal = ((InsectForm)form).Animal;
                else
                    currentAnimal = null;

                if (currentAnimal != null)
                {
                    // Immediately load image preview (this fixes your issue)
                    if (!string.IsNullOrEmpty(currentAnimal.ImagePath) && File.Exists(currentAnimal.ImagePath))
                    {
                        try
                        {
                            picPreview.Image?.Dispose();
                            picPreview.Image = Image.FromFile(currentAnimal.ImagePath);
                        }
                        catch
                        {
                            picPreview.Image = null;
                        }
                    }
                    else
                    {
                        picPreview.Image = null;
                    }

                    // Display full details
                    txtOutput.Text = currentAnimal.ToString();

                    // Pre-fill general fields for possible edit before Add
                    txtName.Text = currentAnimal.Name ?? string.Empty;
                    txtAge.Text = currentAnimal.Age.ToString();
                    txtWeight.Text = currentAnimal.Weight.ToString("F1");
                    try
                    {
                        cmbGender.SelectedItem = currentAnimal.Gender.ToString();
                    }
                    catch
                    {
                        cmbGender.SelectedItem = "Unknown";
                    }

                    btnAdd.Enabled = true;
                }
                else
                {
                    btnAdd.Enabled = false;
                }
            }
        }
        /// <summary>
        /// Handles the "Add" button click event to validate and apply general data (name, age, weight, gender) to the current animal.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (currentAnimal == null)
            {
                MessageBox.Show("No animal to add. Create an animal first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnAdd.Enabled = false;
                return;
            }

            // Validate and apply general data
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtAge.Text) ||
                string.IsNullOrWhiteSpace(txtWeight.Text) || cmbGender.SelectedItem == null)
            {
                MessageBox.Show("Please fill all general data fields.", "Input required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtAge.Text, out int age) || age < 0)
            {
                MessageBox.Show("Age must be a non-negative integer.", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtWeight.Text, out double weight) || weight < 0)
            {
                MessageBox.Show("Weight must be a non-negative number.", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            currentAnimal.Name = txtName.Text;
            currentAnimal.Age = age;
            currentAnimal.Weight = weight;
            string genderStr = cmbGender.SelectedItem.ToString();
            currentAnimal.Gender = Enum.TryParse<GenderType>(genderStr, true, out var g) ? g : GenderType.Unknown;

            txtOutput.Text = currentAnimal.ToString();
            MessageBox.Show("Animal data updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Handles the "About" menu item click event to display the AboutForm, 
        /// which contains information about the application and its developer.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void BtnAbout_Click(object sender, EventArgs e)
        {
            using (var about = new AboutForm())
            {
                about.ShowDialog();
            }
        }
        
        /// <summary>
        /// Handles the "Clear" button click event to reset the form and clear the current animal data.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
            currentAnimal = null;
            btnAdd.Enabled = false;
            txtName.Clear();
            txtAge.Clear();
            txtWeight.Clear();
            cmbGender.SelectedItem = null;
            if (picPreview.Image != null)
            {
                picPreview.Image.Dispose();
                picPreview.Image = null;
            }
        }
    }
}
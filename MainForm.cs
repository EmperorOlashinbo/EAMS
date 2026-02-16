using EAMS.AnimalsGen;
using EAMS.Birds;
using EAMS.Insects;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EAMS
{
    /// <summary>
    /// Main form for the EcoPark Animal Management System (Version 2).
    /// Uses AnimalManager for persistence, supports Add/Change/Delete, and matches the assignment example.
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly AnimalManager _animalManager = new AnimalManager();
        private Animal _currentAnimal;  // temporary object for new/editing

        // UI controls
        private CheckBox chkListAll;
        private ListBox lstCategory;
        private ListBox lstSpecies;
        private ListBox lstAllAnimals;     
        private Button btnCreate;
        private Button btnAdd;
        private Button btnChange;
        private Button btnDelete;
        private TextBox txtOutput;
        private Button btnAbout;
        private Button btnClear;
        private PictureBox picPreview;
        private Button btnLoadImage;

        // General data controls (used for both new and edit)
        private GroupBox grpGeneral;
        private TextBox txtName;
        private TextBox txtAge;
        private TextBox txtWeight;
        private ComboBox cmbGender;

        public MainForm()
        {
            InitializeComponent();
            this.Text = "EcoPark Animal Management System by Ibrahim";
            this.Size = new Size(780, 580);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Menu
            var menu = new MenuStrip();
            var mnuHelp = new ToolStripMenuItem("Help");
            var mnuAbout = new ToolStripMenuItem("About");
            mnuAbout.Click += BtnAbout_Click;
            mnuHelp.DropDownItems.Add(mnuAbout);
            menu.Items.Add(mnuHelp);
            this.MainMenuStrip = menu;
            Controls.Add(menu);

            // Create Animal section
            var lblCreate = new Label { Text = "Create Animal", Location = new Point(10, 30), AutoSize = true };
            Controls.Add(lblCreate);

            lstCategory = new ListBox { Location = new Point(10, 55), Size = new Size(130, 130) };
            lstCategory.Items.AddRange(new[] { "Bird", "Insect", "Mammal", "Reptile" });
            lstCategory.SelectedIndexChanged += LstCategory_SelectedIndexChanged;
            Controls.Add(lstCategory);

            lstSpecies = new ListBox { Location = new Point(150, 55), Size = new Size(130, 130) };
            Controls.Add(lstSpecies);

            chkListAll = new CheckBox { Text = "List all animal species", Location = new Point(10, 195), AutoSize = true };
            chkListAll.CheckedChanged += ChkListAll_CheckedChanged;
            Controls.Add(chkListAll);

            lstAllAnimals = new ListBox { Location = new Point(10, 220), Size = new Size(270, 130) };
            lstAllAnimals.SelectedIndexChanged += LstAllAnimals_SelectedIndexChanged;
            Controls.Add(lstAllAnimals);

            btnCreate = new Button { Text = "Create Animal", Location = new Point(140, 195), Width = 120 };
            btnCreate.Click += BtnCreate_Click;
            Controls.Add(btnCreate);

            // Image preview
            picPreview = new PictureBox
            {
                Location = new Point(410, 55),
                Size = new Size(200, 150),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle
            };
            Controls.Add(picPreview);

            btnLoadImage = new Button { Text = "Load Image", Location = new Point(410, 215), Width = 200 };
            btnLoadImage.Click += BtnLoadImage_Click;
            Controls.Add(btnLoadImage);

            // General Data group
            grpGeneral = new GroupBox { Text = "General Data", Location = new Point(10, 360), Size = new Size(370, 150) };
            Controls.Add(grpGeneral);

            grpGeneral.Controls.Add(new Label { Text = "Name:", Location = new Point(10, 25), AutoSize = true });
            txtName = new TextBox { Location = new Point(90, 22), Width = 260 };
            grpGeneral.Controls.Add(txtName);

            grpGeneral.Controls.Add(new Label { Text = "Age:", Location = new Point(10, 55), AutoSize = true });
            txtAge = new TextBox { Location = new Point(90, 52), Width = 80 };
            grpGeneral.Controls.Add(txtAge);

            grpGeneral.Controls.Add(new Label { Text = "Weight:", Location = new Point(10, 85), AutoSize = true });
            txtWeight = new TextBox { Location = new Point(90, 82), Width = 80 };
            grpGeneral.Controls.Add(txtWeight);

            grpGeneral.Controls.Add(new Label { Text = "Gender:", Location = new Point(10, 115), AutoSize = true });
            cmbGender = new ComboBox { Location = new Point(90, 112), Width = 120, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbGender.Items.AddRange(new[] { "Male", "Female", "Unknown" });
            cmbGender.SelectedIndex = 2;
            grpGeneral.Controls.Add(cmbGender);

            // Action buttons
            btnAdd = new Button { Text = "Add", Location = new Point(290, 112), Width = 70 };
            btnAdd.Click += BtnAdd_Click;
            grpGeneral.Controls.Add(btnAdd);

            btnChange = new Button { Text = "Change", Location = new Point(370, 112), Width = 70, Enabled = false };
            btnChange.Click += BtnChange_Click;
            grpGeneral.Controls.Add(btnChange);

            btnDelete = new Button { Text = "Delete", Location = new Point(450, 112), Width = 70, Enabled = false };
            btnDelete.Click += BtnDelete_Click;
            grpGeneral.Controls.Add(btnDelete);

            // Output
            var grpOutput = new GroupBox { Text = "Animal Details", Location = new Point(410, 280), Size = new Size(340, 240) };
            txtOutput = new TextBox { Multiline = true, ReadOnly = true, Dock = DockStyle.Fill, ScrollBars = ScrollBars.Vertical, Font = new Font("Consolas", 10) };
            grpOutput.Controls.Add(txtOutput);
            Controls.Add(grpOutput);

            // Bottom buttons
            btnClear = new Button { Text = "Clear Output", Location = new Point(540, 530), Width = 190 };
            btnClear.Click += BtnClear_Click;
            Controls.Add(btnClear);

            btnAbout = new Button { Text = "About", Location = new Point(10, 530), Width = 100 };
            btnAbout.Click += BtnAbout_Click;
            Controls.Add(btnAbout);
        }

       

        private void ChkListAll_CheckedChanged(object sender, EventArgs e)
        {
            bool listMode = chkListAll.Checked;
            lstCategory.Enabled = !listMode;
            lstSpecies.Enabled = !listMode;
            lstAllAnimals.Visible = listMode;

            if (listMode)
            {
                lstCategory.ClearSelected();
                lstSpecies.Items.Clear();
            }
            else
            {
                lstAllAnimals.ClearSelected();
            }
        }

        private void LstCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstSpecies.Items.Clear();
            string cat = lstCategory.SelectedItem?.ToString() ?? "";

            switch (cat)
            {
                case "Mammal": lstSpecies.Items.AddRange(Enum.GetNames(typeof(MammalSpecies))); break;
                case "Reptile": lstSpecies.Items.AddRange(Enum.GetNames(typeof(ReptileSpecies))); break;
                case "Bird": lstSpecies.Items.AddRange(Enum.GetNames(typeof(BirdSpecies))); break;
                case "Insect": lstSpecies.Items.AddRange(Enum.GetNames(typeof(InsectSpecies))); break;
            }
        }

        private void LstAllAnimals_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sp = lstAllAnimals.SelectedItem as string;
            if (sp == null) return;

            _currentAnimal = _animalManager.GetAnimalById(sp) as Animal;
            if (_currentAnimal == null) return;

            // Populate general fields for editing
            txtName.Text = _currentAnimal.Name;
            txtAge.Text = _currentAnimal.Age.ToString();
            txtWeight.Text = _currentAnimal.Weight.ToString("F1");
            cmbGender.SelectedItem = _currentAnimal.Gender.ToString();

            txtOutput.Text = _currentAnimal.ToString();

            // Image preview
            if (!string.IsNullOrEmpty(_currentAnimal.ImagePath) && File.Exists(_currentAnimal.ImagePath))
            {
                try
                {
                    picPreview.Image?.Dispose();
                    picPreview.Image = Image.FromFile(_currentAnimal.ImagePath);
                }
                catch { }
            }
            else picPreview.Image = null;

            btnChange.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            // Same as before opens species form
            string category = chkListAll.Checked
                ? GetCategoryFromSpecies(lstAllAnimals.SelectedItem?.ToString() ?? "")
                : lstCategory.SelectedItem?.ToString() ?? "";

            string speciesStr = chkListAll.Checked
                ? lstAllAnimals.SelectedItem?.ToString()
                : lstSpecies.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(speciesStr))
            {
                MessageBox.Show("Please select a category and species.", "Input required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Form form = null;
            if (category == "Mammal" && Enum.TryParse<MammalSpecies>(speciesStr, out var mSp)) form = new MammalForm(mSp);
            else if (category == "Reptile" && Enum.TryParse<ReptileSpecies>(speciesStr, out var rSp)) form = new ReptileForm(rSp);
            else if (category == "Bird" && Enum.TryParse<BirdSpecies>(speciesStr, out var bSp)) form = new BirdForm(bSp);
            else if (category == "Insect" && Enum.TryParse<InsectSpecies>(speciesStr, out var iSp)) form = new InsectForm(iSp);

            if (form?.ShowDialog() == DialogResult.OK)
            {
                if (category == "Mammal")
                    _currentAnimal = ((MammalForm)form).Animal;
                else if (category == "Reptile")
                    _currentAnimal = ((ReptileForm)form).Animal;
                else if (category == "Bird")
                    _currentAnimal = ((BirdForm)form).Animal;
                else if (category == "Insect")
                    _currentAnimal = ((InsectForm)form).Animal;
                else
                    _currentAnimal = null;

                if (_currentAnimal != null)
                {
                    txtOutput.Text = _currentAnimal.ToString();
                    btnAdd.Enabled = true;
                    // Optional: pre-fill general fields
                    txtName.Text = _currentAnimal.Name;
                    txtAge.Text = _currentAnimal.Age.ToString();
                    txtWeight.Text = _currentAnimal.Weight.ToString("F1");
                    cmbGender.SelectedItem = _currentAnimal.Gender.ToString();
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (_currentAnimal == null)
            {
                MessageBox.Show("No animal created yet. Use 'Create Animal' first.", "Info");
                return;
            }

            // Final validation
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name is required.", "Validation");
                return;
            }

            _currentAnimal.Name = txtName.Text;
            _currentAnimal.Age = int.TryParse(txtAge.Text, out int a) ? a : 0;
            _currentAnimal.Weight = double.TryParse(txtWeight.Text, out double w) ? w : 0;
            _currentAnimal.Gender = Enum.TryParse<GenderType>(cmbGender.Text, true, out var g) ? g : GenderType.Unknown;

            _animalManager.AddAnimal(_currentAnimal);

            RefreshAnimalList();
            txtOutput.Text = _currentAnimal.ToString();
            MessageBox.Show("Animal added successfully!", "Success");

            // Reset for next creation
            _currentAnimal = null;
            btnAdd.Enabled = false;
            ClearGeneralFields();
        }

        private void BtnChange_Click(object sender, EventArgs e)
        {
            if (_currentAnimal == null) return;

            // Apply changes from general fields
            _currentAnimal.Name = txtName.Text;
            _currentAnimal.Age = int.TryParse(txtAge.Text, out int a) ? a : _currentAnimal.Age;
            _currentAnimal.Weight = double.TryParse(txtWeight.Text, out double w) ? w : _currentAnimal.Weight;
            _currentAnimal.Gender = Enum.TryParse<GenderType>(cmbGender.Text, true, out var g) ? g : _currentAnimal.Gender;

            _animalManager.ModifyAnimal(_currentAnimal.Id, _currentAnimal);  

            RefreshAnimalList();
            txtOutput.Text = _currentAnimal.ToString();
            MessageBox.Show("Animal updated successfully!", "Success");
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_currentAnimal == null) return;

            if (MessageBox.Show("Delete this animal?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _animalManager.DeleteAnimal(_currentAnimal.Id);
                RefreshAnimalList();
                txtOutput.Clear();
                _currentAnimal = null;
                ClearGeneralFields();
                btnChange.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void RefreshAnimalList()
        {
            lstAllAnimals.Items.Clear();
            foreach (var animal in _animalManager.GetAllAnimals())
            {
                lstAllAnimals.Items.Add(animal.Id); 
            }
        }

        private void ClearGeneralFields()
        {
            txtName.Clear();
            txtAge.Clear();
            txtWeight.Clear();
            cmbGender.SelectedIndex = 2;
            picPreview.Image = null;
        }

        private void BtnLoadImage_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog { Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp" })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        picPreview.Image?.Dispose();
                        picPreview.Image = Image.FromFile(dlg.FileName);
                        if (_currentAnimal != null)
                            _currentAnimal.ImagePath = dlg.FileName;
                    }
                    catch { }
                }
            }
        }

        private void BtnClear_Click(object sender, EventArgs e) => ClearOutput();

        private void ClearOutput()
        {
            txtOutput.Clear();
            _currentAnimal = null;
            ClearGeneralFields();
            btnAdd.Enabled = false;
            btnChange.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void BtnAbout_Click(object sender, EventArgs e)
        {
            using (var about = new AboutForm())
            {
                about.ShowDialog();
            }
        }
        private string GetCategoryFromSpecies(string species)
        {
            if (species == "Dog" || species == "Cat" || species == "Cow" || species == "Horse")
                return "Mammal";
            if (species == "Turtle" || species == "Lizard" || species == "Snake")
                return "Reptile";
            if (species == "Eagle" || species == "Dove")
                return "Bird";
            if (species == "Butterfly" || species == "Bee" || species == "Ant")
                return "Insect";
            return "";
        }
    }
}
using EAMS.Birds;
using EAMS.Birds.species;
using EAMS.Insects;
using EAMS.Mammals.Species;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EAMS
{
    /// <summary>
    /// MainForm is the central user interface for the EcoPark Animal Management System. 
    /// It allows users to create, view, edit and delete animals of various categories (Mammals, Reptiles, Birds, Insects) 
    /// using a combination of ListBoxes for selection and a ListView for displaying all created animals. 
    /// The form also includes a PictureBox for image preview and TextBoxes for showing detailed species information and habitat/food requirements. 
    /// The design emphasizes usability with clear sections for creating animals, viewing details, and managing the list of animals. 
    /// Error handling is implemented to ensure a smooth user experience.
    /// </summary>
    public partial class MainForm : Form
    {
        private Animal currentAnimal;
        private readonly List<Animal> animals = new List<Animal>();

        private CheckBox chkListAll;
        private ListBox lstCategory;
        private ListBox lstSpecies;
        private ListBox lstAllAnimals;
        private Button btnCreate;
        private Button btnAbout;
        private Button btnClear;
        private PictureBox picPreview;
        private Button btnLoadImage;

        /// Events UI
        private Label lblEvents;
        private ListBox lstEvents;

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

        // ListView for all animals
        private ListView lstAnimals;

        // Right-side textboxes (species-specific info and preferred habitat)
        private TextBox txtSpecInfo;
        private TextBox txtHabitat;

        // Change / Delete buttons
        private Button btnChange;
        private Button btnDelete;
        /// <summary>
        /// Initializes the MainForm with all UI components, event handlers, and layout.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            this.Text = "EcoPark Animal Management System by Ibrahim";
            this.Size = new Size(920, 620);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Menu bar
            MenuStrip menu = new MenuStrip();
            var mnuFile = new ToolStripMenuItem("File");
            var mnuHelp = new ToolStripMenuItem("Help");
            var mnuAbout = new ToolStripMenuItem("About");
            mnuAbout.Click += BtnAbout_Click;
            mnuHelp.DropDownItems.Add(mnuAbout);
            menu.Items.Add(mnuFile);
            menu.Items.Add(mnuHelp);
            this.MainMenuStrip = menu;
            Controls.Add(menu);

            // Create Animal label + lists
            var lblCreate = new Label { Text = "Create Animal", Location = new Point(10, 30), AutoSize = true };
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

            // General Data group 
            grpGeneral = new GroupBox { Text = "General Data", Location = new Point(280, 50), Size = new Size(280, 150) };
            lblName = new Label { Text = "Name", Location = new Point(10, 22), AutoSize = true };
            grpGeneral.Controls.Add(lblName);
            txtName = new TextBox { Location = new Point(85, 20), Width = 180 };
            grpGeneral.Controls.Add(txtName);

            lblAge = new Label { Text = "Age", Location = new Point(10, 52), AutoSize = true };
            grpGeneral.Controls.Add(lblAge);
            txtAge = new TextBox { Location = new Point(85, 50), Width = 80 };
            grpGeneral.Controls.Add(txtAge);

            lblWeight = new Label { Text = "Weight", Location = new Point(10, 82), AutoSize = true };
            grpGeneral.Controls.Add(lblWeight);
            txtWeight = new TextBox { Location = new Point(85, 80), Width = 80 };
            grpGeneral.Controls.Add(txtWeight);

            lblGender = new Label { Text = "Gender", Location = new Point(10, 112), AutoSize = true };
            grpGeneral.Controls.Add(lblGender);
            cmbGender = new ComboBox { Location = new Point(85, 120), Width = 120, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbGender.Items.AddRange(new[] { "Male", "Female", "Unknown" });
            cmbGender.SelectedIndex = 2;
            grpGeneral.Controls.Add(cmbGender);

            btnAdd = new Button { Text = "Add", Location = new Point(200, 90), Width = 70, Enabled = false };
            btnAdd.Click += BtnAdd_Click;
            grpGeneral.Controls.Add(btnAdd);
            Controls.Add(grpGeneral);

            // Image preview 
            picPreview = new PictureBox
            {
                Location = new Point(600, 55),
                Size = new Size(180, 120),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.WhiteSmoke
            };
            Controls.Add(picPreview);

            btnLoadImage = new Button { Text = "Load Image", Location = new Point(600, 180), Width = 180 };
            btnLoadImage.Click += BtnLoadImage_Click;
            Controls.Add(btnLoadImage);

            // ListView for all animals 
            lstAnimals = new ListView
            {
                Location = new Point(10, 290),
                Size = new Size(360, 160),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true
            };
            lstAnimals.Columns.Add("Species", 80);
            lstAnimals.Columns.Add("Id", 120);
            lstAnimals.Columns.Add("Name", 80);
            lstAnimals.Columns.Add("Age", 40);
            lstAnimals.Columns.Add("Weight", 60);
            lstAnimals.Columns.Add("Gender", 60);
            lstAnimals.SelectedIndexChanged += LstAnimals_SelectedIndexChanged;
            Controls.Add(lstAnimals);

            // Change and Delete buttons below the ListView
            btnChange = new Button { Text = "Change", Location = new Point(10, lstAnimals.Bottom + 6), Size = new Size(120, 28) };
            btnChange.Click += BtnChange_Click;
            Controls.Add(btnChange);

            btnDelete = new Button { Text = "Delete", Location = new Point(140, lstAnimals.Bottom + 6), Size = new Size(120, 28) };
            btnDelete.Click += BtnDelete_Click;
            Controls.Add(btnDelete);

            // Right side textboxes for species info and habitat/food info
            txtSpecInfo = new TextBox
            {
                Location = new Point(380, 290),
                Size = new Size(200, 160),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Consolas", 10)
            };
            Controls.Add(txtSpecInfo);

            txtHabitat = new TextBox
            {
                Location = new Point(590, 290),
                Size = new Size(240, 160),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Consolas", 10)
            };
            Controls.Add(txtHabitat);

            // Events label and list
            lblEvents = new Label { Text = "Upcoming Events", Location = new Point(590, 455), AutoSize = true };
            lstEvents = new ListBox { Location = new Point(590, 475), Size = new Size(240, 80) };
            Controls.Add(lblEvents);
            Controls.Add(lstEvents);

            // Bottom buttons
            btnAbout = new Button { Text = "About", Location = new Point(10, 520), Width = 100 };
            btnAbout.Click += BtnAbout_Click;
            Controls.Add(btnAbout);

            btnClear = new Button { Text = "Clear Output", Location = new Point(600, 520), Width = 240 };
            btnClear.Click += BtnClear_Click;
            Controls.Add(btnClear);
        }
        /// <summary>
        /// Handles the Load Image button click event. Opens a file dialog to select an image file, and if a valid image is selected, it loads the image into the PictureBox for preview. 
        /// It also updates the current animal's ImagePath property if an animal is currently being created or edited. Error handling is included to ignore any issues with loading the image file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        if (currentAnimal != null) currentAnimal.ImagePath = dlg.FileName;
                    }
                    catch { /* ignore image load errors */ }
                }
            }
        }
        
        /// <summary>
        /// Handles the CheckedChanged event of the ChkListAll checkbox. Enables or disables the category and species lists based on the checkbox state.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
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
        /// Updates the species list based on the selected category in the category list.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
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
        /// Handles the SelectedIndexChanged event of the lstAllAnimals ListBox. When a species is selected from the list of all animals, it determines the corresponding category and updates the category and species lists to reflect the selection. 
        /// This allows users to quickly select a species and have the appropriate category and species lists updated accordingly. Error handling is included to ensure that invalid selections do not cause issues.
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
        /// Handles the SelectedIndexChanged event of the lstAnimals ListView. When an animal is selected from the list, 
        /// it retrieves the corresponding Animal object from the internal list based on the selected item's ID.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void LstAnimals_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstAnimals.SelectedItems.Count == 0)
                {
                    txtSpecInfo.Clear();
                    txtHabitat.Clear();
                    lstEvents.Items.Clear();
                    picPreview.Image?.Dispose();
                    picPreview.Image = null;
                    return;
                }

                var item = lstAnimals.SelectedItems[0];
                string id = item.SubItems.Count > 1 ? (item.SubItems[1].Text ?? string.Empty) : string.Empty;
                if (string.IsNullOrEmpty(id))
                {
                    txtSpecInfo.Clear();
                    txtHabitat.Clear();
                    lstEvents.Items.Clear();
                    return;
                }

                var selectedAnimal = animals.Find(a => string.Equals(a.Id, id, StringComparison.OrdinalIgnoreCase));
                if (selectedAnimal == null) return;

                // Build and show detailed species info and habitat/food info using project originals
                txtSpecInfo.Text = BuildSpeciesInfo(selectedAnimal);
                txtHabitat.Text = BuildHabitatInfo(selectedAnimal);

                // Populate events list (uses IAnimal.GetUpcomingEvents)
                lstEvents.Items.Clear();
                try
                {
                    var events = selectedAnimal.GetUpcomingEvents();
                    if (events != null && events.Count > 0)
                    {
                        foreach (var ev in events) lstEvents.Items.Add(ev);
                    }
                    else
                    {
                        lstEvents.Items.Add("(no scheduled events)");
                    }
                }
                catch
                {
                    lstEvents.Items.Add("(events unavailable)");
                }

                if (!string.IsNullOrEmpty(selectedAnimal.ImagePath) && File.Exists(selectedAnimal.ImagePath))
                {
                    try
                    {
                        picPreview.Image?.Dispose();
                        picPreview.Image = Image.FromFile(selectedAnimal.ImagePath);
                    }
                    catch { picPreview.Image = null; }
                }
                else
                {
                    picPreview.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating selection: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Builds a formatted string containing detailed information about the specified animal, including
        /// species specific attributes.
        /// </summary>
        /// <param name="a">The animal instance for which to build the information string.</param>
        /// <returns>A string with formatted details about the animal and its species.</returns>
        private string BuildSpeciesInfo(Animal a)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"ID: {a.Id}");
            sb.AppendLine($"Name: {a.Name}");
            sb.AppendLine($"Age: {a.Age}");
            sb.AppendLine($"Weight: {a.Weight:F1}");
            sb.AppendLine($"Gender: {a.Gender}");

            // Category specific fields
            if (a is Mammal m)
            {
                sb.AppendLine($"Number of Teeth: {m.NumberOfTeeth}");
                sb.AppendLine($"Tail Length: {m.TailLength} cm");

                // species level mammals
                if (m is Dog d) { sb.AppendLine($"Breed: {d.Breed}"); sb.AppendLine($"Trained: {d.IsTrained}"); }
                else if (m is Cat c) { sb.AppendLine($"Fur Color: {c.FurColor}"); sb.AppendLine($"Indoor: {c.IsIndoor}"); }
                else if (m is Cow cow) { sb.AppendLine($"Milk Production: {cow.MilkProduction} L/day"); }
                else if (m is Horse h) { sb.AppendLine($"Breed: {h.Breed}"); sb.AppendLine($"Is Racing: {h.IsRacing}"); }
            }
            else if (a is Reptile r)
            {
                sb.AppendLine($"Body Length: {r.BodyLength} cm");
                sb.AppendLine($"Lives in Water: {r.LivesInWater}");
                sb.AppendLine($"Aggression level: {r.AggressivenessLevel}/10");

                if (r is Turtle t) { sb.AppendLine($"Shell hardness: {t.ShellHardness}"); sb.AppendLine($"Shell width: {t.ShellWidth} cm"); }
                else if (r is Lizard liz) { sb.AppendLine($"Color: {liz.Color}"); sb.AppendLine($"Can climb: {liz.CanClimb}"); }
                else if (r is Reptiles.Species.Snake s) { sb.AppendLine($"Length: {s.Length} cm"); sb.AppendLine($"Venomous: {s.IsVenomous}"); }
                else if (r is Reptiles.Species.Crocodile croc) { sb.AppendLine($"Jaw Strength: {croc.JawStrength}"); sb.AppendLine($"Saltwater: {croc.IsSaltwater}"); }
            }
            else if (a is Bird b)
            {
                sb.AppendLine($"Wingspan: {b.Wingspan} cm");
                sb.AppendLine($"Tail Length: {b.TailLength} cm");

                if (b is Eagle e) sb.AppendLine($"Is Bald Eagle: {e.IsBald}");
                else if (b is Peacock p) sb.AppendLine($"Plume Color: {p.PlumeColor}");
                else if (b is Dove dv) sb.AppendLine($"Feather Color: {dv.FeatherColor}");
            }
            else if (a is Insects.Insect ins)
            {
                sb.AppendLine($"Number of Wings: {ins.NumberOfWings}");
                sb.AppendLine($"Antenna Length: {ins.AntennaLength} mm");

                // Some insect species
                var lady = ins as Insects.Species.Ladybug;
                if (lady != null) sb.AppendLine($"Spot Count: {lady.SpotCount}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Builds a formatted string containing habitat information, daily food requirements, lifespan, and image path
        /// for the specified animal.
        /// </summary>
        /// <param name="a">The animal for which habitat information is generated.</param>
        /// <returns>A formatted string with habitat details, food requirements, lifespan, and image path.</returns>
        private string BuildHabitatInfo(Animal a)
        {
            var sb = new StringBuilder();

            // Preferred Habitat (basic heuristics; adjust per species as needed)
            string preferred;
            string typeName = a.GetType().Name;
            if (typeName == "Dog" || typeName == "Cat")
                preferred = "Domestic/Urban";
            else if (typeName == "Cow" || typeName == "Horse")
                preferred = "Farm/Pasture";
            else if (typeName == "Turtle")
                preferred = "Freshwater / Shorelines";
            else if (typeName == "Lizard")
                preferred = "Rocky/Woodland";
            else if (typeName == "Snake")
                preferred = "Various (forest/grassland)";
            else if (typeName == "Crocodile")
                preferred = "Freshwater/Saltwater estuaries";
            else if (typeName == "Eagle")
                preferred = "Mountains/Coastlines/Forest";
            else if (typeName == "Dove")
                preferred = "Urban/Parks/Forest edge";
            else if (typeName == "Peacock")
                preferred = "Forest edge/Garden";
            else
                preferred = "General / Various";

            sb.AppendLine($"Preferred Habitat: {preferred}");
            sb.AppendLine();

            // Daily food requirement if available (category classes implement DailyFoodRequirement)
            try
            {
                if (a is Mammal mm)
                {
                    var dict = mm.DailyFoodRequirement();
                    if (dict != null && dict.Count > 0)
                    {
                        sb.AppendLine("Daily Food Requirements:");
                        foreach (var kv in dict)
                        {
                            sb.AppendLine($"- {kv.Key}: {kv.Value}");
                        }
                        sb.AppendLine();
                        sb.AppendLine($"Lifespan: {mm.GetAverageLifeSpan()} years");
                    }
                }
                else if (a is Reptile rr)
                {
                    var dict = rr.DailyFoodRequirement();
                    if (dict != null && dict.Count > 0)
                    {
                        sb.AppendLine("Daily Food Requirements:");
                        foreach (var kv in dict) sb.AppendLine($"- {kv.Key}: {kv.Value}");
                        sb.AppendLine();
                        sb.AppendLine($"Lifespan: {rr.GetAverageLifeSpan()} years");
                    }
                }
                else if (a is Bird bb)
                {
                    var dict = bb.DailyFoodRequirement();
                    if (dict != null && dict.Count > 0)
                    {
                        sb.AppendLine("Daily Food Requirements:");
                        foreach (var kv in dict) sb.AppendLine($"- {kv.Key}: {kv.Value}");
                        sb.AppendLine();
                        sb.AppendLine($"Lifespan: {bb.GetAverageLifeSpan()} years");
                    }
                }
                else if (a is Insects.Insect ii)
                {
                    var dict = ii.DailyFoodRequirement();
                    if (dict != null && dict.Count > 0)
                    {
                        sb.AppendLine("Daily Food Requirements:");
                        foreach (var kv in dict) sb.AppendLine($"- {kv.Key}: {kv.Value}");
                        sb.AppendLine();
                        sb.AppendLine($"Lifespan: {ii.GetAverageLifeSpan()} years");
                    }
                }
            }
            catch
            {
                // ignore if specific implementation missing
            }

            // Show image path at bottom
            sb.AppendLine();
            sb.AppendLine($"Image Path: {a.ImagePath ?? string.Empty}");

            return sb.ToString();
        }
        /// <summary>
        /// Determines the category of an animal based on its species name. 
        /// This is used to map a selected species from the "List all animal species" list back to its corresponding category (Mammal, Reptile, Bird, Insect) when creating a new animal.
        /// </summary>
        /// <param name="species">The species name of the animal.</param>
        /// <returns>The category of the animal (Mammal, Reptile, Bird, Insect) or an empty string if the species is not recognized.</returns>
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
        /// Handles the Click event of the BtnCreate button. This method is responsible for creating a new animal based on the user's selection of category and species.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
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
                if (category == "Mammal" && Enum.TryParse<MammalSpecies>(speciesStr, out var mSp)) form = new MammalForm(mSp);
                else if (category == "Reptile" && Enum.TryParse<ReptileSpecies>(speciesStr, out var rSp)) form = new ReptileForm(rSp);
                else if (category == "Bird" && Enum.TryParse<BirdSpecies>(speciesStr, out var bSp)) form = new BirdForm(bSp);
                else if (category == "Insect" && Enum.TryParse<InsectSpecies>(speciesStr, out var iSp)) form = new InsectForm(iSp);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (form == null) return;

            try
            {
                if (form.ShowDialog() != DialogResult.OK) return;

                // SAFELY obtain Animal from the specific form type
                if (form is MammalForm mf) currentAnimal = mf.Animal;
                else if (form is ReptileForm rf) currentAnimal = rf.Animal;
                else if (form is BirdForm bf) currentAnimal = bf.Animal;
                else if (form is InsectForm inf) currentAnimal = inf.Animal;
                else currentAnimal = null;

                if (currentAnimal == null)
                {
                    MessageBox.Show("Animal creation failed: Form did not produce an Animal.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Load image preview immediately if available
                if (!string.IsNullOrEmpty(currentAnimal.ImagePath) && File.Exists(currentAnimal.ImagePath))
                {
                    try
                    {
                        picPreview.Image?.Dispose();
                        picPreview.Image = Image.FromFile(currentAnimal.ImagePath);
                    }
                    catch { picPreview.Image = null; }
                }
                else
                {
                    picPreview.Image = null;
                }

                // build and show detailed species info and habitat/food info using project originals
                txtSpecInfo.Text = BuildSpeciesInfo(currentAnimal);
                txtHabitat.Text = BuildHabitatInfo(currentAnimal);

                // pre-fill the general data fields with the current animal's properties (if any)
                txtName.Text = currentAnimal.Name ?? string.Empty;
                txtAge.Text = currentAnimal.Age.ToString();
                txtWeight.Text = currentAnimal.Weight.ToString("F1");

                string genderText = currentAnimal.Gender.ToString();
                if (cmbGender.Items.Contains(genderText))
                    cmbGender.SelectedItem = genderText;
                else
                    cmbGender.SelectedIndex = Math.Max(0, cmbGender.Items.IndexOf("Unknown"));

                btnAdd.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unhandled error while creating animal:\r\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Handles the Click event of the BtnAdd button. This method validates the general data input fields, 
        /// updates the current animal's properties with the entered data, and adds the animal to the internal list and ListView.
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

            animals.Add(currentAnimal);
            RefreshListView();

            // update the two info boxes
            txtSpecInfo.Text = BuildSpeciesInfo(currentAnimal);
            txtHabitat.Text = BuildHabitatInfo(currentAnimal);

            MessageBox.Show("Animal added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnAdd.Enabled = false;
            currentAnimal = null;
            txtName.Clear();
            txtAge.Clear();
            txtWeight.Clear();
            cmbGender.SelectedIndex = 2;
        }
        /// <summary>
        /// Handles the Click event of the BtnDelete button. 
        /// This method checks if an animal is selected in the ListView, retrieves the corresponding Animal object from the internal list, and removes it from both the list and the ListView. 
        /// It also clears the species information and habitat textboxes after deletion.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (lstAnimals.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an animal to delete.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var item = lstAnimals.SelectedItems[0];
            string id = item.SubItems.Count > 1 ? (item.SubItems[1].Text ?? string.Empty) : string.Empty;

            var animalToRemove = animals.Find(a => string.Equals(a.Id, id, StringComparison.OrdinalIgnoreCase));
            if (animalToRemove != null)
            {
                animals.Remove(animalToRemove);
                RefreshListView();
                txtSpecInfo.Clear();
                txtHabitat.Clear();
                MessageBox.Show("Animal deleted successfully.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Handles the Click event of the BtnChange button. This method allows the user to edit the details of a selected animal. 
        /// It retrieves the selected animal, opens the appropriate edit form pre-populated with the animal's current data, and if the user saves changes, 
        /// it updates the animal's properties and refreshes the ListView and information boxes accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnChange_Click(object sender, EventArgs e)
        {
            if (lstAnimals.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an animal to change.", "Change", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var item = lstAnimals.SelectedItems[0];
            string id = item.SubItems.Count > 1 ? (item.SubItems[1].Text ?? string.Empty) : string.Empty;

            var animalToEdit = animals.Find(a => string.Equals(a.Id, id, StringComparison.OrdinalIgnoreCase));
            if (animalToEdit == null)
            {
                MessageBox.Show("Selected animal not found.", "Change", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Form form = null;
            try
            {
                // Choose correct edit form overload that accepts existing instance
                if (animalToEdit is Mammal mm) form = new MammalForm(mm);
                else if (animalToEdit is Reptile rr) form = new ReptileForm(rr);
                else if (animalToEdit is Bird bb) form = new BirdForm(bb);
                else if (animalToEdit is Insects.Insect ii) form = new InsectForm(ii);
                else form = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating edit form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (form == null) return;

            try
            {
                if (form.ShowDialog() != DialogResult.OK) return;

                // SAFELY obtain updated Animal from the specific form type
                var newAnimal = (form as dynamic).Animal as Animal;
                if (newAnimal == null) return;

                // Update the existing animal's properties with the new values from the form.
                animalToEdit.Name = newAnimal.Name;
                animalToEdit.Age = newAnimal.Age;
                animalToEdit.Weight = newAnimal.Weight;
                animalToEdit.Gender = newAnimal.Gender;
                animalToEdit.ImagePath = newAnimal.ImagePath;

                // Copy category specific data 
                if (animalToEdit is Mammal origM && newAnimal is Mammal newM)
                {
                    origM.NumberOfTeeth = newM.NumberOfTeeth;
                    origM.TailLength = newM.TailLength;

                    if (origM is Dog oDog && newM is Dog nDog) { oDog.Breed = nDog.Breed; oDog.IsTrained = nDog.IsTrained; }
                    else if (origM is Cat oCat && newM is Cat nCat) { oCat.FurColor = nCat.FurColor; oCat.IsIndoor = nCat.IsIndoor; }
                    else if (origM is Cow oCow && newM is Cow nCow) { oCow.MilkProduction = nCow.MilkProduction; }
                    else if (origM is Horse oHorse && newM is Horse nHorse) { oHorse.Breed = nHorse.Breed; oHorse.IsRacing = nHorse.IsRacing; }
                }
                else if (animalToEdit is Reptile origR && newAnimal is Reptile newR)
                {
                    origR.BodyLength = newR.BodyLength;
                    origR.LivesInWater = newR.LivesInWater;
                    origR.AggressivenessLevel = newR.AggressivenessLevel;

                    if (origR is Turtle oT && newR is Turtle nT) { oT.ShellHardness = nT.ShellHardness; oT.ShellWidth = nT.ShellWidth; }
                    else if (origR is Lizard oL && newR is Lizard nL) { oL.Color = nL.Color; oL.CanClimb = nL.CanClimb; }
                    else if (origR is Reptiles.Species.Snake oS && newR is Reptiles.Species.Snake nS) { oS.Length = nS.Length; oS.IsVenomous = nS.IsVenomous; }
                    else if (origR is Reptiles.Species.Crocodile oC && newR is Reptiles.Species.Crocodile nC) { oC.JawStrength = nC.JawStrength; oC.IsSaltwater = nC.IsSaltwater; }
                }
                else if (animalToEdit is Bird origB && newAnimal is Bird newB)
                {
                    origB.Wingspan = newB.Wingspan;
                    origB.TailLength = newB.TailLength;

                    if (origB is Eagle oE && newB is Eagle nE) oE.IsBald = nE.IsBald;
                    else if (origB is Peacock oP && newB is Peacock nP) oP.PlumeColor = nP.PlumeColor;
                    else if (origB is Dove oD && newB is Dove nD) oD.FeatherColor = nD.FeatherColor;
                }
                else if (animalToEdit is Insects.Insect origI && newAnimal is Insects.Insect newI)
                {
                    origI.NumberOfWings = newI.NumberOfWings;
                    origI.AntennaLength = newI.AntennaLength;

                    var oLady = origI as Insects.Species.Ladybug;
                    var nLady = newI as Insects.Species.Ladybug;
                    if (oLady != null && nLady != null) oLady.SpotCount = nLady.SpotCount;
                }

                RefreshListView();
                txtSpecInfo.Text = BuildSpeciesInfo(animalToEdit);
                txtHabitat.Text = BuildHabitatInfo(animalToEdit);

                // Update preview image
                if (!string.IsNullOrEmpty(animalToEdit.ImagePath) && File.Exists(animalToEdit.ImagePath))
                {
                    try
                    {
                        picPreview.Image?.Dispose();
                        picPreview.Image = Image.FromFile(animalToEdit.ImagePath);
                    }
                    catch { picPreview.Image = null; }
                }
                else
                {
                    picPreview.Image = null;
                }

                MessageBox.Show("Animal updated successfully.", "Change", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unhandled error while changing animal:\r\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Refreshes the ListView that displays the list of animals. 
        /// This method clears the existing items in the ListView and repopulates it with the current list of animals, showing their category, ID, name, age, weight
        /// </summary>
        private void RefreshListView()
        {
            lstAnimals.Items.Clear();
            foreach (var animal in animals)
            {
                var item = new ListViewItem(new[]
                {
                    animal.GetType().Name ?? string.Empty,
                    animal.Id ?? string.Empty,
                    animal.Name ?? string.Empty,
                    animal.Age.ToString(),
                    animal.Weight.ToString("F1"),
                    animal.Gender.ToString()
                });
                lstAnimals.Items.Add(item);
            }
        }
        /// <summary>
        /// Handles the Click event of the BtnAbout button. This method opens the AboutForm as a modal dialog, allowing users to view information about the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAbout_Click(object sender, EventArgs e)
        {
            using (var about = new AboutForm()) about.ShowDialog();
        }
        /// <summary>
        /// Clears all input fields, resets controls, and disposes the preview image in the animal entry form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtSpecInfo.Clear();
            txtHabitat.Clear();
            lstEvents.Items.Clear();
            currentAnimal = null;
            btnAdd.Enabled = false;
            txtName.Clear();
            txtAge.Clear();
            txtWeight.Clear();
            cmbGender.SelectedItem = null;
            if (picPreview.Image != null) { picPreview.Image.Dispose(); picPreview.Image = null; }
        }
    }
}
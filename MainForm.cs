using EAMS.Birds;
using EAMS.Birds.species;
using EAMS.Exceptions;
using EAMS.Insects;
using EAMS.Mammals.Species;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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

        // Persistence state
        private string _currentFilePath = string.Empty;
        private FileFormat _currentFormat = FileFormat.Unknown;

        private enum FileFormat { Unknown, Text, Json, Xml }


        private CheckBox chkListAll;
        private ListBox lstCategory;
        private ListBox lstSpecies;
        private ListBox lstAllAnimals;
        private Button btnCreate;
        private Button btnAbout;
        private Button btnClear;
        private PictureBox picPreview;
        private Button btnLoadImage;

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

        /// Events UI
        private Label lblEvents;
        private ListBox lstEvents;

        // Search / Filter UI
        private Label lblSearch;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnClearSearch;
        private ListBox lstSearchResults;

        private Label lblFilter;
        private ComboBox cmbFilterCategory;
        private Label lblMinAge;
        private TextBox txtMinAge;
        private Label lblMaxAge;
        private TextBox txtMaxAge;
        private Button btnFilter;
        private Button btnClearFilter;

        // ListView for all animals
        private ListView lstAnimals;

        // Right side textboxes (species specific info and preferred habitat)
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
            this.Size = new Size(920, 680);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Menu bar
            MenuStrip menu = new MenuStrip();
            var mnuFile = new ToolStripMenuItem("File");
            var mnuHelp = new ToolStripMenuItem("Help");

            // File menu items
            var mnuFileNew = new ToolStripMenuItem("New", null, MnuFileNew_Click) { ShortcutKeys = Keys.Control | Keys.N };
            var mnuFileOpen = new ToolStripMenuItem("Open...", null, MnuFileOpen_Click);
            var mnuFileSave = new ToolStripMenuItem("Save", null, MnuFileSave_Click) { ShortcutKeys = Keys.Control | Keys.S };
            var mnuFileSaveAs = new ToolStripMenuItem("Save As...", null, MnuFileSaveAs_Click);
            var mnuFileExit = new ToolStripMenuItem("Exit", null, (s, e) => this.Close()) { ShortcutKeys = Keys.Alt | Keys.F4 };

            mnuFile.DropDownItems.AddRange(new ToolStripItem[] { mnuFileNew, mnuFileOpen, new ToolStripSeparator(), mnuFileSave, mnuFileSaveAs, new ToolStripSeparator(), mnuFileExit });

            // Help to About
            var mnuAbout = new ToolStripMenuItem("About", null, BtnAbout_Click);

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

            // Search UI (left column, above ListView)
            lblSearch = new Label { Text = "Search (ID or Name):", Location = new Point(10, 330), AutoSize = true };
            Controls.Add(lblSearch);
            txtSearch = new TextBox { Location = new Point(10, 350), Size = new Size(200, 22) };
            Controls.Add(txtSearch);
            btnSearch = new Button { Text = "Search", Location = new Point(220, 348), Size = new Size(70, 24) };
            btnSearch.Click += BtnSearch_Click;
            Controls.Add(btnSearch);
            btnClearSearch = new Button { Text = "Clear", Location = new Point(300, 348), Size = new Size(60, 24) };
            btnClearSearch.Click += BtnClearSearch_Click;
            Controls.Add(btnClearSearch);

            lstSearchResults = new ListBox { Location = new Point(10, 380), Size = new Size(350, 90) };
            lstSearchResults.DoubleClick += LstSearchResults_DoubleClick;
            Controls.Add(lstSearchResults);

            // Filter UI (left column, below search)
            lblFilter = new Label { Text = "Filter (Category / Age):", Location = new Point(10, 480), AutoSize = true };
            Controls.Add(lblFilter);
            cmbFilterCategory = new ComboBox { Location = new Point(10, 500), Size = new Size(120, 22), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbFilterCategory.Items.AddRange(new[] { "All", "Mammal", "Reptile", "Bird", "Insect" });
            cmbFilterCategory.SelectedIndex = 0;
            Controls.Add(cmbFilterCategory);

            lblMinAge = new Label { Text = "Min Age:", Location = new Point(140, 502), AutoSize = true };
            Controls.Add(lblMinAge);
            txtMinAge = new TextBox { Location = new Point(190, 500), Size = new Size(50, 22) };
            Controls.Add(txtMinAge);

            lblMaxAge = new Label { Text = "Max Age:", Location = new Point(250, 502), AutoSize = true };
            Controls.Add(lblMaxAge);
            txtMaxAge = new TextBox { Location = new Point(305, 500), Size = new Size(50, 22) };
            Controls.Add(txtMaxAge);

            btnFilter = new Button { Text = "Apply Filter", Location = new Point(360, 498), Size = new Size(90, 24) };
            btnFilter.Click += BtnFilter_Click;
            Controls.Add(btnFilter);

            btnClearFilter = new Button { Text = "Clear Filter", Location = new Point(460, 498), Size = new Size(90, 24) };
            btnClearFilter.Click += BtnClearFilter_Click;
            Controls.Add(btnClearFilter);

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
                Location = new Point(10, 585),
                Size = new Size(760, 160),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true
            };
            lstAnimals.Columns.Add("Species", 100);
            lstAnimals.Columns.Add("Id", 200);
            lstAnimals.Columns.Add("Name", 150);
            lstAnimals.Columns.Add("Age", 60);
            lstAnimals.Columns.Add("Weight", 80);
            lstAnimals.Columns.Add("Gender", 80);
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
            lblEvents = new Label { Text = "Upcoming Events", Location = new Point(380, 455), AutoSize = true };
            lstEvents = new ListBox { Location = new Point(380, 475), Size = new Size(450, 80) };
            Controls.Add(lblEvents);
            Controls.Add(lstEvents);

            // Bottom buttons
            btnAbout = new Button { Text = "About", Location = new Point(10, lstAnimals.Bottom + 40), Width = 100 };
            btnAbout.Click += BtnAbout_Click;
            Controls.Add(btnAbout);

            btnClear = new Button { Text = "Clear Output", Location = new Point(600, lstAnimals.Bottom + 40), Width = 240 };
            btnClear.Click += BtnClear_Click;
            Controls.Add(btnClear);
        }
        /// <summary>
        /// Handles the click event for the "New" menu item. 
        /// It prompts the user to confirm resetting to the initial state if there are unsaved animals, 
        /// and if confirmed, it clears all animal data, resets the UI, and clears the current file path and format state.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void MnuFileNew_Click(object sender, EventArgs e)
        {
            if (animals.Count > 0)
            {
                var res = MessageBox.Show("Reset to initial state? Unsaved data will be lost.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res != DialogResult.Yes) return;
            }

            animals.Clear();
            RefreshListView();
            txtSpecInfo.Clear();
            txtHabitat.Clear();
            lstEvents.Items.Clear();
            lstSearchResults.Items.Clear();
            _currentFilePath = string.Empty;
            _currentFormat = FileFormat.Unknown;
        }
        /// <summary>
        /// Handles the click event for the "Open" menu item. It opens a file dialog to select a JSON, text, or XML file, 
        /// and if a valid file is selected, it attempts to load the animal data from the file.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void MnuFileOpen_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "JSON Files (*.json)|*.json|Text Files (*.txt)|*.txt|XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
                ofd.InitialDirectory = Application.StartupPath;
                if (ofd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    LoadFromFile(ofd.FileName);
                    _currentFilePath = ofd.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to open file:\r\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Handles the File Save menu action by saving the current file or prompting for a file path if none is set.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data associated with the click event.</param>
        private void MnuFileSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_currentFilePath))
            {
                MnuFileSaveAs_Click(sender, e);
                return;
            }

            try
            {
                SaveToFile(_currentFilePath, _currentFormat);
                MessageBox.Show("Saved successfully.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DuplicateAnimalException dex)
            {
                MessageBox.Show("Validation error before save:\r\n" + dex.Message, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save file:\r\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        /// Represents a search result with an identifier and label.
        /// </summary>

        private class SearchResult
        {
            public string Id { get; set; }
            public string Label { get; set; }
            public override string ToString() => Label;
        }
        /// <summary>
        /// Handles the search button click event by filtering the animal list based on the search term and displaying
        /// the results.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string term = (txtSearch.Text ?? "").Trim();
            lstSearchResults.Items.Clear();
            if (string.IsNullOrEmpty(term)) return;

            try
            {
                // Use LINQ query syntax as required by the assignment
                var query =
                    from a in animals
                    let name = a.Name ?? string.Empty
                    where a.Id.Equals(term, StringComparison.OrdinalIgnoreCase)
                          || name.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0
                    orderby a.Name, a.Age
                    select a;

                foreach (var a in query)
                {
                    lstSearchResults.Items.Add(new SearchResult { Id = a.Id, Label = $"{a.Name} ({a.GetType().Name}) - {a.Id}" });
                }

                if (lstSearchResults.Items.Count == 0)
                {
                    lstSearchResults.Items.Add(new SearchResult { Id = string.Empty, Label = "(no matches)" });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search failed:\r\n" + ex.ToString(), "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Clears the search text box and the search results list.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void BtnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            lstSearchResults.Items.Clear();
        }
        /// <summary>
        /// Handles the double-click event on the search results list, selecting and focusing the corresponding item in
        /// the main animal list.
        /// </summary>
        /// <param name="sender">The source of the event, typically the search results list.</param>
        /// <param name="e">An EventArgs object containing the event data.</param>
        private void LstSearchResults_DoubleClick(object sender, EventArgs e)
        {
            if (lstSearchResults.SelectedItem is SearchResult sr && !string.IsNullOrEmpty(sr.Id))
            {
                // Select the corresponding item in the main ListView
                for (int i = 0; i < lstAnimals.Items.Count; i++)
                {
                    var it = lstAnimals.Items[i];
                    if (it.SubItems.Count > 1 && string.Equals(it.SubItems[1].Text, sr.Id, StringComparison.OrdinalIgnoreCase))
                    {
                        lstAnimals.Items[i].Selected = true;
                        lstAnimals.EnsureVisible(i);
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// Handles the filter button click event, applies category and age filters to the animal list, and updates the
        /// search results.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        
        private void BtnFilter_Click(object sender, EventArgs e)
        {
            string cat = cmbFilterCategory.SelectedItem?.ToString() ?? "All";
            int minAge = 0;
            int maxAge = int.MaxValue;
            if (!string.IsNullOrWhiteSpace(txtMinAge.Text) && !int.TryParse(txtMinAge.Text, out minAge))
            {
                MessageBox.Show("Invalid min age", "Filter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrWhiteSpace(txtMaxAge.Text) && !int.TryParse(txtMaxAge.Text, out maxAge))
            {
                MessageBox.Show("Invalid max age", "Filter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var query =
                    from a in animals
                    where (cat == "All" || string.Equals(GetCategoryFromType(a), cat, StringComparison.OrdinalIgnoreCase))
                          && a.Age >= minAge && a.Age <= maxAge
                    orderby a.Name, a.Age
                    select a;

                lstSearchResults.Items.Clear();
                foreach (var a in query)
                {
                    lstSearchResults.Items.Add(new SearchResult { Id = a.Id, Label = $"{a.Name} ({a.GetType().Name}) - Age:{a.Age}" });
                }

                if (lstSearchResults.Items.Count == 0)
                    lstSearchResults.Items.Add(new SearchResult { Id = string.Empty, Label = "(no matches)" });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filter failed:\r\n" + ex.ToString(), "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Clears all filter inputs and search results in the UI.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void BtnClearFilter_Click(object sender, EventArgs e)
        {
            cmbFilterCategory.SelectedIndex = 0;
            txtMinAge.Clear();
            txtMaxAge.Clear();
            lstSearchResults.Items.Clear();
        }
        /// <summary>
        /// Determines the category name of the specified animal based on its type.
        /// </summary>
        /// <param name="a">The animal instance to categorize.</param>
        /// <returns>A string representing the category of the animal, such as "Mammal", "Reptile", "Bird", "Insect", or
        /// "Unknown".</returns>
        private string GetCategoryFromType(Animal a)
        {
            if (a is Mammal) return "Mammal";
            if (a is Reptile) return "Reptile";
            if (a is Bird) return "Bird";
            if (a is Insects.Insect) return "Insect";
            return "Unknown";
        }

        /// <summary>
        /// Handles the CheckedChanged event for the chkListAll control, toggling the enabled state and selection of
        /// related list controls based on the checked state.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs object containing the event data.</param>

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
        /// <param name="e">The event data.</param>
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
        /// Handles the selection change in the list of all animals by updating the category and species selections
        /// accordingly.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
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
        /// Handles the selection change in the animal list, updating species information, habitat details, upcoming
        /// events, and the preview image based on the selected animal.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
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
        /// species-specific properties.
        /// </summary>
        /// <param name="a">The animal for which to build the information string.</param>
        /// <returns>A formatted string with the animal's details.</returns>
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
        /// <param name="a">The animal for which to build habitat information.</param>
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
        /// Returns the animal category corresponding to the specified species.
        /// </summary>
        /// <param name="species">The name of the species to categorize.</param>
        /// <returns>The category of the animal as a string, or an empty string if the species is not recognized.</returns>
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
        /// Returns the category name corresponding to the type of the specified animal.
        /// </summary>
        /// <param name="a">The animal whose category is to be determined.</param>
        /// <returns>A string representing the animal's category, such as "Mammal", "Reptile", "Bird", or "Insect"; returns an
        /// empty string if the type is unrecognized.</returns>
        private string GetCategoryFromType(Animal a)
        {
            if (a is Mammal) return "Mammal";
            if (a is Reptile) return "Reptile";
            if (a is Bird) return "Bird";
            if (a is Insects.Insect) return "Insect";
            return "";
        }
        /// <summary>
        /// Handles the Create button click event, validates user selections, opens the appropriate animal creation
        /// form, and updates the UI with the new animal's details if creation is successful.
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
        /// Handles the Add button click event, validates input fields, adds the current animal to the collection,
        /// updates the UI, and displays relevant messages.
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
        /// Handles the deletion of the selected animal from the list and updates the UI accordingly.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
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
        /// Handles the event for changing the details of a selected animal, displaying an edit form and updating the
        /// animal's information upon confirmation.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
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
        /// Opens the About dialog as a modal window.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void BtnAbout_Click(object sender, EventArgs e)
        {
            using (var about = new AboutForm()) about.ShowDialog();
        }
        /// <summary>
        /// Clears all input fields, resets controls, and removes the current animal selection in the form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtSpecInfo.Clear();
            txtHabitat.Clear();
            lstEvents.Items.Clear();
            lstSearchResults.Items.Clear();
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
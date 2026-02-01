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
    public class ReptileForm : Form
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

        // Species-specific
        private NumericUpDown numShellHardnessOrNothing;
        private TextBox txtColor;
        private NumericUpDown numShellWidth;
        private CheckBox chkCanClimb;

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

            
        }
    }
}

using EAMS.Birds.species;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAMS.Birds
{
    public partial class BirdForm : Form
    {
        private BirdSpecies _species;
        public Animal Animal { get; private set; }

        // Common controls (same as Mammal/Reptile)
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

        // Species-specific
        private CheckBox chkIsBald;             
        private TextBox txtFeatherColor;

        private Button btnOK;
        private Button btnCancel;

        
    }
}

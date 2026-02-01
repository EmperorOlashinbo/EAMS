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

        
    }
}

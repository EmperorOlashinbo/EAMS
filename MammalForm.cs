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
    /// Form for entering mammal data.
    /// </summary>
    public class MammalForm : Form
    {
        private MammalSpecies _species;
        public Animal Animal { get; private set; }

        // Common controls
        private TextBox txtName;
        private NumericUpDown numAge;
        private NumericUpDown numWeight;
        private ComboBox cmbGender;
        private TextBox txtImagePath;
        private Button btnLoadImage;
        private PictureBox picImage;
        private NumericUpDown numTeeth;
        private NumericUpDown numTailLength;

        // Species-specific
        private TextBox txtBreedOrColor;
        private CheckBox chkTrainedOrIndoor;

        private Button btnOK;
        private Button btnCancel;

        
    }
}

using EAMS.Insects.Species;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAMS.Insects
{
    public partial class InsectForm : Form
    {
        private InsectSpecies _species;
        public Animal Animal { get; private set; }

        // Common + category controls 
        private TextBox txtName;
        private NumericUpDown numAge;
        private NumericUpDown numWeight;
        private ComboBox cmbGender;
        private TextBox txtImagePath;
        private Button btnLoadImage;
        private PictureBox picImage;
        private NumericUpDown numWings;
        private NumericUpDown numAntennaLength;

        // Species-specific
        private TextBox txtWingPattern;       
        private CheckBox chkCanSting;         
        private NumericUpDown numHoneyProd;   
        private CheckBox chkIsWorker;         

        private Button btnOK;
        private Button btnCancel;

        
    }
}

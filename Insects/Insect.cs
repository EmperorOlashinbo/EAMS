using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Insects
{
    /// <summary>
    /// Insect class inheriting from Animal.
    /// </summary>
    public class Insect : Animal
    {
        private int _numberOfWings;
        private double _antennaLength;
        /// <summary>
        /// Default constructor. Initializes the insect with default values for number of wings and antenna length.
        /// </summary>
        public Insect() : base()
        {
            _numberOfWings = 0;
            _antennaLength = 0.0;
        }
        /// <summary>
        /// Parameterized constructor for insect specific data, including number of wings and antenna length.
        /// </summary>
        /// <param name="numberOfWings"></param>
        /// <param name="antennaLength"></param>
        public Insect(int numberOfWings, double antennaLength) : base()
        {
            NumberOfWings = numberOfWings;
            AntennaLength = antennaLength;
        }
        /// <summary>
        /// Gets or sets the number of wings. Value is constrained to be non-negative. If a negative value is assigned, it defaults to 0.
        /// </summary>
        public int NumberOfWings
        {
            get => _numberOfWings;
            set => _numberOfWings = value >= 0 ? value : 0;
        }
        /// <summary>
        /// Gets or sets the length of the antenna in millimeters. 
        /// Value is constrained to be non-negative. 
        /// If a negative value is assigned, it defaults to 0.0.
        /// </summary>
        public double AntennaLength
        {
            get => _antennaLength;
            set => _antennaLength = value >= 0 ? value : 0.0;
        }
        /// <summary>
        /// Returns a string representation of the insect, 
        /// including base animal data and insect-specific properties such as number of wings and antenna length.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() +
                   $"\nNumber of Wings: {NumberOfWings}" +
                   $"\nAntenna Length: {AntennaLength} mm";
        }
    }
}

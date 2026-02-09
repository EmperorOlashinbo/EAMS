using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Insects.Species
{
    /// <summary>
    /// Derived class for butterflies, adding wing pattern property.
    /// </summary>
    public class Butterfly : Insect
    {
        private string _wingPattern;
        /// <summary>
        /// Default constructor. Initializes the butterfly with an empty wing pattern.
        /// </summary>
        public Butterfly() : base() { _wingPattern = string.Empty; }
        /// <summary>
        /// Parameterized constructor for butterfly specific data, including wing pattern.
        /// </summary>
        /// <param name="numberOfWings"></param>
        /// <param name="antennaLength"></param>
        public Butterfly(int numberOfWings, double antennaLength) : base(numberOfWings, antennaLength)
        {
            _wingPattern = string.Empty;
        }
        /// <summary>
        /// Gets or sets the wing pattern of the butterfly. 
        /// The wing pattern is represented as a string, 
        /// which can describe the colors and shapes found on the butterfly's wings. 
        /// If a null value is assigned, 
        /// it defaults to an empty string to ensure that the property always contains a valid string value.
        /// </summary>
        public string WingPattern
        {
            get => _wingPattern;
            set => _wingPattern = value ?? string.Empty;
        }
        /// <summary>
        /// Returns a string representation including base insect data and wing pattern.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + $"\nWing Pattern: {WingPattern}";
        }
    }
}

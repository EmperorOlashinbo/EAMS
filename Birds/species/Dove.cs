using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Birds.species
{
    /// <summary>
    /// Concrete species class for doves, inheriting from Bird.
    /// </summary>
    public class Dove : Bird
    {
        private string _featherColor;
        /// <summary>
        /// Default constructor. Initializes the dove with an empty feather color.
        /// </summary>
        public Dove() : base() { _featherColor = string.Empty; }
        /// <summary>
        /// Parameterized constructor for dove specific data, including feather color.
        /// </summary>
        /// <param name="wingspan"></param>
        /// <param name="tailLength"></param>
        public Dove(double wingspan, double tailLength) : base(wingspan, tailLength)
        {
            _featherColor = string.Empty;
        }
        /// <summary>
        /// Gets or sets the feather color of the dove.
        /// </summary>
        public string FeatherColor
        {
            get => _featherColor;
            set => _featherColor = value ?? string.Empty;
        }
        /// <summary>
        /// Returns a string representation including base bird data and feather color.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + $"\nFeather Color: {FeatherColor}";
        }
    }
}

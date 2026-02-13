using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Birds.species
{
    /// <summary>
    /// Concrete species class for peacocks, inheriting from Bird.
    /// </summary>
    public class Peacock : Bird
    {
        private string _plumeColor;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Peacock() : base()
        {
            _plumeColor = string.Empty;
        }

        /// <summary>
        /// Parameterized constructor for peacock specific data.
        /// </summary>
        public Peacock(double wingspan, double tailLength) : base(wingspan, tailLength)
        {
            _plumeColor = string.Empty;
        }

        /// <summary>
        /// Gets or sets the plume color of the peacock.
        /// </summary>
        public string PlumeColor
        {
            get => _plumeColor;
            set => _plumeColor = value ?? string.Empty;
        }

        /// <summary>
        /// Returns a string representation including base bird data and plume color.
        /// </summary>
        public override string ToString()
        {
            return base.ToString() + $"\nPlume Color: {PlumeColor}";
        }
    }
}
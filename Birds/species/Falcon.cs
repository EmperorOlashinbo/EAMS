using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Birds.species
{
    /// <summary>
    /// Concrete species class for falcons, inheriting from Bird.
    /// </summary>
    public class Falcon : Bird
    {
        private double _beakLength; // in cm

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Falcon() : base()
        {
            _beakLength = 0.0;
        }

        /// <summary>
        /// Parameterized constructor for falcon specific data.
        /// </summary>
        public Falcon(double wingspan, double tailLength) : base(wingspan, tailLength)
        {
            _beakLength = 0.0;
        }

        /// <summary>
        /// Gets or sets the beak length in centimeters. Value is constrained to be non-negative.
        /// </summary>
        public double BeakLength
        {
            get => _beakLength;
            set => _beakLength = value >= 0 ? value : 0.0;
        }

        /// <summary>
        /// Returns a string representation including base bird data and beak length.
        /// </summary>
        public override string ToString()
        {
            return base.ToString() + $"\nBeak Length: {BeakLength} cm";
        }
    }
}
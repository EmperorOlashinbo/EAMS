using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Reptiles.Species
{
    /// <summary>
    /// Concrete species class for crocodiles, inheriting from Reptile.
    /// </summary>
    public class Crocodile : Reptile
    {
        private int _jawStrength; // scale 0-100
        private bool _isSaltwater;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Crocodile() : base()
        {
            _jawStrength = 0;
            _isSaltwater = false;
        }

        /// <summary>
        /// Parameterized constructor for crocodile specific data.
        /// </summary>
        public Crocodile(double bodyLength, bool livesInWater, int aggressivenessLevel) : base(bodyLength, livesInWater, aggressivenessLevel)
        {
            _jawStrength = 0;
            _isSaltwater = false;
        }

        /// <summary>
        /// Jaw strength on a 0-100 scale. Non-negative and capped at 100.
        /// </summary>
        public int JawStrength
        {
            get => _jawStrength;
            set => _jawStrength = value < 0 ? 0 : (value > 100 ? 100 : value);
        }

        /// <summary>
        /// Indicates whether the crocodile is a saltwater species.
        /// </summary>
        public bool IsSaltwater
        {
            get => _isSaltwater;
            set => _isSaltwater = value;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nJaw Strength: {JawStrength}\nIs Saltwater: {IsSaltwater}";
        }
    }
}
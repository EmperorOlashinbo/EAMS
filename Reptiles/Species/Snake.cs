using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Reptiles.Species
{
    /// <summary>
    /// Concrete species class for snakes, inheriting from Reptile.
    /// </summary>
    public class Snake : Reptile
    {
        private double _length;
        private bool _isVenomous;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Snake() : base()
        {
            _length = 0.0;
            _isVenomous = false;
        }

        /// <summary>
        /// Parameterized constructor passing to base.
        /// </summary>
        /// <param name="bodyLength">Body length.</param>
        /// <param name="livesInWater">Lives in water.</param>
        /// <param name="aggressivenessLevel">Aggressiveness level.</param>
        public Snake(double bodyLength, bool livesInWater, int aggressivenessLevel) : base(bodyLength, livesInWater, aggressivenessLevel)
        {
            _length = 0.0;
            _isVenomous = false;
        }

        public double Length { get => _length; set => _length = value >= 0 ? value : 0.0; }
        public bool IsVenomous { get => _isVenomous; set => _isVenomous = value; }

        /// <summary>
        /// Returns a string representation including base and snake data.
        /// </summary>
        /// <returns>String with snake data.</returns>
        public override string ToString()
        {
            return base.ToString() + $"\nLength: {Length}\nIs Venomous: {IsVenomous}";
        }
    }
}

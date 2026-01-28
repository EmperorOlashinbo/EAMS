using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS
{
    public class Lizard : Reptile
    {
        private string _color;
        private bool _canClimb;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Lizard() : base()
        {
            _color = string.Empty;
            _canClimb = false;
        }
        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="bodyLength">Body length.</param>
        /// <param name="livesInWater">Lives in water</param>
        /// <param name="aggressivenessLevel">Aggressiveness level.</param>
        public Lizard(double bodyLength, bool livesInWater, int aggressivenessLevel) : base(bodyLength, livesInWater, aggressivenessLevel)
        {
            _color = string.Empty;
            _canClimb = false;
        }
        public string Color { get => _color; set => _color = value; }
        public bool CanClimb { get => _canClimb; set => _canClimb = value; }
        
        /// <summary>
        /// returns a string representation of the Lizard object.
        /// </summary>
        /// <returns>String with lizard data</returns>
        public override string ToString()
        {
            return base.ToString() + $"\nColor: {Color}\nCan Climb: {CanClimb}";
        }
    }
}

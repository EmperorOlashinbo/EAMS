using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS
{
    /// <summary>
    /// Concrete species class a Turtle, inheriting from Reptile.
    /// </summary>
    public class Turtle : Reptile
    {
        private int _shellHardness;
        private double _shellWidth;

        /// <summary>
        /// Constructor
        /// </summary>
        public Turtle() : base()
        {
            _shellHardness = 0;
            _shellWidth = 0.0;
        }
        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="bodyLength">Body length.</param>
        /// <param name="livesInWater">Lives in water</param>
        /// <param name="aggressivenessLevel">Aggressiveness level.</param>
        public Turtle(double bodyLength, bool livesInWater, int aggressivenessLevel) : base(bodyLength, livesInWater, aggressivenessLevel)
        {
            _shellHardness = 0;
            _shellWidth = 0.0;
        }
        public int ShellHardness { get => _shellHardness; set { if (value >= 0) _shellHardness = value; } }
        public double ShellWidth { get => _shellWidth; set { if (value >= 0) _shellWidth = value; } }

        /// <summary>
        /// Returns a string representation of the Turtle object.
        /// </summary>
        /// <returns>String with turtle data.</returns>
        public override string ToString()
        {
            return base.ToString() + $"\nShell ´Hardness: {ShellHardness}\nShell Width: {ShellWidth}";
        }

    }
}

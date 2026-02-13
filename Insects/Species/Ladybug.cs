using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Insects.Species
{
    /// <summary>
    /// Concrete species class for ladybugs, inheriting from Insect.
    /// </summary>
    public class Ladybug : Insect
    {
        private int _spotCount;

        public Ladybug() : base()
        {
            _spotCount = 0;
        }

        public Ladybug(int numberOfWings, double antennaLength) : base(numberOfWings, antennaLength)
        {
            _spotCount = 0;
        }

        /// <summary>
        /// Number of spots on the ladybug. Non-negative.
        /// </summary>
        public int SpotCount
        {
            get => _spotCount;
            set => _spotCount = value >= 0 ? value : 0;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nSpot Count: {SpotCount}";
        }
    }
}
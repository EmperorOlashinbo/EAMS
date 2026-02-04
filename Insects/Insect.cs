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

        public Insect() : base()
        {
            _numberOfWings = 0;
            _antennaLength = 0.0;
        }

        public Insect(int numberOfWings, double antennaLength) : base()
        {
            NumberOfWings = numberOfWings;
            AntennaLength = antennaLength;
        }

        public int NumberOfWings
        {
            get => _numberOfWings;
            set => _numberOfWings = value >= 0 ? value : 0;
        }

        public double AntennaLength
        {
            get => _antennaLength;
            set => _antennaLength = value >= 0 ? value : 0.0;
        }

        public override string ToString()
        {
            return base.ToString() +
                   $"\nNumber of Wings: {NumberOfWings}" +
                   $"\nAntenna Length: {AntennaLength} mm";
        }
    }
}

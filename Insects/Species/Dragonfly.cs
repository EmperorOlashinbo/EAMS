using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Insects.Species
{
    /// <summary>
    /// Concrete species class for dragonflies, inheriting from Insect.
    /// </summary>
    public class Dragonfly : Insect
    {
        private double _flightSpeed; // km/h

        public Dragonfly() : base()
        {
            _flightSpeed = 0.0;
        }

        public Dragonfly(int numberOfWings, double antennaLength) : base(numberOfWings, antennaLength)
        {
            _flightSpeed = 0.0;
        }

        /// <summary>
        /// Flight speed in km/h. Non-negative.
        /// </summary>
        public double FlightSpeed
        {
            get => _flightSpeed;
            set => _flightSpeed = value >= 0 ? value : 0.0;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nFlight Speed: {FlightSpeed} km/h";
        }
    }
}
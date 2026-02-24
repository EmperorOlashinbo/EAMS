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
        private double _flightSpeed;
        /// <summary>
        /// Default constructor. Initializes a dragonfly with default values.
        /// </summary>
        public Dragonfly() : base() { _flightSpeed = 0.0; }
        /// <summary>
        /// Parameterized constructor for dragonfly specific data.
        /// </summary>
        /// <param name="numberOfWings">The number of wings of the dragonfly.</param>
        /// <param name="antennaLength">The length of the antenna of the dragonfly in millimeters.</param>
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
        
        /// <summary>
        /// Gets the average lifespan of the dragonfly in weeks.
        /// </summary>
        /// <returns>The average lifespan of the dragonfly in weeks.</returns>
        public override int GetAverageLifeSpan() => 2; // Dragonflies typically live for a few weeks as adults, but can spend years in the nymph stage.

        /// <summary>
        /// Gets the daily food requirements of the dragonfly.
        /// </summary>
        /// <returns>A dictionary containing the daily food requirements of the dragonfly.</returns>
        public override Dictionary<string, string> DailyFoodRequirement() =>
            new Dictionary<string, string>
            {
                ["All day"] = "Flying insects"
            };
        
        /// <summary>
        /// Returns a string representation of the dragonfly, including flight speed.
        /// </summary>
        /// <returns>A string representation of the dragonfly.</returns>
        public override string ToString()
        {
            return base.ToString() + $"\nFlight Speed: {FlightSpeed} km/h";
        }
    }
}
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
        /// <summary>
        /// Default constructor. Initializes a ladybug with default values.
        /// </summary>
        public Ladybug() : base() { _spotCount = 0; }

        /// <summary>
        /// Parameterized constructor for ladybug specific data.
        /// </summary>
        /// <param name="numberOfWings">The number of wings of the ladybug.</param>
        /// <param name="antennaLength">The length of the antenna of the ladybug in millimeters.</param>
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
        
        /// <summary>
        /// Gets the average lifespan of the ladybug in years.
        /// </summary>
        /// <returns>The average lifespan of the ladybug in years.</returns>
        public override int GetAverageLifeSpan() => 1;  // Ladybugs typically live about a year, depending on species and conditions

        /// <summary>
        /// Gets the daily food requirements of the ladybug.
        /// </summary>
        /// <returns>A dictionary containing the daily food requirements of the ladybug.</returns>
        public override Dictionary<string, string> DailyFoodRequirement() =>
            new Dictionary<string, string>
            {
                ["All day"] = "Aphids and small insects"
            };
        
        /// <summary>
        /// Returns a string representation of the ladybug, including spot count.
        /// </summary>
        /// <returns>A string representation of the ladybug.</returns>
        public override string ToString()
        {
            return base.ToString() + $"\nSpot Count: {SpotCount}";
        }
    }
}
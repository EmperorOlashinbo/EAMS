using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Insects.Species
{
    /// <summary>
    /// Derived class for bees, adding stinging ability and honey production properties.
    /// </summary>
    public class Bee : Insect
    {
        private bool _canSting;
        private double _honeyProductionPerDay;
        /// <summary>
        /// Default constructor. Initializes a bee with default values.
        /// </summary>
        public Bee() : base()
        {
            _canSting = true;
            _honeyProductionPerDay = 0.0;
        }
        
        /// <summary>
        /// Parameterized constructor for bee specific data.
        /// </summary>
        /// <param name="numberOfWings">The number of wings of the bee.</param>
        /// <param name="antennaLength">The length of the antenna of the bee in millimeters.</param>
        public Bee(int numberOfWings, double antennaLength) : base(numberOfWings, antennaLength)
        {
            _canSting = true;
            _honeyProductionPerDay = 0.0;
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the bee can sting.
        /// </summary>
        public bool CanSting
        {
            get => _canSting;
            set => _canSting = value;
        }
        
        /// <summary>
        /// Gets or sets the honey production per day in grams.
        /// </summary>
        public double HoneyProductionPerDay
        {
            get => _honeyProductionPerDay;
            set => _honeyProductionPerDay = value >= 0 ? value : 0.0;
        }
        
        /// <summary>
        /// Gets the average lifespan of the bee in weeks.
        /// </summary>
        /// <returns>The average lifespan of the bee in weeks.</returns>
        public override int GetAverageLifeSpan() => 1;   // Worker bees live ~6 weeks
        
        /// <summary>
        /// Gets the daily food requirements of the bee.
        /// </summary>
        /// <returns>A dictionary containing the daily food requirements of the bee.</returns>
        public override Dictionary<string, string> DailyFoodRequirement() =>
            new Dictionary<string, string>
            {
                ["All day"] = "Nectar and pollen"
            };
        
        /// <summary>
        /// Returns a string representation of the bee, including stinging ability and honey production.
        /// </summary>
        /// <returns>A string representation of the bee.</returns>
        public override string ToString()
        {
            return base.ToString() +
                   $"\nCan Sting: {CanSting}" +
                   $"\nHoney Production (g/day): {HoneyProductionPerDay}";
        }
    }
}

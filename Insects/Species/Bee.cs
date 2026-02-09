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
        private double _honeyProductionPerDay;  // in grams
        /// <summary>
        /// Default constructor. Initializes the bee with stinging ability and zero honey production.
        /// </summary>
        public Bee() : base()
        {
            _canSting = true;
            _honeyProductionPerDay = 0.0;
        }
        /// <summary>
        /// Parameterized constructor for bee specific data, including stinging ability and honey production.
        /// </summary>
        /// <param name="numberOfWings"></param>
        /// <param name="antennaLength"></param>
        public Bee(int numberOfWings, double antennaLength) : base(numberOfWings, antennaLength)
        {
            _canSting = true;
            _honeyProductionPerDay = 0.0;
        }
        /// <summary>
        /// Gets or sets the stinging ability of the bee. By default, bees are considered to have stinging ability, but this can be changed if needed.
        /// </summary>
        public bool CanSting
        {
            get => _canSting;
            set => _canSting = value;
        }
        /// <summary>
        /// Gets or sets the honey production per day in grams. Value is constrained to be non-negative.
        /// </summary>
        public double HoneyProductionPerDay
        {
            get => _honeyProductionPerDay;
            set => _honeyProductionPerDay = value >= 0 ? value : 0.0;
        }
        /// <summary>
        /// Returns a string representation including base insect data, stinging ability, and honey production.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() +
                   $"\nCan Sting: {CanSting}" +
                   $"\nHoney Production (g/day): {HoneyProductionPerDay}";
        }
    }
}

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
    public abstract class Insect : Animal
    {
        private int _numberOfWings;
        private double _antennaLength;

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected Insect() : base()
        {
            _numberOfWings = 0;
            _antennaLength = 0.0;
        }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        protected Insect(int numberOfWings, double antennaLength) : base()
        {
            NumberOfWings = numberOfWings;
            AntennaLength = antennaLength;
        }

        /// <summary>
        /// Gets or sets the number of wings. Value is constrained to be non-negative.
        /// </summary>
        public int NumberOfWings
        {
            get => _numberOfWings;
            set => _numberOfWings = value >= 0 ? value : 0;
        }

        /// <summary>
        /// Gets or sets the length of the antenna in millimeters. 
        /// Value is constrained to be non-negative.
        /// </summary>
        public double AntennaLength
        {
            get => _antennaLength;
            set => _antennaLength = value >= 0 ? value : 0.0;
        }

        /// <summary>
        /// Virtual method for sleep time.
        /// </summary>
        public virtual void SetSleepTime()
        {
            Console.WriteLine($"{GetType().Name} rests 6-10 hours per day.");
        }

        /// <summary>
        /// Abstract method: average lifespan.
        /// </summary>
        public abstract int GetAverageLifeSpan();

        /// <summary>
        /// Abstract method: daily food requirement.
        /// </summary>
        public abstract Dictionary<string, string> DailyFoodRequirement();

        /// <summary>
        /// Summary for list display.
        /// </summary>
        public virtual string ToStringSummary()
        {
            return $"{Id,-8} {Name,-12} {Age,6} {Weight,6:F1} {Gender} | Wings: {NumberOfWings}, Antenna: {AntennaLength}mm";
        }

        /// <summary>
        /// Returns a string representation of the insect.
        /// </summary>
        public override string ToString()
        {
            return base.ToString() +
                   $"\nNumber of Wings: {NumberOfWings}" +
                   $"\nAntenna Length: {AntennaLength} mm";
        }
    }
}

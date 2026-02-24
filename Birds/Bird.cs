using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Birds
{
    /// <summary>
    /// Derived class for birds, adding category properties.
    /// </summary>
    public abstract class Bird : Animal
    {
        private double _wingspan;       // in cm
        private double _tailLength;     // in cm 

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Bird() : base()
        {
            _wingspan = 0.0;
            _tailLength = 0.0;
        }

        /// <summary>
        /// Parameterized constructor for bird specific data.
        /// </summary>
        public Bird(double wingspan, double tailLength) : base()
        {
            Wingspan = wingspan;
            TailLength = tailLength;
        }

        /// <summary>
        /// Gets or sets the wingspan value, ensuring it is non-negative.
        /// </summary>
        public double Wingspan
        {
            get => _wingspan;
            set => _wingspan = value >= 0 ? value : 0.0;
        }

        /// <summary>
        /// Gets or sets the length of the tail. Value is constrained to be non-negative.
        /// </summary>
        public double TailLength
        {
            get => _tailLength;
            set => _tailLength = value >= 0 ? value : 0.0;
        }

        /// <summary>
        /// Virtual method for sleep time.
        /// </summary>
        public virtual void SetSleepTime()
        {
            // Default for birds: perching/resting
            Console.WriteLine($"{GetType().Name} perches/rests 8-12 hours per day.");
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
            return $"{Id,-8} {Name,-12} {Age,6} {Weight,6:F1} {Gender} | Wingspan: {Wingspan}cm, Tail: {TailLength}cm";
        }

        /// <summary>
        /// Returns a string representation including base and bird data.
        /// </summary>
        public override string ToString()
        {
            return base.ToString() +
                   $"\nWingspan: {Wingspan} cm" +
                   $"\nTail Length: {TailLength} cm";
        }
    }
}

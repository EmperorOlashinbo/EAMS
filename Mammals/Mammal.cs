using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS
{
    /// <summary>
    /// Derived class for mammals, adding category specific properties. Inherits from Animal.
    /// </summary>
    public abstract class Mammal : Animal
    {
        private int _numberOfTeeth;
        private double _tailLength;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Mammal() : base()
        {
            _numberOfTeeth = 0;
            _tailLength = 0.0;
        }
        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="numberOfTeeth">Number of teeth</param>
        /// <param name="tailLength">Tail length in cm</param>
        public Mammal(int numberOfTeeth, double tailLength) : base()
        {
            _numberOfTeeth = numberOfTeeth >= 0 ? numberOfTeeth: 0;
            _tailLength = tailLength >= 0 ? tailLength : 0.0;
        }
        /// <summary>
        /// Gets or sets the number of teeth the mammal has. 
        /// The number of teeth is represented as an integer,
        /// </summary>
        public int NumberOfTeeth
        {
            get => _numberOfTeeth;
            set
            {
                if (value >= 0)
                    _numberOfTeeth = value;
            }
        }
        /// <summary>
        /// Gets or sets the length of the mammal's tail in centimeters.
        /// </summary>
        public double TailLength
        {
            get => _tailLength;
            set
            {
                if (value >= 0)
                    _tailLength = value;
            }
        }
        /// <summary>
        /// Virtual method for sleep time.
        /// Can be overridden in species classes.
        /// </summary>
        public virtual void SetSleepTime()
        {
            // Default sleep for mammals is 8 hours
            Console.WriteLine($"{GetType().Name} sleeps 8 hours per day.");
        }

        /// <summary>
        /// Abstract method for average lifespan.
        /// </summary>
        public abstract int GetAverageLifeSpan();

        /// <summary>
        /// Abstract method for daily food requirement.
        /// </summary>
        public abstract Dictionary<string, string> DailyFoodRequirement();

        /// <summary>
        /// Summary for list display.
        /// </summary>
        public string ToStringSummary()
        {
            return $"{Id,-8} {Name,-12} {Age,6} {Weight,6:F1} {Gender} | Teeth: {NumberOfTeeth}, Tail: {TailLength}cm";
        }
        /// <summary>
        /// Returns a string representation of the mammal.
        /// </summary>
        /// <returns>String with mammal data.</returns>
        public override string ToString()
        {
            return base.ToString() + $"\nNumber of Teeth: {NumberOfTeeth}\nTail Length: {TailLength} cm";
        }

    }
}

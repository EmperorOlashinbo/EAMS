using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Birds.species
{
    /// <summary>
    /// Concrete species class for eagles, inheriting from Bird.
    /// </summary>
    public class Eagle : Bird
    {
        private bool _isBald;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Eagle() : base() { _isBald = false; }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        public Eagle(double wingspan, double tailLength) : base(wingspan, tailLength)
        {
            _isBald = false;
        }
        /// <summary>
        /// Gets or sets a value indicating whether the eagle is a bald eagle.
        /// </summary>
        public bool IsBald
        {
            get => _isBald;
            set => _isBald = value;
        }
        /// <summary>
        /// Gets the average lifespan of the eagle in years.
        /// </summary>
        /// <returns>The average lifespan of the eagle in years.</returns>
        public override int GetAverageLifeSpan() => 30;
        /// <summary>
        /// Gets the daily food requirements of the eagle.
        /// </summary>
        /// <returns>A dictionary containing the daily food requirements of the eagle.</returns>
        public override Dictionary<string, string> DailyFoodRequirement() =>
            new Dictionary<string, string>
            {
                ["Morning"] = "Small mammals or fish",
                ["Evening"] = "Birds or carrion"
            };
        /// <summary>
        /// Returns a string representation of the eagle, including bald eagle status.
        /// </summary>
        /// <returns>A string representation of the eagle.</returns>
        public override string ToString()
        {
            return base.ToString() + $"\nIs Bald Eagle: {IsBald}";
        }
    }
}

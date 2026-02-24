using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Birds.species
{
    /// <summary>
    /// Concrete species class for falcons, inheriting from Bird.
    /// </summary>
    public class Falcon : Bird
    {
        private double _beakLength;
        /// <summary>
        /// Default constructor. Initializes a falcon with default values.
        /// </summary>
        public Falcon() : base() { _beakLength = 0.0; }

        /// <summary>
        /// Parameterized constructor for falcon specific data.
        /// </summary>
        /// <param name="wingspan">The wingspan of the falcon.</param>
        /// <param name="tailLength">The tail length of the falcon.</param>
        public Falcon(double wingspan, double tailLength) : base(wingspan, tailLength)
        {
            _beakLength = 0.0;
        }
        
        /// <summary>
        /// Gets or sets the beak length of the falcon in centimeters.
        /// </summary>
        public double BeakLength
        {
            get => _beakLength;
            set => _beakLength = value >= 0 ? value : 0.0;
        }
        
        /// <summary>
        /// Gets the average lifespan of the falcon in years.
        /// </summary>
        /// <returns>The average lifespan of the falcon in years.</returns>
        public override int GetAverageLifeSpan() => 15;
        
        /// <summary>
        /// Gets the daily food requirements of the falcon.
        /// </summary>
        /// <returns>A dictionary containing the daily food requirements of the falcon.</returns>
        public override Dictionary<string, string> DailyFoodRequirement() =>
            new Dictionary<string, string>
            {
                ["Morning"] = "Small birds and rodents",
                ["Evening"] = "Insects or small prey"
            };
        
        /// <summary>
        /// Returns a string representation of the falcon, including beak length.
        /// </summary>
        /// <returns>A string representation of the falcon.</returns>
        public override string ToString()
        {
            return base.ToString() + $"\nBeak Length: {BeakLength} cm";
        }
    }
}
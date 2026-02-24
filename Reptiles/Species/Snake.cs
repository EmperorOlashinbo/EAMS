using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Reptiles.Species
{
    /// <summary>
    /// Concrete species class for snakes, inheriting from Reptile.
    /// </summary>
    public class Snake : Reptile
    {
        private double _length;
        private bool _isVenomous;
        /// <summary>
        /// Default constructor. Initializes a snake with default values.
        /// </summary>
        public Snake() : base() { }
        /// <summary>
        /// Parameterized constructor. Initializes a snake with specified values.
        /// </summary>
        /// <param name="bodyLength">Body length of the snake.</param>
        /// <param name="livesInWater">Indicates if the snake lives in water.</param>
        /// <param name="aggressivenessLevel">Aggressiveness level of the snake.</param>
        public Snake(double bodyLength, bool livesInWater, int aggressivenessLevel)
            : base(bodyLength, livesInWater, aggressivenessLevel) { }
        /// <summary>
        /// Gets or sets the length of the snake in centimeters. 
        /// Values less than zero are set to 0.0.
        /// </summary>
        public double Length { get => _length; set => _length = value >= 0 ? value : 0.0; }
        /// <summary>
        /// Gets or sets a value indicating whether the snake is venomous.
        /// </summary>
        public bool IsVenomous { get => _isVenomous; set => _isVenomous = value; }
        /// <summary>
        /// Gets the average lifespan of the snake in years.
        /// </summary>
        /// <returns>The average lifespan of the snake in years.</returns>
        public override int GetAverageLifeSpan() => 20;
        /// <summary>
        /// Gets the daily food requirements of the snake.
        /// </summary>
        /// <returns>A dictionary containing the daily food requirements of the snake.</returns>
        public override Dictionary<string, string> DailyFoodRequirement() =>
            new Dictionary<string, string>
            {
                ["Every 7-14 days"] = "Rodents or prey (depending on size)"
            };
        
        /// <summary>
        /// Returns a string representation of the snake, including length and venomous status.
        /// </summary>
        /// <returns>A string representation of the snake.</returns>
        public override string ToString()
        {
            return base.ToString() + $"\nLength: {Length} cm\nIs Venomous: {IsVenomous}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Birds.species
{
    /// <summary>
    /// Concrete species class for doves, inheriting from Bird.
    /// </summary>
    public class Dove : Bird
    {
        private string _featherColor;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Dove() : base() { _featherColor = string.Empty; }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        public Dove(double wingspan, double tailLength) : base(wingspan, tailLength)
        {
            _featherColor = string.Empty;
        }
        /// <summary>
        /// Gets or sets the feather color of the dove. 
        /// If a null value is assigned, it defaults to an empty string.
        /// </summary>
        public string FeatherColor
        {
            get => _featherColor;
            set => _featherColor = value ?? string.Empty;
        }
        /// <summary>
        /// Gets the average lifespan of the dove in years.
        /// </summary>
        /// <returns>The average lifespan of the dove in years.</returns>
        public override int GetAverageLifeSpan() => 10;
        /// <summary>
        /// Gets the daily food requirements of the dove.
        /// </summary>
        /// <returns>A dictionary containing the daily food requirements of the dove.</returns>
        public override Dictionary<string, string> DailyFoodRequirement() =>
            new Dictionary<string, string>
            {
                ["All day"] = "Seeds, grains, small fruits"
            };
        /// <summary>
        /// Returns a string representation of the dove, including feather color.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + $"\nFeather Color: {FeatherColor}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Insects.Species
{
    /// <summary>
    /// Derived class for butterflies, adding wing pattern property.
    /// </summary>
    public class Butterfly : Insect
    {
        private string _wingPattern;
        /// <summary>
        /// Default constructor. Initializes a butterfly with default values.
        /// </summary>
        public Butterfly() : base() { _wingPattern = string.Empty; }
        /// <summary>
        /// Parameterized constructor for butterfly specific data.
        /// </summary>
        /// <param name="numberOfWings">The number of wings of the butterfly.</param>
        /// <param name="antennaLength">The length of the antenna of the butterfly in millimeters.</param>
        public Butterfly(int numberOfWings, double antennaLength) : base(numberOfWings, antennaLength)
        {
            _wingPattern = string.Empty;
        }
        /// <summary>
        /// Gets or sets the pattern of the wing.
        /// </summary>
        public string WingPattern
        {
            get => _wingPattern;
            set => _wingPattern = value ?? string.Empty;
        }
        
        /// <summary>
        /// Gets the average lifespan of the butterfly in weeks.
        /// </summary>
        /// <returns>The average lifespan of the butterfly in weeks.</returns>
        public override int GetAverageLifeSpan() => 4;   // Typical butterfly lifespan in weeks/months
        
        /// <summary>
        /// Gets the daily food requirements of the butterfly.
        /// </summary>
        /// <returns>A dictionary containing the daily food requirements of the butterfly.</returns>
        public override Dictionary<string, string> DailyFoodRequirement() =>
            new Dictionary<string, string>
            {
                ["All day"] = "Nectar from flowers"
            };
        
        /// <summary>
        /// Returns a string representation of the butterfly, including wing pattern.
        /// </summary>
        /// <returns>A string representation of the butterfly.</returns>
        public override string ToString()
        {
            return base.ToString() + $"\nWing Pattern: {WingPattern}";
        }
        
        /// <summary>
        /// Overrides the GetUpcomingEvents method to provide specific events related to butterflies, such as release and migration monitoring.
        /// </summary>
        /// <returns>A queue of upcoming events for the butterfly species.</returns>
        public override Queue<string> GetUpcomingEvents()
        {
            var q = new Queue<string>();
            q.Enqueue($"Release (butterfly garden) - {DateTime.Now.AddDays(7):yyyy-MM-dd}");
            q.Enqueue($"Migration monitoring - {DateTime.Now.AddDays(90):yyyy-MM-dd}");
            return q;
        }
    }
}

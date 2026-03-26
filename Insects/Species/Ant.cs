using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Insects.Species
{
    /// <summary>
    /// Derived class for ants, adding worker status property.
    /// </summary>
    public class Ant : Insect
    {
        private bool _isWorker;
        /// <summary>
        /// Default constructor. Initializes an ant with default values, 
        /// setting it as a worker by default.
        /// </summary>
        public Ant() : base() { _isWorker = true; }
        
        /// <summary>
        /// Parameterized constructor for ant specific data.
        /// </summary>
        /// <param name="numberOfWings">The number of wings of the ant.</param>
        /// <param name="antennaLength">The length of the antenna of the ant in millimeters.</param>
        public Ant(int numberOfWings, double antennaLength) : base(numberOfWings, antennaLength)
        {
            _isWorker = true;
        }
        
        /// <summary>
        /// Gets or sets the worker status of the ant. By default, 
        /// ants are considered workers, but this can be changed if needed.
        /// </summary>
        public bool IsWorker
        {
            get => _isWorker;
            set => _isWorker = value;
        }
        
        /// <summary>
        /// Gets the average lifespan of the ant in years.
        /// </summary>
        /// <returns>The average lifespan of the ant in years.</returns>
        public override int GetAverageLifeSpan() => 3;   // Worker ants live 1-3 years
        
        /// <summary>
        /// Gets the daily food requirements of the ant.
        /// </summary>
        /// <returns>A dictionary containing the daily food requirements of the ant.</returns>
        public override Dictionary<string, string> DailyFoodRequirement() =>
            new Dictionary<string, string>
            {
                ["All day"] = "Seeds, insects, and honeydew"
            };
        
        /// <summary>
        /// Returns a string representation of the ant, including worker status.
        /// </summary>
        /// <returns>A string representation of the ant.</returns>
        public override string ToString()
        {
            return base.ToString() + $"\nIs Worker: {IsWorker}";
        }
        /// <summary>
        /// Overrides the GetUpcomingEvents method to provide specific events related to ants, such as colony inspections and foraging studies.
        /// </summary>
        /// <returns>A queue of upcoming events for the ant species.</returns>
        public override Queue<string> GetUpcomingEvents()
        {
            var q = new Queue<string>();
            q.Enqueue($"Colony inspection - {DateTime.Now.AddDays(30):yyyy-MM-dd}");
            q.Enqueue($"Foraging study - {DateTime.Now.AddDays(60):yyyy-MM-dd}");
            return q;
        }
    }
}

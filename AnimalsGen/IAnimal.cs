using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS
{
    /// <summary>
    /// Defines the interface for an animal, which includes properties
    /// </summary>
    public interface IAnimal
    {
        string Id { get; }
        string Name { get; set; }
        GenderType Gender { get; set; }
        int Age { get; set; }
        double Weight { get; set; }
        string ImagePath { get; set; }

        string ToString();

        /// <summary>
        /// Returns a queue of upcoming events for the animal.
        /// Species can override this method to provide specific events relevant to them, 
        /// such as feeding times, medical checkups, or enrichment activities.
        /// </summary>
        /// <returns>A queue of upcoming events for the animal.</returns>
        Queue<string> GetUpcomingEvents();
    }
}

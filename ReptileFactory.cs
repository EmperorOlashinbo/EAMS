using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS
{
    /// <summary>
    /// Factory class for creating reptile objects dynamically.
    /// </summary>
    public static class ReptileFactory
    {
        /// <summary>
        /// Creates a reptile based on species and category data.
        /// </summary>
        /// <param name="species">The reptile species.</param>
        /// <param name="bodyLength">Body length.</param>
        /// <param name="livesInWater">Lives in water.</param>
        /// <param name="aggressivenessLevel">Aggressiveness level.</param>
        /// <returns>A Reptile instance.</returns>
        public static Reptile CreateReptile(ReptileSpecies species, double bodyLength, bool livesInWater, int aggressivenessLevel)
        {
            switch (species)
            {
                case ReptileSpecies.Turtle:
                    return new Turtle(bodyLength, livesInWater, aggressivenessLevel);
                case ReptileSpecies.Lizard:
                    return new Lizard(bodyLength, livesInWater, aggressivenessLevel);
                default:
                    throw new ArgumentException("Invalid species.");
            }
        }
    }
}

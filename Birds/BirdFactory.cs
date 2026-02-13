using EAMS.Birds.species;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EAMS.Birds
{
    /// <summary>
    /// Factory class for creating bird objects dynamically.
    /// </summary>
    public static class BirdFactory
    {
        /// <summary>
        /// Creates a bird based on species and category data.
        /// </summary>
        public static Bird CreateBird(BirdSpecies species, double wingspan, double tailLength)
        {
            switch (species)
            {
                case BirdSpecies.Eagle:
                    return new Eagle(wingspan, tailLength);
                case BirdSpecies.Dove:
                    return new Dove(wingspan, tailLength);
                case BirdSpecies.Falcon:
                    return new Falcon(wingspan, tailLength);
                case BirdSpecies.Peacock:
                    return new Peacock(wingspan, tailLength);
                default:
                    throw new ArgumentException("Invalid bird species.");
            }
        }
    }
}
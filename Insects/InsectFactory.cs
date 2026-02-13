using EAMS.Insects.Species;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Insects
{
    /// <summary>
    /// Factory class for creating insect instances based on species.
    /// </summary>
    public static class InsectFactory
    {
        /// <summary>
        /// Creates an insect instance based on the specified species, number of wings, and antenna length.
        /// </summary>
        /// <param name="species"></param>
        /// <param name="numberOfWings"></param>
        /// <param name="antennaLength"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Insect CreateInsect(InsectSpecies species, int numberOfWings, double antennaLength)
        {
            switch (species)
            {
                case InsectSpecies.Butterfly:
                    return new Butterfly(numberOfWings, antennaLength);
                case InsectSpecies.Bee:
                    return new Bee(numberOfWings, antennaLength);
                case InsectSpecies.Ant:
                    return new Ant(numberOfWings, antennaLength);
                case InsectSpecies.Dragonfly:
                    return new Dragonfly(numberOfWings, antennaLength);
                case InsectSpecies.Ladybug:
                    return new Ladybug(numberOfWings, antennaLength);
                default:
                    throw new ArgumentException("Invalid insect species.");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS
{
    /// <summary>
    /// Factory class for creating Mammal objects.
    /// </summary>
    public static class MammalFactory
    {
        /// <summary>
        /// Creates a Mammal object based on the specified species.
        /// </summary>
        /// <param name="species">The mammal species.</param>
        /// <param name="numberOfTeeth">The number of teeth.</param>
        /// <param name="tailLength">The tail length.</param>
        /// <returns>A Mammal instance.</returns>
        public static Mammal CreateMammal(MammalSpecies species, int numberOfteeth, double tailLength)
        {
            switch (species)
            {
                case MammalSpecies.Dog:
                    return new Dog(numberOfteeth, tailLength);
                case MammalSpecies.Cat:
                    return new Cat(numberOfteeth, tailLength);
                default:
                    throw new ArgumentException("Invalid mammal species");
            }
        }
    }
}

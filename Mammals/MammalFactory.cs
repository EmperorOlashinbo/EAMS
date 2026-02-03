using EAMS.Mammals.Species;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS
{
    /// <summary>
    /// Factory class for creating mammal objects dynamically.
    /// </summary>
    public static class MammalFactory
    {
        /// <summary>
        /// Creates a mammal based on species and category data.
        /// </summary>
        /// <param name="species">The mammal species.</param>
        /// <param name="numberOfTeeth">Number of teeth.</param>
        /// <param name="tailLength">Tail length.</param>
        /// <returns>A Mammal instance.</returns>
        public static Mammal CreateMammal(MammalSpecies species, int numberOfTeeth, double tailLength)
        {
            switch (species)
            {
                case MammalSpecies.Dog:
                    return new Dog(numberOfTeeth, tailLength);
                case MammalSpecies.Cat:
                    return new Cat(numberOfTeeth, tailLength);
                case MammalSpecies.Cow:
                    return new Cow(numberOfTeeth, tailLength);
                case MammalSpecies.Horse:
                    return new Horse(numberOfTeeth, tailLength);
                default:
                    throw new ArgumentException("Invalid species.");
            }
        }
    }
}

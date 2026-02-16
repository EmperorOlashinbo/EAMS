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
                    throw new ArgumentException("Invalid mammal species");
            }
        }
    }
}
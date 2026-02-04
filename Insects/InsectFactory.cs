using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Insects
{
    public static class InsectFactory
    {
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
                default:
                    throw new ArgumentException("Invalid insect species.");
            }
        }
    }
}

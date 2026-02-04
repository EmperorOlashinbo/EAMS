using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Insects.Species
{
    public class Bee : Insect
    {
        private bool _canSting;
        private double _honeyProductionPerDay;  // in grams

        public Bee() : base()
        {
            _canSting = true;
            _honeyProductionPerDay = 0.0;
        }

        public Bee(int numberOfWings, double antennaLength) : base(numberOfWings, antennaLength)
        {
            _canSting = true;
            _honeyProductionPerDay = 0.0;
        }

        public bool CanSting
        {
            get => _canSting;
            set => _canSting = value;
        }

        public double HoneyProductionPerDay
        {
            get => _honeyProductionPerDay;
            set => _honeyProductionPerDay = value >= 0 ? value : 0.0;
        }

        public override string ToString()
        {
            return base.ToString() +
                   $"\nCan Sting: {CanSting}" +
                   $"\nHoney Production (g/day): {HoneyProductionPerDay}";
        }
    }
}

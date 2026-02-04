using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Insects.Species
{
    public class Ant : Insect
    {
        private bool _isWorker;

        public Ant() : base() { _isWorker = true; }

        public Ant(int numberOfWings, double antennaLength) : base(numberOfWings, antennaLength)
        {
            _isWorker = true;
        }

        public bool IsWorker
        {
            get => _isWorker;
            set => _isWorker = value;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nIs Worker: {IsWorker}";
        }
    }
}

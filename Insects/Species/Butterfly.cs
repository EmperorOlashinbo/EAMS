using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Insects.Species
{
    public class Butterfly : Insect
    {
        private string _wingPattern;

        public Butterfly() : base() { _wingPattern = string.Empty; }

        public Butterfly(int numberOfWings, double antennaLength) : base(numberOfWings, antennaLength)
        {
            _wingPattern = string.Empty;
        }

        public string WingPattern
        {
            get => _wingPattern;
            set => _wingPattern = value ?? string.Empty;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nWing Pattern: {WingPattern}";
        }
    }
}

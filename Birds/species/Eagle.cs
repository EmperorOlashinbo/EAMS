using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Birds.species
{
    /// <summary>
    /// Concrete species class for eagles, inheriting from Bird.
    /// </summary>
    public class Eagle : Bird
    {
        private bool _isBald;           

        public Eagle() : base() { _isBald = false; }

        public Eagle(double wingspan, double tailLength) : base(wingspan, tailLength)
        {
            _isBald = false;
        }

        public bool IsBald
        {
            get => _isBald;
            set => _isBald = value;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nIs Bald Eagle: {IsBald}";
        }
    }
}

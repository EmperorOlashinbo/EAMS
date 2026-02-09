using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Insects.Species
{
    /// <summary>
    /// Derived class for ants, adding worker status property.
    /// </summary>
    public class Ant : Insect
    {
        private bool _isWorker;
        /// <summary>
        /// Default constructor. Initializes the ant as a worker by default.
        /// </summary>
        public Ant() : base() { _isWorker = true; }
        /// <summary>
        /// Parameterized constructor for ant specific data, including worker status.
        /// </summary>
        /// <param name="numberOfWings"></param>
        /// <param name="antennaLength"></param>
        public Ant(int numberOfWings, double antennaLength) : base(numberOfWings, antennaLength)
        {
            _isWorker = true;
        }
        /// <summary>
        /// Gets or sets the worker status of the ant. By default, ants are considered workers, but this can be changed if needed.
        /// </summary>
        public bool IsWorker
        {
            get => _isWorker;
            set => _isWorker = value;
        }
        /// <summary>
        /// Returns a string representation including base insect data and worker status.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + $"\nIs Worker: {IsWorker}";
        }
    }
}

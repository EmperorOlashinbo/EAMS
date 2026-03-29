using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Exceptions
{
    /// <summary>
    /// Thrown when duplicate animals are detected during validation.
    /// Carries a meaningful message and optional inner exception.
    /// </summary>
    public class DuplicateAnimalException : Exception
    {
        public DuplicateAnimalException()
        {
        }

        public DuplicateAnimalException(string message) : base(message)
        {
        }

        public DuplicateAnimalException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

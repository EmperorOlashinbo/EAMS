using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS
{
    public static class NumericUtility
    {
        /// <summary>
        /// Parses the input string to an integer.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <returns>A tuple containing the parsed integer and a boolean indicating success.</returns>
        public static (int, bool) GetInteger(string text)
        {
            if (int.TryParse(text, out int value))
                return (value, true);
            return (0, false);
        }

        /// <summary>
        /// Parses the input string to a double.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <returns>A tuple containing the parsed double and a boolean indicating success.</returns>
        public static (double, bool) GetDouble(string text)
        {
            if (double.TryParse(text, out double value))
                return (value, true);
            return (0.0, false);
        }
    }
}

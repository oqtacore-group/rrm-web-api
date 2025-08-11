using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oqtacore.Rrm.Domain.Models
{
    public class ParseHelper
    {
        public static string ValueLenghtParse(string value, int maxLenght)
        {
            if (maxLenght > 5)
            {
                return value.Length > maxLenght ? value.Substring(0, maxLenght - 4) + ".." : value;
            }
            else return value;

        }
    }
}

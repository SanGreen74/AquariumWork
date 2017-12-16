using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquariumLibrary.BaseClasses
{
    public static class Random1
    {
        public static Random rnd = new Random();

        public static bool LowChanceOfAttac()
        {
            return (Random1.rnd.Next(400) == 1);
        }
    }
}

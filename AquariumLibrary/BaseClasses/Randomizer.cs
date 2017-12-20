using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquariumLibrary.BaseClasses
{
    public static class Randomizer
    {
        public static Random rnd = new Random();

        public static bool LowChanceOfAttac()
        {
            return (Randomizer.rnd.Next(400) == 1);
        }
    }
}

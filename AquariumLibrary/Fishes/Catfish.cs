using AquariumLibrary.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquariumLibrary.Interfaces;
using System.Drawing;

namespace AquariumLibrary.Fishes
{
    class Catfish : AFish
    {
        public Catfish(PointF location, SizeF size, IAquarium aquarium)
            : base(location, size, aquarium)
        {
        }

    }
}

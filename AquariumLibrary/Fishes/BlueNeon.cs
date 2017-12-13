using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquariumLibrary.AbstractClasses;
using AquariumLibrary.Interfaces;

namespace AquariumLibrary.Fishes
{
    public class BlueNeon : AFish
    {
        public BlueNeon(PointF location, SizeF size, IAquarium aquarium) : base(location, size, aquarium)
        {
            Speed = 4;
        }

    }
}

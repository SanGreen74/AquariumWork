using AquariumLibrary.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquariumLibrary.GameClasses
{
    public static class Settings
    {
        public static class BlueNeon
        {
            public static readonly float Speed = 5;
            public static readonly Size Size = new Size(90, 100);
            public static readonly int SafeDistance = 150;
            public static readonly int CornerRadius = 50;
        }

        public static class SwordsMan
        {
            public static readonly float OfficialSpeed = 1.8f;
            public static readonly float MaxSpeed = 3.6f;
            public static readonly Size Size = new Size(90, 100);
            public static readonly HashSet<FishType> Food = new HashSet<FishType> { FishType.BlueNeon, FishType.Piranha };
        }

        public static class Piranha
        {
            public static readonly float Speed = 2.5f;
            public static readonly Size Size = new Size(90, 100);
            public static readonly HashSet<FishType> Food = new HashSet<FishType> { FishType.BlueNeon };
        }

        public static class Catfish
        {
            public static readonly float Speed = 2;
            public static readonly Size Size = new Size(90, 100);
        }
    }
}

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
        protected override PointF GetNextPoint()
        {
            var random = new Random();
            while(true)
            {
                var nextPoint = new PointF(Location.X + (float)Speed * Direction.X, Location.Y + (float)Speed * Direction.Y);
                if (Aquarium.IsPointBelong(nextPoint) && nextPoint.Y > CatfishBroad) return nextPoint;
                Direction.Rotate(random.Next(0, 180));
            }
        }
        public Catfish(PointF location, SizeF size, IAquarium aquarium)
            : base(location, size, aquarium)
        {
            CatfishBroad = Aquarium.Size.Height / 2.0;

            if (CheckCorrectLocation(location) == false)
                throw new ArgumentException("точка не принадлежит области обитания");
            Speed = 2;
        }
        private double CatfishBroad;
        private bool CheckCorrectLocation(PointF location)
        {
            if (location.Y >= CatfishBroad)
                return true;
            else
                return false;
        }

    }
}

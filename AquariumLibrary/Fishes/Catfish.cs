using AquariumLibrary.AbstractClasses;
using System;
using AquariumLibrary.Interfaces;
using System.Drawing;

namespace AquariumLibrary.Fishes
{
    public class Catfish : AFish
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

        public override void OnCollision(AFish anotherObject)
        {
            throw new NotImplementedException();
        }

        public Catfish(PointF location, SizeF size, IAquarium aquarium)
            : base(location, size, aquarium)
        {
            CatfishBroad = Aquarium.Size.Height / 2.0;

            if (CheckCorrectLocation(location) == false)
                throw new ArgumentException("Точка не принадлежит области обитания");
            Speed = 2;
        }
        private double CatfishBroad;
        private bool CheckCorrectLocation(PointF location)
        {
            return location.Y >= CatfishBroad;
        }

    }
}

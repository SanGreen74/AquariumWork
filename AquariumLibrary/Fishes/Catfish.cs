using AquariumLibrary.AbstractClasses;
using System;
using AquariumLibrary.Interfaces;
using System.Drawing;
using AquariumLibrary.BaseClasses;

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
                Direction = Direction.Rotate(random.Next(0, 180));
            }
        }

        public override void OnCollision(AFish anotherObject)
        {
            //TODO
        }

        public Catfish(PointF location, SizeF size, IAquarium aquarium)
            : base(location, size, aquarium)
        {
            CatfishBroad = Aquarium.Size.Height / 2.0;

            if (CheckCorrectLocation(location) == false)
                throw new ArgumentException("Точка не принадлежит области обитания");
            Speed = 2;
            PushState(Walking);
        }
            private int counter = 268;
        private void Walking()
        {
            if (nextPointSleep == null)
                nextPointSleep = RandomPoint();
            var vector = new VectorF(Location, (PointF)nextPointSleep);
            if(vector.GetLength() < GetStepLength())
            {
                nextPointSleep = null;
                counter = 180;
                PushState(Sleep);
                return;
            }
            var point = GetNextPoint();
            MoveTo(point);
        }
        private void Sleep()
        {
            counter--;
            if (counter < 0)
                PopState();
        }
        private double CatfishBroad;
        private bool CheckCorrectLocation(PointF location)
        {
            return location.Y >= CatfishBroad;
        }
        private PointF RandomPoint()
        {
            var rndX = Random1.rnd.Next(0, Aquarium.Size.Width);
            var rndY = Random1.rnd.Next((int)CatfishBroad, Aquarium.Size.Height);
            var point1 = new PointF(rndX, rndY);
            Direction = new VectorF(point1.X - Location.X, point1.Y - Location.Y);
            return point1;
        }
        private PointF? nextPointSleep;   
       public double GetStepLength()
        {
            var dx = (float)Speed * Direction.X;
            var dy = (float)Speed * Direction.Y;
            return Math.Sqrt(dx*dx + dy*dy);
        }   
    }
}

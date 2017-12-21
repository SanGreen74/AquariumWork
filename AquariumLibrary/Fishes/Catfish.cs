using AquariumLibrary.AbstractClasses;
using System;
using AquariumLibrary.Interfaces;
using System.Drawing;
using AquariumLibrary.BaseClasses;
using AquariumLibrary.GameClasses;

namespace AquariumLibrary.Fishes
{
    public class Catfish : AFish
    {
        public Catfish(PointF location, SizeF size, IAquarium aquarium)
            : base(location, size, aquarium)
        {
            _catfishBroad = Aquarium.Size.Height / 2.0;

            if (CheckCorrectLocation(location) == false)
                throw new ArgumentException("Точка не принадлежит области обитания");

            Speed = Settings.Catfish.Speed;
            PushState(Walking, FishState.Walking);
            Type = FishType.Catfish;
        }

        protected override PointF GetNextPoint()
        {
            while (true)
            {
                var nextPoint = new PointF(Location.X + Speed * Direction.X, Location.Y + Speed * Direction.Y);
                if (Aquarium.IsPointBelong(nextPoint) && nextPoint.Y > _catfishBroad)
                    return nextPoint;
                Direction = Direction.Rotate(Randomizer.Next(-90, 90) * Math.PI / 180);
            }
        }

        public override void OnCollision(AGameObject anotherObject)
        {
            // Не успели
        }

        private void Walking()
        {
            if (_nextPointSleep == null)
            {
                _nextPointSleep = GetRandomPoint();
                Direction = new VectorF(((PointF)_nextPointSleep).X - Location.X, ((PointF)_nextPointSleep).Y - Location.Y);
            }

            var vectorToNextPoint = new VectorF(Location, (PointF)_nextPointSleep);
            if (vectorToNextPoint.GetLength() < GetStepLength())
            {
                _nextPointSleep = null;
                _counter = 60*3;
                PushState(Sleep, FishState.Sleep);
            }
            else
                MoveTo(GetNextPoint());
        }

        private void Sleep()
        {
            _counter--;
            if (_counter < 0)
                PopState();
        }

        private bool CheckCorrectLocation(PointF location)
        {
            return location.Y >= _catfishBroad;
        }

        private PointF GetRandomPoint()
        {
            var x = Randomizer.Next(0, Aquarium.Size.Width);
            var y = Randomizer.Next((int)_catfishBroad, Aquarium.Size.Height);
            var randomPoint = new PointF(x, y);
            return randomPoint;
        }

        public double GetStepLength()
        {
            var dx = Speed * Direction.X;
            var dy = Speed * Direction.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        private readonly double _catfishBroad;

        private PointF? _nextPointSleep;

        private int _counter = 268;
    }
}

using System.Drawing;
using AquariumLibrary.AbstractClasses;
using AquariumLibrary.BaseClasses;
using AquariumLibrary.Interfaces;

namespace AquariumLibrary.Fishes
{
    public class BlueNeon : AFish
    {
        public BlueNeon(PointF location, SizeF size, IAquarium aquarium)
            : base(location, size, aquarium)
        {
            Speed = 2.24;
            //Flock = Aquarium.DistributeToFlock(this);
            PushState(Walking);
        }

        public Flock Flock;
        private void Walking()
        {
            var point = GetNextPoint();
            MoveTo(point);
        }

        protected override PointF GetNextPoint()
        {
            if (Flock == null) return base.GetNextPoint();
            if (Flock.Leader == this) return base.GetNextPoint();
            var point = Flock.GetChildrenPosition(this);
            Direction = new VectorF(point.X - Location.X, point.Y - Location.Y);
            var nextPoint = new PointF(Location.X + (float)Speed * Direction.X, Location.Y + (float)Speed * Direction.Y);
            if (!Aquarium.IsPointBelong(nextPoint)) return base.GetNextPoint();
            return nextPoint;
        }

        public override void OnCollision(AFish anotherObject)
        {
        }
    }
}

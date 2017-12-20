using System;
using System.Drawing;
using System.Linq;
using AquariumLibrary.AbstractClasses;
using AquariumLibrary.BaseClasses;
using AquariumLibrary.Interfaces;
using MoreLinq;

namespace AquariumLibrary.Fishes
{
    public class BlueNeon : AFish
    {
        public BlueNeon(PointF location, SizeF size, IAquarium aquarium)
            : base(location, size, aquarium)
        {
            Speed = 6;
            PushState(Walking, FishState);
        }
        
        private const int SafeDistance = 150;
        private const int CornerRadius = 30;

        public override void OnCollision(AFish anotherObject)
        {

        }

        private void Walking()
        {
            var danger = IsDangerous();
            if (danger != null && danger.IsDanger)
            {
                PushState(RunAway, FishState.RunAway);
                return;
            }
            MoveTo(GetNextPoint());
        }

        private void RunAway()
        {
            var danger = IsDangerous();
            if (danger == null || !danger.IsDanger)
            {
                PopState();
                return;
            }
            RunAwayFrom(danger.Hunter);
        }

        private void RunAwayFrom(AFish hunter)
        {
            if (IsLocationInCorner())
                return;
            var coordX = Location.X - hunter.Location.X;
            var coordY = Location.Y - hunter.Location.Y;
            Direction = new VectorF(coordX, coordY);
            MoveTo(GetNextPoint());
        }

        protected override PointF GetNextPoint()
        {
            while (true)
            {
                var nextPoint = new PointF(Location.X + (float)Speed * Direction.X*1.2f,
                    Location.Y + (float)Speed * Direction.Y* 1.2f);
                if (Aquarium.IsPointBelong(nextPoint)) return nextPoint;
                Direction = Direction.Rotate(Randomizer.rnd.Next(0, 180));
            }
        }

        private Danger IsDangerous()
        {
            var nearestHunter = GetNearestHunter();
            return nearestHunter == null ?
                null : new Danger(nearestHunter, DistanceTo(nearestHunter) < SafeDistance);
        }

        private AFish GetNearestHunter()
        {
            var hunters = Aquarium.GetFishes().Where(x => x is IHunter);

            return !hunters.Any() ? null : hunters.MinBy(DistanceTo);

            return Aquarium
                .GetFishes()
                .Where(x => x is IHunter)
                .FirstOrDefault(/*x => ((IHunter)x).Victim == this*/);
        }

        private bool IsLocationInCorner()
        {
            var p1 = new VectorF(Location, new PointF(0, 0));
            var p2 = new VectorF(Location, new PointF(Aquarium.Size.Width, 0));
            var p3 = new VectorF(Location, new PointF(0, Aquarium.Size.Height));
            var p4 = new VectorF(Location, new PointF(Aquarium.Size.Width, Aquarium.Size.Height));
            return p1.GetLength() < CornerRadius
                || p2.GetLength() < CornerRadius
                || p3.GetLength() < CornerRadius
                || p4.GetLength() < CornerRadius;
        }

        private class Danger
        {
            public readonly AFish Hunter;
            public readonly bool IsDanger;
            public Danger(AFish hunter, bool isDanger)
            {
                IsDanger = isDanger;
                Hunter = hunter;
            }
        }
    }
}

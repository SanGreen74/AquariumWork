using System.Drawing;
using System.Linq;
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
            PushState(Walking);
        }

        private const int SafeDistance = 50;

        private void Walking()
        {
            var danger = IsDangerous();
            if (danger != null && danger.IsDanger)
            {
                PushState(RunAway);
                return;
            }
            MoveTo(GetNextPoint());
        }

        private Danger IsDangerous()
        {
            var nearestHunter = GetNearestHunter();
            return nearestHunter == null ? 
                null : new Danger(nearestHunter, DistanceTo(nearestHunter) < SafeDistance);
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

        private AFish GetNearestHunter()
        {
            var fish = Aquarium
                .GetFishes()
                .Where(x => x is IHunter)
                .FirstOrDefault(x => ((IHunter) x).Victim == this);
            return fish;
        }

        private bool IsLocationInCorner()
        {
            var p1 = new VectorF(Location, new PointF(0, 0));
            var p2 = new VectorF(Location, new PointF(Aquarium.Size.Width, 0));
            var p3 = new VectorF(Location, new PointF(0, Aquarium.Size.Height));
            var p4 = new VectorF(Location, new PointF(Aquarium.Size.Width, Aquarium.Size.Height));
            return p1.GetLength() < 30 || p2.GetLength() < 30 || p3.GetLength() < 30 || p4.GetLength() < 30;
        }

        public override void OnCollision(AFish anotherObject)
        {
            
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

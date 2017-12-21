using System;
using System.Drawing;
using System.Linq;
using AquariumLibrary.AbstractClasses;
using AquariumLibrary.BaseClasses;
using AquariumLibrary.GameClasses;
using AquariumLibrary.Interfaces;
using MoreLinq;

namespace AquariumLibrary.Fishes
{
    public class BlueNeon : AFish
    {
        public BlueNeon(PointF location, SizeF size, IAquarium aquarium)
            : base(location, size, aquarium)
        {
            Type = FishType.BlueNeon;
            Speed = Settings.BlueNeon.Speed;
            PushState(Walking, FishState.Walking);
        }

        private void Walking()
        {
            var danger = GetDangerous();
            if (danger != null && danger.IsDanger)
            {
                PushState(RunAway, FishState.RunAway);
                return;
            }
            MoveTo(GetNextPoint());
        }

        private void RunAway()
        {
            var danger = GetDangerous();
            if (danger == null || !danger.IsDanger)
                PopState();
            else
                RunAwayFrom(danger.Hunter);
        }

        private void RunAwayFrom(AFish hunter)
        {
            if (IsLocationInCorner())
                return;
            Direction = new VectorF(Location.X - hunter.Location.X, 
                                    Location.Y - hunter.Location.Y);
            MoveTo(GetNextPoint());
        }

        protected override PointF GetNextPoint()
        {
            while (true)
            {
                var nextPoint = new PointF(Location.X + _speedCoefficient*Speed * Direction.X,
                    Location.Y + _speedCoefficient * Speed * Direction.Y);
                if (Aquarium.IsPointBelong(nextPoint))
                    return nextPoint;
                if (State == FishState.RunAway && !IsLocationInCorner())
                    MoveAroundWalls();
                else
                    Direction = Direction.Rotate(Randomizer.Next(-90, 90) * Math.PI / 180);
            }
        }

        private Danger GetDangerous()
        {
            var nearestHunter = GetNearestHunter();
            return nearestHunter == null ?
                null : new Danger(nearestHunter, DistanceTo(nearestHunter) < Settings.BlueNeon.SafeDistance);
        }

        private AFish GetNearestHunter()
        {
            var hunters = Aquarium.GetFishes().Where(x => x is IHunter);
            return !hunters.Any() ? null : hunters.MinBy(DistanceTo);
        }

        // bad code
        private void MoveAroundWalls()
        {
            if (Direction.Angle > 0 && Direction.Angle < 90)
            {
                if (Aquarium.Size.Height - Location.Y < Size.Height)
                    Direction = VectorF.Rigth;
                if (Aquarium.Size.Width - Location.X < Size.Width)
                    Direction = VectorF.Down;
            }
            else if (Direction.Angle > 90 && Direction.Angle < 180)
            {
                if (Location.X < Size.Width)
                    Direction = VectorF.Down;
                if (Aquarium.Size.Height - Location.Y < Size.Height)
                    Direction = VectorF.Left;
            }
            else if (Direction.Angle > 180 && Direction.Angle < 270)
            {
                if (Location.X < Size.Width)
                    Direction = VectorF.Up;
                if (Location.Y < Size.Height)
                    Direction = VectorF.Left;
            }
            else if (Direction.Angle > 270 && Direction.Angle < 360)
            {
                if (Location.Y < Size.Height)
                    Direction = VectorF.Rigth;
                if (Aquarium.Size.Width - Location.X < Size.Width)
                    Direction = VectorF.Up;
            }
            else
                Direction = Direction.Rotate(Randomizer.Next(-90, 90) * Math.PI / 180);
        }

        private bool IsLocationInCorner()
        {
            var p1 = new VectorF(Location, new PointF(0, 0));
            var p2 = new VectorF(Location, new PointF(Aquarium.Size.Width, 0));
            var p3 = new VectorF(Location, new PointF(0, Aquarium.Size.Height));
            var p4 = new VectorF(Location, new PointF(Aquarium.Size.Width, Aquarium.Size.Height));
            return p1.GetLength() < Settings.BlueNeon.CornerRadius
                || p2.GetLength() < Settings.BlueNeon.CornerRadius
                || p3.GetLength() < Settings.BlueNeon.CornerRadius
                || p4.GetLength() < Settings.BlueNeon.CornerRadius;
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

        public override void OnCollision(AGameObject anotherObject)
        {
            // todo collect cash
        }

        private float _speedCoefficient => State == FishState.RunAway ? 2f : 1;
    }
}

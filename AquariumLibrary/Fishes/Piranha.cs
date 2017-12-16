using AquariumLibrary.AbstractClasses;
using AquariumLibrary.Interfaces;
using System;
using System.Linq;
using System.Drawing;
using AquariumLibrary.BaseClasses;

namespace AquariumLibrary.Fishes
{
    public class Piranha : AFish, IHunter
    {
        public Piranha(PointF location, SizeF size, IAquarium aquarium)
            : base(location, size, aquarium)
        {
            Speed = 1;
            PushState(Walking);
        }

        private AFish _victim;

        public AFish Victim
        {
            get
            {
                return _victim;
            }
            private set
            {
                _victim = value;
            }
        }

        public AFish FindNextVictim()
        {
            return Aquarium.GetFishes().FirstOrDefault(fish => Random1.rnd.Next(400) == 0);
        }

        public void Walking()
        {
            MoveTo(GetNextPoint());
        }

        protected override PointF GetNextPoint()
        {
            var nextPoint = new PointF(Location.X + (float)Speed * Direction.X, Location.Y + (float)Speed * Direction.Y);

            if (Victim != null)
                if (!IsCollision(Victim))
                {
                    Direction = new VectorF(Victim.Location.X - Location.X, Victim.Location.Y - Location.Y);
                    nextPoint = new PointF(Location.X + (float)Speed * Direction.X,
                                           Location.Y + (float)Speed * Direction.Y);
                    return nextPoint;
                }
                else
                    Victim = null;

            if (Victim == null && Aquarium.IsPointBelong(nextPoint))
            {
                Victim = FindNextVictim();
                return base.GetNextPoint();
            }

             return base.GetNextPoint();
        }

        public override void OnCollision(AFish anotherObject)
        {
        }
    }
}
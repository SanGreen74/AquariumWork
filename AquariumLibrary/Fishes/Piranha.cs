using AquariumLibrary.AbstractClasses;
using AquariumLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using AquariumLibrary.BaseClasses;

namespace AquariumLibrary.Fishes
{
    class Piranha : AFish, IHunter
    {
        public Piranha(PointF location, SizeF size, IAquarium aquarium)
            : base(location, size, aquarium)
        {
            Speed = 2;
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
            foreach (var fish in Aquarium.GetFishes())
            {
                if (Random1.rnd.Next(10) == 0)
                {
                    return fish;
                }
            }
            return null;
        }

        protected override PointF GetNextPoint()
        {
            if (Victim == null)
                Victim = FindNextVictim();
            if (Victim != null)
            {
                Direction = new VectorF(Victim.Location.X - Location.X, Victim.Location.Y - Location.Y);
                var nextPoint = new PointF(Location.X + (float)Speed * Direction.X, Location.Y + (float)Speed * Direction.Y);
                return nextPoint;
            }
            return base.GetNextPoint();
        }
    }
}

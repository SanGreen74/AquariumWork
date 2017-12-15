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
    public class Piranha : AFish, IHunter
    {
        public Piranha(PointF location, SizeF size, IAquarium aquarium)
            : base(location, size, aquarium)
        {
            Speed = 1;
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

        public bool IsInside(AFish fish)
        {
            return (Math.Max(Location.X - 30, fish.Location.X - 25) < Math.Min(Location.X + 30, fish.Location.X + 25))
            && (Math.Max(Location.Y - 15, fish.Location.Y - 10) < Math.Min(Location.Y + 15, fish.Location.Y + 10));
        }

        protected override PointF GetNextPoint()
        {
            var nextPoint = new PointF(Location.X + (float)Speed * Direction.X, Location.Y + (float)Speed * Direction.Y);

            if (Victim != null)
                if (!IsInside(Victim))
                {
                    Direction = new VectorF(Victim.Location.X - Location.X, Victim.Location.Y - Location.Y);
                    nextPoint = new PointF(Location.X + (float)Speed * Direction.X,
                                           Location.Y + (float)Speed * Direction.Y);
                    return nextPoint;
                }
                else
                    Victim = null;

            if (Victim == null && IsPointBelongAquarium(nextPoint))
            {
                Victim = FindNextVictim();
                return base.GetNextPoint();
            }

             return base.GetNextPoint();
        }

        protected override void HandleCollision(AFish fish)
        {
            if (!FoodSet.Contains(fish.GetType()) || Victim != fish) return;
            Victim = null;
            fish.Die();
            var bn3 = new BlueNeon(new PointF(150, 50), new SizeF(30, 15), Aquarium);

        }
    }
}

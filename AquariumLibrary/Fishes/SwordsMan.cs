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
    public class SwordsMan : AFish, IHunter
    {
        public SwordsMan(PointF location, SizeF size, IAquarium aquarium) : base(location, size, aquarium)
        {
            Speed = 1.2;
        }

        private AFish _victim;

        private double officialSpeed = 1.2;

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
                if (GetSizeOf(fish.Size) < GetSizeOf(Size) && IsNoticed(fish))
                {
                    //if (Random1.LowChanceOfAttac())
                    //{
                        return fish;
                    //}
                }
            }
            return null;
        }

        protected override PointF GetNextPoint()
        {
            if (Victim == null)
            {
                Victim = FindNextVictim();
                Speed = officialSpeed;
            }
            if (Victim != null && IsNoticed(Victim))
            {
                Direction = new VectorF(Victim.Location.X - Location.X, Victim.Location.Y - Location.Y);
                var nextPoint = new PointF(Location.X + (float)Speed * Direction.X, Location.Y + (float)Speed * Direction.Y);
                Speed += Speed * 0.0035; 
                return nextPoint;
            }
            return base.GetNextPoint();
        }

        /// <summary>
        /// Находится ли жертва anotherFish в зоне видимости
        /// </summary>
        /// <param name="anotherFish">Жертва</param>
        public bool IsNoticed(AFish anotherFish)
        {
            var radiusOfVisibility = Size.Width * 1.5;
            var distanceToAnotherFish = Math.Sqrt((anotherFish.Location.X - Location.X) * (anotherFish.Location.X - Location.X)
                                                + (anotherFish.Location.Y- Location.Y) * (anotherFish.Location.Y - Location.Y));
            return radiusOfVisibility >= distanceToAnotherFish;
        }

        public double GetSizeOf(SizeF size)
        {
            return size.Height * size.Width;
        }
    }
}

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
            Speed = 4;
            PushState(Walking, FishState.Walking);
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
            return Aquarium.GetFishes().FirstOrDefault(fish => Randomizer.LowChanceOfAttac() && fish.FishType != FishType.Piranha);
        }

        public void Walking()
        {
            MoveTo(GetNextPoint());
            Victim = FindNextVictim();
            if (Victim != null)
            {
                PushState(Attack, FishState.Attack);
            }

        }

        public void Attack()
        {
            if(Victim == null)
            {
                PopState();
                return;
            }
            MoveTo(GetVictimNextPoint());
        }

        public override void OnCollision(AFish anotherObject)
        {
            if(anotherObject == Victim)
            {
                Victim.Die();
                Victim = null;
            }
        }

        public PointF GetVictimNextPoint()
        {
            Direction = new VectorF(Victim.Location.X - Location.X, Victim.Location.Y - Location.Y);
            var nextPoint = new PointF(Location.X + (float)Speed * Direction.X, Location.Y + (float)Speed * Direction.Y);
            return nextPoint;
        }
    }
}
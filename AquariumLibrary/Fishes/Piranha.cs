using AquariumLibrary.AbstractClasses;
using AquariumLibrary.Interfaces;
using System.Linq;
using System.Drawing;
using AquariumLibrary.BaseClasses;
using AquariumLibrary.GameClasses;

namespace AquariumLibrary.Fishes
{
    public class Piranha : AFish, IHunter
    {
        public Piranha(PointF location, SizeF size, IAquarium aquarium)
            : base(location, size, aquarium)
        {
            Speed = Settings.Piranha.Speed;
            PushState(Walking, FishState.Walking);
            Type = FishType.Piranha;
        }

        public void Walking()
        {
            Victim = FindNextVictim();
            if (Victim != null)
            {
                PushState(Attack, FishState.Attack);
                Victim.OnDie += ResetVictim;
            }
            else
                MoveTo(GetNextPoint());
        }

        public void Attack()
        {
            if (Victim == null)
                PopState();
            else
                MoveTo(GetVictimNextPoint());
        }

        public override void OnCollision(AGameObject anotherObject)
        {
            if (anotherObject != Victim) return;
            Victim.Die();
            Victim = null;
        }

        public PointF GetVictimNextPoint()
        {
            Direction = new VectorF(Victim.Location.X - Location.X, Victim.Location.Y - Location.Y);
            var nextPoint = new PointF(Location.X + Speed * Direction.X, Location.Y + Speed * Direction.Y);
            return nextPoint;
        }

        public AFish FindNextVictim()
        {
            return Aquarium.GetFishes()
                .FirstOrDefault(fish => Randomizer.Success(0.4)
                                        && Settings.Piranha.Food.Contains(fish.Type));
        }

        private void ResetVictim(AFish fish)
        {
            if (Victim == fish)
            {
                Victim.OnDie -= ResetVictim;
                Victim = null;
            }
        }
        public AFish Victim { get; private set; }
    }
}
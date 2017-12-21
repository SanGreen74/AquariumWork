using AquariumLibrary.AbstractClasses;
using AquariumLibrary.Interfaces;
using System.Linq;
using System.Drawing;
using AquariumLibrary.BaseClasses;
using AquariumLibrary.GameClasses;

namespace AquariumLibrary.Fishes
{
    public class SwordsMan : AFish, IHunter
    {
        public SwordsMan(PointF location, SizeF size, IAquarium aquarium) : base(location, size, aquarium)
        {
            Speed = Settings.SwordsMan.OfficialSpeed;
            PushState(Walking, FishState.Walking);
            Type = FishType.SwordsMan;
        }

        public AFish Victim { get; private set; }

        public void Walking()
        {
            Victim = FindNextVictim();
            if (Victim != null && Randomizer.Success(0.4))
            {
                Victim.OnDie += ResetVictim;
                Speed = Settings.SwordsMan.MaxSpeed;
                PushState(Attack, FishState.Attack);
                return;
            }
            MoveTo(GetNextPoint());
        }

        public void Attack()
        {
            if (Victim == null || !IsVisibility(Victim))
            {
                Speed = Settings.SwordsMan.OfficialSpeed;
                if (Victim != null)
                {
                    Victim.OnDie -= ResetVictim;
                    Victim = null;
                }
                PopState();
                return;
            }
            MoveTo(GetVictimNextPoint());
        }

        public override void OnCollision(AGameObject anotherObject)
        {
            if (anotherObject != Victim || State != FishState.Attack) return;
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
            return Aquarium
                .GetFishes()
                .Where(x => Settings.SwordsMan.Food.Contains(x.Type))
                .Where(IsVisibility)
                .FirstOrDefault();
        }

        private void ResetVictim(AFish fish)
        {
            if (Victim == fish)
            {
                Victim.OnDie -= ResetVictim;
                Victim = null;
            }
        }

        private bool IsVisibility(AGameObject gameObject)
        {
            return DistanceTo(gameObject) <= 3 * Size.Width;
        }
    }
}

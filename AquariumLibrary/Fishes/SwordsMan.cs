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
            PushState(Walking, FishState.Walking);
        }

        private AFish _victim;

        private AFish _enemy;

        private double officialSpeed = 0.8;

        private double maxSpeed = 1.8;

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

        public AFish Enemy
        {
            get
            {
                return _enemy;
            }
            private set
            {
                _enemy = value;
            }
        }

        public void Walking()
        {
            MoveTo(GetNextPoint());
            Victim = FindNextVictim();
            Enemy = CheckNearestEnemy();
            if (Victim != null )
            {
                if (Randomizer.LowChanceOfAttac())
                {
                    Speed = maxSpeed;
                    PopState();
                    PushState(Attack, FishState.Attack);
                }
            }
            if (Enemy != null )
            {
                Speed = maxSpeed;
                PushState(RunAway, FishState.RunAway);
            }
        }

        public void Attack()
        {
            MoveTo(GetVictimNextPoint());
            if(DistanceTo(Victim) > Size.Width*1.5)
            {
                PopState();
                Speed = officialSpeed;
                PushState(Walking, FishState.Walking);
            }
        }

        public void RunAway()
        {
            MoveTo(GetNextPoint());
            if (DistanceTo(Enemy) > Size.Width * 1.5)
            {
                PopState();
                Speed = officialSpeed;
            }
        }

        protected override PointF GetNextPoint()
        {
            return base.GetNextPoint();
        }

        public override void OnCollision(AFish anotherObject)
        {
            
        }

        public PointF GetVictimNextPoint()
        {
            Direction = new VectorF(Victim.Location.X - Location.X, Victim.Location.Y - Location.Y);
            var nextPoint = new PointF(Location.X + (float)Speed * Direction.X, Location.Y + (float)Speed * Direction.Y);
            return nextPoint;
        }

        private AFish CheckNearestEnemy()
        {
            return Aquarium.GetFishes()
               .Where(x => x.GetType() != typeof(SwordsMan))
               .Where(x => DistanceTo(x) < Size.Width * 2)
               .Where(x => x.Size.Height * x.Size.Width > Size.Width * Size.Height)
               .FirstOrDefault();
        }

        public AFish FindNextVictim()
        {
            return Aquarium.GetFishes()
                .Where(x => x.GetType() != typeof(SwordsMan))
                .Where(x => DistanceTo(x) < Size.Width * 2)
                .Where(x => x.Size.Height * x.Size.Width < Size.Width * Size.Height)
                .FirstOrDefault();
        }
    }
}

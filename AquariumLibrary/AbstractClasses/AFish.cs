using System;
using AquariumLibrary.BaseClasses;
using AquariumLibrary.Interfaces;
using System.Drawing;
using System.Linq;

namespace AquariumLibrary.AbstractClasses
{
    public abstract class AFish : AGameObject, IMovable
    {
        protected AFish(PointF location, SizeF size, IAquarium aquarium)
            : base(location, size, aquarium)
        {
            _brain = new Brain();
        }

        public void MoveTo(PointF point)
        {
            Location = point;
        }

        protected virtual PointF GetNextPoint()
        {
            while (true)
            {
                var nextPoint = new PointF(Location.X + Speed * Direction.X, 
                                           Location.Y + Speed * Direction.Y);
                if (Aquarium.IsPointBelong(nextPoint)) return nextPoint;
                Direction = Direction.Rotate(Randomizer.Next(-90, 90) * Math.PI / 180);
            }
        }

        public bool IsCollision(AFish anotherObject)
        {
            return Rectangle.IntersectsWith(anotherObject.Rectangle);
        }

        public void HandleCollisions()
        {
            Aquarium.GetFishes().ToList().ForEach(x =>
            {
                if (x.IsCollision(this))
                    x.OnCollision(this);
            });
        }

        protected void PushState(Action action, FishState state)
        {
            if (action != null)
                _brain.PushState(action, state);
        }

        protected void PopState()
        {
            _brain.PopState();
        }

        public void Die()
        {
            OnDie?.Invoke(this);
        }

        public void Update()
        {
            _brain.Update();
        }

        public delegate void FishHandler(AFish fish);

        public event FishHandler OnDie;

        /// <summary>
        /// Скорость рыбы
        /// </summary>
        public float Speed { get; protected set; }

        public FishType Type { get; protected set; }

        public FishState State => _brain.CurrentState;

        private readonly Brain _brain;
    }

    public enum FishType
    {
        BlueNeon,
        Piranha,
        Catfish,
        SwordsMan
    }

    public enum FishState
    {
        Walking,
        RunAway,
        Sleep,
        Attack,
        None
    }
}
using System;
using AquariumLibrary.BaseClasses;
using AquariumLibrary.Interfaces;
using System.Drawing;
using System.Linq;

namespace AquariumLibrary.AbstractClasses
{
    public abstract class AFish : AGameObject, IMovable, ICollision<AFish>
    {
        /// <summary>
        /// Скорость рыбы
        /// </summary>
        public double Speed { get; protected set; }
        public FishType FishType { get; protected set; }

        public FishState FishState => _brain.CurrentState;

        private readonly Brain _brain;

        public void MoveTo(PointF point)
        {
            Location = point;
        }

        protected virtual PointF GetNextPoint()
        {
            while (true)
            {
                var nextPoint = new PointF(Location.X + (float)Speed * Direction.X, 
                                           Location.Y + (float)Speed * Direction.Y);
                if (Aquarium.IsPointBelong(nextPoint)) return nextPoint;
                Direction = Direction.Rotate(Randomizer.rnd.Next(0, 180));
            }
        }
        public bool IsPointInside(PointF point)
        {
            var rectangle = Rectangle;
            return rectangle.Left < point.X && point.X < rectangle.Right &&
                   rectangle.Top < point.Y && point.Y < rectangle.Bottom;
        }

        public bool IsCollision(AFish anotherObject)
        {
            return Rectangle.IntersectsWith(anotherObject.Rectangle);
        }

        public abstract void OnCollision(AFish anotherObject);

        public void HandleCollisions()
        {
            Aquarium.GetFishes().ToList().ForEach(x =>
            {
                if (x.IsCollision(this))
                    x.OnCollision(this);
            });
        }
        public override void Update()
        {
            _brain.Update();
        }

        protected AFish(PointF location, SizeF size, IAquarium aquarium)
            : base(location, size, aquarium)
        {
            _brain = new Brain();
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

        public delegate void FishHandler(AFish fish);

        public event FishHandler OnDie;

        public void Die()
        {
            OnDie?.Invoke(this);
        }
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
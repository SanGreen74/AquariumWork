using System;
using System.Collections.Generic;
using AquariumLibrary.BaseClasses;
using AquariumLibrary.Interfaces;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AquariumLibrary.AbstractClasses
{
    public abstract class AFish : AGameObject, IMovable, ICollision<AFish>, IThinker<AFish>
    {
        /// <summary>
        /// Скорость рыбы
        /// </summary>
        public double Speed { get; protected set; }
        public Brain<AFish> Brain { get; protected set; }

        protected VectorF _direction = VectorF.Rigth;

        /// <summary>
        /// Еденичный нормализованный вектор, указывающий направление рыбы
        /// </summary>
        /// <remarks>Не еденичный вектор нормализуется и принудительно приводится к еденичному</remarks>
        public VectorF Direction
        {
            get => _direction;
            set => _direction = Math.Abs(1 - value.GetLength()) > 0.1 ?
                                value.Normalized : value;
        }

        /// <summary>
        /// Принадлежит ли точка point аквариуму
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        protected bool IsPointBelongAquarium(PointF point)
        {
            return 0 < point.X && point.X < Aquarium.Size.Width &&
                   0 < point.Y && point.Y < Aquarium.Size.Height;
        }

        public void Move()
        {
            Location = GetNextPoint();
        }

        protected virtual PointF GetNextPoint()
        {
            while (true)
            {
                var nextPoint = new PointF(Location.X + (float)Speed * Direction.X, Location.Y + (float)Speed * Direction.Y);
                if (IsPointBelongAquarium(nextPoint)) return nextPoint;
                Direction = Direction.Rotate(Random1.rnd.Next(0, 180));
            }
        }

        public void Die()
        {
            Aquarium.RemoveGameObject(this);
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

        protected AFish(PointF location, SizeF size, IAquarium aquarium)
            : base(location, size, aquarium)
        {
        }

        public abstract void Update();

        public void SetBrain(Brain<AFish> brain)
        {
            Brain = brain ?? throw new ArgumentNullException(nameof(brain));
        }
    }
}

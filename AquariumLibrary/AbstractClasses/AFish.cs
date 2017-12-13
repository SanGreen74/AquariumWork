using System;
using AquariumLibrary.BaseClasses;
using AquariumLibrary.Interfaces;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace AquariumLibrary.AbstractClasses
{
    public abstract class AFish : AGameObject, IMovable
    {

        protected VectorF _direction = VectorF.Rigth;
        /// <summary>
        /// Еденичный нормализованный вектор, указывающий направление рыбы
        /// </summary>
        /// <remarks>Вектор обязательно должен быть еденичным</remarks>
        public VectorF Direction
        {
            get { return _direction; }
            protected set
            {
                if (Math.Abs(1 - value.GetLength()) > 0.1)
                    throw new ArgumentException("Длина вектора должна быть равна еденице");
                _direction = value;
            }
        }

        /// <summary>
        /// Принадлежит ли точка point аквариуму
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        protected bool IsPointInsideAquarium(PointF point)
        {
            return (0 < point.X && point.X < Aquarium.Size.Width &&
                    0 < point.Y && point.Y < Aquarium.Size.Height);
        }

        public void Move()
        {
            Location = GetNextPoint();
        }

        protected PointF GetNextPoint()
        {
            var random = new Random();
            while (true)
            {
                var nextPoint = new PointF(Location.X + (float)Speed * Direction.X, Location.Y + (float)Speed * Direction.Y);
                if (IsPointInsideAquarium(nextPoint)) return nextPoint;
                Direction = Direction.Rotate(random.Next(0,180));
            }
        }

        protected AFish(PointF location, SizeF size, IAquarium aquarium)
            : base(location, size, aquarium)
        {
        }
    }
}

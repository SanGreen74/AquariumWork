using System;
using System.Drawing;
using AquariumLibrary.BaseClasses;
using AquariumLibrary.Interfaces;

namespace AquariumLibrary.AbstractClasses
{
    public abstract class AGameObject
    {
        /// <summary>
        /// Создает новый экземпляр объекта
        /// </summary>
        /// <param name="location">Координаты левого верхнего угла прямоугольника, описывающего данный объект</param>
        /// <param name="size">Размеры прямоугольника, описывающего данный объект</param>
        /// <param name="aquarium">Аквариум, в котором должен будет находиться объект</param>
        protected AGameObject(PointF location, SizeF size, IAquarium aquarium)
        {
            Location = location;
            Size = size;
            Aquarium = aquarium;
            Aquarium.AddObject(this);
            Direction = VectorF.RandomVectorF;
        }

        /// <summary>
        /// Текущий аквариум, в котором находится объект
        /// </summary>
        public IAquarium Aquarium { get; }

        /// <summary>
        /// Размер объекта
        /// </summary>
        public SizeF Size { get; protected set; }

        /// <summary>
        /// Координаты объекта
        /// </summary>
        public PointF Location { get; protected set; }

        /// <summary>
        /// Прямоугольник описывающий объект.
        /// </summary>
        public RectangleF Rectangle => new RectangleF(Location, Size);

        /// <summary>
        /// Возвращает расстояние до/от объекта otherObject
        /// </summary>
        /// <param name="otherObject">Объект, до которого считается расстояние</param>
        public double DistanceTo(AGameObject otherObject)
        {
            var otherLocation = otherObject.Location;
            return Math.Sqrt((Location.X - otherLocation.X) * (Location.X - otherLocation.X)
                             + (Location.Y - otherLocation.Y) * (Location.Y - otherLocation.Y));
        }

        /// <summary>
        /// Метод, обрабатывающий состояние объекта на каждой итерации
        /// </summary>
        public abstract void Update();

        private VectorF _direction;

        /// <summary>
        /// Еденичный нормализованный вектор, указывающий направление рыбы
        /// </summary>
        /// <remarks>Не еденичный вектор нормализуется и принудительно приводится к еденичному</remarks>
        public VectorF Direction
        {
            get { return _direction; }
            set
            {
                _direction = Math.Abs(1 - value.GetLength()) > 0.1 ?
                    value.Normalized : value;
            }
        }
    }

}

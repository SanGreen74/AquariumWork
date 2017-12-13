using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquariumLibrary.Interfaces;

namespace AquariumLibrary.AbstractClasses
{
    public abstract class AGameObject : ICollision
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
        /// Скорость рыбы
        /// </summary>
        public double Speed { get; protected set; }

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

        public bool IsPointInside(PointF point)
        {
            var rectangle = Rectangle;
            return rectangle.Left < point.X && point.X < rectangle.Right &&
                   rectangle.Top < point.Y && point.Y < rectangle.Bottom;
        }

        public bool IsCollision(AGameObject otherGameObject)
        {
            return Rectangle.IntersectsWith(otherGameObject.Rectangle);
        }
    }

}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquariumLibrary.AbstractClasses
{
    public abstract class AGameObject
    {
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
    }

}

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
        public PointF Size { get; protected set; }

        /// <summary>
        /// Координаты объекта
        /// </summary>
        public PointF Location { get; protected set; }
    }

}

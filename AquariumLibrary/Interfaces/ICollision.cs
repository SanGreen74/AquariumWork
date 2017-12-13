using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquariumLibrary.AbstractClasses;

namespace AquariumLibrary.Interfaces
{
    public interface ICollision
    {
        /// <summary>
        /// Принадлежит ли точка point объекту
        /// </summary>
        /// <param name="point">Точка PointF</param>
        bool IsPointInside(PointF point);

        /// <summary>
        /// Есть ли соприкосновение с объектом otherGameObject
        /// </summary>
        /// <param name="otherGameObject"></param>
        bool IsCollision(AGameObject otherGameObject);
    }
}

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
        /// Принадлежит ли точка объекту
        /// </summary>
        /// <param name="point">Точка PointF</param>
        /// <returns></returns>
        bool IsPointInside(PointF point);

        /// <summary>
        /// Есть ли столкновение между двумя объектами
        /// </summary>
        /// <param name="otheGameObject"></param>
        /// <returns></returns>
        bool IsCollision(AGameObject otheGameObject);
    }
}

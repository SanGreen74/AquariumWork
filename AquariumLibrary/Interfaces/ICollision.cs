using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquariumLibrary.AbstractClasses;

namespace AquariumLibrary.Interfaces
{
    public interface ICollision<T>
    {
        /// <summary>
        /// Принадлежит ли точка point объекту
        /// </summary>
        /// <param name="point">Точка PointF</param>
        bool IsPointInside(PointF point);

        /// <summary>
        /// Есть ли соприкосновение с объектом anotherObject
        /// </summary>
        /// <param name="anotherObject"></param>
        bool IsCollision(T anotherObject);

        /// <summary>
        /// Обрабатывает столкновение с объектом anotherObject
        /// </summary>
        /// <param name="anotherObject">Объект, с которым обрабатывается столкновение</param>
        void OnCollision(T anotherObject);
    }
}

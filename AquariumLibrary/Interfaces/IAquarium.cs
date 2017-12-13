using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquariumLibrary.AbstractClasses;

namespace AquariumLibrary.Interfaces
{
    public interface IAquarium
    {
        /// <summary>
        /// Возвращает размеры аквариума
        /// </summary>
        Size Size { get; }

        /// <summary>
        /// Возвращает список всех объектов, находящихся в аквариуме
        /// </summary>
        IEnumerable<AGameObject> GetGameObjects();

        /// <summary>
        /// Возвращает список всех рыб, находящихся в аквариуме
        /// </summary>
        IEnumerable<AFish> GetFishes();

        /// <summary>
        /// Принадлежит ли точка point аквариуму
        /// </summary>
        /// <param name="point">точка</param>
        bool IsPointBelongAquarium(PointF point);
    }
}

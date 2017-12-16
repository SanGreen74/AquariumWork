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
        bool IsPointBelong(PointF point);


        /// <summary>
        /// Проверяет координаты на корректность относительно ширины и высоты аквариума.
        /// </summary>
        /// <param name="position"></param>
        bool IsCorrectLocation(PointF position);

        /// <summary>
        /// Добавляет новый объект newGameObject в аквариум.
        /// </summary>
        /// <param name="newGameObject"></param>
        void AddNewGameObject(AGameObject newGameObject);

        /// <summary>
        /// Удаляет из аквариума элемента gameObject если таковой имеется в аквариуме
        /// </summary>
        /// <param name="gameObject"></param>
        void RemoveGameObject(AGameObject gameObject);
    }
}

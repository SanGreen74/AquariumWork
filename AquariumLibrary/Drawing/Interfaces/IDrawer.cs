using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquariumLibrary.AbstractClasses;
using AGameObject = AquariumLibrary.AbstractClasses.AGameObject;

namespace AquariumLibrary.Drawing.Interfaces
{
    public interface IDrawer
    {
        /// <summary>
        /// Рисует игровой объект на указанной графе
        /// </summary>
        /// <param name="graph">Граф, на котором будет рисоваться объект</param>
        /// <param name="gameObject">Объект, который нужно нарисовать</param>
        void Draw(Graphics graph, AGameObject gameObject);

        /// <summary>
        /// Устанавливает соотвествие между объектом в конкретном состоянии и картинкой в данный момент
        /// </summary>
        /// <param name="typeOfGameObject">Тип объекта</param>
        /// <param name="state">Его состояние</param>
        /// <param name="image">Изображение соответствующее типу объекта в указанном состоянии</param>
        /// <param name="onceImage">Применить ли изображение ко всем состояниям. Truе - применить / False - не применять</param>
        void SetSettings(Type typeOfGameObject, Enum state, Image image, bool onceImage);
    }
}

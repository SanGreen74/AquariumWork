using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquariumLibrary.BaseClasses;

namespace AquariumLibrary.Interfaces
{
    public interface IThinker<T>
        where T : class
    {
        /// <summary>
        /// Устанавливает новый мозг brain текущему объекту.
        /// </summary>
        /// <param name="brain">Новый мозг</param>
        void SetBrain(Brain<T> brain);
    }
}

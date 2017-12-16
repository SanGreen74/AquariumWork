using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquariumLibrary.AbstractClasses;

namespace AquariumLibrary.BaseClasses
{
    /// <summary>
    /// Мозг, вызывающий обработку актуального состояния
    /// </summary>
    /// <typeparam name="T">Объект, к которому применяется мозг</typeparam>
    public class Brain<T>
        where T : class
    {
        private readonly T _host;
        private readonly Stack<Action> _states;

        /// <summary>
        /// Создает новый экземляр мозга, указывая его обладателя host
        /// </summary>
        /// <param name="host">Хозяин, к которому привязывается текущий мозг</param>
        public Brain(T host)
        {
            _host = host;
            _states = new Stack<Action>();
        }

        /// <summary>
        /// Вызывает работу текущего состояния
        /// </summary>
        public void Update()
        {
            if (_states.Count != 0)
                _states.Peek().Invoke();
        }


        /// <summary>
        /// Добавляет новое состояние newState, которое будет являться актуальным, до тех пор
        /// пока не будет оно не будет удалено.
        /// </summary>
        /// <param name="newState"></param>
        public void PushState(Action newState)
        {
            _states.Push(newState);
        }


        /// <summary>
        /// Удаляет последнее добавленное состояние.
        /// </summary>
        public void PopState()
        {
            if (_states.Count != 0)
                _states.Pop();
        }
    }
}

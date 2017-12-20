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
    /// <typeparam name="T">Объект, за которым закрепляется мозг</typeparam>
    public class Brain
    {
        private readonly Stack<ActionAndState> _states;
        public int Count => _states.Count;
        public FishState CurrentState => _states.Count == 0 ? FishState.None : _states.Peek().State;
        public Brain()
        {
            _states = new Stack<ActionAndState>();
        }

        /// <summary>
        /// Вызывает работу текущего состояния
        /// </summary>
        public void Update()
        {
            if (_states.Count != 0)
                _states.Peek().Action.Invoke();
        }

        /// <summary>
        /// Добавляет новое состояние action, которое будет являться актуальным, до тех пор
        /// пока не будет оно не будет удалено.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="state">Состояние рыбы</param>
        public void PushState(Action action, FishState state)
        {
            _states.Push(new ActionAndState(action,state));
        }

        /// <summary>
        /// Удаляет последнее добавленное состояние.
        /// </summary>
        public void PopState()
        {
            if (_states.Count != 0)
                _states.Pop();
        }

        private class ActionAndState
        {
            public readonly Action Action;
            public readonly FishState State;

            public ActionAndState(Action action, FishState state)
            {
                Action = action;
                State = state;
            }
        }
    }
}

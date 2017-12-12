using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquariumLibrary.Interfaces
{
    public interface IMovable
    {
        /// <summary>
        /// Двигает объект по текущему направлению
        /// </summary>
        void Move();
    }
}

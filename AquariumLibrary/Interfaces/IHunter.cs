using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquariumLibrary.AbstractClasses;

namespace AquariumLibrary.Interfaces
{
    public interface IHunter
    {
        /// <summary>
        /// Жертва за которой охотится охотник
        /// </summary>
        AFish Victim { get; }

        /// <summary>
        /// Случайный выбор следующей жертвы
        /// </summary>
        /// <returns></returns>
        AFish FindNextVictim();
    }
}
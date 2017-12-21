using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquariumLibrary.AbstractClasses;
using AquariumLibrary.Interfaces;

namespace AquariumLibrary.Factories
{
    public abstract class AFactory<T>
    {
        public abstract T Get(string name);
    }
}

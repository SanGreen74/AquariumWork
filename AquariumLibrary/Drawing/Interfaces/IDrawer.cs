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
        void Draw(Graphics graph, AGameObject gameObject);
        void SetSettings(Type typeOfGameObject, FishState state, Image image, bool onceImage);
    }
}

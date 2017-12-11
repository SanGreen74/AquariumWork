using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquariumLibrary.BaseClasses;
using AquariumLibrary.Interfaces;

namespace AquariumLibrary.AbstractClasses
{
    public abstract class AFish : AGameObject, IMovable, ICollision 
    {
        public VectorF Direction { get; protected set; }
        public abstract void Move();

        public abstract bool IsPointInside(PointF point);

        public abstract bool IsCollision(AGameObject otheGameObject);
    }
}

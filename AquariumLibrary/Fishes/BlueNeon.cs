using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquariumLibrary.AbstractClasses;

namespace AquariumLibrary.Fishes
{
    public class BlueNeon : AFish
    {
        public override void Move()
        {
            throw new NotImplementedException();
        }

        public override bool IsPointInside(PointF point)
        {
            var rectangle = Rectangle;
            return (rectangle.Left <= point.X && point.X < rectangle.Right
                    && rectangle.Bottom <= point.Y && point.Y <= rectangle.Top);
        }

        public override bool IsCollision(AGameObject otheGameObject)
        {
            return Rectangle.IntersectsWith(otheGameObject.Rectangle);
        }
    }
}

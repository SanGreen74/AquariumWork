using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquariumLibrary.AbstractClasses;

namespace AquariumLibrary.BaseClasses
{
    public class Flock
    {
        public int Count => _childrens.Count + 1;
        public int ID { get; }
        public static int MaxFlockLength { get; } = 5;

        private readonly HashSet<Children> _childrens;
        private static Dictionary<int, PointF> CountToOffsets => new Dictionary<int, PointF>
        {
            [1] = new PointF(15, 15),
            [2] = new PointF(15, -15),
            [3] = new PointF(-15, 15),
            [4] = new PointF(-15, -15),
        };

        public AFish Leader { get; private set; }

        public Flock(AFish leader)
        {
            Leader = leader;
            _childrens = new HashSet<Children>();
        }

        public void SetNewLeader(AFish newLeader)
        {
            Leader = newLeader;
        }

        public PointF GetChildrenPosition(AFish fish)
        {
            var offs = _childrens.FirstOrDefault(x => x.Fish == fish);
            var point = new PointF(Leader.Location.X + offs.OffsetX, Leader.Location.Y + offs.OffsetY);
            return point;
        }

        public void AddNewFish(AFish fish)
        {
            if (Leader == null)
                Leader = fish;
            else
                _childrens.Add(new Children(fish, CountToOffsets[Count]));
        }

        public void RemoveFish(AFish fish)
        {
            if (fish == Leader)
            {
                RemoveLeader();
                return;
            }
            _childrens.RemoveWhere(children => children.Fish == fish);
        }

        private void RemoveLeader()
        {
            Leader = null;
            if (Count == 0) return;
            var children = _childrens.FirstOrDefault();
            _childrens.Remove(children);
            if (children != null) Leader = children.Fish;
        }

        public class Children
        {
            public float OffsetX;
            public float OffsetY;
            public AFish Fish;

            public Children(AFish fish, PointF offsetPoint)
            {
                Fish = fish;
                OffsetX = offsetPoint.X;
                OffsetY = offsetPoint.Y;
            }
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}

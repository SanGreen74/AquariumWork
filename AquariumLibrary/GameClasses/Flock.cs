using System;
using System.Collections.Generic;
using System.Linq;
using AquariumLibrary.AbstractClasses;

namespace AquariumLibrary.GameClasses
{
    public class Flock
    {
        private readonly HashSet<Unit> _units = new HashSet<Unit>();

        public AFish Leader { get; private set; }
        public int Count => _units.Count;
        public void Add(AFish fish)
        {
            if (Leader == null)
                Leader = fish;
            fish.OnDie += Remove;
            _units.Add(GetUnitWithOffsets(fish));
        }

        public void Remove(AFish fish)
        {
            var unit = _units.FirstOrDefault(x => x.Fish == fish);
            if (unit == null)
                throw new ArgumentNullException(nameof(unit));
            _units.Remove(unit);
            unit.Fish.OnDie -= Remove;
            if (Count == 0)
            {
                Destroy();
                return;
            }
            if (fish == Leader)
                Redisign();
        }

        private void Redisign()
        {
            var unit = _units.FirstOrDefault();
            unit.OffsetX = 0;
            unit.OffsetY = 0;
            Leader = unit.Fish;
            throw new NotImplementedException();
        }

        private Unit GetUnitWithOffsets(AFish fish)
        {
            // TODO Calculating coordinates
            throw new NotImplementedException();
        }
        public delegate void FlockDestroy(Flock flock);
        public event FlockDestroy OnDestroy;
        private void Destroy()
        {
            OnDestroy?.Invoke(this);
        }
    }

    internal class Unit
    {
        public AFish Fish;
        public float OffsetX;
        public float OffsetY;

        public Unit(AFish fish, float offsetX, float offsetY)
        {
            Fish = fish;
            OffsetX = offsetX;
            OffsetY = offsetY;
        }
    }
}

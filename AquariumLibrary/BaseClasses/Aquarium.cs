using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AquariumLibrary.AbstractClasses;
using AquariumLibrary.Fishes;
using AquariumLibrary.Interfaces;

namespace AquariumLibrary.BaseClasses
{
    public class Aquarium : IAquarium
    {
        private readonly HashSet<AGameObject> _gameObjects;
        private Dictionary<FishType, HashSet<Flock>> _fishTypeToFlocks;

        public Aquarium(Size size)
        {
            Size = size;
            _gameObjects = new HashSet<AGameObject>();
            _fishTypeToFlocks = new Dictionary<FishType, HashSet<Flock>>();
        }

        public Size Size { get; }

        public IEnumerable<AGameObject> GetGameObjects()
        {
            return _gameObjects;
        }

        public IEnumerable<AFish> GetFishes()
        {
            return _gameObjects.OfType<AFish>();
        }

        public void AddObject(AGameObject newGameObject)
        {
            if (!IsCorrectLocation(newGameObject.Location))
                throw new ArgumentException();
            _gameObjects.Add(newGameObject);
        }

        public void RemoveObject(AGameObject gameObject)
        {
            if (_gameObjects.Contains(gameObject))
                _gameObjects.Remove(gameObject);
        }

        public Flock DistributeToFlock(AFish fish)
        {
            return _fishTypeToFlocks.ContainsKey(fish.FishType) ? 
                DistributeToExistingFlock(fish) : CreateNewFlock(fish);
        }

        private Flock CreateNewFlock(AFish fish)
        {
            var flock = new Flock(fish);
            if (!_fishTypeToFlocks.ContainsKey(fish.FishType))
            _fishTypeToFlocks[fish.FishType] = new HashSet<Flock>();
            _fishTypeToFlocks[fish.FishType].Add(flock);
            return flock;
        }

        private Flock DistributeToExistingFlock(AFish fish)
        {
            var flock = _fishTypeToFlocks[fish.FishType].Last();
            if (flock.Count >= Flock.MaxFlockLength)
                return CreateNewFlock(fish);
            flock.AddNewFish(fish);
            return flock;
        }

        public bool IsCorrectLocation(PointF position)
        {
            return 0 <= position.X && position.X <= Size.Width && 0 <= position.Y && position.Y <= Size.Height;
        }
        public bool IsPointBelong(PointF point)
        {
            return 0 < point.X && point.X < this.Size.Width &&
                   0 < point.Y && point.Y < this.Size.Height;
        }
    }
}

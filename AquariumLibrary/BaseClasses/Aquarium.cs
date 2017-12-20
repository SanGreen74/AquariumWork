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
        private HashSet<Flock> _flocks;
        public Aquarium(Size size)
        {
            Size = size;
            _gameObjects = new HashSet<AGameObject>();
            _flocks = new HashSet<Flock>();
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
            if (!(newGameObject is AFish fish)) return;
            fish.OnDie += RemoveObject;
        }

        public void RemoveObject(AGameObject gameObject)
        {
            if (_gameObjects.Contains(gameObject))
                _gameObjects.Remove(gameObject);
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

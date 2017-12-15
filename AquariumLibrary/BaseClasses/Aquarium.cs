using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquariumLibrary.AbstractClasses;
using AquariumLibrary.Fishes;
using AquariumLibrary.Interfaces;

namespace AquariumLibrary.BaseClasses
{
    public class Aquarium : IAquarium
    {
        private HashSet<AGameObject> _gameObjects;

        public Aquarium(Size size)
        {
            Size = size;
            _gameObjects = new HashSet<AGameObject>();
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

        public void AddNewGameObject(AGameObject newGameObject)
        {
            if (!IsCorrectLocation(newGameObject.Location))
                throw new ArgumentException();
            _gameObjects.Add(newGameObject);
        }

        public void RemoveGameObject(AGameObject gameObject)
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
            return (0 < point.X && point.X < this.Size.Width &&
                    0 < point.Y && point.Y < this.Size.Height);
        }
    }
}

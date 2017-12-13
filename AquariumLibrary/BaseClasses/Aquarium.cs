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
        private List<AGameObject> _gameObjects;

        public Aquarium(Size size)
        {
            Size = size;
            Init();
        }

        private void Init()
        {
            _gameObjects = new List<AGameObject>();
            for (var i = 1; i < 10; i++)
            {
                var location = new PointF(30, i*10);
                var size = new SizeF(50,20);
                _gameObjects.Add(new BlueNeon(location, size, this));
            }
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
    }
}

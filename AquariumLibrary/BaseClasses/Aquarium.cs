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
            var location = new PointF();
            var size = new SizeF();
            _gameObjects = new List<AGameObject>();
            for (var i = 1; i < 3; i++)
            {
                location = new PointF(30, i*10);
                size = new SizeF(50,20);
                _gameObjects.Add(new BlueNeon(location, size, this));
                
            }
            location = new PointF(200, 200);
            size = new SizeF(60, 30);
            _gameObjects.Add(new Piranha(location, size, this));
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

        public bool IsPointBelongAquarium(PointF point)
        {
            return (0 < point.X && point.X < this.Size.Width &&
                    0 < point.Y && point.Y < this.Size.Height);
        }
    }
}

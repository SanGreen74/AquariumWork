using System;
using System.Drawing;
using AquariumLibrary.AbstractClasses;
using AquariumLibrary.Fishes;
using AquariumLibrary.Interfaces;
using FluentAssertions;
using NUnit.Framework;

namespace AquariumLibrary.Tests
{
    [TestFixture]
    public class GameObjectShould
    {
        private IAquarium _aquarium;
        private AGameObject _neon1;
        private AGameObject _neon2;
        private AGameObject _neon3;
        private AGameObject _neon4;
        private readonly SizeF _defaultFishSize = new SizeF(5f, 2f);
        private const double Epsilon = 0.01;

        [SetUp]
        public void SetUp()
        {
            _neon1 = new BlueNeon(new PointF(10f, 10f), _defaultFishSize, _aquarium);
            _neon2 = new BlueNeon(new PointF(9f, 9f), _defaultFishSize, _aquarium);
            _neon3 = new BlueNeon(new PointF(8f, 8f), _defaultFishSize, _aquarium);
            _neon4 = new BlueNeon(new PointF(8f, 8.001f), _defaultFishSize, _aquarium);
        }

        [Test]
        public void NeonsCollision()
        {
            _neon1.IsCollision(_neon2).Should().BeTrue();
            _neon1.IsCollision(_neon4).Should().BeTrue();
        }

        [Test]
        public void NeonsNotCollision()
        {
            _neon1.IsCollision(_neon3).Should().BeFalse();
        }

        [Test]
        public void PointsInsideInNeon1()
        {
            var point1 = new PointF(_neon1.Location.X + _defaultFishSize.Width / 5f,
                                    _neon1.Location.Y + _defaultFishSize.Height / 5f);
            var point2 = new PointF(_neon1.Location.X + _defaultFishSize.Width - 0.01F,
                                    _neon1.Location.Y + _defaultFishSize.Height - 0.01f);

            _neon1.IsPointInside(point1).Should().BeTrue();
            _neon1.IsPointInside(point2).Should().BeTrue();
        }

        [Test]
        public void PointsNotInsideInNeon1()
        {
            var point1 = new PointF(_neon1.Location.X + _defaultFishSize.Width * 2f,
                _neon1.Location.Y + _defaultFishSize.Height * 2f);
            var point2 = new PointF(_neon1.Location.X + _defaultFishSize.Width,
                _neon1.Location.Y + _defaultFishSize.Height);

            _neon1.IsPointInside(point1).Should().BeFalse();
            _neon1.IsPointInside(point2).Should().BeFalse();
        }

        [Test]
        public void DistanceBetweenCorrect()
        {
            var zero = Math.Abs(Math.Sqrt(2) - _neon1.DistanceTo(_neon2));
            zero.Should().BeLessThan(Epsilon);
            _neon1.DistanceTo(_neon1).Should().BeLessThan(Epsilon);
        }
    }
}



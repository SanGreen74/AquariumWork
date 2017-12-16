using System;
using System.Drawing;
using AquariumLibrary.AbstractClasses;
using AquariumLibrary.BaseClasses;
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
        private AFish _neon1;
        private AFish _neon2;
        private AFish _neon3;
        private AFish _neon4;
        private readonly SizeF _defaultFishSize = new SizeF(5f, 2f);
        private const double Epsilon = 0.01;

        [SetUp]
        public void SetUp()
        {
            _aquarium = new Aquarium(new Size(500,500));
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

        [Test]
        public void CorrectSetDirection()
        {
            var zero = CalculateError(1.0, _neon1.Direction.GetLength());
            zero.Should().BeLessThan(Epsilon);
            _neon1.Direction = new VectorF(5123f, 2132f);
            zero = CalculateError(1.0, _neon1.Direction.GetLength());
            zero.Should().BeLessThan(Epsilon);
        }

        private double CalculateError(double should, double actual)
        {
            return Math.Abs(should - actual);
        }
    }
}



using System;
using System.Drawing;
using System.Linq;
using AquariumLibrary.BaseClasses;
using AquariumLibrary.Fishes;
using AquariumLibrary.Interfaces;
using FluentAssertions;
using NUnit.Framework;

namespace AquariumLibrary.Tests
{
    [TestFixture]
    public class AquariumShould
    {
        private IAquarium _aquarium;

        [SetUp]
        public void SetUp()
        {
            _aquarium = new Aquarium(new Size(500, 500));
        }

        [Test]
        public void CorrectAddNewObject()
        {
            var randomCount = new Random().Next(50);
            BlueNeon neon;
            for (var i = 0; i < randomCount; i++)
                neon = new BlueNeon(new PointF(10, 10), new SizeF(10, 10), _aquarium);
            _aquarium.GetGameObjects().Count().Should().Be(randomCount);
        }

        [Test]
        public void AddObjectWithIncorrectLocation()
        {
            //void createNewFish() => new BlueNeon(new PointF(_aquarium.Size.Width + 1, 10), _aquarium.Size, _aquarium);
            //Assert.Throws<ArgumentException>(createNewFish);
        }

        [Test]
        public void RemovingGameObject()
        {
            BlueNeon neon = null;
            var randomCount = new Random().Next(1, 50);
            for (var i = 0; i < randomCount; i++)
                neon = new BlueNeon(new PointF(10, 10), new SizeF(10, 10), _aquarium);
            _aquarium.RemoveObject(neon);
            _aquarium.GetGameObjects().Count().Should().Be(randomCount - 1);
        }
    }
}

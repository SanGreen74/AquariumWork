using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AquariumLibrary.AbstractClasses;
using AquariumLibrary.Fishes;
using AquariumLibrary.GameClasses;
using AquariumLibrary.Interfaces;
using FluentAssertions;
using NUnit.Framework;

namespace AquariumLibrary.Tests
{
    [TestFixture]
    public class BlueNeonShould
    {
        private BlueNeon _neon;
        private IHunter _hunter;
        private IAquarium _aquarium;

        [SetUp]
        public void SetUp()
        {
            _aquarium = new Aquarium(new Size(600,600));
            _neon = new BlueNeon(new PointF(400, 400), Settings.BlueNeon.Size, _aquarium);
            _hunter = new Piranha(new PointF(420, 420), Settings.Piranha.Size, _aquarium);
            _aquarium.AddObject(_neon);
            _aquarium.AddObject((AFish)_hunter);
        }

        [Test]
        public void NeonShouldRun()
        {
            _neon.Update();
            _neon.State.Should().Be(FishState.RunAway);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquariumLibrary.BaseClasses;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AquariumLibrary.Tests
{
    [TestFixture]
    public class VectorFShould
    {
        private VectorF _vector1;
        private const double Epsilon = 0.01;

        [SetUp]
        public void SetUp()
        {
            _vector1 = new VectorF(123f, 342f).Normalized;
        }

        [Test]
        public void VectorLengthIsOne()
        {
            var zero = Math.Abs(1 - _vector1.GetLength());
            zero.Should().BeLessThan(Epsilon);
        }
    }
}

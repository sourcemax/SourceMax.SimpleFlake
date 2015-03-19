using System;
using System.Linq;
using System.Numerics;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using SourceMax.SimpleFlake;

namespace SourceMax.SimpleFlake.Tests {

    [TestClass]
    public class FlakeTests {

        [TestMethod]
        public void Create_FromValue0_GeneratesProperValue() {
            this.TestFlakeValue(0);
        }

        [TestMethod]
        public void Create_FromValue1_GeneratesProperValue() {
            this.TestFlakeValue(1);
        }

        [TestMethod]
        public void Create_FromValueInt32Max_GeneratesProperValue() {
            this.TestFlakeValue(Int32.MaxValue);
        }

        [TestMethod]
        public void Create_FromValueInt64Max_GeneratesProperValue() {
            this.TestFlakeValue(Int64.MaxValue);
        }

        [TestMethod]
        public void Create_WithDifferentValues_AreEqual() {

            // Arrange
            var seed = 123456;

            // Act
            var flake1 = Flake.Create<Flake>(seed);
            var flake2 = Flake.Create<Flake>(seed);

            // Assert
            Assert.IsTrue(flake1 == flake2);
            Assert.AreEqual(flake1, flake2);
        }

        [TestMethod]
        public void Create_WithSameValue_AreNotEqual() {

            // Arrange
            var seed = 123456;

            // Act
            var flake1 = Flake.Create<Flake>(seed);
            var flake2 = Flake.Create<Flake>(seed + 1);

            // Assert
            Assert.IsTrue(flake1 != flake2);
            Assert.AreNotEqual(flake1, flake2);
        }

        private void TestFlakeValue(BigInteger value) {

            // Arrange
            var flake = Flake.Create<Flake>(value);

            // Act
            var newValue = flake.Value;

            // Assert
            Assert.AreEqual(newValue, value);
        }
    }
}
using System;
using System.Linq;
using System.Numerics;
using System.Diagnostics;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Me = SourceMax.SimpleFlake.Base58;


namespace SourceMax.SimpleFlake.Tests.Base58 {

    [TestClass]
    public class FlakeTests {

        //[TestMethod]
        //public void Create_1MillionFlakes_PerformsOK() {

        //    //var generator = new Generator<Flake>();       // 4100 +/- milliseconds
        //    var generator = new FlakeGenerator();    // 2800 +/- milliseconds
        //    //var generator = new FastGenerator();          // 2700 +/- milliseconds

        //    var stopwatch = Stopwatch.StartNew();

        //    //Enumerable.Range(0, 1000000).ToList().ForEach(i => generator.CreateFlake());
        //    for (int i = 0; i < 1000000; i++) {
        //        generator.CreateFlake();
        //    }

        //    Debug.WriteLine("Took " + stopwatch.ElapsedMilliseconds + " milliseconds");
        //}

        [TestMethod]
        public void Create_FromValue0_GeneratesProperBase58String() {
            this.TestFlakeBase58String(0, "111111111111");
        }

        [TestMethod]
        public void Create_FromValue1_GeneratesProperBase58String() {
            this.TestFlakeBase58String(1, "111111111112");
        }

        [TestMethod]
        public void Create_FromValueInt32Max_GeneratesProperBase58String() {
            this.TestFlakeBase58String(Int32.MaxValue, "1111114GmR58");
        }

        [TestMethod]
        public void Create_FromValueInt64Max_GeneratesProperBase58String() {
            this.TestFlakeBase58String(Int64.MaxValue, "1NQm6nKp8qFC");
        }

        [TestMethod]
        public void Create_WithTwoSequentialValues_GeneratesSortdedIDs() {

            // Arrange
            var seed = 123456;

            // Act
            var id1 = Flake.Create<Me.Flake>(seed).ToString();
            var id2 = Flake.Create<Me.Flake>(seed + 1).ToString();

            // Assert
            Assert.IsTrue(String.Compare(id1, id2, StringComparison.Ordinal) < 0);
        }

        [TestMethod]
        public void Create_WithTheSameNumber_GeneratesIdenticalIDs() {

            // Arrange
            var seed = 123456;

            // Act
            var id1 = Flake.Create<Me.Flake>(seed).ToString();
            var id2 = Flake.Create<Me.Flake>(seed).ToString();

            // Assert
            Assert.IsTrue(String.Compare(id1, id2, StringComparison.Ordinal) == 0);
        }

        private void TestFlakeBase58String(BigInteger value, string targetString) {
            
            // Arrange
            var flake = Flake.Create<Me.Flake>(value);

            // Act
            var base58String = flake.ToString();

            // Assert
            Assert.AreEqual(targetString, base58String);
        }
    }
}
using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using SUT = SourceMax.SimpleFlake.Base58;

namespace SourceMax.SimpleFlake.Tests.Base58 {

    [TestClass]
    public class FlakeGeneratorTests {

        [TestMethod]
        public void TestMethod1() {

            var generator = new SUT.FlakeGenerator();
            var id = generator.CreateFlake().ToString();
        }
    }
}

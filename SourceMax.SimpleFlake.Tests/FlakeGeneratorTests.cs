using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SourceMax.SimpleFlake.Tests {

    [TestClass]
    public class FlakeGeneratorTests {

        [TestMethod]
        public void TestMethod1() {

            var generator = new FlakeGenerator<Flake>();
            var id = generator.CreateFlake().ToString();
        }
    }
}

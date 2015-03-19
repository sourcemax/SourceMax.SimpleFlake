using System.Numerics;

using Base = SourceMax.SimpleFlake;

namespace SourceMax.SimpleFlake.Base58 {

    public class FlakeGenerator : Base.FlakeGenerator<Base58.Flake>, IFlakeGenerator {

        protected override Flake InstantiateNewFlake(BigInteger value) {

            // Doing creation this will will mitigate the Flake.Create method's need
            // to use Activator.CreateInstance, which will be much more performant
            return new Base58.Flake(value);
        }
    }
}
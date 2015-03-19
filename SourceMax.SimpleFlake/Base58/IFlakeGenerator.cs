using System;

namespace SourceMax.SimpleFlake.Base58 {

    public interface IFlakeGenerator {

        Flake CreateFlake();

        Flake CreateFlake(DateTime timestamp);
    }
}
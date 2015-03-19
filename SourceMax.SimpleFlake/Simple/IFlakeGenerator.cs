using System;

namespace SourceMax.SimpleFlake.Simple {

    public interface IFlakeGenerator {

        Flake CreateFlake();

        Flake CreateFlake(DateTime timestamp);
    }
}
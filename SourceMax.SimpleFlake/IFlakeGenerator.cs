using System;

namespace SourceMax.SimpleFlake {

    /// <summary>
    /// An interface representing the generic FlakeGenerator contract.
    /// </summary>
    /// <typeparam name="T">An implementation of Flake or a subclass of Flake.</typeparam>
    public interface IFlakeGenerator<T> where T : Flake {

        T CreateFlake();

        T CreateFlake(DateTime timestamp);
    }
}
using System;
using System.Numerics;

namespace SourceMax.SimpleFlake {
    
    /// <summary>
    /// Basic data structure for a Flake that wraps a BigInteger number.
    /// </summary>
    /// <remarks>
    /// This base class serves as the foundation for inheriting classes that might want to 
    /// implement different serialization.
    /// </remarks>
    public class Flake {

        /// <summary>
        /// Constructs the Flake based on the value.
        /// </summary>
        /// <param name="value">The BigInteger value to build the Flake.</param>
        public Flake(BigInteger value) {
            this.Value = value;
        }
        
        /// <summary>
        /// The underlying value of the Flake.
        /// </summary>
        public BigInteger Value { get; private set; }

        /// <summary>
        /// Static method
        /// </summary>
        /// <typeparam name="T">The type T (of Flake) to instantiate.</typeparam>
        /// <param name="value">The BigInteger value to build the Flake.</param>
        /// <returns>A Flake of type T.</returns>
        /// <remarks>
        /// This is relatively slow because it uses reflection to
        /// create the instance. Therefore, it's best to avoid this
        /// code in a high throughput situation where generating
        /// Flakes by the millions. 
        /// </remarks>
        public static T Create<T>(BigInteger value) where T : Flake {
            return (T)Activator.CreateInstance(typeof(T), value);
        }

        /// <summary>
        /// Converts the underlying value of the Flake into its equivalent string representation. 
        /// </summary>
        /// <returns>The serialized representation of the Flake's Value.</returns>
        public override string ToString() {
            return this.Value.ToString();
        }
    }
}
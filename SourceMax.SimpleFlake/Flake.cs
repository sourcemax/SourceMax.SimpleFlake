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

        public override bool Equals(System.Object obj) {
            
            // If parameter is null return false.
            if (obj == null) {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Flake other = obj as Flake;
            if ((System.Object)other == null) {
                return false;
            }

            // Return true if the fields match:
            return (this.Value == other.Value);
        }

        public bool Equals(Flake other) {

            // If parameter is null return false:
            if ((object)other == null) {
                return false;
            }

            // Return true if the fields match:
            return (this.Value == other.Value);
        }

        public static bool operator ==(Flake a, Flake b) {

            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b)) {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null)) {
                return false;
            }

            // Return true if the fields match:
            return a.Value == b.Value;
        }

        public static bool operator !=(Flake a, Flake b) {
            return !(a == b);
        }
    }
}
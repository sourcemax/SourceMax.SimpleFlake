using System;
using System.Numerics;
using System.Security.Cryptography;

namespace SourceMax.SimpleFlake {

    public class FlakeGenerator<T> : IFlakeGenerator<T> where T : Flake {

        // If not other value is supplied, use this as the Epoch value
        private static readonly DateTime DEFAULT_EPOCH = new DateTime(2000, 01, 01);

        /// <summary>
        /// The Epoch point in time from which the timestamp will be calculated.
        /// </summary>
        public DateTime Epoch { get; private set; }

        /// <summary>
        /// Default constructor that uses a default value of 2000-01-01 as the Epoch.
        /// </summary>
        public FlakeGenerator() : this(DEFAULT_EPOCH) {
            // Just use the DEFAULT_EPOCH if nothing else is provide
        }

        /// <summary>
        /// Constructor that provides an alternate Epoch for calculating timestamps.
        /// </summary>
        /// <param name="customEpoch">The DateTime value to use as the Epoch when generating timestamps.</param>
        public FlakeGenerator(DateTime customEpoch) {
            this.Epoch = customEpoch;
        }

        /// <summary>
        /// Creates a Flake object based on the current time that can be used as part of a distributed ID system.
        /// </summary>
        /// <returns>A Flake that wraps an underlying BigInteger value made up of 41 bits of timestamp data plus 23 bits of random bits to avoid collisions in a distributed system.</returns>
        public virtual T CreateFlake() {
            return this.CreateFlake(DateTime.UtcNow);
        }

        /// <summary>
        /// Creates a Flake object based on a given date and time that can be used as part of a distributed ID system.
        /// </summary>
        /// <returns>A Flake that wraps an underlying BigInteger value made up of 41 bits of timestamp data plus 23 bits of random bits to avoid collisions in a distributed system.</returns>
        public virtual T CreateFlake(DateTime timestamp) {

            if (timestamp < this.Epoch) {
                throw new Exception("The timestamp cannot be earlier than the Epoch time being used.");
            }

            // 64-bit Id will be 41 bits of milliseconds + 23 bits random
            var bigTicks = this.GetMillisecondsSinceEpoch(timestamp);

            // Get some random bits
            var saltBytes = this.GetRandomBytes(3);

            // Convert the random bytes to a BigInteger so we can bit shift
            var bigSalt = new BigInteger(saltBytes);

            // We are going to bit-shift the two values so we end up with the 
            // correct number of bits in each value
            bigSalt = BigInteger.Abs(bigSalt) >> 1;     // Shift right to go from 24 bits to 23
            bigTicks = bigTicks << 23;                  // Shift left to make room for the Salt

            // Bitwise OR the bits together to get one big 64-bit string where the first 41 bits
            // are the timestamp and the last 23 are the salt
            var result = bigTicks | bigSalt;

            // Create a new Flake of type T that will be returned
            var flake = this.InstantiateNewFlake(result);

            return flake;
        }

        /// <summary>
        /// Calculates the number of milliseconds that have elapsed since the Epoch time.
        /// </summary>
        /// <param name="now">DateTime value after Epoch time.</param>
        /// <returns>A BigInteger value representing the number of milliseconds since the Epoch time.</returns>
        protected virtual BigInteger GetMillisecondsSinceEpoch(DateTime now) {

            var millisecondsSinceEpoch = (now - this.Epoch).TotalMilliseconds;
            var bigMillisecondsSinceEpoch = new BigInteger(millisecondsSinceEpoch);
            return bigMillisecondsSinceEpoch;
        }

        /// <summary>
        /// Generates a random set of bytes.
        /// </summary>
        /// <param name="count">The number of bytes to randomly generate.</param>
        /// <returns>An array of randomized bytes of the given length.</returns>
        protected virtual byte[] GetRandomBytes(int count) {
            
            var randomBytes = new byte[count];
            RandomNumberGenerator.Create().GetBytes(randomBytes);
            return randomBytes;
        }

        /// <summary>
        /// Creates a Flake instance of type T based on the given value.
        /// </summary>
        /// <param name="value">The underlying value of the Flake being created.</param>
        /// <returns>A Flake of type T.</returns>
        /// <remarks>
        /// The default implementation relies on reflection to create the instance, so this
        /// method should be overridden in subclasses in a way that avoids reflection, 
        /// especially in situations where high throughput is required.
        /// </remarks>
        protected virtual T InstantiateNewFlake(BigInteger value) {
            return Flake.Create<T>(value);
        }
    }
}
using System;
using System.Numerics;

using Base = SourceMax.SimpleFlake;

namespace SourceMax.SimpleFlake.Base58 {

    public class Flake : Base.Flake {

        private const string BASE58_DIGITS = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";

        private string Base58Value { get; set; }

        public Flake(BigInteger value) : base(value) {

            // Nothing to do here but pass along the constructor param to the base
        }

        public override string ToString() {

            // Use a default length of 12
            return this.ToString(12);
        }

        public string ToString(int length) {

            // Since this.Value cannot change, we only need to calculate
            // the string representation once.
            if (this.Base58Value == null) {

                BigInteger intData = this.Value;
                string result = "";

                while (intData > 0) {

                    int remainder = (int)(intData % 58);
                    intData = intData / 58;
                    result = BASE58_DIGITS[remainder] + result;
                }

                // Pad the string to get the correct length
                this.Base58Value = result.PadLeft(length, BASE58_DIGITS[0]);
            }

            return this.Base58Value;
        }

        public static Flake Create(string value) {

            BigInteger intData = 0;

            for (int i = 0; i < value.Length; i++) {

                int digit = BASE58_DIGITS.IndexOf(value[i]); // Slow

                if (digit < 0) {
                    throw new FormatException(string.Format("Invalid Base58 character `{0}` at position {1}", value[i], i));
                }

                intData = intData * 58 + digit;
            }

            return new Flake(intData);
        }
    }
}
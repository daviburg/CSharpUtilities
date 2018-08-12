// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MathAlgorithms.cs" company="Microsoft">
//   This file is part of Daviburg Utilities.
//
//   Daviburg Utilities is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//   
//   Daviburg Utilities is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//   GNU General Public License for more details.
//
//   You should have received a copy of the GNU General Public License
//   along with Daviburg Utilities.  If not, see <see href="https://www.gnu.org/licenses"/>.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Daviburg.Utilities
{
    using System;

    public static class MathAlgorithms
    {
        /// <summary>
        /// Swaps values if left value is greater than right value.
        /// </summary>
        /// <remarks>Using temporary variable approach as in-place XOR swap is frowned upon when memory constraints do not dictate it.</remarks>
        public static void SwapIfGreater<T>(ref T leftValue, ref T rightValue) where T : System.IComparable<T>
        {
            T temporaryVariable;
            if (leftValue.CompareTo(rightValue) > 0)
            {
                temporaryVariable = leftValue;
                leftValue = rightValue;
                rightValue = temporaryVariable;
            }
        }

        /// <summary>
        /// Modulo of a power, made to fit in available type by aggressively applying the modulo
        /// </summary>
        /// <param name="_base">The base number for the power operation.</param>
        /// <param name="exponent">The exponent for the power operation.</param>
        /// <param name="modulus">The modulos for the modulo operation.</param>
        /// <remarks>
        /// Power modulo is a special case of modular arithmetic multiplicative property
        /// (A * B) % C == ((A % C) * (B % C)) % C
        /// Hence (Base ^ Exponent) % C == ((Base % C) ^ Exponent) % C
        /// and furthermore (Base ^ Exponent) % C == ((Base % C) * ((Base % C) ^ (Exponent - 1))) % C
        /// </remarks>
        public static long PowerModulo(long _base, long exponent, long modulus)
        {
            if (exponent == 0)
            {
                return 1;
            }

            // (_base ^ exponent) % modulo = (((_base ^ (exponent - 2  ^ n)) % modulo) * (_base ^ (2 ^ n)) ) % modulo
            // Decompose exponent in powers of 2, which happen to be all the bits equal to 1.
            // Start with (_base ^ (2 ^ 0) % modulo) where 2 ^ 0 is 1 hence it's just _base % modulo
            // The result will be multipled by this only if the exponent bit at offset 0 is equal to 1.
            // Then it will be used to calculate the next power modulo.
            var intermediaryPowerModulo = _base % modulus;
            long result = 1;

            do
            {
                if ((exponent & 1) == 1)
                {
                    result = (result * intermediaryPowerModulo) % modulus;

                    // Once the exponent has been shifted sufficiently to the right to be just 1, we've applied the last multiplication needed.
                    if (exponent == 1)
                    {
                        break;
                    }
                }

                // We haven't exhausted the 1 bits in the exponent yet, shifting right further.
                exponent >>= 1;

                // Each power of 2 in the exponent doubles the number of base multiplications needed.
                intermediaryPowerModulo = (intermediaryPowerModulo * intermediaryPowerModulo) % modulus;
            } while (true);

            return result;
        }

        /// <summary>
        /// Gets the greatest common divisor using the binary algorithm.
        /// </summary>
        public static long GreatestCommonDivisor(long leftValue, long rightValue)
        {
            if (leftValue < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(leftValue), leftValue, "Only natural numbers are supported.");
            }

            if (rightValue < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rightValue), rightValue, "Only natural numbers are supported.");
            }

            if (leftValue == rightValue)
            {
                return leftValue;
            }

            // Special case zeroes to ensure loop termination going forward.
            if (leftValue == 0)
            {
                return rightValue;
            }

            if (rightValue == 0)
            {
                return leftValue;
            }

            var shiftCount = 0;

            // While both values are even, keep binary shifting right i.e. divide by 2 as it is a common divisor.
            // Both values are even if their right most bit is zero.
            // We store the common factors of 2 so we can apply it back to the GCD found on the remaining values.
            // In lower level languages than C# this can be optimized with built-in trailing zero count function.
            for (; ((leftValue | rightValue) & 1) == 0; shiftCount++)
            {
                leftValue >>= 1;
                rightValue >>= 1;
            }

            // If one value is still even, then the other one must be odd, as per the exit condition of the previous iteration.
            // When one is even and the other odd, 2 is not a common divisor, hence the even number can be divided by 2 while the other remains the same.
            while ((leftValue & 1) == 0)
            {
                leftValue >>= 1;
            }

            // From here on, leftValue is always odd.
            // Now that we have an odd value, we are looking for the odd number which is the gcd of the remaining values
            do
            {
                // Remove all factors of 2 in rightValue, as we ensured current leftValue is odd, hence 2 is not a common divisor.
                while ((rightValue & 1) == 0)
                {
                    rightValue >>= 1;
                }

                // Here both values are odd.
                // Swap if necessary so leftValue <= rightValue.
                MathAlgorithms.SwapIfGreater(ref leftValue, ref rightValue);

                // Setting rightValue to the difference guarantees it is even, while leftValue remains odd.
                // If the difference is zero, we stop as the remaining greatest common divisor is met (and in left value).
                // If the difference is not zero, we need to seek the gcd between it and the smallest value (in left value).
                rightValue = rightValue - leftValue;
            } while (rightValue != 0);

            // Re-apply the common factors of 2.
            return leftValue << shiftCount;
        }
    }
}

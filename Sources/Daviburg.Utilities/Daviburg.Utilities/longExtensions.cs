// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LongExtensions.cs" company="Microsoft">
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
    using System.Collections.Generic;

    public static class LongExtensions
    {
        /// <summary>
        /// Gets the integral part of the squared root of a given number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <remarks>This formula is frequently used as upper search limit for primes and divisors.</remarks>
        public static long IntegralPartOfSquareRoot(this long value) => Convert.ToInt64(Math.Floor(Math.Sqrt(value)));

        public static long Power(this long value, long exponent) => Convert.ToInt64(Math.Pow(value, exponent));

        /// <summary>
        /// Calculates the summation of natural numbers up to given value.
        /// </summary>
        /// <param name="value">The natural number to compute summation of.</param>
        /// <remarks>See also <see cref="IntExtensions.Summation(int)"/>.</remarks>
        public static long Summation(this long value)
        {
            return value * (value + 1) / 2;
        }

        /// <summary>
        /// Calculates the natural number which summation of is closest to the given value.
        /// </summary>
        /// <param name="sum">The summation to infer a natural number from.</param>
        /// <remarks>This works because square root of n*n + n is closest to n.</remarks>
        public static long AntiSummation(this long sum)
        {
            return Convert.ToInt64(Math.Floor(Math.Sqrt(sum * 2)));
        }

        /// <summary>
        /// Prime factorization of a given natural number.
        /// </summary>
        /// <param name="value">The natural number.</param>
        /// <remarks>The natural number must be greater or equal to two. This method is roughly equivalent to a trial division algorithm optimized by testing only for primes numbers.</remarks>
        public static List<PrimeFactor> PrimeFactorization(this long value)
        {
            if (value <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, "Prime factorization is only applicable to natural numbers greater than one. ");
            }

            var primeFactors = new List<PrimeFactor>();

            for (int primeOrder = 1; value != 1; primeOrder++)
            {
                var exponent = 0;

                // NOTE(daviburg): the primes table is zero-based while the order of primes starts at 1
                while (value % Primes.Singleton[primeOrder - 1] == 0)
                {
                    value /= Primes.Singleton[primeOrder - 1];
                    exponent++;
                }

                if (exponent != 0)
                {
                    primeFactors.Add(new PrimeFactor(order: primeOrder, exponent: exponent));
                }
                else
                {
                    // Short-cut the search once we pass the square root limit, as the remaining value is prime
                    if (Primes.Singleton[primeOrder - 1] > value.IntegralPartOfSquareRoot())
                    {
                        primeFactors.Add(new PrimeFactor(baseNumber: value, exponent: 1));
                        break;
                    }
                }
            }

            return primeFactors;
        }

        public static List<PrimeFactor> BlendPrimeFactorization(this long value)
        {
            if (value <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, "Prime factorization is only applicable to natural numbers greater than one. ");
            }

            var primeFactors = new List<PrimeFactor>();

            for (int primeOrder = 1; value != 1 && primeOrder < 100; primeOrder++)
            {
                var exponent = 0;

                // NOTE(daviburg): the primes table is zero-based while the order of primes starts at 1
                while (value % Primes.Singleton[primeOrder - 1] == 0)
                {
                    value /= Primes.Singleton[primeOrder - 1];
                    exponent++;
                }

                if (exponent != 0)
                {
                    primeFactors.Add(new PrimeFactor(order: primeOrder, exponent: exponent));
                }
                else
                {
                    // Short-cut the search once we pass the square root limit, as the remaining value is prime
                    if (Primes.Singleton[primeOrder - 1] > value.IntegralPartOfSquareRoot())
                    {
                        primeFactors.Add(new PrimeFactor(baseNumber: value, exponent: 1));
                        return primeFactors;
                    }
                }
            }

            // If we stopped testing small prime factors before we found all the primes, do the remaining through Rho
            if (value != 1)
            {
                primeFactors.AddRange(value.RhoPrimeFactorization());
            }

            return primeFactors;
        }

        public static List<PrimeFactor> RhoPrimeFactorization(this long value)
        {
            if (value <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, "Prime factorization is only applicable to natural numbers greater than one. ");
            }

            var primeFactors = new List<PrimeFactor>();

            // If value is already prime, Rho will fail to find a factor and spins needlessly.
            // Thus don't even start when value is prime.
            if (value.IsPrime())
            {
                primeFactors.Add(new PrimeFactor(baseNumber: value, exponent: 1));
                return primeFactors;
            }

            for (int offset = 1; value != 1 && offset < 20; offset++)
            {
                long factor = default(long);
                if (value.TryFindFactor(offset, ref factor))
                {
                    while (!factor.IsPrime())
                    {
                        long furtherFactor = default(long);
                        if (factor.TryFindFactor(offset, ref furtherFactor))
                        {
                            factor = furtherFactor;
                        }
                        else
                        {
                            // Guessing didn't work but we know this factor isn't prime so revert to brute force - trial by division.
                            var primeIndex = 0;
                            while (factor % Primes.Singleton[primeIndex] != 0)
                            {
                                primeIndex++;
                            }

                            factor /= Primes.Singleton[primeIndex];
                        }
                    }

                    var exponent = 1;
                    value /= factor;
                    while (value % factor == 0)
                    {
                        exponent++;
                        value /= factor;
                    }

                    primeFactors.Add(new PrimeFactor(baseNumber: factor, exponent: exponent));

                    // Stop when new remaining factor a prime. Rho will fail to find a factor and spins needlessly.
                    if (value.IsPrime())
                    {
                        primeFactors.Add(new PrimeFactor(baseNumber: value, exponent: 1));
                        value = 1;
                    }
                }
            }

            // If we stopped because Rho spinned without finding a divisor on a non-prime value, we need to resort to trial by division.
            if (value != 1)
            {
                primeFactors.AddRange(value.PrimeFactorization());
            }

            return primeFactors;
        }

        /// <summary>
        /// The Miller primality test uses a witness sub-routine to determine if a value may be prime
        /// </summary>
        /// <param name="value">The value to be tested for primality.</param>
        /// <param name="powersOfTwo">The powers of 2 from the value - 1.</param>
        /// <param name="factor">The factor from the value - 1.</param>
        /// <param name="witness">The witness value.</param>
        /// <returns>False if value is composite. True if value *could* be prime.</returns>
        /// <remarks>Although this is a translation of the pseudo code from Wikipedia, for prime number 4447483681 witness 2 will exhaust the loop and claim it's a composite.</remarks>
        private static bool WitnessTest(long value, long powersOfTwo, long factor, long witness)
        {
            // First iteration 0 starting power modulo is (witness ^ ((2 ^ 0) * factor)) % value
            // i.e. just (witness ^ factor) % value
            var powerModulo = MathAlgorithms.PowerModulo(_base: witness, exponent: factor, modulus: value);

            // First condition for being identified as a composite is (witness ^ factor) % value != 1
            // If that condition is not met, stop right away.
            if (powerModulo == 1 || powerModulo == value - 1)
            {
                return true;
            }

            // value - 1 was decomposed in (2 ^ powersOfTwo) * factor
            // Iterate powersOfTwo times to compute all (witness ^ ((2 ^ iteration) * factor)) % value - let's call then f(iteration)
            for (var iteration = 0; iteration < powersOfTwo - 1; iteration++)
            {
                // f(iteration + 1) == (f(iteration) ^ 2) % value
                powerModulo = (powerModulo * powerModulo) % value;

                if (powerModulo == 1)
                {
                    return false;
                }

                if (powerModulo == value - 1)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines if a value is a prime number.
        /// </summary>
        /// <param name="value">Value to be tested as prime number.</param>
        /// <remarks>
        /// As the largest possible value for long is 2,147,483,647, the Miller's test is deterministic
        /// once tested with witnesses 2, 7 and 61 as  n < 4,759,123,141 per Wikipedia <see href="https://en.wikipedia.org/wiki/Miller%E2%80%93Rabin_primality_test#Deterministic_variants_of_the_test"/>.
        /// </remarks>
        public static bool IsPrime(this long value)
        {
            // Eliminate obvious non-prime values below 2 or even. Special case 2 is prime.
            if (value <= 2 || value % 2 == 0)
            {
                return (value == 2);
            }

            // Remaining odd values up to 7 are all prime.
            if (value <= 7)
            {
                return true;
            }

            if (value % 3 == 0)
            {
                return false;
            }

            // Decompose value - 1 (which is even as value is odd) into (2 ^ powersOfTwo) * factor
            // Using bit shifting to the right for each power of 2.
            var factor = value >> 1;
            long powersOfTwo = 1;
            while ((factor & 1) != 1)
            {
                factor >>= 1;
                powersOfTwo++;
            }

            if (value < 2047)
            {
                return LongExtensions.WitnessTest(value, powersOfTwo, factor, 2);
            }

            if (value < 1373653)
            {
                return LongExtensions.WitnessTest(value, powersOfTwo, factor, 2) && 
                    LongExtensions.WitnessTest(value, powersOfTwo, factor, 3);
            }

            if (value < 9080191)
            {
                return LongExtensions.WitnessTest(value, powersOfTwo, factor, 31) && 
                    LongExtensions.WitnessTest(value, powersOfTwo, factor, 73);
            }

            return LongExtensions.WitnessTest(value, powersOfTwo, factor, 2) &&
                LongExtensions.WitnessTest(value, powersOfTwo, factor, 7) &&
                LongExtensions.WitnessTest(value, powersOfTwo, factor, 61);
        }

        public static long NextPseudoRandomValue(this long value, int offset, long modulus)
        {
            return (value * value + offset) % modulus;
        }

        public static bool TryFindFactor(this long value, int offset, ref long factor)
        {
            long pseudoRandomSequence = 2; long doubleSpeedPseudoRandomSequence = 2; factor = 1;
            do
            {
                pseudoRandomSequence = pseudoRandomSequence.NextPseudoRandomValue(offset: offset, modulus: value);
                doubleSpeedPseudoRandomSequence = doubleSpeedPseudoRandomSequence
                    .NextPseudoRandomValue(offset: offset, modulus: value)
                    .NextPseudoRandomValue(offset: offset, modulus: value);
                factor = MathAlgorithms.GreatestCommonDivisor(Math.Abs(pseudoRandomSequence - doubleSpeedPseudoRandomSequence), value);
            }
            while (factor == 1);

            return !(factor == value);
        }

        public static int CountOfDivisors(this long value)
        {
            return new Composite(value.BlendPrimeFactorization()).CountOfDivisors;
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Primes.cs" company="Microsoft">
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

    /// <summary>
    /// Helper class to incrementally discover small prime numbers.
    /// </summary>
    /// <remarks>This class is not meant to discover large prime numbers.</remarks>
    public class Primes
    {
        private static readonly Lazy<Primes> PrimeInstance = new Lazy<Primes>(() => new Primes());
        private long[] primes = { 2, 3, 5, 7, 11, 13 };

        public static Primes Singleton => PrimeInstance.Value;

        /// <summary>
        /// Prevents construction of instances by others, enforcing singleton pattern so prime numbers are only computed once.
        /// </summary>
        private Primes()
        {
        }

        /// <summary>
        /// Determines if an odd value is a prime number by trying to divide it by all the primes we found so far.
        /// </summary>
        /// <param name="value">Value to be tested as prime number.</param>
        /// <remarks>This is known as the Trial division method for checking if a number is prime.</remarks>
        private bool IsPrime(long value)
        {
            // If we haven't found a prime factor lower or equal to the square root, then there won't be a higher prime factor as this factor would need another lower factor to be multiplied by to result in our candidate.
            var maxTest = Convert.ToInt64(Math.Floor(Math.Sqrt(value)));

            // Although our array of prime numbers is zero-based, because we only evaluate odd numbers they will never have the first primer number two as prime factor.
            int existingPrimeIndex = 1;
            while (this.primes[existingPrimeIndex] <= maxTest)
            {
                if (value % this.primes[existingPrimeIndex] == 0)
                {
                    return false;
                }

                existingPrimeIndex++;
            }

            return true;
        }

        /// <summary>
        /// Given the last greatest prime number discovered so far, find the next prime number.
        /// </summary>
        /// <param name="lastPrime">The last greatest prime number discovered so far.</param>
        private long FindNextPrime(long lastPrime)
        {
            // Skip by two - prime numbers are never even but for the first prime value 2,
            // and we've primed our internal array well beyond that.
            long candidate = lastPrime + 2;
            while (!this.IsPrime(candidate))
            {
                candidate += 2;
            }

            return candidate;
        }

        public long this[int index]
        {
            get
            {
                if (index < this.primes.Length)
                {
                    return this.primes[index];
                }

                var oldMax = this.primes.Length;
                Array.Resize(ref this.primes, index + 1);
                for (int indexForNewPrime = oldMax; indexForNewPrime <= index; indexForNewPrime++)
                {
                    this.primes[indexForNewPrime] = this.FindNextPrime(this.primes[indexForNewPrime - 1]);
                }

                return this.primes[index];
            }
        }

        public long LargestPrimeFactorOf(long value)
        {
            var ceiling = Convert.ToInt64(Math.Floor(Math.Sqrt(value)));
            var primeIndex = 0;
            var quotient = value;

            do
            {
                while (quotient % this[primeIndex] != 0)
                {
                    primeIndex++;
                }

                quotient = quotient / this[primeIndex];
            }
            while (this[primeIndex] <= ceiling && quotient >= this.primes[this.primes.Length - 1]);

            return this[primeIndex];
        }

        /// <summary>
        /// Finds the smallest number that is a multiple of all the positive integral numbers up to the specified value
        /// </summary>
        /// <param name="value">The upper limit of integers which must all be quotients of the result.</param>
        public long SmallestMultipleOfAllNumbersTo(int value)
        {
            // All the prime numbers that are less than the square root of our upper limit value may result in non prime quotients if dividing our result
            // so that all the numbers are quotients of the result. Greater prime numbers are never needed to match non prime quotients,
            // so we will only need them at 'power 1'.
            var ceiling = Convert.ToInt64(Math.Floor(Math.Sqrt(value)));
            var multiple = (long)1;

            // All the prime numbers that are less or equal to our upper limit value must be quotients of our result,
            // so this is the upper limit of our iteration.
            for (var index = 0; this[index] <= value; index++)
            {
                if (this[index] <= ceiling)
                {
                    // We need the prime number to match as many non prime quotients of our result value as possible.
                    // Hence prime power x is less than value
                    // Hence x times log10 prime is less than log10 value
                    // Hence x (integer) is floor of log10 value divided by log10 prime
                    multiple *= Convert.ToInt64(Math.Pow(this[index], Math.Floor(Math.Log10(value) / Math.Log10(this[index]))));
                }
                else
                {
                    // 'power 1' - this prime is too large to 'form' other non-prime quotients less or equal to value
                    multiple *= this[index];
                }
            }

            return multiple;
        }
    }
}

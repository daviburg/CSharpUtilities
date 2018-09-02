﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IntExtensions.cs" company="Microsoft">
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

    public static class IntExtensions
    {
        public static bool IsPalindrome(this int value)
        {
            var asString = value.ToString();
            for (int index = 0; index < asString.Length >> 1; index++)
            {
                if (asString[index] != asString[asString.Length - (index + 1)])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Calculates the summation of natural numbers up to given value.
        /// </summary>
        /// <param name="value">The natural number to compute summation of.</param>
        /// <remarks>A known formula which you can find a demonstration of at <seealso href="https://trans4mind.com/personal_development/mathematics/series/sumNaturalNumbers.htm#mozTocId914933"/>.</remarks>
        public static long Summation(this int value)
        {
            return (long)value * ((long)value + 1) >> 1;
        }

        /// <summary>
        /// Calculates the summation of squares of natural numbers up to given value.
        /// </summary>
        /// <param name="value">The natural number to compute square summation of.</param>
        /// <remarks>See an approchable demonstration by difference at <seealso href="https://trans4mind.com/personal_development/mathematics/series/sumNaturalSquares.htm"/>.</remarks>
        public static long SquareSummation(this int value)
        {
            return ((long)value * ((long)value + 1) * (2 * (long)value + 1)) / 6;
        }

        public static List<PrimeFactor> PrimeFactorization(this int value) =>  ((long)value).PrimeFactorization();

        /// <summary>
        /// Gets the integral part of the squared root of a given number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <remarks>This formula is frequently used as upper search limit for primes and divisors.</remarks>
        public static int IntegralPartOfSquareRoot(this int value) => Convert.ToInt32(Math.Floor(Math.Sqrt(value)));

        /// <summary>
        /// Gets the next value in the Collatz sequence.
        /// </summary>
        /// <param name="value">The current value (should be a natural number).</param>
        /// <returns>The next value in the Collatz sequence if the input is a natural number. Otherwise the return value is undefined.</returns>
        public static int CollatzSequenceNext(this int value) => (value & 1) == 0 ? value >> 1 : checked(checked(value * 3) + 1);

        /// <summary>
        /// Calculates the factorial of the value.
        /// </summary>
        /// <param name="value">The value to compute the factorial of.</param>
        /// <returns>The factorial of the value.</returns>
        /// <remarks>For better performance use <see cref="Factorials"/> instead.</remarks>
        public static long Factorial(this int value)
        {
            if (value < 2)
            {
                return 1;
            }

            long factorial = 1;
            for (; value > 2; value--)
            {
                factorial *= value;
            }

            // Use faster bit shifting instead of the last multiplication by 2
            return factorial << 1;
        }

        /// <summary>
        /// Calculates the factorial of the value divided by the factorial of the limit.
        /// </summary>
        /// <param name="value">The value to compute the factorial of.</param>
        /// <param name="excludedLimit">The limit which factorial divides the factorial of the value.</param>
        public static long PartialFactorial(this int value, int excludedLimit)
        {
            if (value <= 20)
            {
                return Factorials.Singleton[value] / Factorials.Singleton[excludedLimit];
            }

            long factorial = 1;
            for (; value > excludedLimit; value--)
            {
                factorial *= value;
            }

            return factorial;
        }

        public static long Power(this int value, int exponent) => ((long)value).Power(exponent);
    }
}

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
            }

            return primeFactors;
        }
    }
}

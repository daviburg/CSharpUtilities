// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Factorials.cs" company="Microsoft">
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
    /// A table of pre-computed factorial values.
    /// </summary>
    /// <remarks>
    /// Because the growth of factorial quickly exceeds the long type capacity, the table is small and a good compromize of memory use vs computation.
    /// <seealso href="https://oeis.org/A000142"/>.
    /// </remarks>
    public class Factorials
    {
        private static readonly Lazy<Factorials> FactorialsInstance = new Lazy<Factorials>(() => new Factorials());
        private long[] factorials =
        {
            1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880, 3628800, 39916800, 479001600, 6227020800, 87178291200, 1307674368000,
            20922789888000, 355687428096000, 6402373705728000, 121645100408832000, 2432902008176640000
        };

        public static Factorials Singleton => FactorialsInstance.Value;

        /// <summary>
        /// Prevents construction of instances by others, enforcing singleton pattern.
        /// </summary>
        private Factorials()
        {
        }

        public long this[int index]
        {
            get
            {
                if (index >= this.factorials.Length)
                {
                    throw new OverflowException($"Arithmetic operation resulted in an overflow. The factorial of {index} exceeds the capacity of type long.");
                }

                return this.factorials[index];
            }
        }
    }
}

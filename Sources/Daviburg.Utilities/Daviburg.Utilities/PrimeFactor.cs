// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrimeFactor.cs" company="Microsoft">
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
    /// <summary>
    /// A prime factor, expressed by its order or by its prime number, and by its exponent
    /// </summary>
    public class PrimeFactor : Power
    {
        /// <summary>
        /// Initializes a <see cref="PrimeFactor"/> instance from the prime order and exponent.
        /// </summary>
        /// <param name="order">The prime order.</param>
        /// <param name="exponent">The exponent to apply to the prime.</param>
        // NOTE(daviburg): the primes table is zero-based while the order of primes starts at 1
        public PrimeFactor(int order, double exponent): base(Primes.Singleton[order - 1], exponent)
        {
        }

        /// <summary>
        /// Initializes a <see cref="PrimeFactor"/> instance from the prime number and exponent.
        /// </summary>
        /// <param name="baseNumber">The prime number.</param>
        /// <param name="exponent">The exponent to apply to the prime.</param>
        public PrimeFactor(double baseNumber, double exponent) : base(baseNumber, exponent)
        {
        }
    }
}
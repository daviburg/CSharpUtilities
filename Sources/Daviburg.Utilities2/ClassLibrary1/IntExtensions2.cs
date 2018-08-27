// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IntExtensions2.cs" company="Microsoft">
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

namespace Daviburg.Utilities2
{
    using Daviburg.Utilities;
    using System.Linq;
    using System.Numerics;

    public static class IntExtensions2
    {
        /// <summary>
        /// Sum of digits of 2^n.
        /// </summary>
        /// <param name="exponent">The power of 2 exponent.</param>
        /// <seealso href="https://oeis.org/A001370"/>
        public static int DigitsSummationPowerOfTwo(this int exponent) => (new BigInteger(1) << exponent).ToString().Aggregate(seed: 0, func: (partialSummation, digit) => partialSummation + digit.ToInt32());
    }
}

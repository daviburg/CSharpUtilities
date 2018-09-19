// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IntegerPowerValueComparer.cs" company="Microsoft">
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

    public class IntegerPowerValueComparer : IEqualityComparer<IntegerPower>
    {
        private static readonly Lazy<IntegerPowerValueComparer> ComparerInstance = new Lazy<IntegerPowerValueComparer>(() => new IntegerPowerValueComparer());

        public static IntegerPowerValueComparer Singleton => ComparerInstance.Value;

        /// <summary>
        /// Prevents construction of instances by others, enforcing singleton pattern so the comparer is not constructed again and again.
        /// </summary>
        private IntegerPowerValueComparer()
        {
        }

        public bool Equals(IntegerPower x, IntegerPower y)
        {
            return object.ReferenceEquals(x, y) ?
                true :
                x == null || y == null ?
                false :
                x.BigValue == y.BigValue;
        }

        public int GetHashCode(IntegerPower product)
        {
            return product == null ? 0 : product.BigValue.GetHashCode();
        }
    }
}

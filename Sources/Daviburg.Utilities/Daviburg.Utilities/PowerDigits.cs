// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PowerDigitsSingletons.cs" company="Microsoft">
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
    using System.Collections.Concurrent;
    using System.Linq;
 
    public class PowerDigitsSingletons
    {
        private static readonly Lazy<PowerDigitsSingletons> Instance = new Lazy<PowerDigitsSingletons>(() => new PowerDigitsSingletons());

        private static readonly ConcurrentDictionary<int, PowerDigits> Instances = new ConcurrentDictionary<int, PowerDigits>();

        public static PowerDigitsSingletons Singleton => Instance.Value;

        /// <summary>
        /// Prevents construction of instances by others, enforcing singleton pattern.
        /// </summary>
        private PowerDigitsSingletons()
        {
        }

        public long[] this[int exponent] => PowerDigitsSingletons.Instances.GetOrAdd(exponent, key =>  new PowerDigits(key)).digitPowers;

        internal class PowerDigits
        {
            internal long[] digitPowers;

            internal PowerDigits(int exponent)
            {
                digitPowers = Enumerable.Range(0, 10).Select(digit => new IntegerPower(digit, exponent).Value).ToArray();
            }
        }
    }
}

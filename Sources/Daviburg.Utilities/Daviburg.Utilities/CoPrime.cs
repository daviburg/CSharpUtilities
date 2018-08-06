// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoPrime.cs" company="Microsoft">
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
    using System.Collections.Generic;

    /// <summary>
    /// Pair of integers which greatest common divisor is one.
    /// </summary>
    /// <remarks>
    /// See also <see href="https://en.wikipedia.org/wiki/Coprime_integers">.
    /// This implementation is limited to positive coprimes.
    /// </remarks>
    public class CoPrime
    {
        private CoPrime()
        {
        }

        public int IntegerA { get; private set; }

        public int IntegerB { get; private set; }

        public static CoPrime OddOddRootVertex => new CoPrime { IntegerA = 3, IntegerB = 1 };
        public static CoPrime EvenOddRootVertex => new CoPrime { IntegerA = 2, IntegerB = 1 };

        public List<CoPrime> NextTriplet =>
            new List<CoPrime>()
            {
                new CoPrime { IntegerA = this.IntegerA * 2 - this.IntegerB, IntegerB = this.IntegerA },
                new CoPrime { IntegerA = this.IntegerA * 2 + this.IntegerB, IntegerB = this.IntegerA },
                new CoPrime { IntegerA = this.IntegerA + this.IntegerB * 2, IntegerB = this.IntegerB }
            };
        
    }
}

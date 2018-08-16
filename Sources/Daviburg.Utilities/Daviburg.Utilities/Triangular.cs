// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Triangular.cs" company="Microsoft">
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
    public class Triangular
    {
        private long? value;

        public Triangular(long order)
        {
            this.Order = order;
        }

        private Triangular(long order, long value)
        {
            this.Order = order;
            this.value = value;
        }

        public long Order { get; private set; }

        public long Value => (value ?? (value = this.Order.Summation())).Value;

        public Triangular NextTriangularNumber()
        {
            var nextOrder = this.Order + 1;
            return new Triangular(nextOrder, this.Value + nextOrder);
        }

        public static Triangular ClosestTriangularNumber(long value)
        {
            // Triangular numbers of order n is also the summation of natural numbers up to n.
            // So the anti-summation will give us the order of the closest triangular number.
            // Then re-doing the summation gives us the triangular number.
            return new Triangular(value.AntiSummation());
        }
    }
}

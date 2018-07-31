// --------------------------------------------------------------------------------------------------------------------
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
    public static class IntExtensions
    {
        public static bool IsPalindrome(this int value)
        {
            var asString = value.ToString();
            for (int index = 0; index < asString.Length / 2; index++)
            {
                if (asString[index] != asString[asString.Length - (index + 1)])
                {
                    return false;
                }
            }

            return true;
        }
    }
}

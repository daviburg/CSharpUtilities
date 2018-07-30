// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Microsoft">
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
//
//   Do note that this file contains a single method TryParseExact which *different* license terms are described below.
//
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Daviburg.Utilities
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Extensions for <see cref="String"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Tries parsing exactly the provided string per the specified format to extract the arguments. The format of the string representation must match the specified format exactly.
        /// </summary>
        /// <param name="data">The formatted string.</param>
        /// <param name="format">A format specifier that defines the required format of data.</param>
        /// <param name="values">Out parameter for the argument values.</param>
        /// <param name="ignoreCase">Whether to ignore the casing of characters or not.</param>
        /// <remarks>MIT License - from https://github.com/mikeobrien/mikeobrien.github.com/blob/master/LICENSE and code at https://github.com/mikeobrien/mikeobrien.github.com/blob/master/_posts/2009-2-18-parseexact-for-strings.html </remarks>
        /// <copyright>
        ///         Copyright(c) 2010 Ultraviolet Catastrophe
        ///
        /// Permission is hereby granted, free of charge, to any person obtaining a copy
        /// of this software and associated documentation files(the "Software"), to deal
        /// in the Software without restriction, including without limitation the rights
        /// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
        /// copies of the Software, and to permit persons to whom the Software is furnished
        /// to do so, subject to the following conditions:
        ///
        /// The above copyright notice and this permission notice shall be included in all
        /// copies or substantial portions of the Software.
        ///
        /// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
        /// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
        /// PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
        /// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
        /// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
        /// SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
        /// </copyright>
        public static bool TryParseExact(
            this string data,
            string format,
            out string[] values,
            bool ignoreCase)
        {
            if (data == null)
            {
                values = new string[] { };
                return false;
            }

            int tokenCount = 0;
            format = Regex.Escape(format).Replace(oldValue: "\\{", newValue: "{");

            for (tokenCount = 0; ; tokenCount++)
            {
                string token = $"{{{tokenCount}}}";
                if (!format.Contains(token))
                {
                    break;
                }

                format = format.Replace(oldValue: token, newValue: $"(?'group{tokenCount}'.*)");
            }

            var match = new Regex(format, ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None).Match(data);

            if (tokenCount != (match.Groups.Count - 1))
            {
                values = new string[] { };
                return false;
            }
            else
            {
                values = new string[tokenCount];
                for (int index = 0; index < tokenCount; index++)
                {
                    values[index] = match.Groups[$"group{index}"].Value;
                }

                return true;
            }
        }
    }
}

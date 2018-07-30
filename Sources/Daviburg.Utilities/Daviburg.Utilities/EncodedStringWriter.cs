// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EncodedStringWriter.cs" company="Microsoft">
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
    using System.IO;
    using System.Text;

    /// <summary>
    /// The string writer with character encoding.
    /// </summary>
    /// <remarks>This class is implemented because the base class string writer encoding property is read-only with no constructor overload to set it.</remarks>
    public class EncodedStringWriter : StringWriter
    {
        /// <summary>
        /// The character encoding.
        /// </summary>
        private readonly Encoding encoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="EncodedStringWriter"/> class with specific character encoding.
        /// </summary>
        /// <param name="encoding">The character encoding.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.IO.StringWriter.#ctor", Justification = "We are specifically only changing the encoding property of the existing base class.")]
        public EncodedStringWriter(Encoding encoding)
        {
            this.encoding = encoding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EncodedStringWriter"/> class with the specified format control and with specific character encoding.
        /// </summary>
        /// <param name="formatProvider">An <see cref="T:System.IFormatProvider"/> object that controls formatting.</param>
        /// <param name="encoding">The encoding.</param>
        public EncodedStringWriter(IFormatProvider formatProvider, Encoding encoding)
            : base(formatProvider)
        {
            this.encoding = encoding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EncodedStringWriter"/> class that writes to the specified <see cref="T:System.Text.StringBuilder"/> and with specific character encoding.
        /// </summary>
        /// <param name="sb">The <see cref="T:System.Text.StringBuilder"/> object to write to.</param>
        /// <param name="encoding">The encoding.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.IO.StringWriter.#ctor(System.Text.StringBuilder)", Justification = "We are specifically only changing the encoding property of the existing base class.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "sb", Justification = "As-is from inherited base class.")]
        public EncodedStringWriter(StringBuilder sb, Encoding encoding)
            : base(sb)
        {
            this.encoding = encoding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EncodedStringWriter"/> class that writes to the specified <see cref="T:System.Text.StringBuilder"/>, with specific character encoding and has the specified format provider.
        /// </summary>
        /// <param name="sb">The <see cref="T:System.Text.StringBuilder"/> object to write to.</param>
        /// <param name="formatProvider">An <see cref="T:System.IFormatProvider"/> object that controls formatting.</param>
        /// <param name="encoding">The encoding.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "sb", Justification = "As-is from inherited base class.")]
        public EncodedStringWriter(StringBuilder sb, IFormatProvider formatProvider, Encoding encoding)
            : base(sb, formatProvider)
        {
            this.encoding = encoding;
        }

        /// <summary>
        /// Gets the character encoding.
        /// </summary>
        public override Encoding Encoding
        {
            get { return this.encoding ?? base.Encoding; }
        }
    }
}

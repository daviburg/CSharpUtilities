// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionExtensions.cs" company="Microsoft">
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
    using System;
    using System.Resources;
    using System.Xml;
    using Daviburg.Utilities;

    public static class ExceptionExtensions
    {
        /// <summary>
        /// The resource id of the invalid XML character exception message, as found in the code shared at <see href="https://referencesource.microsoft.com/download.html"/>
        /// </summary>
        private const string InvalidXmlCharExceptionMessageFormatResourceId = "Xml_InvalidCharacter";

        /// <summary>
        /// <see cref="ResourceManager"/> for <see cref="System.Xml"/> containing assembly.
        /// </summary>
        private static Lazy<ResourceManager> systemXmlResourceManager = new Lazy<ResourceManager>(() => new ResourceManager("System.Xml", typeof(XmlElement).Assembly));

        /// <summary>
        /// Determines if the exception is for an invalid XML character.
        /// </summary>
        /// <param name="ex">The exception to analyze.</param>
        /// <remarks>
        /// .NET is using generic ArgumentException type exception which makes it difficult to programmatically say with confidence the reason of the exception.
        /// Due to 64 bit inlining of InvalidXmlChar private method, we can't rely on stack frame matching.
        /// This helper is matching the localized string back to the format resource string to match with high reliability regardless of local the exception reason.
        /// </remarks>
        public static bool IsInvalidXmlCharException(this Exception ex)
        {
            return ex is ArgumentException &&
                ex.Message.TryParseExact(
                    format: systemXmlResourceManager.Value.GetString(name: ExceptionExtensions.InvalidXmlCharExceptionMessageFormatResourceId, culture: null),
                    values: out string[] values,
                    ignoreCase: false);
        }
    }
}

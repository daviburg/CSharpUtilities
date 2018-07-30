// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlSchemaExtensions.cs" company="Microsoft">
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
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Schema;

    public static class XmlSchemaExtensions
    {
        // TODO: move these to XmlSchemaExtensions, add XmlDoc extensions if needed. Write a version not using a memorystream but maybe a stringbuilder instead for large schemas.
        public static async System.Threading.Tasks.Task<string> ToStringAsync(this XmlSchema schema, Encoding encoding)
        {
            var xmlWriterSettings = new XmlWriterSettings
            {
                Async = true,
                NewLineHandling = NewLineHandling.None
            };
            var outputMemoryStream = new MemoryStream();
            try
            {
                using (var stringWriter = new StreamWriter(stream: outputMemoryStream, encoding: encoding, bufferSize: 512, leaveOpen: true))
                using (var xmlTextWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
                using(var streamReader = new StreamReader(outputMemoryStream))
                {
                    schema.Write(xmlTextWriter);
                    await xmlTextWriter
                        .FlushAsync()
                        .ConfigureAwait(continueOnCapturedContext: false);

                    outputMemoryStream.Seek(0, SeekOrigin.Begin);
                    return await streamReader.ReadToEndAsync();
                }
            }
            finally
            {
                if (outputMemoryStream != null)
                {
                    outputMemoryStream.Dispose();
                }
            }
        }

        // TODO: also move to .net std 1.1 helper
        public static string ToString(this XmlSchema schema, Encoding encoding)
        {
            using (var stringWriter = new EncodedStringWriter(encoding))
            {
                schema.Write(stringWriter);
                return stringWriter.ToString();
            }
        }
    }
}

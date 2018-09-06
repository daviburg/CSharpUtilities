// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlCharacters.cs" company="Microsoft">
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

namespace Daviburg.FixToXmlCharacters
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    
    public static class XmlCharacters
    {
        [FunctionName("FixCharacters")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processing a request.");

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            // Consider adding  || data.escapeStyle == null later
            if (data == null || data.text == null)
            {
                return new BadRequestObjectResult("Please pass text properties in the input Json object.");
            }

            /* Consider adding optional behaviors with different escape styles
            switch (data.escapeStyle)
            {
                case "":
                    break;
                default:
                    return new BadRequestObjectResult("escapeStyle value not recognized.");
            }*/

            // See reference https://www.w3.org/TR/xml11/#sec-normalization-checking
            // XML 1.1 "All XML parsed entities (including document entities) should be fully normalized[...]"
            // See reference https://www.w3.org/TR/xml11/#sec-references
            // for hexadecimal character reference format '&#x' [0-9a-fA-F]+ ';'
            // See reference https://docs.microsoft.com/en-us/dotnet/api/system.char.maxvalue?view=netframework-4.7.2
            // for hexadecimal maximum length of char (0xFFFF) hence 4 digits representation and not just the 2 of the W3C examples.
            return (ActionResult)new JsonResult(
                value: new
                {
                    text = Convert.ToBase64String(
                        inArray: Encoding.UTF8.GetBytes(
                            s: new string(
                                value: ((string)Encoding.UTF8.GetString(Convert.FromBase64String((string)data.text)))
                                    .Normalize()
                                    .SelectMany(character => XmlConvert.IsXmlChar(character) ? new[] { character } : $"&#x{Convert.ToInt32(character):X4};".ToArray())
                                    .ToArray())))
                });
        }
    }
}

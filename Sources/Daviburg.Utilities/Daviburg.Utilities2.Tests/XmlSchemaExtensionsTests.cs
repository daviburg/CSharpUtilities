// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlSchemaExtensionsTests.cs" company="Microsoft">
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

namespace Daviburg.Utilities2.Tests
{
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Schema;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class XmlSchemaExtensionsTests
    {
        [TestMethod]
        public async Task SchemaToStringAsyncTest()
        {
            XmlSchema schema = GetTestSchema();

            var xmlAsString = await schema.ToStringAsync(Encoding.UTF8);
            StringAssert.StartsWith(xmlAsString, "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

            xmlAsString = await schema.ToStringAsync(Encoding.Unicode);
            StringAssert.StartsWith(xmlAsString, "<?xml version=\"1.0\" encoding=\"utf-16\"?>");
        }

        [TestMethod]
        public void SchemaToStringTest()
        {
            XmlSchema schema = GetTestSchema();

            var xmlAsString = schema.ToString(Encoding.UTF8);
            StringAssert.StartsWith(xmlAsString, "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

            xmlAsString = schema.ToString(Encoding.Unicode);
            StringAssert.StartsWith(xmlAsString, "<?xml version=\"1.0\" encoding=\"utf-16\"?>");
        }

        private static XmlSchema GetTestSchema()
        {
            XmlSchema schema = new XmlSchema();

            // <xs:element name="cat" type="xs:string"/>
            XmlSchemaElement elementCat = new XmlSchemaElement();
            schema.Items.Add(elementCat);
            elementCat.Name = "cat";
            elementCat.SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");

            // <xs:element name="dog" type="xs:string"/>
            XmlSchemaElement elementDog = new XmlSchemaElement();
            schema.Items.Add(elementDog);
            elementDog.Name = "dog";
            elementDog.SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");

            // <xs:element name="redDog" substitutionGroup="dog" />
            XmlSchemaElement elementRedDog = new XmlSchemaElement();
            schema.Items.Add(elementRedDog);
            elementRedDog.Name = "redDog";
            elementRedDog.SubstitutionGroup = new XmlQualifiedName("dog");

            // <xs:element name="brownDog" substitutionGroup ="dog" />
            XmlSchemaElement elementBrownDog = new XmlSchemaElement();
            schema.Items.Add(elementBrownDog);
            elementBrownDog.Name = "brownDog";
            elementBrownDog.SubstitutionGroup = new XmlQualifiedName("dog");

            // <xs:element name="pets">
            XmlSchemaElement elementPets = new XmlSchemaElement();
            schema.Items.Add(elementPets);
            elementPets.Name = "pets";

            // <xs:complexType>
            XmlSchemaComplexType complexType = new XmlSchemaComplexType();
            elementPets.SchemaType = complexType;

            // <xs:choice minOccurs="0" maxOccurs="unbounded">
            XmlSchemaChoice choice = new XmlSchemaChoice();
            complexType.Particle = choice;
            choice.MinOccurs = 0;
            choice.MaxOccursString = "unbounded";

            // <xs:element ref="cat"/>
            XmlSchemaElement catRef = new XmlSchemaElement();
            choice.Items.Add(catRef);
            catRef.RefName = new XmlQualifiedName("cat");

            // <xs:element ref="dog"/>
            XmlSchemaElement dogRef = new XmlSchemaElement();
            choice.Items.Add(dogRef);
            dogRef.RefName = new XmlQualifiedName("dog");
            return schema;
        }
    }
}

//---------------------------------------------------------------------
// <copyright file="ClientTypeKeyIdentificationTest.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;

namespace AstoriaUnitTests.Tests.Client
{
    using System.Linq;
    using Microsoft.OData.Client;
    using Microsoft.OData.Edm;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests if client type key properties can be correctly identified
    /// </summary>
    [TestClass]
    public class ClientTypeKeyIdentificationTest
    {
        [Key("Fluid")]
        private class TestTypeOne
        {
            public int Fluid { get; set; }

            public int TestTypeOneId { get; set; }

            public int Id { get; set; }

            public int TestTypeOneID { get; set; }

            public int ID { get; set; }
        }

        private class TestTypeTwo
        {
            public int Fluid { get; set; }

            public int TestTypeTwoId { get; set; }

            public int Id { get; set; }

            public int TestTypeTwoID { get; set; }

            public int ID { get; set; }
        }

        private class TestTypeThree
        {
            public int Fluid { get; set; }

            public int TestTypeThreeId { get; set; }

            public int Id { get; set; }

            public int ID { get; set; }
        }

        private class TestTypeFour
        {
            public int Fluid { get; set; }

            public int TestTypeFourId { get; set; }

            public int Id { get; set; }
        }

        private class TestTypeFive
        {
            public int Fluid { get; set; }

            public int Id { get; set; }
        }

        [TestMethod]
        public void TestClientTypeKeyIdentification()
        {
            var combinations = new[]
            {
                new { Type = typeof(TestTypeOne), KeyName = "Fluid" },
                new { Type = typeof(TestTypeTwo), KeyName = "TestTypeTwoID" },
                new { Type = typeof(TestTypeThree), KeyName = "ID" },
                new { Type = typeof(TestTypeFour), KeyName = "TestTypeFourId" },
                new { Type = typeof(TestTypeFive), KeyName = "Id" }
            };

            foreach (var combination in combinations)
            {
                TestAndVerifyKeyProperty(combination.Type, combination.KeyName);
            }
        }

        private void TestAndVerifyKeyProperty(Type clrType, string keyName)
        {
            ClientEdmModel clientEdmModel = new ClientEdmModel(ODataProtocolVersion.V4);
            IEdmEntityType entityType = (IEdmEntityType)clientEdmModel.GetOrCreateEdmType(clrType);
            Assert.AreEqual(keyName, entityType.DeclaredKey.Single().Name);
        }
    }
}

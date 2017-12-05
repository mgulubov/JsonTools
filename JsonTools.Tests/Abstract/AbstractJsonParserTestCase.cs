using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using JsonTools.Interfaces;

namespace JsonTools.Tests.Abstract
{
    [TestClass]
    public abstract class AbstractJsonParserTestCase
    {
        protected readonly string basicJson = "{ \"id\": \"91939\", \"name\" : \"Bruce Wayne\", \"p4ssw@rd\": \"Th4D4rkKn!ghtR3turns\", \"info\": \"\\\"You don\'t get it son. This isn\'t a mudhole . . . it\'s an operating table . . . And I\'m the surgeon.\\\"\"}";
        protected readonly string advancedJson = "{ \"str\": \"string\", \"bool true\": true , \"bool false\": False, \"arr\": [1, \"two\"], \"obj\": {\"id\": 1, \"name\": \"user\"}}";

        public TestContext TestContext { get; set; }

        protected abstract IJsonParser GetJsonParserInstance();

        [TestMethod]
        public void TestParseBasicJsonString()
        {
            IJsonParser jsonParser = this.GetJsonParserInstance();
            IJsonNode jsonNode = jsonParser.Parse(this.basicJson);

            Assert.IsNotNull(jsonNode);
            Assert.IsNull(jsonNode.Key);

            Assert.IsTrue(jsonNode.KeyExists("id"));
            Assert.IsTrue(jsonNode["id"].IsString());
            Assert.AreEqual<string>("91939", (string)jsonNode["id"].InnerValue);
            Assert.AreEqual<Type>(typeof(string), jsonNode["id"].InnerType);

            Assert.IsTrue(jsonNode.KeyExists("name"));
            Assert.IsTrue(jsonNode["name"].IsString());
            Assert.AreEqual<string>("Bruce Wayne", (string)jsonNode["name"].InnerValue);
            Assert.AreEqual<Type>(typeof(string), jsonNode["name"].InnerType);

            Assert.IsTrue(jsonNode.KeyExists("p4ssw@rd"));
            Assert.IsTrue(jsonNode["p4ssw@rd"].IsString());
            Assert.AreEqual<string>("Th4D4rkKn!ghtR3turns", (string)jsonNode["p4ssw@rd"].InnerValue);
            Assert.AreEqual<Type>(typeof(string), jsonNode["p4ssw@rd"].InnerType);

            Assert.IsTrue(jsonNode.KeyExists("info"));
            Assert.IsTrue(jsonNode["info"].IsString());
            Assert.AreEqual<string>("\\\"You don\'t get it son. This isn\'t a mudhole . . . it\'s an operating table . . . And I\'m the surgeon.\\\"", (string)jsonNode["info"].InnerValue);
            Assert.AreEqual<Type>(typeof(string), jsonNode["info"].InnerType);
        }

        [TestMethod]
        public void TestParseAdvancedJsonString()
        {
            IJsonParser jsonParser = this.GetJsonParserInstance();
            IJsonNode rootNode = jsonParser.Parse(this.advancedJson);

            Assert.IsNotNull(rootNode);
            Assert.IsNull(rootNode.Key);

            Assert.IsTrue(rootNode.KeyExists("str"));
            Assert.IsTrue(rootNode["str"].IsString());
            Assert.AreEqual<string>("string", (string)rootNode["str"].InnerValue);

            Assert.IsTrue(rootNode.KeyExists("bool true"));
            Assert.IsTrue(rootNode["bool true"].IsBool());
            Assert.AreEqual<bool>(true, (bool)rootNode["bool true"].InnerValue);

            Assert.IsTrue(rootNode.KeyExists("bool false"));
            Assert.IsTrue(rootNode["bool false"].IsBool());
            Assert.AreEqual<bool>(false, (bool)rootNode["bool false"].InnerValue);
        }
    }
}

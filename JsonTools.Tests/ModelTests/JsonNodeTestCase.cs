namespace JsonTools.Tests.ModelTests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Interfaces;
    using Models;
    using Tests.Abstract;

    [TestClass]
    public class JsonNodeTestCase : AbstractJsonNodeTestCase
    {
        protected override IJsonValue GetJsonValueInstance(string value)
        {
            return new JsonValue(value);
        }

        protected override IJsonNode GetJsonNodeInstance(string key)
        {
            return new JsonNode(key);
        }
    }
}

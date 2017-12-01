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
    public class JsonValueTestCase : AbstractJsonValueTestCase
    {
        protected override IJsonValue GetJsonValueInstance(string value)
        {
            return new JsonValue(value);    
        }

        protected override IJsonValue GetJsonValueInstance(IJsonNode value)
        {
            return new JsonValue(value);
        }

        protected override IJsonNode GetJsonNodeInstance(string key)
        {
            return new JsonNode(key);
        }
    }
}

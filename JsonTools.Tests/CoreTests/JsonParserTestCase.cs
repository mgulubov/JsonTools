using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using JsonTools.Interfaces;
using JsonTools.Core;
using JsonTools.Tests.Abstract;

namespace JsonTools.Tests.CoreTests
{
    [TestClass]
    public class JsonParserTestCase : AbstractJsonParserTestCase
    {
        protected override IJsonParser GetJsonParserInstance()
        {
            return new JsonParser();
        }
    }
}

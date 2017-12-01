namespace JsonTools.Tests.Abstract
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Interfaces;

    [TestClass]
    public abstract class AbstractJsonValueTestCase
    {
        protected string StringValueA = "VALUE_A";
        protected string StringValueB = "VALUE_B";
        protected string KeyA = "KEY_A";
        protected string KeyB = "KEY_B";

        protected Type StringType = typeof(string);
        protected Type JsonNodeType = typeof(IJsonNode);

        protected IJsonNode JsonNodeA;
        protected IJsonNode JsonNodeB;

        protected AbstractJsonValueTestCase() 
        {
            this.JsonNodeA = this.GetJsonNodeInstance(this.KeyA);
            this.JsonNodeB = this.GetJsonNodeInstance(this.KeyB);
        }

        public TestContext TestContext { get; set; }

        protected abstract IJsonValue GetJsonValueInstance(string value);
        protected abstract IJsonValue GetJsonValueInstance(IJsonNode value);
        protected abstract IJsonNode GetJsonNodeInstance(string key);

        [TestMethod]
        public void TestStringValueContructorNullValue()
        {
            IJsonValue jsonValue = this.GetJsonValueInstance(default(string));

            Assert.IsNull(jsonValue.InnerValue);
            Assert.IsNull(jsonValue.InnerType);
        }

        [TestMethod]
        public void TestIJsonNodeCOnstructorNullValue()
        {
            IJsonValue jsonValue = this.GetJsonValueInstance(default(IJsonNode));

            Assert.IsNull(jsonValue.InnerValue);
            Assert.IsNull(jsonValue.InnerType);
        }

        [TestMethod]
        public void TestStringConstructorDoesNotThrowExceptionOnValidValue()
        {
            IJsonValue jsonValue = this.GetJsonValueInstance(this.StringValueA);
        }

        [TestMethod]
        public void TestIJsonNodeConstructorDoesNotThrowExceptionOnValidValue()
        {
            IJsonValue jsonValue = this.GetJsonValueInstance(this.JsonNodeA);
        }

        [TestMethod]
        public void TestInnerValuePropertyStringValue()
        {
            IJsonValue jsonValueString = this.GetJsonValueInstance(this.StringValueA);

            Assert.IsNotNull(jsonValueString.InnerValue);
            Assert.AreEqual(this.StringValueA, jsonValueString.InnerValue);
        }

        [TestMethod]
        public void TestInnerValuePropertyJsonNodeValue()
        {
            IJsonValue jsonValueNode = this.GetJsonValueInstance(this.JsonNodeA);

            Assert.IsNotNull(jsonValueNode.InnerValue);
            Assert.AreEqual(this.JsonNodeA, jsonValueNode.InnerValue);
        }

        [TestMethod]
        public void TestInnerTypePropertyStringValue()
        {
            IJsonValue jsonValue = this.GetJsonValueInstance(this.StringValueA);

            Assert.IsNotNull(jsonValue.InnerType);
            Assert.AreEqual(this.StringType, jsonValue.InnerType);
        }

        [TestMethod]
        public void TestInnerTypePropertyJsonNodeValue()
        {
            IJsonValue jsonValue = this.GetJsonValueInstance(this.JsonNodeA);

            Assert.IsNotNull(jsonValue.InnerType);
            Assert.AreEqual(this.JsonNodeType, jsonValue.InnerType);
        }

        [TestMethod]
        public void TestIsStringMethodReturnsTrueStringValue()
        {
            IJsonValue jsonValueString = this.GetJsonValueInstance(this.StringValueA);

            Assert.IsTrue(jsonValueString.IsString());
            Assert.AreEqual<string>(this.StringValueA, (string)jsonValueString.InnerValue);
        }

        [TestMethod]
        public void TestIsStringMethodReturnsFalseNodeValue()
        {
            IJsonValue jsonValue = this.GetJsonValueInstance(this.JsonNodeA);

            Assert.IsFalse(jsonValue.IsString());
        }

        [TestMethod]
        public void TestIsNodeMethodReturnsTrueNodeValue()
        {
            IJsonValue jsonValue = this.GetJsonValueInstance(this.JsonNodeA);

            Assert.IsTrue(jsonValue.IsNode());
            Assert.AreEqual<IJsonNode>(this.JsonNodeA, (IJsonNode)jsonValue.InnerValue);
        }

        [TestMethod]
        public void TestIsNodeMethodReturnsFalseStringValue()
        {
            IJsonValue jsonValue = this.GetJsonValueInstance(this.StringValueA);

            Assert.IsFalse(jsonValue.IsNode());
        }
    }
}

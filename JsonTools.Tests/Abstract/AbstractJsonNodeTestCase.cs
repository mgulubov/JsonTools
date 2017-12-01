using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using JsonTools.Interfaces;

namespace JsonTools.Tests.Abstract
{
    [TestClass]
    public abstract class AbstractJsonNodeTestCase
    {
        private string StringValueA = "VALUE_A";
        private string StringValueB = "VALUE_B";

        private string RootKey = null;
        private string KeyA = "KEY_A";
        private string KeyB = "KEY_B";

        private IJsonValue JsonValueA;
        private IJsonValue JsonValueB;

        protected AbstractJsonNodeTestCase()
        {
            this.JsonValueA = this.GetJsonValueInstance(this.StringValueA);
            this.JsonValueB = this.GetJsonValueInstance(this.StringValueB);
        }

        public TestContext TestContext { get; set; }

        protected abstract IJsonValue GetJsonValueInstance(string value);
        protected abstract IJsonNode GetJsonNodeInstance(string key);

        [TestMethod]
        public void TestKeyPropertyInitialValueNull()
        {
            IJsonNode jsonNode = this.GetJsonNodeInstance(default(string));

            Assert.IsNull(jsonNode.Key);
        }

        [TestMethod]
        public void TestKeyPropertyNotNull()
        {
            IJsonNode jsonNode = this.GetJsonNodeInstance(this.KeyA);

            Assert.IsNotNull(jsonNode.Key);
            Assert.AreEqual(this.KeyA, jsonNode.Key);
            Assert.AreEqual<string>(this.KeyA, jsonNode.Key);
        }

        [TestMethod]
        public void TestParentNodePropertyInitialValue()
        {
            IJsonNode jsonNode = this.GetJsonNodeInstance(this.KeyA);

            Assert.IsNull(jsonNode.ParentNode);
        }

        [TestMethod]
        public void TestParentNodePropertySetValue()
        {
            IJsonNode jsonNode = this.GetJsonNodeInstance(this.KeyA);
            IJsonNode parentNode = this.GetJsonNodeInstance(this.KeyB);

            jsonNode.ParentNode = parentNode;

            Assert.IsNotNull(jsonNode.ParentNode);
            Assert.AreEqual(parentNode, jsonNode.ParentNode);
            Assert.AreEqual<IJsonNode>(parentNode, jsonNode.ParentNode);

            Assert.AreEqual(parentNode.Key, jsonNode.ParentNode.Key);
            Assert.AreEqual<string>(parentNode.Key, jsonNode.ParentNode.Key);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestGetValueByKeyThrowsExceptionOnNonExistingKey()
        {
            IJsonNode jsonNode = this.GetJsonNodeInstance(this.KeyA);

            IJsonValue jsonValue = jsonNode[this.KeyB];
        }

        [TestMethod]
        public void TestGetSetValueByKey()
        {
            IJsonNode jsonNode = this.GetJsonNodeInstance(this.RootKey);

            jsonNode[this.KeyA] = this.JsonValueA;
            jsonNode[this.KeyB] = this.JsonValueB;

            IJsonValue jsonValueA = jsonNode[this.KeyA];
            IJsonValue jsonValueB = jsonNode[this.KeyB];

            Assert.AreEqual(this.JsonValueA.InnerValue, jsonValueA.InnerValue);
            Assert.AreEqual(this.JsonValueB.InnerValue, jsonValueB.InnerValue);
        }

        [TestMethod]
        public void TestKeysPropertyNoValues()
        {
            IJsonNode jsonNode = this.GetJsonNodeInstance(this.RootKey);
            IList<string> keysList = jsonNode.Keys.ToList<string>();

            Assert.AreEqual<int>(0, keysList.Count);
        }

        [TestMethod]
        public void TestKeysPropertyWithValues()
        {
            IJsonNode jsonNode = this.GetJsonNodeInstance(this.RootKey);
            jsonNode[this.KeyA] = this.JsonValueA;
            jsonNode[this.KeyB] = this.JsonValueB;

            IList<string> orderedKeys = jsonNode.Keys.OrderBy<string, string>(k => k).ToList<string>();

            Assert.AreEqual<int>(2, orderedKeys.Count);
            Assert.AreEqual<string>(this.KeyA, orderedKeys[0]);
            Assert.AreEqual<string>(this.KeyB, orderedKeys[1]);
        }

        [TestMethod]
        public void TestValuesPropertyNoValues()
        {
            IJsonNode jsonNode = this.GetJsonNodeInstance(this.RootKey);
            IList<IJsonValue> values = jsonNode.Values.ToList<IJsonValue>();

            Assert.AreEqual<int>(0, values.Count);
        }

        [TestMethod]
        public void TestValuesPropertyWithValues()
        {
            IJsonNode jsonNode = this.GetJsonNodeInstance(this.RootKey);
            jsonNode[this.KeyA] = this.JsonValueA;
            jsonNode[this.KeyB] = this.JsonValueB;

            IList<IJsonValue> orderedValues = jsonNode.Values.OrderBy<IJsonValue, string>(v => (string)v.InnerValue).ToList<IJsonValue>();

            Assert.AreEqual<int>(2, orderedValues.Count);
            Assert.AreEqual(this.JsonValueA, orderedValues[0]);
            Assert.AreEqual(this.JsonValueB, orderedValues[1]);
            Assert.AreEqual<string>(this.StringValueA, (string)orderedValues[0].InnerValue);
            Assert.AreEqual<string>(this.StringValueB, (string)orderedValues[1].InnerValue);
        }

        [TestMethod]
        public void TestKeyExists()
        {
            IJsonNode jsonNode = this.GetJsonNodeInstance(this.RootKey);
            jsonNode[this.KeyA] = this.JsonValueA;

            Assert.IsTrue(jsonNode.KeyExists(this.KeyA));
            Assert.IsFalse(jsonNode.KeyExists(this.KeyB));
        }
    }
}

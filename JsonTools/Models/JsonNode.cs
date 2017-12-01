namespace JsonTools.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Interfaces;

    public class JsonNode : IJsonNode
    {
        private IDictionary<string, IJsonValue> innerValuesMap;

        public JsonNode(string key)
        {
            this.innerValuesMap = new Dictionary<string, IJsonValue>();

            this.Key = key;
        }

        public string Key { get; private set; }

        public IJsonNode ParentNode { get; set; }

        public IJsonValue this[string key] 
        {
            get
            {
                this.ValidateGetValueKey(key);

                return this.innerValuesMap[key];
            }
            set 
            {
                this.ValidateSetValueKey(key);

                this.innerValuesMap[key] = value;
            }
        }

        public IEnumerable<string> Keys 
        {
            get 
            {
                return this.innerValuesMap.Keys;
            }
        }

        public IEnumerable<IJsonValue> Values
        {
            get
            {
                return this.innerValuesMap.Values;
            }
        }

        public bool KeyExists(string key)
        {
            return (!string.IsNullOrEmpty(key)) && (this.innerValuesMap.ContainsKey(key));
        }

        private void ValidateGetValueKey(string key)
        {
            this.ValidateKeyIsNotNull(key);
            this.ValidateKeyIsNotEmpty(key);
            this.ValidateKeyExists(key);
        }

        private void ValidateSetValueKey(string key)
        {
            this.ValidateKeyIsNotNull(key);
            this.ValidateKeyIsNotEmpty(key);
        }

        private void ValidateKeyIsNotNull(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key", "Value can't be null.");
            }
        }

        private void ValidateKeyIsNotEmpty(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentOutOfRangeException("key", "Value can't be an empty string.");
            }
        }

        private void ValidateKeyExists(string key)
        {
            if (!this.KeyExists(key))
            {
                throw new KeyNotFoundException("No values found for key: " + key);
            }
        }
    }
}

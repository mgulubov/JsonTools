namespace JsonTools.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Interfaces;

    public class JsonValue : IJsonValue
    {
        public JsonValue(string value)
        {
            this.Initialize(value);
        }

        public JsonValue(IJsonNode value)
        {
            this.Initialize(value);
        }

        public object InnerValue { get; private set; }

        public Type InnerType { get; private set; }

        public bool IsString()
        {
            return (this.InnerType != null) && (this.InnerType == typeof(string));
        }

        public bool IsNode()
        {
            return (this.InnerType != null) && (this.InnerType == typeof(IJsonNode));
        }

        public bool IsBool()
        {
            return (this.InnerType != null) && (this.InnerType == typeof(bool));
        }

        private void Initialize(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                this.Initialize(default(object), null);
                return;
            }

            if (this.StringIsBoolValue(value))
            {
                this.Initialize(Convert.ToBoolean(value.Trim().ToLower()), typeof(bool));
                return;
            }

            this.Initialize(value, typeof(string));
        }

        private void Initialize(IJsonNode value)
        {
            if (value == default(IJsonNode))
            {
                this.Initialize(default(object), null);
                return;
            }

            this.Initialize(value, typeof(IJsonNode));
        }

        private void Initialize(object innerValue, Type innerType)
        {
            this.InnerValue = innerValue;
            this.InnerType = innerType;
        }

        private bool StringIsBoolValue(string value)
        {
            string sanitizedValue = value.Trim().ToLower();

            return (sanitizedValue == "true") || (sanitizedValue == "false");
        }
    }
}

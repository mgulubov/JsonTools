using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JsonTools.Interfaces;

namespace JsonTools.Models
{
    public class JsonValue : IJsonValue
    {
        public JsonValue(string value)
            : this(value, typeof(string))
        { }

        public JsonValue(IJsonNode value)
            : this(value, typeof(IJsonNode))
        { }

        public JsonValue(object value)
            : this(value, typeof(object))
        { }

        private JsonValue(object innerValue, Type innerType)
        {
            if (innerValue == null)
            {
                innerType = null;
            }

            this.InnerValue = innerValue;
            this.InnerType = innerType;
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
    }
}

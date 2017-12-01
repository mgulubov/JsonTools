namespace JsonTools.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IJsonNode
    {
        string Key { get; }

        IJsonNode ParentNode { get; set; }

        IJsonValue this[string key] { get; set; }

        IEnumerable<string> Keys { get; }

        IEnumerable<IJsonValue> Values { get; }

        bool KeyExists(string key);
    }
}

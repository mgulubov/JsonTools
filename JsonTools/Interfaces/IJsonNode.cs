using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonTools.Interfaces
{
    public interface IJsonNode
    {
        string Key { get; }

        IJsonNode ParentNode { get; }

        IJsonValue this[string key] { get; set; }

        IEnumerable<string> Keys { get; }

        IEnumerable<IJsonValue> Values { get; }

        bool KeyExists(string key);
    }
}

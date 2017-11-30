using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonTools.Interfaces
{
    public interface IJsonValue
    {
        object InnerValue { get; }

        Type InnerType { get; }

        bool IsString();

        bool IsNode();
    }
}

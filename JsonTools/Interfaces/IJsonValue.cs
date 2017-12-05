namespace JsonTools.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IJsonValue
    {
        object InnerValue { get; }

        Type InnerType { get; }

        bool IsString();

        bool IsInt();

        bool IsNode();

        bool IsBool();
    }
}

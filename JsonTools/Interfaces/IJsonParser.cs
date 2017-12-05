using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonTools.Interfaces
{
    public interface IJsonParser
    {
        IJsonNode Parse(string jsonString);
    }
}

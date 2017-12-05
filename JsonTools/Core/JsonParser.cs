using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using JsonTools.Interfaces;
using JsonTools.Models;

namespace JsonTools.Core
{
    public class JsonParser : IJsonParser
    {
        StreamReader StreamReader;

        public IJsonNode Parse(string jsonString)
        {
            jsonString = this.SanitizeString(jsonString);
            this.SetStreamReader(jsonString);
            IJsonNode rootNode = new JsonNode(null);

            using (this.StreamReader)
            {
                while (true)
                {
                    int i = this.StreamReader.Peek();
                    char c = (char)i;
                    if (i == -1 || c == '}') 
                    {
                        break;
                    }

                    string key = this.GetNextKey();
                    IJsonValue value = this.GetNextValue();

                    rootNode[key] = value;
                    continue;
                }
            }

            return rootNode;
        }

        private string SanitizeString(string jsonString)
        { 
            jsonString = jsonString.Replace(System.Environment.NewLine, "");
            jsonString = jsonString.Replace('\t', ' ');
            jsonString = Regex.Replace(jsonString, @"""\s+:", @""":");

            return jsonString;
        }

        private void SetStreamReader(string jsonString)
        {
            Stream jsonStream = this.ConvertStringToStream(jsonString);
            this.StreamReader = new StreamReader(jsonStream);
        }

        private Stream ConvertStringToStream(string str)
        {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream);

            streamWriter.Write(str);
            streamWriter.Flush();
            memoryStream.Position = 0;

            return memoryStream;
        }

        private string GetNextKey()
        { 
            while (true)
            {
                char c = (char)this.StreamReader.Peek();
                this.StreamReader.Read();
                if (c == '\"')
                {
                    break;
                }
            }

            StringBuilder builder = new StringBuilder();
            while (true)
            {
                char c = (char)this.StreamReader.Peek();
                this.StreamReader.Read();

                builder.Append(c);
                if (c == '\"')
                {
                    char nextC = (char)this.StreamReader.Peek();
                    if (nextC == ':')
                    {
                        builder.Remove(builder.Length - 1, 1);
                        break;
                    }
                }
            }

            return builder.ToString();
        }

        private IJsonValue GetNextValue()
        {
            while (true)
            {
                char c = (char)this.StreamReader.Peek();
                this.StreamReader.Read();
                if (c == '\"' || c == 't' || c == 'f')
                {
                    break;
                }
            }

            StringBuilder stringBuilder = new StringBuilder();
            while (true)
            {
                char c = (char)this.StreamReader.Peek();
                this.StreamReader.Read();

                stringBuilder.Append(c);
                if (c == '\"')
                {
                    char nextC = (char)this.StreamReader.Peek();
                    if (nextC == ',' || nextC == '}')
                    {
                        stringBuilder.Remove(stringBuilder.Length - 1, 1);
                        break;
                    }
                }
            }

            IJsonValue value = new JsonValue(stringBuilder.ToString());
            return value;
        }
    }
}

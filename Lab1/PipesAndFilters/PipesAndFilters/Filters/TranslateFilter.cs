using PipesAndFilters.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipesAndFilters.Filters
{
    public class TranslateFilter : IFilter
    {
        public IMessage Run(IMessage message)
        {
            if (message.Headers.TryGetValue("RequestFormat", out var format))
            {
                if (format == "Bytes")
                {
                    string[] parts = message.Body.Split('-');
                    byte[] bytes = parts.Select(byte.Parse).ToArray();
                    message.Body = Encoding.ASCII.GetString(bytes);
                }
            }

            if (message.Headers.TryGetValue("ResponseFormat", out var responseFormat))
            {
                if (responseFormat == "Bytes")
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(message.Body);
                    message.Body = string.Join("-", bytes);
                }
            }

            return message;
        }
    }

}

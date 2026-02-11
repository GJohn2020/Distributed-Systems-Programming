using PipesAndFilters.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipesAndFilters.Filters
{
    public class AuthenticateFilter : IFilter
    {
        public IMessage Run(IMessage message)
        {
            if (message.Headers.TryGetValue("User", out var userId)) 
            {
                ServerEnvironment.SetCurrentUser(int.Parse(userId));
            }
            return message;
        }
    }
}

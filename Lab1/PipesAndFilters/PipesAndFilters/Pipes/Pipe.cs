using PipesAndFilters.Filters;
using PipesAndFilters.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipesAndFilters.Pipes
{
    class Pipe : IPipe
    {
        private readonly List<IFilter> _filters;
        public Pipe()
        {
            _filters = new List<IFilter>();
        }
        public IMessage ProcessMessage(IMessage message)
        {
            foreach (var filter in _filters) {
                message=filter.Run(message);
            }
            return message;
        }

        public void RegisterFilter(IFilter filter)
        {
            _filters.Add(filter);
        }
    }
}

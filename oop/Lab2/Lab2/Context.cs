using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Context
    {
        private IStrategy strategy;
        public Context() { }
        public Context(IStrategy strategy)
        {
            this.strategy = strategy;
        }

        public void SetStrategy(IStrategy strategy)
        {
            this.strategy = strategy;
        }

        public string Find(string query)
        {
            return strategy.Find(query);
        }
    }
}

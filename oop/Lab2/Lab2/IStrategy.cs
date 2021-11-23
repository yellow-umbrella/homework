using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public interface IStrategy
    {
        public string Find(List<string> attributes, Dictionary<string, string> format, HashSet<string> usedNodes);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lab2
{
    class DomStrategy : IStrategy
    {
        XmlDocument xmlDoc;
        public DomStrategy(string file)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load(file);
        }

        private void RecurseNodes(XmlNode node, StringBuilder sb, Dictionary<string, string> format)
        {
            foreach (XmlAttribute attr in node.Attributes)
            {
                sb.AppendFormat("{0}{1} ", format[attr.Name], attr.Value);
                sb.AppendLine();
            }
            foreach (XmlNode n in node.ChildNodes)
            {
                RecurseNodes(n, sb, format);
            }
        }

        public string Find(List<string> attributes, Dictionary<string, string> format, HashSet<string> usedNodes)
        {
            string query = createQuery(attributes);
            var nodes = xmlDoc.SelectNodes(query);
            var sb = new StringBuilder();
            foreach (XmlNode node in nodes)
            {
                var _node = node;
                while (_node.Name != "Class")
                {
                    _node = _node.ParentNode;
                }
                if (!usedNodes.Contains(_node.Attributes[0].Value))
                {
                    RecurseNodes(_node, sb, format);
                    usedNodes.Add(_node.Attributes[0].Value);
                }
            }
            return sb.ToString();
        }

        public HashSet<string> getAttr(string attribute)
        {
            HashSet<string> res = new HashSet<string>();
            var attributes = xmlDoc.SelectNodes("//@" + attribute);
            foreach (XmlAttribute attr in attributes)
            {
                res.Add(attr.Value);   
            }
            return res;
        }

        private string createQuery(List<string> attributes)
        {
            StringBuilder sb = new StringBuilder();
            List<string> _attributes = new List<string>();
            foreach (string attr in attributes)
            {
                if (attr != "")
                {
                    _attributes.Add("=\"" + attr + "\"");
                } 
                else
                {
                    _attributes.Add(attr);
                }
            }
            sb.AppendFormat("//Class[@ClassName{0}][@SeatsNum{1}]/Day[@DayName{2}]/Pair[@PairNum{3}][@Professor{4}]",
                _attributes[0], _attributes[1], _attributes[2], _attributes[3], _attributes[4]);
            return sb.ToString();
        }
    }
}

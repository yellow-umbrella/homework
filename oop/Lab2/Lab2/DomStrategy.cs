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
        public DomStrategy()
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load("XmlDocument.xml");
        }
        private void parsingXmlDocument()
        {
            RecurseNodes(xmlDoc.DocumentElement);
        }

        private string RecurseNodes(XmlNode node)
        {
            var sb = new StringBuilder();
            RecurseNodes(node, 0, sb);
            return sb.ToString();
        }

        private void RecurseNodes(XmlNode node, int level, StringBuilder sb)
        {
            sb.AppendFormat("{0,-2} Type:{1,-9} Name:{2,-13} Attr:",
                            level, node.NodeType, node.Name);
            foreach (XmlAttribute attr in node.Attributes)
            {
                sb.AppendFormat("{0}={1} ", attr.Name, attr.Value);
            }
            sb.AppendLine();
            foreach (XmlNode n in node.ChildNodes)
            {
                RecurseNodes(n, level + 1, sb);
            }
        }

        public string Find(string query)
        {
            var node = xmlDoc.SelectSingleNode(query);
            return RecurseNodes(node);
        }
    }
}

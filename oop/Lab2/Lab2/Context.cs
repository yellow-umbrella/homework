using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;
using System.Xml.Linq;

namespace Lab2
{
    class Context
    {
        private IStrategy strategy;
        private const string file = "../../../ClassSchedule.xml";
        private const string style = "../../../ClassSchedule.xsl";
        private const string xml = "../../../ResClassSchedule.xml";
        private const string html = "../../../ClassSchedule.html";
        public string File { get => file; }

        private Dictionary<string, string> format = new Dictionary<string, string>()
        {
            {"ClassName","номер кабінету : " },
            {"SeatsNum","\tкількість місць : " },
            {"DayName","\t> " },
            {"PairNum","\t\t-номер пари : " },
            {"Professor","\t\t  викладач : " }
        };

        HashSet<string> usedNodes;

        public Context() 
        {
            usedNodes = new HashSet<string>();
        }
        public Context(IStrategy strategy)
        {
            this.strategy = strategy;
            usedNodes = new HashSet<string>();
        }

        public void SetStrategy(IStrategy strategy)
        {
            this.strategy = strategy;
        }

        public string Find(List<string> attributes)
        {
            usedNodes = new HashSet<string>();
            return strategy.Find(attributes, format, usedNodes);
        }

        public void ConvertToHtml()
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            updateXml();
            xslt.Load(style);
            xslt.Transform(xml, html);
        }

        public HashSet<string> GetAttributes(string attr)
        {
            var dom = new DomStrategy(file);
            return dom.GetAttr(attr);
        }

        private void updateXml()
        {
            var doc = XDocument.Load(file);
            var res = (from cls in doc.Descendants("Class")
                       where usedNodes.Count == 0 || usedNodes.Contains(cls.Attribute("ClassName").Value)
                       select cls).ToList();
            var newDoc = new XDocument();
            var root = new XElement("root");
            foreach (var node in res)
            {
                root.Add(node);
            }
            newDoc.Add(root);
            newDoc.Save(xml);
        }
    }
}

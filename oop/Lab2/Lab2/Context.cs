using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace Lab2
{
    class Context
    {
        private IStrategy strategy;
        private string file = "../../../ClassSchedule.xml";
        private string style = "../../../ClassSchedule.xsl";
        private string xml = "../../../ResClassSchedule.xml";
        private string html = "../../../ClassSchedule.html";
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

        public Context() { }
        public Context(IStrategy strategy)
        {
            this.strategy = strategy;
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
            xslt.Load(style);
            xslt.Transform(file, html);
        }
    }
}

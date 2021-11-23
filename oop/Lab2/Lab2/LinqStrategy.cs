using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab2
{
    class LinqStrategy : IStrategy
    {
        public LinqStrategy(string file)
        {
            doc = XDocument.Load(file);
        }

        XDocument doc;

        public string Find(List<string> attributes, Dictionary<string, string> format, HashSet<string> usedNodes)
        {
            var result = (from cls in doc.Descendants("Class")
                          where (attributes[0] == "" || cls.Attribute("ClassName").Value == attributes[0])
                            && (attributes[1] == "" || cls.Attribute("SeatsNum").Value == attributes[1])

                            from day in cls.Descendants("Day")
                            where attributes[2] == "" || day.Attribute("DayName").Value == attributes[2]

                                from pair in day.Descendants("Pair")
                                where (attributes[3] == "" || pair.Attribute("PairNum").Value == attributes[3])
                                && (attributes[4] == "" || pair.Attribute("Professor").Value == attributes[4]) &&
                                usedNodes.Add(cls.Attribute("ClassName").Value)
                                    select cls).ToList();

            return formatResult(result, format);
        }

        private string formatResult(List<XElement> nodes, Dictionary<string, string> format)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var node in nodes)
            {
                foreach (var attr in node.Attributes())
                {
                    sb.AppendFormat("{0}{1}", format[attr.Name.LocalName], attr.Value);
                    sb.AppendLine();
                }
                if (node.HasElements) 
                {     
                    sb.Append(formatResult(node.Elements().ToList(), format));
                }
            }
            return sb.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lab2
{
    class SaxStrategy : IStrategy
    {
        
        public SaxStrategy(string file)
        {
            this.file = file;
        }

        private string file;

        public string Find(List<string> attributes, Dictionary<string, string> format, HashSet<string> usedNodes)
        {
            var sb = new StringBuilder();
            var node = new StringBuilder();
            var attr = createQuery(attributes);
            string id = "";
            bool check = false;
            var xmlReader = new XmlTextReader(file);
            while (xmlReader.Read())
            {
                if (xmlReader.Name == "Class")
                {
                    if (check)
                    {
                        sb.Append(node);
                        usedNodes.Add(id);
                    }
                    node = new StringBuilder();
                    check = true;
                }

                if (xmlReader.HasAttributes)
                {
                    while (xmlReader.MoveToNextAttribute())
                    {
                        if (format.ContainsKey(xmlReader.Name))
                        {
                            if (xmlReader.Name == "ClassName")
                            {
                                id = xmlReader.Value;
                            }
                            node.AppendFormat("{0}{1}", format[xmlReader.Name], xmlReader.Value);
                            if (attr[xmlReader.Name] != "" && attr[xmlReader.Name] != xmlReader.Value)
                            {
                                check = false;
                            }
                            node.AppendLine();
                        }
                    }
                }
            }
            xmlReader.Close();
            return sb.ToString();
        }

        private Dictionary<string, string> createQuery(List<string> attributes)
        {
            return new Dictionary<string, string>()
            {
                {"ClassName", attributes[0] },
                {"SeatsNum", attributes[1] },
                {"DayName", attributes[2] },
                {"PairNum", attributes[3] },
                {"Professor", attributes[4] }
            };
        }
    }
}

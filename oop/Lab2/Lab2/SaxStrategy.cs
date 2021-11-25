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
            var ind = makeInd();
            bool check = false;
            string id = "";
            bool[] isCorrect = new bool[attr.Count];
            var xmlReader = new XmlTextReader(file);

            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Name == "Class")
                {
                    if (check)
                    {
                        sb.Append(node);
                        usedNodes.Add(id);
                    }
                    node = new StringBuilder();
                    check = false;
                    isCorrect = new bool[attr.Count];
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
                            int index = ind[xmlReader.Name];
                            if ((attr[xmlReader.Name] == "" || attr[xmlReader.Name] == xmlReader.Value) 
                                && (index == 0 || isCorrect[index - 1]))
                            {
                                isCorrect[index] = true;
                            }
                            else
                            {
                                isCorrect[index] = false;
                            }
                            node.AppendLine();   
                        }
                    }
                }

                if (isCorrect[attr.Count - 1])
                {
                    check = true;
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

        private Dictionary<string, int> makeInd()
        {
            return new Dictionary<string, int>()
            {
                {"ClassName", 0 },
                {"SeatsNum", 1 },
                {"DayName", 2 },
                {"PairNum", 3 },
                {"Professor", 4 }
            };
        }
    }
}

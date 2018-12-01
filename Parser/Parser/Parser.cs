using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public class ANode
    {
        public string token_type;
        public char token_value;
        public ANode(string token_type, char token_value)
        {
            this.token_type = token_type;
            this.token_value = token_value;
        }
    }
    
    public class Parser
    {
        
        List<char> list = new List<char>();
        
        private void InsertL(int i)
        {
            bool check = false;
            while (i > 0)
            {
                if (list[i - 1] == '/' || list[i - 1] == '*' || list[i - 1] == '+' || list[i - 1] == '-')
                {
                    list.Insert(i, '(');
                    check = true;
                    break;
                }
                else
                {
                    if (list[i - 1] == ')')
                    {
                        int cnt = 1;
                        while (cnt != 0)
                        {
                            i--;
                            if (list[i - 1] == ')')
                                cnt++;
                            if (list[i - 1] == '(')
                                cnt--;
                        }
                    }
                    else
                        i--;
                }
            }
            if (!check)
                list.Insert(0, '(');
        }
        private void InsertR(int i)
        {
            bool check = false;
            while (i < list.Count - 1)
            {
                if (list[i + 1] == '/' || list[i + 1] == '*' || list[i + 1] == '+' || list[i + 1] == '-')
                {
                    list.Insert(i + 1, ')');
                    check = true;
                    break;
                }
                else
                {
                    if (list[i + 1] == '(')
                    {
                        int cnt = 1;
                        while (cnt != 0)
                        {
                            i++;
                            if (list[i + 1] == '(')
                                cnt++;
                            if (list[i + 1] == ')')
                                cnt--;
                        }
                    }
                    else
                        i++;
                }
            }
            if (!check)
                list.Add(')');
        }
        public List<ANode> Parse(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                list.Add(s[i]);
            }
            ParseDel();
            List<ANode> result = new List<ANode>();
            foreach (char a in list)
            {
                string type;
                char value;
                if (a == '(' || a == '(')
                {
                    type = "скобка";
                    value = a;
                    ANode token0 = new ANode(type, value);
                    result.Add(token0);
                    continue;
                }
                if (a == '+' || a == '*' || a == '/' || a == '-')
                {
                    type = "операция";
                    value = a;
                    ANode token1 = new ANode(type, value);
                    result.Add(token1);

                }
                type = "цифра";
                value = a;
                ANode token2 = new ANode(type, value);
                result.Add(token2);
            }
            return result;
        }
        private void ParseDel()
        {
            for (int i = 0; i < list.Count; i++)
                if (list[i] == '/')
                {
                    InsertL(i);
                    InsertR(i + 1);
                    i += 2;
                }
            ParsePow();
        }
        private void ParsePow()
        {
            for (int i = 0; i < list.Count; i++)
                if (list[i] == '*')
                {
                    InsertL(i);
                    InsertR(i + 1);
                    i += 2;
                }

            ParseDiff();
        }
        private void ParseDiff()
        {
            for (int i = 0; i < list.Count; i++)
                if (list[i] == '-')
                {
                    InsertL(i);
                    InsertR(i + 1);
                    i += 2;
                }

            ParsePlus();
        }
       
        private void ParsePlus()
        {
            for (int i = 0; i < list.Count; i++)
                if (list[i] == '+')
                {
                    InsertL(i);
                    InsertR(i + 1);
                    i += 2;
                }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            Parser parser = new Parser();
            List<ANode> list = parser.Parse(s);
            foreach(ANode node in list)
            {
                Console.Write("[ " + node.token_type + ", " + node.token_value + " ], ");
            }
            
        }
    }
}

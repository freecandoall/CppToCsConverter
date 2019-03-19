using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PacketGenerator
{
    class StreamParser
    {
        public static List<string> Parse(StreamReader _cpp_stream)
        {
            List<string> readList = new List<string>();
            readList.Clear();

            string line = null;
            while (null != (line = _cpp_stream.ReadLine()))
            {
                AddLine(readList, line);
            }

            return readList.Where(x => !IsEmpty(x)).ToList();
        }

        private static void AddLine(List<string> _original, string _line)
        {
            if (IsEmpty(_line))
                return;

            string tmp = _line.Replace("\t\t","\t").Replace('\t', ' ');
            tmp = tmp.Trim();

            if (tmp.StartsWith("//"))
            {
                _original.Add(Trim(_line));
                return;
            }

            if (tmp.Contains("{"))
            {
                string replaced = tmp.Replace("{", "{");
                string[] re = replaced.Split('{').Where(x => !IsEmpty(x)).ToArray();
                int count = re.Length;
                if (count < 1)
                {
                    _original.Add("{");
                    if (replaced.Length > 1)
                    {
                        _original.Add(Trim(replaced.Substring(1)));
                    }
                }
                else
                {
                    for (int i = 0; i < count; ++i)
                    {
                        _original.Add(Trim(re[i]));
                        _original.Add("{");
                    }
                }
            }
            else if (tmp.Contains("}") || tmp.Contains("};"))
            {
                string replaced = tmp.Replace("};", "}");
                string[] re = replaced.Split('}').Where(x => !IsEmpty(x)).ToArray();
                int count = re.Length;
                if (count <= 1)
                {
                    _original.Add("}");
                    if (replaced.Length > 1)
                    {
                        _original.Add(Trim(replaced.Substring(1)));
                    }
                }
                else
                {
                    for (int i = 0; i < count; ++i)
                    {
                        _original.Add("}");
                        _original.Add(Trim(re[i]));
                    }
                }
            }
            else if (tmp.Contains("/*") && !tmp.Contains("*/"))
            {
                _original.Add("");
                _original.Add(Trim("/*"));
                _original.Add("");
            }
            else if (!tmp.Contains("/*") && tmp.Contains("*/"))
            {
                _original.Add("");
                _original.Add(Trim("*/"));
                _original.Add("");
            }
            else
            {
                _original.Add(Trim(_line));
            }
        }

        private static string Trim(string _line)
        {
            string[] tmp = _line.Replace('\t', ' ').Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray();

            string result = "";
            int count = tmp.Length;
            for (int i = 0; i < count; ++i)
            {
                result += tmp[i];
                if (i < count - 1)
                    result += " ";
            }
            return result;
        }

        private static bool IsEmpty(string _line)
        {
            if (string.IsNullOrEmpty(_line))
                return true;

            if (_line.Equals(' '))
                return true;

            string[] split = _line.Split('\t').Where(x => !string.IsNullOrEmpty(x)).ToArray();
            if (split.Length <= 0)
                return true;

            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketGenerator.Converter.Identify
{
    class ElemReplace
    {
        public static string Replace(string _line)
        {
            Namespace(ref _line);

            return _line;
        }

        public static bool Include(ref string _line)
        {
            if (ElemIdentify.IsInclude(_line))
            {
                string lower = _line.ToLower();
                if (lower.Contains("<iostream>"))
                {
                    _line = "using System;";
                }
                else if (lower.Contains("<windows.h>") == true)
                {
                    _line = "using System.Windows.Forms;";
                }
                else if (lower.Contains("<string>") == true)
                {
                    _line = "using System.Text;";
                }
                else
                {
                    _line = ElementParser.FORCE_COMMENT+ " " + _line;
                }
                
                return true;
            }

            return false;
        }

        public static bool Namespace(ref string _line)
        {
            if (ElemIdentify.IsUsingNamespace(_line))
            {
                string line = _line.Substring(0, _line.IndexOf(";"));

                string comment = "";
                if (_line.Contains("//"))
                {
                    int idx = _line.IndexOf("//");
                    comment = _line.Substring(idx);
                }

                string[] split = line.Split('=');
                if (split.Length == 2)
                {
                    string left = split[0];
                    string right = split[1];

                    left = left.Replace("namespace", "using");
                    right = right.Trim().Replace("::", ".").Replace(";", "");
                    if (right.StartsWith("."))
                    {
                        right = right.Substring(1);
                    }

                    _line = string.Format("{0} = {1};{2}", left, right, comment);
                    return true;
                }
            }

            return false;
        }

        public static bool DefineConst(ref string _line)
        {
            if (ElemIdentify.IsDefine(_line))
            {
                if (_line.Trim().Contains("("))
                {
                    _line = ElementParser.FORCE_COMMENT + " " + _line;
                    return true;
                }

                string tmpStr = _line.Replace("\t", " ");

                string defineName = "";
                string defineValue = "";

                string[] split = tmpStr.Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                if (split[0].Equals("#define") && split.Length >= 3)
                {
                    defineName = split[1];
                    defineValue = split[2];
                }
                else if (split[0].Equals("#define") && split.Length == 2)
                {
                    split = split[1].Split('\t').Where(x => !string.IsNullOrEmpty(x)).ToArray();

                    int count = split.Length;
                    if (count > 1)
                    {
                        defineName = split[0];
                        defineValue = split[1];
                    }
                }

                defineValue = defineValue.Replace('\"', ' ').Trim();
                if (defineValue.Contains("//"))
                {
                    if (defineValue.Trim().StartsWith("http:") == false &&
                        defineValue.Trim().StartsWith("https:") == false)
                    {
                        defineValue = defineValue.Substring(0, defineValue.IndexOf('/'));
                    }
                }

                if (!string.IsNullOrEmpty(defineName) && !string.IsNullOrEmpty(defineValue))
                {
                    int start = _line.IndexOf(defineValue);
                    int finish = start + defineValue.Length;

                    string comment = _line.Substring(finish);

                    uint value;
                    if (uint.TryParse(defineValue, out value))
                    {
                        if (string.IsNullOrEmpty(comment))
                        {
                            _line = string.Format("public const uint {0} = {1};", defineName, defineValue);
                        }
                        else
                        {
                            _line = string.Format("public const uint {0} = {1};\t//{2}", defineName, defineValue, comment);
                        }

                        return true;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(comment))
                        {
                            _line = string.Format("public const string {0} = {1};", defineName, defineValue);
                        }
                        else
                        {
                            _line = string.Format("public const string {0} = \"{1}\";\t//{2}", defineName, defineValue, comment);
                        }

                        return true;
                    }
                }
                else
                {
                    _line = ElementParser.FORCE_COMMENT + " " + _line;
                }
            }

            return false;
        }

        public static bool ConstMember(ref string _line)
        {
            if (ElemIdentify.IsConstMember(_line))
            {
                _line = _line.Replace("static const", "public const");
                return true;
            }

            return false;
        }

        public static bool ForceComment(ref string _line)
        {
            if (ElemIdentify.IsForceComment(_line))
            {
                _line = ElementParser.FORCE_COMMENT + _line;
                return true;
            }

            return false;
        }
    }
}

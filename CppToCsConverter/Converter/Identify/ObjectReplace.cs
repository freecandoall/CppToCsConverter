using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PacketGenerator.Converter.Identify
{
    class ObjectReplace
    {
        public static bool Cout(ref string _line)
        {
            if (ObjectIdentify.IsCout(_line))
            {
                _line = _line.Replace("std::cout <<", "\tConsole.Write(");
                _line = _line.Replace("cout <<", "\tConsole.Write(");
                _line = _line.Replace("<< std::endl", " + \"\\n\"");
                _line = _line.Replace("<< endl", " + \"\\n\"");
                _line = _line.Replace("std::cout<<", "\tConsole.Write(");
                _line = _line.Replace("cout<<", "\tConsole.Write(");
                _line = _line.Replace("<<endl", " + \"\\n\"");
                _line = _line.Replace("<<std::endl", " + \"\\n\"");
                _line = _line.Replace(";", ");");
                _line = _line.Replace("<<", "+");
                return true;
            }

            return false;
        }

        public static bool Cin(ref string _line)
        {
            if (ObjectIdentify.IsCin(_line))
            {
                _line = _line.Replace("std::cin >>", "");
                _line = _line.Replace("std::cin>>", "");
                _line = _line.Replace("cin >>", "");
                _line = _line.Replace(";", "");
                _line = _line.Replace("cin>>", "");
                _line = _line + " = Console.ReadLine();";
                return true;
            }

            return false;
        }

        public static bool Null(ref string _line)
        {
            if (ObjectIdentify.IsNull(_line))
            {
                _line = _line.Replace("nullptr", "null");
                return true;
            }

            return false;
        }

        public static bool InheritProtection(ref string _line)
        {
            if (ObjectIdentify.IsInheritProtection(_line))
            {
                _line = _line.Replace(":public", ":");
                _line = _line.Replace(": public", ":");
                return true;
            }

            return false;
        }

        public static bool EnumForcePublic(ref string _line)
        {
            if (ObjectIdentify.IsEnumForcePublic(_line))
            {
                //enum E_CODE
                _line = _line.Replace("private", "public");
                if (!_line.Contains("public"))
                {
                    _line = "public " + _line;
                }

                return true;
            }

            return false;
        }

        public static bool NewClass(ref string _line, string _class_name)
        {
            if (ObjectIdentify.IsNewClass(_line))
            {
                return true;
            }

            return false;
            //if ((_line.Trim().StartsWith(_class_name) == false))
            //    return false;

            //if (_line.Trim().Contains("."))
            //    return false;

            //_line = _line.Replace(";", " = new ") + _class_name + "();";

            //return true;
        }

        public static bool InConst(ref string _line)
        {
            if (ObjectIdentify.IsInConst(_line))
            {
                _line = _line.Replace("const;", ";");
                _line = _line.Replace("const;", "");
                return true;
            }

            return false;
        }

        public static bool If(ref string _line)
        {
            if (ObjectIdentify.IsIf(_line))
            {
                int start = _line.IndexOf("(");
                int finish = _line.IndexOf(")");
                int count = finish - start; // variable length
                string var = _line.Substring(start + 1, count - 1);
                _line = _line.Replace(var, var + " != 0");
                return true;
            }

            return false;
        }

        public static bool Uint(ref string _line)
        {
            if (ObjectIdentify.IsUint(_line))
            {
                _line = _line.Replace("unsigned int", "\tuint");
                return true;
            }


            return false;
        }

        public static bool UShort(ref string _line)
        {
            if (ObjectIdentify.IsUint(_line))
            {
                _line = _line.Replace("unsigned short", "\tushort");
                return true;
            }

            return false;
        }

        public static bool ULong(ref string _line)
        {
            if (ObjectIdentify.IsULong(_line))
            {
                _line = _line.Replace("unsigned __int64", "\tulong");
                return true;
            }

            return false;
        }

        public static bool String(ref string _line)
        {
            if (ObjectIdentify.IsULong(_line))
            {
                _line = _line.Replace("std::string", "string");
                _line = _line.Replace("STRING", "string");
                return true;
            }

            return false;
        }

        public static bool StringInitialize(ref string _line)
        {
            if (ObjectIdentify.IsStringInitialize(_line))
            {
                int start = _line.IndexOf("(");
                int finish = _line.IndexOf(",");
                int end = _line.IndexOf(";");

                int count = finish - start; // variable length
                string var = _line.Substring(start + 1, count - 1);
                string comment = _line.Substring(end).Replace(";", "");

                string tmpTap = _line.Substring(0, start);

                MatchCollection matches = Regex.Matches(tmpTap, "\t");

                //_line = MakeTapString(matches.Count) + var + " = \"\";\t//" + comment;
                _line = var + " = \"\";\t//" + comment;

                return true;
            }

            return false;
        }

        public static bool CharArray(ref string _line)
        {
            if (ObjectIdentify.IsCharArray(_line))
            {
                _line = _line.Replace("char", "string");

                int start = _line.IndexOf("[");
                int finish = _line.IndexOf("]");
                int end = _line.IndexOf(";");

                int count = finish - start; // variable length
                string var = _line.Substring(start + 1, count - 1);
                string comment = _line.Substring(end).Replace(";", "");
                string varName = _line.Substring(0, start);

                _line = string.Format("{0};\t{1}\t{2}\t[{3}]", varName, comment, ElementParser.FORCE_COMMENT, var);

                return true;
            }

            return false;
        }

        public static bool IntArray(ref string _line)
        {
            if (ObjectIdentify.IsIntArray(_line))
            {
                string trim = _line.Trim();

                int start = _line.IndexOf("[");
                int finish = _line.IndexOf("]");
                int end = _line.IndexOf(";");

                //string varName = trim(start + 1, count - 1);

                int count = finish - start; // variable length
                string var = _line.Substring(start + 1, count - 1);
                string comment = _line.Substring(end).Replace(";", "");

                string tmpTrim = trim.Trim('\t').Replace("int", "");
                string varName = tmpTrim.Substring(0, tmpTrim.IndexOf("["));

                //_line = string.Format("int[]\t{0}[{1}];\t//{2}", varName, var, comment);
                _line = string.Format("int[]\t{0};\t{1} {2}\t[{3}]", varName, comment, ElementParser.FORCE_COMMENT, var);

                return true;
            }

            return false;
        }
    }
}

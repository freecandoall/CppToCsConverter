using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketGenerator.Converter.Identify
{
    class ElemIdentify
    {
        public static bool IsComment(string _line)
        {
            if (_line.StartsWith("//"))
                return true;

            if (_line.StartsWith("/*") && _line.Contains("*/"))
                return true;

            return false;
        }

        public static bool IsGroupComment(string _line)
        {
            if (_line.StartsWith("/*"))
                return true;

            return false;
        }

        public static bool IsOpenComment(string _line)
        {
            if (!_line.StartsWith("//") && _line.Contains("/*") && !_line.Contains("*/"))
                return true;

            return false;
        }

        public static bool IsCloseComment(string _line)
        {
            if (!_line.StartsWith("//") && !_line.Contains("/*") && _line.Contains("*/"))
                return true;

            return false;
        }

        public static bool IsForceComment(string _line)
        {
            if (_line.StartsWith("#pragma"))
                return true;

            if (_line.StartsWith("__pragma"))
                return true;

            if (_line.Contains("using namespace") && _line.Contains(";"))
                return true;

            if (_line.Contains("using std::"))
                return true;

            if (_line.Contains("friend"))
                return true;

            if (_line.Contains("typedef"))
                return true;

            if (IsMacroFunction(_line))
                return true;

            return false;
        }

        public static bool IsInclude(string _line)
        {
            if (_line.StartsWith("#include"))
                return true;

            return false;
        }

        public static bool IsDefine(string _line)
        {
            if (_line.StartsWith("#define"))
                return true;

            return false;
        }

        public static bool IsUsingNamespace(string _line)
        {
            if (_line.Contains("namespace") && _line.Contains("=") && _line.Contains(";"))
                return true;

            //if (_line.Contains("using") && _line.Contains("=") && _line.Contains(";"))
            if (_line.Contains("using") && _line.Contains(";"))
                return true;

            return false;
        }

        public static bool IsNamespace(string _line)
        {
            if (_line.Contains("namespace") && !_line.Contains("=") && !_line.Contains(";"))
                return true;

            return false;
        }

        public static bool IsPredefineClass(string _line)
        {
            if (_line.Contains("class") && _line.Contains(";"))
                return true;

            return false;
        }

        public static bool IsClass(string _line)
        {
            if (_line.Contains("class") && !_line.Contains(";"))
                return true;

            return false;
        }

        public static bool IsPredefineStruct(string _line)
        {
            if (_line.Contains("struct") && _line.Contains(";"))
                return true;

            return false;
        }

        public static bool IsStruct(string _line)
        {
            if (_line.Contains("struct") && !_line.Contains(";"))
                return true;

            return false;
        }

        public static bool IsEnum(string _line)
        {
            if (_line.Contains("enum") && !_line.Contains(";"))
                return true;

            return false;
        }

        public static bool IsEnumMember(string _line)
        {
            if (_line.Contains(","))
            {
                string s = _line.Substring(0, _line.IndexOf(","));

                if (!s.Contains("(") && !s.Contains(":") && !s.Contains("["))
                    return true;
            }

            return false;
        }

        public static bool IsFunction(string _line)
        {
            if (_line.Contains("(") && _line.Contains(")") &&
                !_line.Contains(",") && !_line.Contains(";") &&
                !_line.Contains("#define"))
                return true;

            return false;
        }

        public static bool IsMacroFunction(string _line)
        {
            if (IsFunction(_line))
            {
                string funcName = _line.Substring(0, _line.IndexOf('('));
                if (funcName.Split().Length <= 1)
                {
                    if (!funcName.Equals(funcName.ToUpper()))
                        return true;
                }
            }

            return false;
        }

        public static bool IsConstMember(string _line)
        {
            if (_line.Contains("static") && _line.Contains("=") && _line.Contains(";"))
                return true;

            if (_line.Contains("const") && _line.Contains("=") && _line.Contains(";"))
                return true;

            return false;
        }

        public static bool IsMember(string _line)
        {
            if (_line.Contains(";"))
                return true;

            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketGenerator.Converter.Identify
{
    class ObjectIdentify
    {
        public static bool IsCout(string _line)
        {
            if (_line.Contains("cout"))
            {
                string trim = _line.Trim();
                if (trim.StartsWith("cout <<") || trim.StartsWith("std::cout <<"))
                    return true;
            }

            return false;
        }

        public static bool IsCin(string _line)
        {
            if (_line.Contains("cin"))
            {
                string trim = _line.Trim();
                if (trim.StartsWith("cin >>") || trim.StartsWith("std::cin >>"))
                    return true;
            }

            return false;
        }

        public static bool IsNull(string _line)
        {
            if (_line.Contains("nullptr"))
                return true;

            return false;
        }

        public static bool IsInheritProtection(string _line)
        {
            string trim = _line.Trim();
            if (trim.Contains(":public") || trim.Contains(": public"))
                return true;

            return false;
        }

        public static bool IsEnumForcePublic(string _line)
        {
            if (_line.Contains("enum"))
                return true;

            return false;
        }

        public static bool IsNewClass(string _line)
        {
            return false;
            //if ((_line.Trim().StartsWith(_class_name) == false))
            //    return false;

            //if (_line.Trim().Contains("."))
            //    return false;

            //_line = _line.Replace(";", " = new ") + _class_name + "();";

            //return true;
        }

        public static bool IsIf(string _line)
        {
            if (_line.Trim().Contains("if(") || _line.Trim().Contains("if ("))
            {
                if (!_line.Trim().Contains("==") && !_line.Trim().Contains("!="))
                    return true;
            }

            return false;
        }

        public static bool IsInConst(string _line)
        {
            if (_line.Contains("const;") || _line.Contains("const"))
                return true;

            return false;
        }

        public static bool IsUint(string _line)
        {
            if (_line.Contains("unsigned int"))
                return true;

            return false;
        }

        public static bool IsUShort(string _line)
        {
            if (_line.Contains("unsigned short"))
                return true;

            return false;
        }

        public static bool IsULong(string _line)
        {
            if (_line.Contains("unsigned __int64"))
                return true;

            return false;
        }

        public static bool IsString(string _line)
        {
            if (_line.Contains("std::string"))
                return true;

            if (_line.Contains("STRING"))
                return true;

            return false;
        }

        public static bool IsStringInitialize(string _line)
        {
            if (_line.Contains("MEMSET"))
                return true;

            return false;
        }

        public static bool IsCharArray(string _line)
        {
            if (!_line.Contains(ElementParser.FORCE_COMMENT))
            {
                string trim = _line.Trim();
                if (trim.Contains("//"))
                {
                    trim = trim.Substring(0, trim.IndexOf("//"));
                }

                if (trim.Contains("char") && trim.Contains("["))
                    return true;

            }

            return false;
        }

        public static bool IsIntArray(string _line)
        {
            if (!_line.Contains(ElementParser.FORCE_COMMENT))
            {
                string trim = _line.Trim();
                if (trim.Contains("//"))
                {
                    trim = trim.Substring(0, trim.IndexOf("//"));
                }

                if (trim.Contains("int") && trim.Contains("["))
                    return true;
            }

            return false;
        }


    }
}

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PacketGenerator.Converter.Structure;
using PacketGenerator.Converter.Identify;

namespace PacketGenerator.Converter
{
    class ElementParser
    {
        public const string FORCE_COMMENT = "//[ForceComment] ";
        public const string AUTO_GEN_CLASS = "AutoGen_by_Converter";

        public static ElemRoot Parse(List<string> _list)
        {
            return (ElemRoot)ParseEmelent(new ElemRoot(), -1, _list);
        }

        private static IElement ParseEmelent(IElement _parent, int _indent, List<string> _list)
        {
            try
            {
                string curLine = "";
                ElemType curType = ElemType.None;
                IElement curElem = null;

                int curIndent = _parent.indent = _indent;

                ++curIndent;

                while (_list.Count > 0)
                {
                    curLine = _list[0];
                    _list.RemoveAt(0);

                    if (IsCloseBrace(_parent.elemType, curLine))
                        break;

                    if (_parent.elemType == ElemType.GroupComment)
                    {
                        curElem = ElementFactory.Create(_parent, ElemType.Comment, curLine, curIndent);
                    }
                    else
                    {
                        curType = ElementFactory.GetType(ref curLine);
                        curElem = ElementFactory.Create(_parent, curType, curLine, curIndent);

                        if (IsOpenBrace(curType, curLine, _list))
                        {
                            _parent.Add(ParseEmelent(curElem, curIndent, _list));

                            continue;
                        }
                    }

                    _parent.Add(curElem);
                }

                --curIndent;
            }
            catch (Exception e)
            {
                string msg = string.Format("ElementParser::ParseEmelent() {0} ", e);
                Debug.Assert(false, msg);
                throw new Exception(msg);
            }

            return _parent;
        }

        private static bool IsOpenBrace(ElemType _type, string _cur, List<string> _list)
        {
            if (ElemIdentify.IsOpenComment(_cur))
                return true;

            if (_list.Count > 0)
            {
                string nextLine = _list[0];

                if (nextLine.Equals("{") || nextLine.Equals("("))
                {
                    if (IElement.IsBraceType(_type))
                    {
                        _list.RemoveAt(0);
                        return true;
                    }
                    else
                    {
                        string msg = "ElementParser::IsOpenBrace() Invalid open brace.";
                        msg += string.Format(" type = {0}, cur = {1}, next = {2}", _type, _cur, nextLine);
                        Debug.Assert(false, msg);
                        throw new Exception(msg);
                    }
                }
            }
                
            return false;
        }

        private static bool IsCloseBrace(ElemType _root_type, string _line)
        {
            if (_root_type == ElemType.GroupComment)
            {
                if (!ElemIdentify.IsOpenComment(_line) && ElemIdentify.IsCloseComment(_line))
                    return true;

                return false;
            }

            if (_line.Equals("}") || _line.Equals(")"))
                return true;

            return false;
        }
    }
}
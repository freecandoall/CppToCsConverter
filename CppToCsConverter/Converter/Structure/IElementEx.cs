using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PacketGenerator.Converter.Identify;
using PacketGenerator.Converter.Link;

namespace PacketGenerator.Converter.Structure
{
    public partial class IElement
    {
        public static bool HasChild(IElement _elem)
        {
            if (_elem.child.Count > 0 || _elem.constList.Count > 0)
                return true;

            return false;
        }

        public static bool IsLostParentConstMember(IElement _elem)
        {
            if (!ElemIdentify.IsConstMember(_elem.originData) && !ElemIdentify.IsDefine(_elem.originData))
                return false;

            if (_elem.elemType == ElemType.Comment ||
                _elem.elemType == ElemType.ForceComment)
                return false;

            if (_elem.parent.elemType != ElemType.Root &&
                _elem.parent.elemType != ElemType.NameSpace)
                return false;

            return true;
        }

        private static void AddConstMembers(IElement _elem)
        {
            int count = _elem.constList.Count;
            if (count <= 0)
                return;

            int curIndent = _elem.indent + 1;
            AddFormatLine(curIndent, _elem.tmpCollecting, "public static class " + ElementParser.AUTO_GEN_CLASS);
            AddFormatLine(curIndent, _elem.tmpCollecting, "{");

            IElement constElem = null;
            var iter = _elem.constList.GetEnumerator();
            while (iter.MoveNext())
            {
                constElem = iter.Current;

                AddFormatLine(constElem.indent + 1, _elem.tmpCollecting, DataToStringList(constElem));
            }

            AddFormatLine(curIndent, _elem.tmpCollecting, "}");
        }

        private static void AddRange(IElement _elem, List<string> _lines)
        {
            var iter = _lines.GetEnumerator();
            while (iter.MoveNext())
            {
                _elem.tmpCollecting.AddLast(iter.Current);
            }
        }

        private static void AddFormatLine(int _indent, LinkedList<string> _contain, string _line)
        {
            _contain.AddLast(MakeIndent(_indent) + _line);
        }

        private static void AddFormatLine(int _indent, LinkedList<string> _contain, LinkedList<string> _lines)
        {
            int count = _lines.Count;
            var iter = _lines.GetEnumerator();
            while (iter.MoveNext())
            {
                _contain.AddLast(MakeIndent(_indent) + iter.Current);
            }
        }

        private static LinkedList<string> DataToStringList(IElement _elem)
        {
            _elem.formatData.Clear();

            LinkedList<string> list = new LinkedList<string>();
            List<string> tmp = _elem.Replace();

            string tmpData = "";

            int count = tmp.Count;
            for (int i=0; i<count; ++i)
            {
                tmpData = tmp[i];
                Link.BrokenLinker.Link(ref tmpData);
                list.AddLast(tmpData);
            }

            return list;
        }

        private static string MakeIndent(int _indent)
        {
            string tap = "";

            for (int i = 0; i < _indent; ++i)
                tap += "\t";

            return tap;
        }

        public static bool IsBraceType(IElement _elem)
        {
            return IsBraceType(_elem.elemType);
        }
        public static bool IsBraceType(ElemType _type)
        {
            switch (_type)
            {
                case ElemType.NameSpace:
                case ElemType.Class:
                case ElemType.Struct:
                case ElemType.Enum:
                case ElemType.Function:
                case ElemType.GroupComment:
                    return true;
            }

            return false;
        }
    }
}

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PacketGenerator.Converter.Structure;
using PacketGenerator.Converter.Identify;

namespace PacketGenerator.Converter
{
    class ElementFactory
    {
        public static IElement Create(IElement _parent, ElemType _type, string _line, int _indent)
        {
            IElement elem = null;

            switch (_type)
            {
                case ElemType.Comment:      elem = new ElemComment(_parent, _line);  break;
                case ElemType.GroupComment: elem = new ElemGroupComment(_parent, _line); break;
                case ElemType.ForceComment: elem = new ElemForceComment(_parent, _line); break;
                case ElemType.Include:      elem = new ElemInclude(_parent, _line); break;
                case ElemType.Define:       elem = new ElemDefine(_parent, _line); break;
                case ElemType.NameSpace:    elem = new ElemNamespace(_parent, _line); break;
                case ElemType.Class:        elem = new ElemClass(_parent, _line); break;
                case ElemType.Struct:       elem = new ElemStruct(_parent, _line); break;
                case ElemType.Enum:         elem = new ElemEnum(_parent, _line); break;
                case ElemType.EnumMember:   elem = new ElemEnumMember(_parent, _line); break;
                case ElemType.Function:     elem = new ElemFunction(_parent, _line); break;
                case ElemType.ConstMember:  elem = new ElemConstMember(_parent, _line); break;
                case ElemType.Member:       elem = new ElemMember(_parent, _line); break;

                case ElemType.CustomGen:    elem = new ElemCustomGen(_parent, _line); break;
            }

            if (null != elem)
            {
                elem.indent = _indent;
                return elem;
            }

            string msg = "ElementFactory::Create() Not implemented type. ElemType = " + _type;
            Debug.Assert(false, msg);
            throw new Exception(msg);
        }

        public static ElemType GetType(ref string _line)
        {
            if (ElemIdentify.IsComment(_line))
                return ElemType.Comment;

            if (ElemIdentify.IsGroupComment(_line))
                return ElemType.GroupComment;

            if (ElemIdentify.IsForceComment(_line))
                return ElemType.ForceComment;

            if (ElemIdentify.IsInclude(_line) ||
                ElemIdentify.IsUsingNamespace(_line))
                return ElemType.Include;

            if (ElemIdentify.IsDefine(_line))
                return ElemType.Define;

            if (ElemIdentify.IsNamespace(_line))
                return ElemType.NameSpace;

            if (ElemIdentify.IsClass(_line))
                return ElemType.Class;

            if (ElemIdentify.IsStruct(_line))
                return ElemType.Struct;

            if (ElemIdentify.IsEnum(_line))
                return ElemType.Enum;

            if (ElemIdentify.IsEnumMember(_line))
                return ElemType.EnumMember;

            if (ElemIdentify.IsFunction(_line))
                return ElemType.Function;

            if (ElemIdentify.IsConstMember(_line))
                return ElemType.ConstMember;

            if (ElemIdentify.IsMember(_line))
                return ElemType.Member;

            string msg = "ElementFactory::GetType() Not implemented type. line = " + _line;
            Debug.Assert(false, msg);
            throw new Exception(msg);
        }
    }
}

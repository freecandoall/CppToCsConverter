using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PacketGenerator.Converter.Identify;

namespace PacketGenerator.Converter.Structure
{
    public enum ElemType
    {
        None = -1,

        Root = 0,
        Comment,
        GroupComment,
        ForceComment,
        Include,
        Define,
        NameSpace,
        Class,
        Struct,
        Enum,
        EnumMember,
        Function,
        ConstMember,
        Member,

        CustomGen   // PacketGenerator가 자동생성해주는 타입
    }

    public partial class IElement
    {
        public int indent { get; set; }
        public ElemType elemType { get; private set; }

        public string originData { get; private set; }
        protected List<string> formatData = new List<string>();

        protected IElement parent = null;
        protected LinkedList<IElement> child = new LinkedList<IElement>();
        protected LinkedList<IElement> constList = new LinkedList<IElement>();

        private LinkedList<string> tmpCollecting = new LinkedList<string>();

        public IElement(IElement _parent, ElemType _type, string _data)
        {
            parent      = _parent;
            elemType    = _type;
            originData  = DebugInfo.AddElemType(_data, _type);

            child.Clear();
            formatData.Clear();
        }

        protected virtual void BeginCollecting() { }
        protected virtual void EndCollecting() { }
        protected virtual List<string> Replace()
        {
            formatData.Add(originData + "\t//[FIX ME] " + this.GetType());
            return formatData;
        }

        public void Add(IElement _elem)
        {
            if (IsLostParentConstMember(_elem))
            {
                this.constList.AddLast(_elem);
                return;
            }

            this.child.AddLast(_elem);
        }

        public IElement Collecting()
        {
            BeginCollecting();

            var iter = this.child.GetEnumerator();
            while (iter.MoveNext())
            {
                iter.Current.Collecting();
            }

            EndCollecting();

            return this;
        }

        public List<string> ToList()
        {
            Begin();

            AddConstMembers(this);

            IElement elem = null;

            var iter = this.child.GetEnumerator();
            while (iter.MoveNext())
            {
                elem = iter.Current;

                if (HasChild(elem))
                {
                    AddRange(this, elem.ToList());
                }
                else
                {
                    AddFormatLine(elem.indent, this.tmpCollecting, DataToStringList(elem));
                }
            }

            return End();
        }

        private void Begin()
        {
            this.tmpCollecting.Clear();

            if (this.elemType == ElemType.Root)
            {
                AddFormatLine(indent, this.tmpCollecting, DataToStringList(this));
            }

            if (IsBraceType(this))
            {
                string openChar = this.elemType == ElemType.GroupComment ? "/*" : "{";

                AddFormatLine(indent, this.tmpCollecting, DataToStringList(this));
                AddFormatLine(indent, this.tmpCollecting, openChar);
            }
        }

        private List<string> End()
        {
            if (IsBraceType(this))
            {
                string closeChar = this.elemType == ElemType.GroupComment ? "*/" : "}";
                AddFormatLine(indent, tmpCollecting, closeChar);
            }

            List<string> tmpList = new List<string>();

            return this.tmpCollecting.ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PacketGenerator.Converter.Identify;

namespace PacketGenerator.Converter.Structure
{
    sealed class ElemMember : IElement
    {
        public ElemMember(IElement _parent, string _data) : base(_parent, ElemType.Member, _data) { }

        protected override List<string> Replace()
        {
            string tmp = originData;

            ObjectReplace.Uint(ref tmp);
            ObjectReplace.UShort(ref tmp);
            ObjectReplace.ULong(ref tmp);
            ObjectReplace.String(ref tmp);
            ObjectReplace.StringInitialize(ref tmp);
            ObjectReplace.CharArray(ref tmp);
            ObjectReplace.IntArray(ref tmp);

            formatData.Add(tmp);

            //[PacketAttribute(mSequence = 1, mSize = (int)Adapter.AutoGen_by_Converter.MAX_LEN_PK_ID)]

            return formatData;
        }
    }
}

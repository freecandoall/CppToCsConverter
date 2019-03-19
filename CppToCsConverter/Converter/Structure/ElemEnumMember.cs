using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketGenerator.Converter.Structure
{
    sealed class ElemEnumMember : IElement
    {
        public ElemEnumMember(IElement _parent, string _data) : base(_parent, ElemType.EnumMember, _data) { }

        protected override List<string> Replace()
        {
            formatData.Add(originData);
            return formatData;
        }
    }
}

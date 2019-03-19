using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PacketGenerator.Converter.Identify;

namespace PacketGenerator.Converter.Structure
{
    sealed class ElemEnum : IElement
    {
        public ElemEnum(IElement _parent, string _data) : base(_parent, ElemType.Enum, _data) { }

        protected override List<string> Replace()
        {
            string tmp = originData;

            ObjectReplace.EnumForcePublic(ref tmp);

            formatData.Add(tmp);

            return formatData;
        }
    }
}

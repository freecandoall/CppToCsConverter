using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PacketGenerator.Converter.Identify;

namespace PacketGenerator.Converter.Structure
{
    sealed class ElemConstMember : IElement
    {
        public ElemConstMember(IElement _parent, string _data) : base(_parent, ElemType.ConstMember, _data) { }

        protected override List<string> Replace()
        {
            string tmp = originData;

            ElemReplace.ConstMember(ref tmp);
            ObjectReplace.Uint(ref tmp);

            formatData.Add(tmp);

            return formatData;
        }
    }
}

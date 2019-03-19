using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PacketGenerator.Converter.Identify;

namespace PacketGenerator.Converter.Structure
{
    sealed class ElemForceComment : IElement
    {
        public ElemForceComment(IElement _parent, string _data) : base(_parent, ElemType.ForceComment, _data) { }

        protected override List<string> Replace()
        {
            string tmp = originData;

            ElemReplace.ForceComment(ref tmp);

            formatData.Add(tmp);

            return formatData;
        }
    }
}

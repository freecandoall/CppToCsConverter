using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketGenerator.Converter.Structure
{
    sealed class ElemClass : IElement
    {
        public ElemClass(IElement _parent, string _data) : base(_parent, ElemType.Class, _data) { }

        protected override List<string> Replace()
        {
            formatData.Add(originData);
            return formatData;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketGenerator.Converter.Structure
{
    sealed class ElemFunction : IElement
    {
        public ElemFunction(IElement _parent, string _data) : base(_parent, ElemType.Function, _data) { }

        protected override List<string> Replace()
        {
            formatData.Add(originData);
            return formatData;
        }
    }
}

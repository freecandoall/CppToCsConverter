using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketGenerator.Converter.Structure
{
    class ElemCustomGen : IElement
    {
        public ElemCustomGen(IElement _parent, string _data) : base(_parent, ElemType.CustomGen, _data) { }

        protected override List<string> Replace()
        {
            formatData.Add(originData);

            return formatData;
        }
    }
}

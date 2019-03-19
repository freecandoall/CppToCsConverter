using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PacketGenerator.Converter.Identify;

namespace PacketGenerator.Converter.Structure
{
    sealed class ElemDefine : IElement
    {
        public ElemDefine(IElement _parent, string _data) : base(_parent, ElemType.Define, _data) { }

        protected override List<string> Replace()
        {
            string tmp = originData;

            ElemReplace.DefineConst(ref tmp);

            formatData.Add(tmp);

            return formatData;
        }
    }
}
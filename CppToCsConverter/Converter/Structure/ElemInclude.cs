using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PacketGenerator.Converter.Identify;

namespace PacketGenerator.Converter.Structure
{
    sealed class ElemInclude : IElement
    {
        public ElemInclude(IElement _parent, string _data) : base(_parent, ElemType.Include, _data) { }

        protected override List<string> Replace()
        {
            string tmp = originData;

            if (Link.BrokenLinker.IsPacketInclude(tmp))
            {
                formatData.Add("using Packet;\t" + ElementParser.FORCE_COMMENT);
                formatData.Add("using System.Runtime.InteropServices;");
            }
            else
            {
                ElemReplace.Include(ref tmp);
                ElemReplace.Namespace(ref tmp);

                formatData.Add(tmp);
            }

            return formatData;
        }
    }
}

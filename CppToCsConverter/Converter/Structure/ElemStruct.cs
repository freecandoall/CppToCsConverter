using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PacketGenerator.Converter.Identify;

namespace PacketGenerator.Converter.Structure
{
    sealed class ElemStruct : IElement
    {
        public ElemStruct(IElement _parent, string _data) : base(_parent, ElemType.Struct, _data) { }

        protected override List<string> Replace()
        {
            if (CustomIdentify.IsInheritPacketHeader(originData))
            {
                formatData.Add("[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]");

                string tmp = originData;
                CustomReplace.PacketHeader(ref tmp);

                formatData.Add(tmp);
            }
            else
            {
                formatData.Add(originData);
            }
            
            return formatData;
        }

        protected override void EndCollecting()
        {
            base.EndCollecting();

            if (CustomIdentify.IsInheritPacketHeader(originData))
            {
                int idx = 0;
                int customIndext = this.indent + 1;

                if (child.Count > 0)
                {
                    LinkedListNode<IElement> node = child.First;
                    LinkedListNode<IElement> terminator = null;

                    while (node != null && node != terminator)
                    {
                        if (node.Value.elemType == ElemType.Member)
                        {
                            string desc = string.Format("[PacketAttribute(mSequence = {0})]", idx);
                            IElement customElem = ElementFactory.Create(this, ElemType.CustomGen, desc, customIndext);

                            try
                            {
                                child.AddBefore(node, customElem);
                            }
                            catch (Exception e)
                            {
                                Debug.Assert(false, string.Format("{0}", e));
                            }

                            ++idx;
                        }

                        node = node.Next;
                    }
                }
            }
        }
    }
}

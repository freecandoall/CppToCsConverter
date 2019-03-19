﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketGenerator.Converter.Structure
{
    sealed class ElemRoot : IElement
    {
        public ElemRoot() : base(null, ElemType.Root, "") { }

        protected override List<string> Replace()
        {
            formatData.Add("//////////////////////////////////////////////////////////////////");
            formatData.Add("//");
            formatData.Add("//\tAuto generated by PacketGenerator");
            formatData.Add("//\tDon't fix me!!!");
            formatData.Add("//");
            formatData.Add("//////////////////////////////////////////////////////////////////");

            return formatData;
        }
    }
}

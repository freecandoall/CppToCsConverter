using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketGenerator.Converter
{
    class DebugInfo
    {
        public const bool USE_ELEMENT_TYPE = false;

        public static string AddElemType(string _data, Structure.ElemType _type)
        {
            if (USE_ELEMENT_TYPE)
                return string.Format("{0}\t//[ElemType::{1}]", _data, _type);

            return _data;
        }
    }
}

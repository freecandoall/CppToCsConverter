using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using PacketGenerator.Converter;
using PacketGenerator.Converter.Structure;

namespace PacketGenerator
{
    class CSConvert
    {
        public static CSConvert Parse(StreamReader _sr)
        {
            return new CSConvert(_sr);
        }

        public List<string> Collect()
        {
            return result;
        }


        private List<string> result = new List<string>();

        private CSConvert(StreamReader _sr)
        {
            result.Clear();

            List<string> readList = StreamParser.Parse(_sr);

            ElemRoot elem = ElementParser.Parse(readList);

            result.AddRange(elem.Collecting().ToList());
        }
    }
}

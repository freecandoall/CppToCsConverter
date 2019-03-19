using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketGenerator.Converter.Identify
{
    class CustomReplace
    {
        public static bool PacketHeader(ref string _line)
        {
            if (CustomIdentify.IsInheritPacketHeader(_line))
            {
                _line = _line.Replace(": public Header", ": Header");
                _line = _line.Replace("struct", "class");
                return true;
            }

            return false;
        }

        public static bool UsingPacket(ref string _line)
        {
            string trim = _line.Trim();
            if (trim.Contains("::Packet"))
            {
                _line = "using Packet;";
                return true;
            }

            return false;
        }

        public static bool AddPacketAttribute(List<string> _formatData, string _line)
        {

            return false;
        }
    }
}

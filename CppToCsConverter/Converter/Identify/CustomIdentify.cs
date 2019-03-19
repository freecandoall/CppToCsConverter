using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketGenerator.Converter.Identify
{
    class CustomIdentify
    {
        public static bool IsInheritPacketHeader(string _line)
        {
            if (_line.Contains(":") && _line.Contains("Header"))
                return true;

            return false;
        }

        public static bool IsUsingPacket(string _line)
        {
            if (_line.Contains("::Packet"))
                return true;

            return false;
        }
    }
}

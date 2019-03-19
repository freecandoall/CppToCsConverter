using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PacketGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("\n\n\n\n\n");
                int argLen = args.Length;
                if (argLen > 0)
                {
                    for (int i = 0; i < argLen; ++i)
                    {
                        string[] split = args[i].Split('#');

                        string srcRoot = split[0];
                        string dstRoot = split[1];
                        string srcFile = split[2];

                        string[] extSplit = srcFile.Split('.');
                        if (extSplit[1].ToLower().Equals("h"))
                        {
                            ConvertHeader(srcRoot, dstRoot, extSplit[0]);
                        }
                        else
                        {
                            ConvertCPP(srcRoot, dstRoot, extSplit[0]);
                        }
                    }
                }
                else
                {
                    // Generator 개발용

                    const string srcRoot = "../../../TestConvert/InputCpp/";
                    const string dstRoot = "../../../TestConvert/OutputCs/";

                    ConvertHeader(srcRoot, dstRoot, "Type");
                }
                Console.Write("\n\n\n\n\n");
                Console.Write("[PacketGenerator] succeded\n");
            }
            catch (Exception e)
            {
                Debug.Assert(false);
                Console.Write("\n\n\n\n\n");
                Console.Write("[PacketGenerator] error\n{0}", e);
            }
        }

        private static void ConvertHeader(string _src_root, string _dst_root, string _file_name)
        {
            Convert(_src_root + _file_name + ".h", _dst_root + _file_name + ".cs", _file_name);
        }

        private static void ConvertCPP(string _src_root, string _dst_root, string _file_name)
        {
            Convert(_src_root + _file_name + ".cpp", _dst_root + _file_name + ".cs", _file_name);
        }

        private static void Convert(string _src_cpp, string _dst_cs, string _file_name)
        {
            string desc = "";
            desc += string.Format("src : {0}\n", _src_cpp);
            desc += string.Format("dst : {0}\n", _dst_cs);
            Console.Write(desc);

            using (StreamReader sCpp = new StreamReader(_src_cpp, Encoding.Default))
            {
                //Write(_dst_cs, CppCollector.Parse(_file_name, sCpp).collect);
                Write(_dst_cs, CSConvert.Parse(sCpp).Collect());
            }
        }

        private static void Write(string _dst, List<string> _collect)
        {
            using (StreamWriter sCs = new StreamWriter(_dst, false))
            {
                int count = _collect.Count;
                for (int i = 0; i < count; ++i)
                {
                    sCs.WriteLine(_collect[i]);
                }
            }
        }
    }
}

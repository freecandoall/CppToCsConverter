using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketGenerator.Converter.Link
{
    class BrokenLinker
    {
        public static bool IsPacketInclude(string _line)
        {
            if (_line.Contains("#include") && _line.Contains("Header.h"))
                return true;

            return false;
        }

        public static bool Link(ref string _line)
        {
            if (_line.StartsWith("//"))
                return false;

            if (PacketCode(ref _line))
                return true;

            if (ChildPacketHeader(ref _line))
                return true;

            if (ChildPacketMember(ref _line))
                return true;

            return false;
        }

        private static bool PacketCode(ref string _line)
        {
            if (Replace_PacketCode(ref _line, "Support", "HandShake_Code")) return true;
            if (Replace_PacketCode(ref _line, "Connect", "Connect_Code")) return true;
            if (Replace_PacketCode(ref _line, "Account", "Login_Code")) return true;
            if (Replace_PacketCode(ref _line, "Buddy", "Buddy_Code")) return true;
            if (Replace_PacketCode(ref _line, "Buddy", "Party_Code")) return true;
            if (Replace_PacketCode(ref _line, "Buddy", "Match_Code")) return true;
            if (Replace_PacketCode(ref _line, "Champ", "Champ_Code")) return true;
            if (Replace_PacketCode(ref _line, "Chat", "Chat_Code")) return true;
            if (Replace_PacketCode(ref _line, "InGame", "InGame_Code")) return true;
            if (Replace_PacketCode(ref _line, "InGame", "GiveUp_Code")) return true;
            if (Replace_PacketCode(ref _line, "InGame", "Entity_Code")) return true;
            if (Replace_PacketCode(ref _line, "InGame", "Result_Code")) return true;
            if (Replace_PacketCode(ref _line, "InGame", "Party_Code")) return true;
            if (Replace_PacketCode(ref _line, "InGame", "Match_Code")) return true;
            if (Replace_PacketCode(ref _line, "InGame", "Room_Code")) return true;
            if (Replace_PacketCode(ref _line, "Mail", "Mail_Code")) return true;
            if (Replace_PacketCode(ref _line, "Quest", "Quest_Code")) return true;
            if (Replace_PacketCode(ref _line, "Rank", "Info_Code")) return true;
            if (Replace_PacketCode(ref _line, "Shop", "Shop_Code")) return true;
            if (Replace_PacketCode(ref _line, "Tutorial", "Tutorial_Code")) return true;
            if (Replace_PacketCode(ref _line, "GM", "GM_Code")) return true;
            if (Replace_PacketCode(ref _line, "Server", "Server_Code")) return true;
            if (Replace_PacketCode(ref _line, "GMToolEvent", "GMToolEvent_Code")) return true;
            if (Replace_PacketCode(ref _line, "Cheat", "Cheat_Code")) return true;

            return false;
        }
        private static bool Replace_PacketCode(ref string _line, string _namespace, string _at)
        {
            string at = string.Format("= {0},", _at);

            if (_line.Contains(at))
            {
                //string to = string.Format("= AutoGen_{0}.{1},", _namespace, _at);
                string to = string.Format("= {0}.{1}.{2},", _namespace, ElementParser.AUTO_GEN_CLASS, _at);

                _line = _line.Replace(at, to);

                return true;
            }

            return false;
        }

        private static bool ChildPacketHeader(ref string _line)
        {
            //if (_line.Contains(": Header"))
            //{
            //    _list.Add("[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]");

            //    string tmp = _line.Replace("struct", "class");
            //    tmp = tmp.Replace("private", "public");

            //    if (!tmp.Contains("public"))
            //    {
            //        tmp = "public " + tmp;
            //    }

            //    _list.Add(tmp);

            //    return true;
            //}

            return false;
        }



        private static bool ChildPacketMember(ref string _line)
        {
            if (Replace_ChildPacketMember(ref _line, "Connect", "CN_ClientOpen")) return true;
            if (Replace_ChildPacketMember(ref _line, "Connect", "SN_ClientOpen")) return true;

            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_CreateUser")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_CreateUser")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_AccountInfo")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_AccountInfo")) return true;

            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_LobbyServerInfo")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_LobbyServerInfo")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_Login")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_Login")) return true;

            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_SelectJaraList")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_SelectJaraList_Start")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "NN_SelectJaraList")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_SelectJaraList_End")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_LoginPlatform")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_LoginPlatform")) return true;

            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_AddPlatform")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_AddPlatform")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_PlatformChangeGuest")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_PlatformChangeGuest")) return true;

            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_CreateNickName")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_CreateNickName")) return true;

            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_GameDropOut")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_GameDropOut")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_CancelGameDropOut")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_CancelGameDropOut")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_Booster")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_Booster")) return true;

            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_RankSeasonInfo")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_RankSeasonInfo")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_SelectOptionItem")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_SelectOptionItem")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_SelectBaseChamp")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_SelectBaseChamp")) return true;

            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_RecordList")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_RecordListStart")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "NN_RecordList")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_RecordListEnd")) return true;

            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_ReconnectInfo")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_ReconnectInfo")) return true;

            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_ChangeLogin")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_ChangeLogin")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_PushStateInfo")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_PushStateInfo")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_PushStateUpdate")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_PushStateUpdate")) return true;

            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_ResponseSurvey")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_ResponseSurvey")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_GetSurveyStep")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_GetSurveyStep")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_LimitedChampList")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_LimitedChampList")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_LimitedGameMode")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_LimitedGameMode")) return true;

            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_ComplimentUser")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_ComplimentUser")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SN_ComplimentUser")) return true;

            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_ReportUser")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_ReportUser")) return true;

            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_ComplimentInfo")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_ComplimentInfo")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_FreeSupplyTimeList")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_FreeSupplyTimeList")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_AchieveDayList")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_AchieveDayList")) return true;

            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_AchieveFixList")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_StartAchieveFixList")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "NN_AchieveFixList")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_EndAchieveFixList")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SN_SuccessAchieveList")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_AchieveDayReward")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_AchieveDayReward")) return true;

            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_AchieveFixReward")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_AchieveFixReward")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_ChampPeriodInfo")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_ChampPeriodInfo")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SN_UserChattingBlock")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SN_UserRankBlock")) return true;

            if (Replace_ChildPacketMember(ref _line, "Account", "CQ_MapRecordList")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_MapRecordListStart")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "NN_MapRecordList")) return true;
            if (Replace_ChildPacketMember(ref _line, "Account", "SA_MapRecordListEnd")) return true;

            return false;
        }
        private static bool Replace_ChildPacketMember(ref string _line, string _namespace, string _at)
        {
            string at = string.Format("CODE::{0};", _at);

            if (_line.Contains(at))
            {
                //string to = string.Format("(uint){0}.CODE.E_CODE.{1};", _namespace, _at);
                //_line = _line.Replace(at, to);

                _line = string.Format("public {1}() : base((uint)({0}.CODE.E_CODE.{1})) {{ }}", _namespace, _at);

                return true;
            }

            return false;
        }
    }
}

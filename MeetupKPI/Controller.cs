using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using API;
using System.IO;
using System.Text.RegularExpressions;

namespace MeetupKPI
{
    public class Controller
    {

        // this method just return all groups and each group its related info.
        // header for this return: {GroupName, GroupId, GroupTotalMembers, GroupCreated, GroupUrlName, GroupOrganizerName, GroupDescription, GroupRating}
        public void GetAllGroups()
        {
            var getGroups = new GetGroups();
            List<string[]> AllGroups = getGroups.GetAllGroups("CA", "AB", "Calgary", "100");


            // header for this return: {GroupName, GroupId, GroupTotalMembers, GroupCreated, GroupUrlName, GroupOrganizerName, GroupDescription, GroupRating}

            List<string[]> sanitizedAllGroup = new List<string[]>();

            //replace , to . within array to avoid confusing csv files, there are lots of , in group description
            foreach (string[] oneGroup in AllGroups)
            {
                //remove | since some names contain |
                string[] sa = oneGroup.Select(x => x.Replace("|", " ").Replace(",", ".")).ToArray();

                //remove these.....terrible stuff!
                //, : comma, U+002C
                //LF: Line Feed, U+000A
                //VT: Vertical Tab, U+000B
                //FF: Form Feed, U+000C
                //CR: Carriage Return, U+000D
                //CR + LF: CR(U + 000D) followed by LF(U + 000A)
                //NEL: Next Line, U+0085
                //LS: Line Separator, U+2028
                //PS: Paragraph Separator, U+2029
                //write results to a csv file.
                string str = Regex.Replace(string.Join(",", sa), @"[\u000A\u000B\u000C\u000D\u2028\u2029\u0085]+", String.Empty);

                //string s = Regex.Replace(string.Join("|", oneGroup), "[^a-zA-Z0-9 -@.]", " ");
                //string s = Regex.Replace(string.Join("|", oneGroup), "[^a-zA-Z0-9 -@.]", " ");
                //string s = Regex.Replace(string.Join("|", sa1), @"[\u000A\u000B\u000C\u000D\u2028\u2029\u0085\u002c]+", String.Empty);
                //string s = s.Replace(',', '.');
                //string[] sa = s.Split(',').ToArray();//Select(x => x.Replace(',', '.')).ToArray();

                sanitizedAllGroup.Add(str.Split(',').ToArray());


            }
            File.WriteAllLines("AllGroups.csv", sanitizedAllGroup.Select(x => string.Join(",", x)));
        }

        public void SomeGroupsAndMembers(string[] GroupsId)
        {

            var getMembers = new GetMembers();

            List<string[]> CombinedGroupAndMembers = new List<string[]>();

            foreach (string GroupId in GroupsId)
            {

                ////first get group info then get that group members info
                var getGroup = new GetGroups();
                // grpIdinfo header is: { GroupName, Group_Id, GroupMembers, GroupCreated, GroupUrlName, GroupOrganizer_name, GroupDescription, Group_Total_Events}
                string[] GroupInfo = getGroup.GetGroupInfo(GroupId);


                string[] sanitizedGroupInfo = new string[8];
                //replace , to . within array to avoid confusing csv files, there are lots of , in group description
                //remove | since some names contain |
                string[] sa = GroupInfo.Select(x => x.Replace("|", " ").Replace(",", ".")).ToArray();

                //remove these.....terrible stuff!
                //, : comma, U+002C
                //LF: Line Feed, U+000A
                //VT: Vertical Tab, U+000B
                //FF: Form Feed, U+000C
                //CR: Carriage Return, U+000D
                //CR + LF: CR(U + 000D) followed by LF(U + 000A)
                //NEL: Next Line, U+0085
                //LS: Line Separator, U+2028
                //PS: Paragraph Separator, U+2029
                //write results to a csv file.
                string str = Regex.Replace(string.Join(",", sa), @"[\u000A\u000B\u000C\u000D\u2028\u2029\u0085]+", String.Empty);

                //string s = Regex.Replace(string.Join("|", oneGroup), "[^a-zA-Z0-9 -@.]", " ");
                //string s = Regex.Replace(string.Join("|", oneGroup), "[^a-zA-Z0-9 -@.]", " ");
                //string s = Regex.Replace(string.Join("|", sa1), @"[\u000A\u000B\u000C\u000D\u2028\u2029\u0085\u002c]+", String.Empty);
                //string s = s.Replace(',', '.');
                //string[] sa = s.Split(',').ToArray();//Select(x => x.Replace(',', '.')).ToArray();

                sanitizedGroupInfo = str.Split(',').ToArray();

                File.AppendAllText("Groups.csv", string.Join(",", sanitizedGroupInfo) + Environment.NewLine);


                ////replace , to . within array to avoid confusing csv files, there are lots of , in group description
                //GroupInfo = GroupInfo.Select(x => x.Replace(",", ".").Trim('\r', '\n')).ToArray();
                ////GroupInfo = GroupInfo.Select(x => x.Replace(",", ".").Trim()).ToArray();


                //// this is just for debug
                ////Console.WriteLine(string.Join(",", GroupInfo));

                ////LF: Line Feed, U+000A
                ////VT: Vertical Tab, U+000B
                ////FF: Form Feed, U+000C
                ////CR: Carriage Return, U+000D
                ////CR + LF: CR(U + 000D) followed by LF(U + 000A)
                ////NEL: Next Line, U+0085
                ////LS: Line Separator, U+2028
                ////PS: Paragraph Separator, U+2029
                ////write results to a csv file.
                //File.AppendAllText("Group.csv", Regex.Replace(string.Join(",", GroupInfo), @"[\u000A\u000B\u000C\u000D\u2028\u2029\u0085]+", String.Empty));


                //get all members info for this item(group id)
                List<string[]> grpMembersInfo = getMembers.GetMembersDetail(GroupId);

                foreach (var member in grpMembersInfo)
                {

                    string[] tmp = new string[14];

                    GroupInfo.CopyTo(tmp, 0);
                    member.CopyTo(tmp, GroupInfo.Length);

                    CombinedGroupAndMembers.Add(tmp);

                }
            }

        }

        public void GetEveryGroupWithMembers()
        {
            var getGroups = new GetGroups();
            Logger.Log(DateTime.Now.ToString("yyyy-M-d HH:mm:ss") + " getGroups.GetAllGroups...");
            List<string[]> AllGroups = getGroups.GetAllGroups("CA", "AB", "Calgary", "100");

            // header for this return: {GroupName, GroupId, GroupTotalMembers, GroupCreated, GroupUrlName, GroupOrganizerName, GroupDescription, GroupEvents}
            List<string[]> sanitizedAllGroup = new List<string[]>();

            //inserting header
            //sanitizedAllGroup.
            //replace , to . within array to avoid confusing csv files, there are lots of , in group description
            foreach (string[] oneGroup in AllGroups)
            {
                //remove | since some names contain |
                string[] sa = oneGroup.Select(x => x.Replace("|", " ").Replace(",", ".")).ToArray();

                //remove these.....terrible stuff!
                //, : comma, U+002C
                //LF: Line Feed, U+000A
                //VT: Vertical Tab, U+000B
                //FF: Form Feed, U+000C
                //CR: Carriage Return, U+000D
                //CR + LF: CR(U + 000D) followed by LF(U + 000A)
                //NEL: Next Line, U+0085
                //LS: Line Separator, U+2028
                //PS: Paragraph Separator, U+2029
                //write results to a csv file.
                string str = Regex.Replace(string.Join(",", sa), @"[\u000A\u000B\u000C\u000D\u2028\u2029\u0085]+", String.Empty);

                //string s = Regex.Replace(string.Join("|", oneGroup), "[^a-zA-Z0-9 -@.]", " ");
                //string s = Regex.Replace(string.Join("|", oneGroup), "[^a-zA-Z0-9 -@.]", " ");
                //string s = Regex.Replace(string.Join("|", sa1), @"[\u000A\u000B\u000C\u000D\u2028\u2029\u0085\u002c]+", String.Empty);
                //string s = s.Replace(',', '.');
                //string[] sa = s.Split(',').ToArray();//Select(x => x.Replace(',', '.')).ToArray();

                sanitizedAllGroup.Add(str.Split(',').ToArray());
                Logger.Log(DateTime.Now.ToString("yyyy-M-d HH:mm:ss") + " Group "+sa[1].ToString() + " logged in sanitizedAllGroup...");
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //getting members for groups //////////////////////////////////////////////////////////////////////////////////
            var getMembers = new GetMembers();

            List<string[]> CombinedGroupAndMembers = new List<string[]>();
            int currentGroup = 1;
            foreach (string[] GroupInfo in sanitizedAllGroup)
            {
                string[] s = GroupInfo;
                string GroupId = s[1];
                ////first get group info then get that group members info
                var getGroup = new GetGroups();
                // grpIdinfo header is: { GroupName, Group_Id, GroupMembers, GroupCreated, GroupUrlName, GroupOrganizer_name, GroupDescription, Group_Total_Events}
                //= string[] GroupInfo = getGroup.GetGroupInfo(GroupId);


                //string[] sanitizedGroupInfo = new string[8];
                //replace , to . within array to avoid confusing csv files, there are lots of , in group description
                //remove | since some names contain |
                //string[] sa = GroupInfo.Select(x => x.Replace("|", " ").Replace(",", ".")).ToArray();
                //string str = Regex.Replace(string.Join(",", sa), @"[\u000A\u000B\u000C\u000D\u2028\u2029\u0085]+", String.Empty);
                //sanitizedGroupInfo = str.Split(',').ToArray();
                //File.AppendAllText("Groups.csv", string.Join(",", sanitizedGroupInfo) + Environment.NewLine);
                //get all members info for this item(group id)
                List<string[]> getMembersDetail = getMembers.GetMembersDetail(GroupId);
                int currentMember = 1;
                foreach (var member in getMembersDetail)
                {
                    
                    string[] tmp = new string[14];
                    GroupInfo.CopyTo(tmp, 0);
                    member.CopyTo(tmp, GroupInfo.Length);
                    CombinedGroupAndMembers.Add(tmp);
                    Logger.Log(DateTime.Now.ToString("yyyy-M-d HH:mm:ss") + "--> Member: " +currentMember + " of " + getMembersDetail.Count() +" from group: "+ currentGroup + " of " + sanitizedAllGroup.Count() + " logged in CombinedGroupAndMembers...");
                    currentMember++;
                }
                currentGroup++;
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            CombinedGroupAndMembers.Insert(0,new List<string> { "GroupName, GroupId, GroupTotalMembers, GroupCreated, GroupUrlName, GroupOrganizerName, GroupDescription, GroupEvents", "MemberName", "MemberId", "MemberJoined", "MemberStatus", "MemberTopics", "MemeberVisited" }.ToArray());
            File.WriteAllLines("GetEveryGroupWithMembers.csv", CombinedGroupAndMembers.Select(x => string.Join(",", x) /*string.Join(",", CombinedGroupAndMembers) + Environment.NewLine*/));
            
        }
    }
}

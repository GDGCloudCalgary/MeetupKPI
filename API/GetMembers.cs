using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace API
{
    public class GetMembers
    {
        public void GetMembersDetail()
        {

        }

        public List<string[]> GetMembersDetail(string GroupsId)
        {
            // get total number of time we need to send requests to the Meetup API server.
            // we have to split request in pages of 200 responses, meetup policy 
            var url0 = new Uri("https://api.meetup.com/2/members?key=" + WebGen.Meetup_API_Key + "&format=json&sign=true" + "&group_id=" + GroupsId);
            string jsonString0 = WebGen.GetPageAsString(url0);
            var rootObject0 = JsonConvert.DeserializeObject<api2members.Rootobject>(jsonString0);

            int GroupMembers = rootObject0.meta.total_count;
            //if (GroupMembers > 5)
            //{
            //    GroupMembers = 5;
            //}
            int pageSize = 200;
            int totalRequest = (GroupMembers / pageSize) + 1;
            
            // list to keep group memebers information
            List<string[]> JsonDS = new List<string[]>();

            for (int i = 0; i < totalRequest; i++)
            {
                var url = new Uri("https://api.meetup.com/2/members?key=" + WebGen.Meetup_API_Key + "&format=json&sign=true" + "&group_id=" + GroupsId + "&offset=" + i.ToString());
                string jsonString = WebGen.GetPageAsString(url);
                var rootObject = JsonConvert.DeserializeObject<api2members.Rootobject>(jsonString);

                foreach (var result in rootObject.results)
                {
                    string MemberName = result.name ?? "Is null";
                    string MemberId = result.id.ToString();
                    string MemberJoined = DateTimeOffset.FromUnixTimeMilliseconds(result.joined).ToString("yyyy-MM-dd");
                    string MemberStatus = result.status.ToString();
                    string MemberTopics = result.topics.Length.ToString();
                    string MemeberVisited = DateTimeOffset.FromUnixTimeMilliseconds(result.visited).ToString("yyyy-MM-dd");

                     
                    Regex rgx = new Regex("[^a-zA-Z0-9 -]");

                    JsonDS.Add(new string[] { rgx.Replace(MemberName, ""), MemberId, MemberJoined, MemberStatus, MemberTopics, MemeberVisited });
                }
            }

            return JsonDS;
        }

    }
}

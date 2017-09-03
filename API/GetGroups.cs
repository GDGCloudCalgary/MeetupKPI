using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Threading.Tasks;
using System.Collections;




namespace API
{
    public class GetGroups
    {
        public void GetGroupsId()
        {

        }


        public string[] GetGroupInfo(string GroupId)
        {
            // get one group info
            var url0 = new Uri("https://api.meetup.com/2/groups?key=" + WebGen.Meetup_API_Key + "&format=json&sign=true" + "&country=Ca" + "&city=Calgary" + "&state=AB" + "&radius=25" + "&page=20" + "&group_id=" + GroupId);
            string jsonString0 = WebGen.GetPageAsString(url0);
            var rootObject0 = JsonConvert.DeserializeObject<api2OneGroupInfo.Rootobject>(jsonString0);
            var result = rootObject0.results;

            // list to keep group name and info
            string[] JsonDS = new string[8];

            string GroupName = result[0].name;
            string GroupOrganizer_name = result[0].organizer.name;
            string GroupDescription = result[0].description;
            string GroupTotalEvents = "";
            string Group_Id = result[0].id.ToString();
            string GroupMembers = result[0].members.ToString();
            string GroupCreated = DateTimeOffset.FromUnixTimeMilliseconds(result[0].created).ToString("yyyy-MM-dd");
            string GroupUrlName = result[0].urlname.ToString();

            // get total number of events for this group
            var url1 = new Uri("https://api.meetup.com/2/events?key=" + WebGen.Meetup_API_Key + "&format=json&sign=true" + "&group_id=" + Group_Id);
            string jsonString1 = WebGen.GetPageAsString(url1);
            var rootObject1 = JsonConvert.DeserializeObject<api2groupEvents.Rootobject>(jsonString1);

            //GroupTotalEvents = result.topics.Count().ToString();

            JsonDS = new string[] { GroupName, Group_Id, GroupMembers, GroupCreated, GroupUrlName, GroupOrganizer_name, GroupDescription, GroupTotalEvents };

            return JsonDS;
        }

        public List<string[]> GetAllGroups(string Country, string Province, string City, string Radius)
        {

            // get total number of counts
            var url0 = new Uri("https://api.meetup.com/2/groups?key=" + WebGen.Meetup_API_Key + "&format=json&sign=true" + "&country=" + Country + "&city=" + City + "&state=" + Province + "&radius=25");
            string jsonString0 = WebGen.GetPageAsString(url0);
            var rootObject0 = JsonConvert.DeserializeObject<api2group.Rootobject>(jsonString0);

            int totalGroups = rootObject0.meta.total_count;
           
            int pageSize = 200;
            int totalRequest = (totalGroups / pageSize) + 1;
           

            // list to keep group name info
            List<string[]> JsonDS = new List<string[]>();

            for (int i = 0; i < totalRequest; i++)
            {
                var url = new Uri("https://api.meetup.com/2/groups?key=" + WebGen.Meetup_API_Key + "&format=json&sign=true" + "&country=" + Country + "&city=" + City + "&state=" + Province + "&radius=25" + "&page=200" + "&offset=" + i.ToString());
                string jsonString = WebGen.GetPageAsString(url);
                var rootObject = JsonConvert.DeserializeObject<api2group.Rootobject>(jsonString);

                foreach (var result in rootObject.results)
                {

                    string GroupOrganizerName = "unknown";
                    string GroupDescription = "unknown";
                    string GroupTotalEvents = "unknown";
                    string GroupName = result.name;
                    string GroupId = result.id.ToString();
                    string GroupTotalMembers = result.members.ToString();
                    string GroupCreated = DateTimeOffset.FromUnixTimeMilliseconds(result.created).ToString("yyyy-MM-dd");
                    string GroupUrlName = result.urlname.ToString();


                    if (result.organizer != null)
                    {
                        GroupOrganizerName = result.organizer.name.ToString();
                    }
                    else
                    {
                        GroupOrganizerName = "unknown or not provided";
                    }

                    if (result.description != null)
                    {
                        GroupDescription = result.description.ToString();
                    }
                    else
                    {
                        GroupDescription = "unknown or not provided";
                    }

                    GroupTotalEvents = result.topics.Count().ToString();

                    Regex rgx = new Regex("[^a-zA-Z0-9 -]");

                    
                    JsonDS.Add(new string[] { rgx.Replace(GroupName, ""), GroupId, GroupTotalMembers, GroupCreated, GroupUrlName, GroupOrganizerName, rgx.Replace(GroupDescription, ""), GroupTotalEvents });
                }
            }

            return JsonDS;
        }


    }
}

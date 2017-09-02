using System;
using System.Collections.Generic;
using System.Text;

namespace api2groupEvents
{
    class api2groupEvents
    {
    }


    public class Rootobject
    {
        public Result[] results { get; set; }
        public Meta meta { get; set; }
    }

    public class Meta
    {
        public string next { get; set; }
        public string method { get; set; }
        public int total_count { get; set; }
        public string link { get; set; }
        public int count { get; set; }
        public string description { get; set; }
        public string lon { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string id { get; set; }
        public long updated { get; set; }
        public string lat { get; set; }
    }

    public class Result
    {
        public int utc_offset { get; set; }
        public Venue venue { get; set; }
        public int headcount { get; set; }
        public string visibility { get; set; }
        public int waitlist_count { get; set; }
        public long created { get; set; }
        public int maybe_rsvp_count { get; set; }
        public string description { get; set; }
        public string event_url { get; set; }
        public int yes_rsvp_count { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public long time { get; set; }
        public long updated { get; set; }
        public Group group { get; set; }
        public string status { get; set; }
    }

    public class Venue
    {
        public string country { get; set; }
        public string localized_country_name { get; set; }
        public string city { get; set; }
        public string address_1 { get; set; }
        public string name { get; set; }
        public float lon { get; set; }
        public int id { get; set; }
        public string state { get; set; }
        public float lat { get; set; }
        public bool repinned { get; set; }
    }

    public class Group
    {
        public string join_mode { get; set; }
        public long created { get; set; }
        public string name { get; set; }
        public float group_lon { get; set; }
        public int id { get; set; }
        public string urlname { get; set; }
        public float group_lat { get; set; }
        public string who { get; set; }
    }

}

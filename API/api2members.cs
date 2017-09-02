using System;
using System.Collections.Generic;
using System.Text;

namespace api2members
{
    class api2members
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
        public string signed_url { get; set; }
        public string id { get; set; }
        public long updated { get; set; }
        public string lat { get; set; }
    }

    public class Result
    {
        public string country { get; set; }
        public string city { get; set; }
        public Topic[] topics { get; set; }
        public long joined { get; set; }
        public string link { get; set; }
        public Photo photo { get; set; }
        public float lon { get; set; }
        public Other_Services other_services { get; set; }
        public string name { get; set; }
        public long visited { get; set; }
        public Self self { get; set; }
        public int id { get; set; }
        public string state { get; set; }
        public float lat { get; set; }
        public string status { get; set; }
        public string hometown { get; set; }
        public string bio { get; set; }
    }

    public class Photo
    {
        public string highres_link { get; set; }
        public int photo_id { get; set; }
        public string base_url { get; set; }
        public string type { get; set; }
        public string photo_link { get; set; }
        public string thumb_link { get; set; }
    }

    public class Other_Services
    {
        public Facebook facebook { get; set; }
        public Twitter twitter { get; set; }
    }

    public class Facebook
    {
        public string identifier { get; set; }
    }

    public class Twitter
    {
        public string identifier { get; set; }
    }

    public class Self
    {
        public Common common { get; set; }
    }

    public class Common
    {
    }

    public class Topic
    {
        public string urlkey { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }


}

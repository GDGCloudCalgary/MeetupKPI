using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MeetupKPI
{
    class Program
    {



        static void Main(string[] args)
        {
            
            Logger.Log(DateTime.Now.ToString("yyyy-M-d HH:mm:ss") + " Starting...");
            //////////////////////////////////////////////////////////////////////////////////
            Controller controller = new Controller();
            //controller.SomeGroupsAndMembers(new string[] { "22102008", "23333900", "328955","450603" });
            //controller.GetAllGroups();
            controller.GetEveryGroupWithMembers();
        }

      


    }

  


}
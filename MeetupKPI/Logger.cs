using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupKPI
{
    class Logger
    {
        static string LogFile;  //log file path for debug info
        static string fileNamePrefix = null;
        static System.IO.StreamWriter swLogFile = null;
        public static void Log(string logMessage)
        {
            ///////////////////////////////////////////////////////////////////////////////////
            fileNamePrefix = string.Format("Statistics" + "-{0:yyyy-M-d}", DateTime.Now);
            LogFile = @".\" + fileNamePrefix + ".log"; //log file path for all debug info
            /// create log files if needed
            /// 
            if (!File.Exists(LogFile))
            {
                try
                {
                    using (swLogFile = File.CreateText(LogFile))
                    {
                        swLogFile.WriteLine(System.DateTime.Now.ToString("yyyy-M-d HH:mm:ss") + " Opening new log file...");
                        swLogFile.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error creating LogFile file: " + ex.Message);
                }
            }

            using (StreamWriter sw = File.AppendText(LogFile))
            {
                sw.WriteLine(logMessage);
                sw.Close();
            }
        }
    }
}

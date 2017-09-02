using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Linq;


namespace API
{
    class WebGen
    {
        public const string Meetup_API_Key = "MEETUP_API_KEY";

        public static string GetPageAsString(Uri address)
        {
            string Result = null;
            string RateLimit = "";

            // check if meetup api key is set
            
            Debug.Assert(Meetup_API_Key != "MEETUP_API_KEY", "Edit WebGen.cs and replace MEETUP_API_KEY with your Meetup API Key.");



            while (Result == null)
            {
                try
                {
                    var request = WebRequest.Create(address) as HttpWebRequest;
                    using (var response = request.GetResponseAsync().Result)
                    {
                        var reader = new StreamReader(response.GetResponseStream());
                        Result = reader.ReadToEnd();
                        RateLimit = response.Headers["X-RateLimit-Remaining"];
                        if (Convert.ToInt16(RateLimit) < 10)
                        {
                            Console.WriteLine("X-RateLimit-Limit: " + response.Headers["X-RateLimit-Limit"] +
                           ", " + "X-RateLimit-Remaining: " + response.Headers["X-RateLimit-Remaining"] +
                           ", " + "X-RateLimit-Reset: " + response.Headers["X-RateLimit-Reset"]);
                        }
                    }

                }
                catch (WebException we)
                {
                    Console.WriteLine(we.Message);
                    Console.WriteLine("Exception, sonoozing for 15 seconds...");
                    System.Threading.Thread.Sleep(15000);
                    using (var stream = we.Response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        Console.WriteLine(reader.ReadToEnd());
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something more serious happened, like for example you don't have network access we cannot talk about a server exception here as the server probably was never reached");
                    Console.WriteLine(ex.Message);
                }

                finally
                {

                }

                //if (Convert.ToInt16(RateLimit) < 2)
                //{
                //    RateLimit = "Ratelimit Reached";

                //    Console.WriteLine("Ratelimit Reached, sonoozing for 15 seconds...");
                //    System.Threading.Thread.Sleep(15000);
                //}
                if(Result == null)
                {
                    Console.WriteLine("WebGet Result is Null, retrying in 15 seconds....");
                    System.Threading.Thread.Sleep(15000);
                }
            }
            if (Result == "")
            { int x= 0; }

            return Result;
        }
    }
}

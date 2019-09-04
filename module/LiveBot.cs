using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace liveBot.module
{
    
    public class LiveBot
    {
        public static JObject jsonParse;
        public static RestClient rClient = new RestClient("https://graph.facebook.com/v4.0");
        public static LiveVideoResult result;

        public static void Run()
        {
            Console.WriteLine("fbChatbot running\n");
            var curPath = Directory.GetCurrentDirectory();
            if (!File.Exists(curPath + "/config.json"))
            {
                Console.WriteLine("[ERROR] config.json File Not Found!");
                Console.ReadKey();
                Environment.Exit(1);
            }
            else
            {
                Console.WriteLine("[INFO] config.json Found!");
            }
            StreamReader sr = new StreamReader("config.json");
            jsonParse = (JObject)JsonConvert.DeserializeObject(sr.ReadToEnd());
            Console.WriteLine("GET USER STREAM ID VIDEO ...");
            String url = jsonParse.GetValue("userID").ToString() + "/live_videos";

            RestRequest rNewStream = new RestRequest(url, Method.GET);
            //Steam Info
            rNewStream.AddParameter("broadcast_status", jsonParse.GetValue("status").ToString());
            rNewStream.AddParameter("access_token", jsonParse.GetValue("token").ToString());
            
            var tempResult = rClient.Execute(rNewStream).Content;
            // Console.WriteLine(tempResult);
            result = JsonConvert.DeserializeObject<LiveVideoResult>(tempResult);

            foreach (StreamVideo video in result.data)
            {
                Console.WriteLine("[INFO] Stream Request Success!\nStream URL : rtmps://live-api-s.facebook.com:443/rtmp/");
                Console.WriteLine("Stream Id : " + video.id);
                Console.WriteLine("Stream Key : " + video.stream_url);
            }
            Console.WriteLine("[INFO]Attempting to open stream on another thread");
            Task.Run(() => SeverSentClient.Run(getRequestURL()));
            Console.WriteLine("[INFO]Sever current run on http://localhost:5000/");
        }
        public static string getRequestURL()
        {
            var urlResult = "https://streaming-graph.facebook.com/" + result.data.First().id + "/live_comments?" +
            "access_token=" + jsonParse.GetValue("token").ToString() + "&" +
             "comment_rate=ten_per_second&" +
             "fields=from{name,id},message";
            return urlResult;
        }

        
    }
}
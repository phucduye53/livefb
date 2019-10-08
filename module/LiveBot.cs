using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using liveBot.EntityFramework.models;
using liveBot.Repository;
using livefb.EntityFramework.models;
using livefb.Repository;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace liveBot.module
{

    public class LiveBot
    {
        private readonly IUnitOfWork _uow;
        public LiveBot(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public static JObject jsonParse;
        public static RestClient rClient = new RestClient("https://graph.facebook.com/v4.0");
        public static LiveVideoResult result;

        public static StreamSesson sesson;

        public void Run()
        {
            //START READ CONFIG
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
            //READ CONFIG END

            
            Console.WriteLine("GET USER STREAM ID VIDEO ...");
            String url = jsonParse.GetValue("userID").ToString() + "/live_videos";

            RestRequest rNewStream = new RestRequest(url, Method.GET);
            //Stream Info
            rNewStream.AddParameter("broadcast_status", jsonParse.GetValue("status").ToString());
            rNewStream.AddParameter("access_token", jsonParse.GetValue("token").ToString());

            var tempResult = rClient.Execute(rNewStream).Content;
            // Console.WriteLine(tempResult);
            result = JsonConvert.DeserializeObject<LiveVideoResult>(tempResult);

            //GET STREAM SESSON
            sesson = StreamInfo(result.data);

            DisplayStreamInfo(sesson);

            Console.WriteLine("[INFO]Attempting to initiate a Server-Sent Events subscription");

            var serverSentClinet = new SeverSentClient();
            Task.Run(() => serverSentClinet.Run(getRequestURL(), _uow, sesson));
            
            Console.WriteLine("[INFO]Sever update live video comments run on http://localhost:5000/");
        }
        public static string getRequestURL()
        {
            var urlResult = "https://streaming-graph.facebook.com/" + result.data.First().id + "/live_comments?" +
            "access_token=" + jsonParse.GetValue("token").ToString() + "&" +
             "comment_rate=ten_per_second&" +
             "fields=from{name,id},message";
            return urlResult;
        }

        public StreamSesson StreamInfo(StreamVideo[] arr)
        {
            foreach (StreamVideo video in arr)
            {
                var sesson = _uow.StreamSessonReporitory.Get(p => p.StreamId == video.id);
                if (sesson == null)
                {
                    var newSesson = new StreamSesson(video);
                    _uow.StreamSessonReporitory.Add(newSesson);
                    _uow.SaveChanges();
                    return newSesson;
                }
                else
                {
                    return sesson;
                }

            }
            return null;
        }
        
      


        public static void DisplayStreamInfo(StreamSesson sesson)
        {
            Console.WriteLine("[INFO] Stream Request Success!\nStream URL : rtmps://live-api-s.facebook.com:443/rtmp/");
            Console.WriteLine("Stream sesson Id : " + sesson.StreamId);
            Console.WriteLine("Stream sesson Key : " + sesson.StreamUrl);
        }


        


    }
}
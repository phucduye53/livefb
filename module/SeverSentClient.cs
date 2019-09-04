using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;

namespace liveBot.module
{
    public class SeverSentClient
    {
        public static void Run(string url)
        {
            using (var client = new HttpClient())
            {
                using (var stream = client.GetStreamAsync(url).Result)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        string result;
                        while (true)
                        {
                            result = reader.ReadLine();
                            if (result != string.Empty && result != null)
                            {
                                if (result != ": ping")
                                {
                                    result = "{" + result + "}";
                                    var commentResult = JsonConvert.DeserializeObject<commentResult>(result);
                                    Console.WriteLine(commentResult.data.from.name + ":" + commentResult.data.message);
                                }
                            }





                        }
                    }
                }
            }
        }
    }
}
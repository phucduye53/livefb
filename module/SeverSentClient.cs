using System;
using System.IO;
using System.Net.Http;
using liveBot.EntityFramework.models;
using livefb.Repository;
using Newtonsoft.Json;

namespace liveBot.module
{
    public class SeverSentClient
    {
        public static void Run(string url,IUnitOfWork _uow)
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
                                    var user = new User(commentResult);
                                    _uow.UserRepository.Add(user);
                                    _uow.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
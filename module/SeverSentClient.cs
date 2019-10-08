using System;
using System.IO;
using System.Net.Http;
using liveBot.EntityFramework.models;
using livefb.EntityFramework.models;
using livefb.Repository;
using Newtonsoft.Json;

namespace liveBot.module
{
    public class SeverSentClient
    {
        public void Run(string url,IUnitOfWork _uow,StreamSesson sesson)
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
                                    var user = CheckUser(_uow,commentResult);
                                    // WILL CHECK USER BEFORE ADD COMMENT
                                    var comment = AddComment(_uow,commentResult,user,sesson);
                                }
                            }
                        }
                    }
                }
            }
        }
        
        public User CheckUser(IUnitOfWork _uow, commentResult comment)
        {

            var user = _uow.UserRepository.Get(p => p.FacebookId == comment.data.id);
            if (user == null)
            {
                var newUser = new User(comment);
                _uow.UserRepository.Add(newUser);
                _uow.SaveChanges();
                return newUser;
            }
            else
            {
                return user;
            }

        }
        public Comment AddComment(IUnitOfWork _uow, commentResult comment,User user,StreamSesson sesson)
        {
            var newComment = new Comment(comment.data,user,sesson);
            _uow.CommentRepository.Add(newComment);
            _uow.SaveChanges();
            return newComment;


        }

      
    }
}
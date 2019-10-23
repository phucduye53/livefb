using liveBot.module;
using livefb.EntityFramework.models;
using livefb.Repository;

namespace livefb.Services.StreamSessons
{
    public class StreamSessonService : IStreamSessonService
    {
       private readonly IUnitOfWork unitOfWork;
       public StreamSessonService(IUnitOfWork unitOfWork)
       {
           this.unitOfWork = unitOfWork;
       }

        public StreamSesson GetStreamData(StreamVideo[] arr)
        {
            throw new System.NotImplementedException();
        }

        private bool CheckStreamId(string id)
        {
            var result = unitOfWork.StreamSessonReporitory.Get(p=>p.StreamId==id);
            if(result != null )
            {
                return true;
            }else{
                return false;
            }
        }
    }
}
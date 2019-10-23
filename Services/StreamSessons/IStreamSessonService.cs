using liveBot.module;
using livefb.EntityFramework.models;

namespace livefb.Services.StreamSessons
{
    public interface IStreamSessonService
    {
        StreamSesson GetStreamData(StreamVideo[] arr);
    }
}
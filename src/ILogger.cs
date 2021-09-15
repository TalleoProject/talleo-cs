using System;

namespace Logging
{

    public enum Level
    {
        FATAL = 0,
        ERROR = 1,
        WARNING = 2,
        INFO = 3,
        DEBUGGING = 4,
        TRACE = 5
    };

    public interface ILogger : IWrapper
    {
        void Log(in string category, in Level level, in long time, in string body);
    };
}

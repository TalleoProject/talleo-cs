using System;
using System.Runtime.InteropServices;

class UnmanagedStreamLogger
{
    [DllImport("TalleoWrapper")] public static extern IntPtr StreamLogger_Create();
    [DllImport("TalleoWrapper")] public static extern IntPtr StreamLogger_CreateWithLevel(Logging.Level level);
    [DllImport("TalleoWrapper")] public static extern IntPtr StreamLogger_CreateWithStream(IntPtr stream);
    [DllImport("TalleoWrapper")] public static extern IntPtr StreamLogger_CreateWithStreamLevel(IntPtr stream, Logging.Level level);
    [DllImport("TalleoWrapper")] public static extern void StreamLogger_Destroy(IntPtr logger);
    [DllImport("TalleoWrapper")] public static extern void StreamLogger_attachToStream(IntPtr logger, IntPtr stream);

}


namespace Logging
{
    class StreamLogger : CommonLogger
    {
        public StreamLogger()
        {
            wrappedClass = UnmanagedStreamLogger.StreamLogger_Create();
        }
        public StreamLogger(Level level)
        {
            wrappedClass = UnmanagedStreamLogger.StreamLogger_CreateWithLevel(level);
        }

        public StreamLogger(Talleo.OutputStream stream)
        {
            wrappedClass = UnmanagedStreamLogger.StreamLogger_CreateWithStream(stream.unwrap());
        }
        public StreamLogger(Talleo.OutputStream stream, Level level)
        {
            wrappedClass = UnmanagedStreamLogger.StreamLogger_CreateWithStreamLevel(stream.unwrap(), level);
        }

        protected StreamLogger(IntPtr logger)
        {
            wrappedClass = logger;
        }

        ~StreamLogger()
        {
            UnmanagedStreamLogger.StreamLogger_Destroy(wrappedClass);
        }
        public void attachToStream(Talleo.OutputStream stream)
        {
            UnmanagedStreamLogger.StreamLogger_attachToStream(wrappedClass, stream.unwrap());
        }
        // IWrapper
        public static new StreamLogger wrap(IntPtr logger)
        {
            return new StreamLogger(logger);
        }
    }
}
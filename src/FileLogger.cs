using System;
using System.Runtime.InteropServices;

class UnmanagedFileLogger
{
    [DllImport("TalleoWrapper")] public static extern IntPtr FileLogger_Create();

    [DllImport("TalleoWrapper")] public static extern IntPtr FileLogger_CreateWithLevel(Logging.Level level);

    [DllImport("TalleoWrapper")] public static extern void FileLogger_Destroy(IntPtr logger);

    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)] public static extern void FileLogger_init(IntPtr logger, in string filename);
}
namespace Logging
{
    class FileLogger : StreamLogger
    {
        public FileLogger()
        {
            wrappedClass = UnmanagedFileLogger.FileLogger_Create();
        }
        public FileLogger(Level level)
        {
            wrappedClass = UnmanagedFileLogger.FileLogger_CreateWithLevel(level);
        }

        protected FileLogger(IntPtr logger)
        {
            wrappedClass = logger;
        }
        ~FileLogger()
        {
            UnmanagedFileLogger.FileLogger_Destroy(wrappedClass);
        }
        public void init(in string filename)
        {
            UnmanagedFileLogger.FileLogger_init(wrappedClass, filename);
        }
        // IWrapper
        public static new FileLogger wrap(IntPtr logger)
        {
            return new FileLogger(logger);
        }
    }
}
using System;
using System.Runtime.InteropServices;

class UnmanagedCommonLogger
{
    [DllImport("TalleoWrapper")] public static extern void CommonLogger_Destroy(IntPtr logger);

    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)] public static extern void CommonLogger_Log(IntPtr logger, in string category, in Logging.Level level, in long time, in string body);

    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)] public static extern void CommonLogger_enableCategory(IntPtr logger, in string category);

    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)] public static extern void CommonLogger_disableCategory(IntPtr logger, in string category);

    [DllImport("TalleoWrapper")] public static extern void CommonLogger_setMaxLevel(IntPtr logger, in Logging.Level level);

    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)] public static extern void CommonLogger_setPattern(IntPtr logger, in string pattern);
}

namespace Logging
{
    class CommonLogger : ILogger
    {
        protected IntPtr wrappedClass;

        protected CommonLogger()
        {

        }
        protected CommonLogger(IntPtr logger)
        {
            wrappedClass = logger;
        }

        ~CommonLogger()
        {
            UnmanagedCommonLogger.CommonLogger_Destroy(wrappedClass);
        }

        public void Log(in string category, in Level level, in long time, in string body)
        {
            UnmanagedCommonLogger.CommonLogger_Log(wrappedClass, category, level, time, body);
        }
        public void enableCategory(in string category)
        {
            UnmanagedCommonLogger.CommonLogger_enableCategory(wrappedClass, category);
        }
        public void disableCategory(in string category)
        {
            UnmanagedCommonLogger.CommonLogger_disableCategory(wrappedClass, category);
        }
        public void setMaxLevel(Level level)
        {
            UnmanagedCommonLogger.CommonLogger_setMaxLevel(wrappedClass, level);
        }

        public void setPattern(in string pattern)
        {
            UnmanagedCommonLogger.CommonLogger_setPattern(wrappedClass, pattern);
        }
        // IWrapper
        public virtual IntPtr unwrap()
        {
            return wrappedClass;
        }

        public static CommonLogger wrap(IntPtr logger)
        {
            return new CommonLogger(logger);
        }
    }
}

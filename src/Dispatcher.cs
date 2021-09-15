using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

class UnmanagedDispatcher
{
    /* Constructor */
    [DllImport("TalleoWrapper")]
    public static extern IntPtr Dispatcher_Create();

    /* Destructor */
    [DllImport("TalleoWrapper")]
    public static extern void Dispatcher_Destroy(IntPtr dispatcher);

    /* Class methods */
    [DllImport("TalleoWrapper")]
    public static extern void Dispatcher_clear(IntPtr dispatcher);
    [DllImport("TalleoWrapper")]
    public static extern void Dispatcher_dispatch(IntPtr dispatcher);
    [DllImport("TalleoWrapper")]
    public static extern IntPtr Dispatcher_getCurrentContext(IntPtr dispatcher);
    [DllImport("TalleoWrapper")]
    public static extern void Dispatcher_interrupt(IntPtr dispatcher);
    [DllImport("TalleoWrapper")]
    public static extern void Dispatcher_interruptWithContext(IntPtr dispatcher, in IntPtr context);
    [DllImport("TalleoWrapper")]
    public static extern bool Dispatcher_interrupted(IntPtr dispatcher);
    [DllImport("TalleoWrapper")]
    public static extern void Dispatcher_pushContext(IntPtr dispatcher, in IntPtr context);
    [DllImport("TalleoWrapper")]
    public static extern void Dispatcher_remoteSpawn(IntPtr dispatcher, in IntPtr procedure);
    [DllImport("TalleoWrapper")]
    public static extern void Dispatcher_yield(IntPtr dispatcher);

    // Platform-specific

    [SupportedOSPlatform("windows")]
    [DllImport("TalleoWrapper")]
    public static extern void Dispatcher_addTimer(IntPtr dispatcher, UInt64 time, in IntPtr context);
    [SupportedOSPlatform("windows")]
    [DllImport("TalleoWrapper")]
    public static extern IntPtr Dispatcher_getCompletionPort(IntPtr dispatcher);
    [SupportedOSPlatform("windows")]
    [DllImport("TalleoWrapper")]
    public static extern void Dispatcher_interruptTimer(IntPtr dispatcher, UInt64 time, in IntPtr context);


    [SupportedOSPlatform("macos")]
    [DllImport("TalleoWrapper")]
    public static extern int Dispatcher_getKqueue(IntPtr dispatcher);

    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("android")]
    [DllImport("TalleoWrapper")]
    public static extern int Dispatcher_getEpoll(IntPtr dispatcher);

    [UnsupportedOSPlatform("windows")]
    [DllImport("TalleoWrapper")]
    public static extern int Dispatcher_getTimer(IntPtr dispatcher);
    [UnsupportedOSPlatform("windows")]
    [DllImport("TalleoWrapper")]
    public static extern void Dispatcher_pushTimer(IntPtr dispatcher, int timer);

    // Common
    [DllImport("TalleoWrapper")]
    public static extern void Dispatcher_getReusableContext(IntPtr dispatcher, out IntPtr context);
    [DllImport("TalleoWrapper")]
    public static extern void Dispatcher_pushReusableContext(IntPtr dispatcher, in IntPtr context);
}

namespace System
{

    public class Dispatcher : IWrapper
    {
        IntPtr wrappedClass;

        /* Constructor */
        public Dispatcher()
        {
            wrappedClass = UnmanagedDispatcher.Dispatcher_Create();
        }

        protected Dispatcher(IntPtr dispatcher)
        {
            wrappedClass = dispatcher;
        }

        /* Destructor */
        ~Dispatcher()
        {
            UnmanagedDispatcher.Dispatcher_Destroy(wrappedClass);
        }

        /* Class methods */
        public void clear()
        {
            UnmanagedDispatcher.Dispatcher_clear(wrappedClass);
        }
        public void dispatch()
        {
            UnmanagedDispatcher.Dispatcher_dispatch(wrappedClass);
        }
        public IntPtr getCurrentContext()
        {
            return UnmanagedDispatcher.Dispatcher_getCurrentContext(wrappedClass);
        }
        public void interrupt()
        {
            UnmanagedDispatcher.Dispatcher_interrupt(wrappedClass);
        }
        public void interrupt(in IntPtr context)
        {
            UnmanagedDispatcher.Dispatcher_interruptWithContext(wrappedClass, context);
        }
        public bool interrupted()
        {
            return UnmanagedDispatcher.Dispatcher_interrupted(wrappedClass);
        }
        public void pushContext(in IntPtr context)
        {
            UnmanagedDispatcher.Dispatcher_pushContext(wrappedClass, context);
        }
        public void remoteSpawn(Action procedure)
        {
            GCHandle gch = GCHandle.Alloc(procedure);
            UnmanagedDispatcher.Dispatcher_remoteSpawn(wrappedClass, GCHandle.ToIntPtr(gch));
        }
        public void yield()
        {
            UnmanagedDispatcher.Dispatcher_yield(wrappedClass);
        }

        // Platform-specific

        [SupportedOSPlatform("windows")]
        public void addTimer(UInt64 time, in IntPtr context)
        {
            UnmanagedDispatcher.Dispatcher_addTimer(wrappedClass, time, context);
        }
        [SupportedOSPlatform("windows")]
        public IntPtr getCompletionPort()
        {
            return UnmanagedDispatcher.Dispatcher_getCompletionPort(wrappedClass);
        }
        [SupportedOSPlatform("windows")]
        public void interruptTimer(UInt64 time, in IntPtr context)
        {
            UnmanagedDispatcher.Dispatcher_interruptTimer(wrappedClass, time, context);
        }

        [SupportedOSPlatform("macos")]
        public int getKqueue()
        {
            return UnmanagedDispatcher.Dispatcher_getKqueue(wrappedClass);
        }

        [SupportedOSPlatform("linux")]
        [SupportedOSPlatform("android")]
        public int getEpoll()
        {
            return UnmanagedDispatcher.Dispatcher_getEpoll(wrappedClass);
        }

        [UnsupportedOSPlatform("windows")]
        public int getTimer()
        {
            return UnmanagedDispatcher.Dispatcher_getTimer(wrappedClass);
        }
        [UnsupportedOSPlatform("windows")]
        public void pushTimer(int timer)
        {
            UnmanagedDispatcher.Dispatcher_pushTimer(wrappedClass, timer);
        }

        // Common
        public void getReusableContext(out IntPtr context)
        {
            UnmanagedDispatcher.Dispatcher_getReusableContext(wrappedClass, out context);
        }
        public void pushReusableContext(in IntPtr context)
        {
            UnmanagedDispatcher.Dispatcher_pushReusableContext(wrappedClass, context);
        }
        // IWrapper
        public IntPtr unwrap()
        {
            return wrappedClass;
        }

        static Dispatcher wrap(IntPtr dispatcher)
        {
            return new Dispatcher(dispatcher);
        }
    }
}
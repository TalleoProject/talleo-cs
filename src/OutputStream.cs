using System;
using System.Runtime.InteropServices;

class UnmanagedOutputStream
{
    [DllImport("TalleoWrapper")] public static extern IntPtr OutputStream_Create(IntPtr buffer);

    [DllImport("TalleoWrapper")] public static extern void OutputStream_Destroy(IntPtr stream);
}

namespace Talleo
{
    interface IStreamBuffer : IWrapper
    {

    }
    class OutputStream : IStream
    {
        IntPtr wrappedClass;

        public OutputStream(IStreamBuffer buffer)
        {
            wrappedClass = UnmanagedOutputStream.OutputStream_Create(buffer.unwrap());
        }

        protected OutputStream(IntPtr stream)
        {
            wrappedClass = stream;
        }

        ~OutputStream()
        {
            UnmanagedOutputStream.OutputStream_Destroy(wrappedClass);
        }
        //IWrapper
        public IntPtr unwrap()
        {
            return wrappedClass;
        }

        static OutputStream wrap(IntPtr stream)
        {
            return new OutputStream(stream);
        }
    }
}
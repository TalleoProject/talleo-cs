using System;

public interface IWrapper
{
    IntPtr unwrap() { return IntPtr.Zero; }
};
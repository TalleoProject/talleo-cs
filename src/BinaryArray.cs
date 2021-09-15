using System.Runtime.InteropServices;
class UnmanagedBinaryArray
{
    [DllImport("TalleoWrapper", EntryPoint = "BinaryArray_Cleanup")]
    public static extern void Cleanup(in UnmanagedTalleo.BinaryArray ba);
}
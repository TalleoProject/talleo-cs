using System;
using System.Runtime.InteropServices;
class ArrayUtilities
{
    [DllImport("TalleoWrapper")]
    public static extern void BlockDetailsArray_Destroy(IntPtr array);
    [DllImport("TalleoWrapper")]
    public static extern void BlockDetailsResultsArray_Destroy(IntPtr array, UInt64 items);
    [DllImport("TalleoWrapper")]
    public static extern void BlockShortEntryArray_Destroy(IntPtr array);
    [DllImport("TalleoWrapper")]
    public static extern void HashArray_Destroy(IntPtr array);
    [DllImport("TalleoWrapper")]
    public static extern void ITransactionReaderArray_Destroy(IntPtr array);
    [DllImport("TalleoWrapper")]
    public static extern void outs_for_amountArray_Destroy(IntPtr array);
    [DllImport("TalleoWrapper")]
    public static extern void RawBlockArray_Destroy(IntPtr array);

    [DllImport("TalleoWrapper")]
    public static extern void String_Destroy(IntPtr _string); /* allocated using strdup() etc. */
    [DllImport("TalleoWrapper")]
    public static extern void TransactionDetailsArray_Destroy(IntPtr array);
    [DllImport("TalleoWrapper")]
    public static extern void TransactionOutputInformationArray_Destroy(IntPtr array);
    [DllImport("TalleoWrapper")]
    public static extern void TransactionsInBlockInfo_Destroy(IntPtr array);
    [DllImport("TalleoWrapper")]
    public static extern void UInt32Array_Destroy(IntPtr array);
    [DllImport("TalleoWrapper")]
    public static extern void UInt64Array_Destroy(IntPtr array);
}
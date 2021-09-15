using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
class UnmanagedCachedBlock
{
    [DllImport("TalleoWrapper")]
    public static extern IntPtr CachedBlock_Create(in UnmanagedTalleo.BlockTemplate block);
    [DllImport("TalleoWrapper")]
    public static extern void CachedBlock_Destroy(IntPtr block);
    [DllImport("TalleoWrapper")]
    public static extern ref UnmanagedTalleo.BlockTemplate CachedBlock_getBlock(IntPtr block);
    [DllImport("TalleoWrapper")]
    public static extern ref Crypto.Hash CachedBlock_getTransactionTreeHash(IntPtr block);
    [DllImport("TalleoWrapper")]
    public static extern ref Crypto.Hash CachedBlock_getBlockHash(IntPtr block);
    [DllImport("TalleoWrapper")]
    public static extern ref Crypto.Hash CachedBlock_getBlockLongHash(IntPtr block);
    [DllImport("TalleoWrapper")]
    public static extern ref Crypto.Hash CachedBlock_getAuxiliaryBlockHeaderHash(IntPtr block);
    [DllImport("TalleoWrapper")]
    public static extern void CachedBlock_getBlockHashingBinaryArray(IntPtr block, out UnmanagedTalleo.BinaryArray array);
    [DllImport("TalleoWrapper")]
    public static extern void CachedBlock_getParentBlockBinaryArray(IntPtr block, bool headerOnly, out UnmanagedTalleo.BinaryArray array);
    [DllImport("TalleoWrapper")]
    public static extern void CachedBlock_getParentBlockHashingBinaryArray(IntPtr block, bool headerOnly, out UnmanagedTalleo.BinaryArray array);
    [DllImport("TalleoWrapper")]
    public static extern UInt32 CachedBlock_getBlockIndex(IntPtr block);
}
namespace Talleo
{
    using BinaryArray = List<Byte>;
    class CachedBlock : IWrapper
    {
        IntPtr wrappedClass;
        CachedBlock(in BlockTemplate block) {
            UnmanagedTalleo.BlockTemplate _block = ConversionTools.convertBlockTemplate(block);
            wrappedClass = UnmanagedCachedBlock.CachedBlock_Create(_block);
        }
        protected CachedBlock(IntPtr block)
        {
            wrappedClass = block;
        }
        BlockTemplate getBlock()
        {
            UnmanagedTalleo.BlockTemplate _block = UnmanagedCachedBlock.CachedBlock_getBlock(wrappedClass);
            return ConversionTools.convertBlockTemplate(_block);
        }
        Crypto.Hash getTransactionTreeHash()
        {
            return UnmanagedCachedBlock.CachedBlock_getTransactionTreeHash(wrappedClass);
        }
        Crypto.Hash getBlockHash()
        {
            return UnmanagedCachedBlock.CachedBlock_getBlockHash(wrappedClass);
        }
        Crypto.Hash getBlockLongHash()
        {
            return UnmanagedCachedBlock.CachedBlock_getBlockLongHash(wrappedClass);
        }
        Crypto.Hash getAuxiliaryBlockHeaderHash()
        {
            return UnmanagedCachedBlock.CachedBlock_getAuxiliaryBlockHeaderHash(wrappedClass);
        }
        BinaryArray getBlockHashingBinaryArray()
        {
            UnmanagedTalleo.BinaryArray _array;
            UnmanagedCachedBlock.CachedBlock_getBlockHashingBinaryArray(wrappedClass, out _array);
            BinaryArray ret = ConversionTools.convertBinaryArray(_array);
            UnmanagedBinaryArray.Cleanup(_array);
            return ret;
        }
        BinaryArray getParentBlockBinaryArray(bool headerOnly)
        {
            UnmanagedTalleo.BinaryArray _array;
            UnmanagedCachedBlock.CachedBlock_getParentBlockBinaryArray(wrappedClass, headerOnly, out _array);
            BinaryArray ret = ConversionTools.convertBinaryArray(_array);
            UnmanagedBinaryArray.Cleanup(_array);
            return ret;
        }
        BinaryArray getParentBlockHashingBinaryArray(bool headerOnly)
        {
            UnmanagedTalleo.BinaryArray _array;
            UnmanagedCachedBlock.CachedBlock_getParentBlockHashingBinaryArray(wrappedClass, headerOnly, out _array);
            BinaryArray ret = ConversionTools.convertBinaryArray(_array);
            UnmanagedBinaryArray.Cleanup(_array);
            return ret;
        }
        UInt32 getBlockIndex()
        {
            return UnmanagedCachedBlock.CachedBlock_getBlockIndex(wrappedClass);
        }
        // IWrapper
        public IntPtr unwrap()
        {
            return wrappedClass;
        }

        static CachedBlock wrap(IntPtr block)
        {
            return new CachedBlock(block);
        }
    }
}
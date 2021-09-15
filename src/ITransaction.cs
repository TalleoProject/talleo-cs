using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

class UnmanagedTransactionReader
{
    [DllImport("TalleoWrapper")] public static extern Crypto.Hash ITransactionReader_getTransactionHash(IntPtr reader);
    [DllImport("TalleoWrapper")] public static extern Crypto.Hash ITransactionReader_getTransactionPrefixHash(IntPtr reader);
    [DllImport("TalleoWrapper")] public static extern Crypto.PublicKey ITransactionReader_getTransactionPublicKey(IntPtr reader);
    [DllImport("TalleoWrapper")] public static extern bool ITransactionReader_getTransactionSecretKey(IntPtr reader, out Crypto.SecretKey key);
    [DllImport("TalleoWrapper")] public static extern UInt64 ITransactionReader_getUnlockTime(IntPtr reader);

    // extra
    [DllImport("TalleoWrapper")] public static extern bool ITransactionReader_getPaymentId(IntPtr reader, out Crypto.Hash paymentId);
    [DllImport("TalleoWrapper")] public static extern bool ITransactionReader_getExtraNonce(IntPtr reader, out UnmanagedTalleo.BinaryArray nonce);
    [DllImport("TalleoWrapper")] public static extern void ITransactionReader_getExtra(IntPtr reader, out UnmanagedTalleo.BinaryArray extra);

    // inputs
    [DllImport("TalleoWrapper")] public static extern UInt64 ITransactionReader_getInputCount(IntPtr reader);
    [DllImport("TalleoWrapper")] public static extern UInt64 ITransactionReader_getInputTotalAmount(IntPtr reader);
    [DllImport("TalleoWrapper")] public static extern Talleo.TransactionTypes.InputType ITransactionReader_getInputType(IntPtr reader, UInt64 index);
    [DllImport("TalleoWrapper")] public static extern void ITransactionReader_getInput(IntPtr reader, UInt64 index, out UnmanagedTalleo.KeyInput input);

    // outputs
    [DllImport("TalleoWrapper")] public static extern UInt64 ITransactionReader_getOutputCount(IntPtr reader);
    [DllImport("TalleoWrapper")] public static extern UInt64 ITransactionReader_getOutputTotalAmount(IntPtr reader);
    [DllImport("TalleoWrapper")] public static extern Talleo.TransactionTypes.OutputType ITransactionReader_getOutputType(IntPtr reader, UInt64 index);
    [DllImport("TalleoWrapper")] public static extern void ITransactionReader_getOutput(IntPtr reader, UInt64 index, out Talleo.KeyOutput output, out UInt64 amount);

    // signatures
    [DllImport("TalleoWrapper")] public static extern UInt64 ITransactionReader_getRequiredSignaturesCount(IntPtr reader, UInt64 inputIndex);
    [DllImport("TalleoWrapper")] public static extern bool ITransactionReader_findOutputsToAccount(IntPtr reader, in Talleo.AccountPublicAddress addr, in Crypto.SecretKey viewSecretKey, out IntPtr outs, out UInt64 outsCount, out UInt64 outputAmount);

    // various checks
    [DllImport("TalleoWrapper")] public static extern bool ITransactionReader_validateInputs(IntPtr reader);
    [DllImport("TalleoWrapper")] public static extern bool ITransactionReader_validateOutputs(IntPtr reader);
    [DllImport("TalleoWrapper")] public static extern bool ITransactionReader_validateSignatures(IntPtr reader);

    // serialized transaction
    [DllImport("TalleoWrapper")] public static extern void ITransactionReader_getTransactionData(IntPtr reader, out UnmanagedTalleo.BinaryArray data);

}
namespace Talleo
{
    using GlobalOutputsContainer = List<TransactionTypes.GlobalOutput>;
    using BinaryArray = List<Byte>;

    namespace TransactionTypes
    {

        public enum InputType : Byte { Invalid, Key, Generating };
        public enum OutputType : Byte { Invalid, Key };

        public struct GlobalOutput
        {
            public Crypto.PublicKey targetKey;
            public UInt32 outputIndex;
        };

        public struct OutputKeyInfo
        {
            public Crypto.PublicKey transactionPublicKey;
            public UInt64 transactionIndex;
            public UInt64 outputInTransaction;
        };

        public struct InputKeyInfo
        {
            public UInt64 amount;
            public GlobalOutputsContainer outputs;
            public OutputKeyInfo realOutput;
        };
    }

    //
    // ITransactionReader
    //
    public interface ITransactionReader : IWrapper
    {
        Crypto.Hash getTransactionHash();
        Crypto.Hash getTransactionPrefixHash();
        Crypto.PublicKey getTransactionPublicKey();
        bool getTransactionSecretKey(out Crypto.SecretKey key);
        UInt64 getUnlockTime();

        // extra
        bool getPaymentId(out Crypto.Hash paymentId);
        bool getExtraNonce(out BinaryArray nonce);
        BinaryArray getExtra();

        // inputs
        UInt64 getInputCount();
        UInt64 getInputTotalAmount();
        TransactionTypes.InputType getInputType(UInt64 index);
        void getInput(UInt64 index, out KeyInput input);

        // outputs
        UInt64 getOutputCount();
        UInt64 getOutputTotalAmount();
        TransactionTypes.OutputType getOutputType(UInt64 index);
        void getOutput(UInt64 index, out KeyOutput output, out UInt64 amount);

        // signatures
        UInt64 getRequiredSignaturesCount(UInt64 inputIndex);
        bool findOutputsToAccount(in AccountPublicAddress addr, in Crypto.SecretKey viewSecretKey, out List<UInt32> outs, out UInt64 outputAmount);

        // various checks
        bool validateInputs();
        bool validateOutputs();
        bool validateSignatures();

        // serialized transaction
        BinaryArray getTransactionData();

    };

    class TransactionReader : ITransactionReader
    {
        IntPtr wrappedClass;

        protected TransactionReader(IntPtr reader) { wrappedClass = reader; }

        // Class members
        public Crypto.Hash getTransactionHash()
        {
            return UnmanagedTransactionReader.ITransactionReader_getTransactionHash(wrappedClass);
        }
        public Crypto.Hash getTransactionPrefixHash()
        {
            return UnmanagedTransactionReader.ITransactionReader_getTransactionPrefixHash(wrappedClass);
        }
        public Crypto.PublicKey getTransactionPublicKey()
        {
            return UnmanagedTransactionReader.ITransactionReader_getTransactionPublicKey(wrappedClass);
        }
        public bool getTransactionSecretKey(out Crypto.SecretKey key)
        {
            return UnmanagedTransactionReader.ITransactionReader_getTransactionSecretKey(wrappedClass, out key);
        }
        public UInt64 getUnlockTime()
        {
            return UnmanagedTransactionReader.ITransactionReader_getUnlockTime(wrappedClass);
        }

        // extra
        public bool getPaymentId(out Crypto.Hash paymentId)
        {
            return UnmanagedTransactionReader.ITransactionReader_getPaymentId(wrappedClass, out paymentId);
        }
        public bool getExtraNonce(out BinaryArray nonce)
        {
            UnmanagedTalleo.BinaryArray ba;
            bool ret = UnmanagedTransactionReader.ITransactionReader_getExtraNonce(wrappedClass, out ba);
            nonce = ConversionTools.convertBinaryArray(ba);
            UnmanagedBinaryArray.Cleanup(ba);
            return ret;
        }
        public BinaryArray getExtra()
        {
            UnmanagedTalleo.BinaryArray ba = new UnmanagedTalleo.BinaryArray();
            UnmanagedTransactionReader.ITransactionReader_getExtra(wrappedClass, out ba);
            BinaryArray _ba = ConversionTools.convertBinaryArray(ba);
            UnmanagedBinaryArray.Cleanup(ba);
            return _ba;
        }

        // inputs
        public UInt64 getInputCount()
        {
            return UnmanagedTransactionReader.ITransactionReader_getInputCount(wrappedClass);
        }
        public UInt64 getInputTotalAmount()
        {
            return UnmanagedTransactionReader.ITransactionReader_getInputTotalAmount(wrappedClass);
        }
        public TransactionTypes.InputType getInputType(UInt64 index)
        {
            return UnmanagedTransactionReader.ITransactionReader_getInputType(wrappedClass, index);
        }
        public void getInput(UInt64 index, out KeyInput input)
        {
            UnmanagedTalleo.KeyInput _input;
            UnmanagedTransactionReader.ITransactionReader_getInput(wrappedClass, index, out _input);
            input = ConversionTools.convertKeyInput(_input);
        }

        // outputs
        public UInt64 getOutputCount()
        {
            return UnmanagedTransactionReader.ITransactionReader_getOutputCount(wrappedClass);
        }
        public UInt64 getOutputTotalAmount()
        {
            return UnmanagedTransactionReader.ITransactionReader_getOutputTotalAmount(wrappedClass);
        }
        public TransactionTypes.OutputType getOutputType(UInt64 index)
        {
            return UnmanagedTransactionReader.ITransactionReader_getOutputType(wrappedClass, index);
        }
        public void getOutput(UInt64 index, out KeyOutput output, out UInt64 amount)
        {
            UnmanagedTransactionReader.ITransactionReader_getOutput(wrappedClass, index, out output, out amount);
        }

        // signatures
        public UInt64 getRequiredSignaturesCount(UInt64 inputIndex)
        {
            return UnmanagedTransactionReader.ITransactionReader_getRequiredSignaturesCount(wrappedClass, inputIndex);
        }
        public bool findOutputsToAccount(in AccountPublicAddress addr, in Crypto.SecretKey viewSecretKey, out List<UInt32> outs, out UInt64 outputAmount)
        {
            IntPtr _outs;
            UInt64 outsCount;
            outs = new List<UInt32>();
            bool ret = UnmanagedTransactionReader.ITransactionReader_findOutputsToAccount(wrappedClass, addr, viewSecretKey, out _outs, out outsCount, out outputAmount);
            for (int i = 0; (UInt64)i < outsCount; i++)
            {
                outs.Add((UInt32)(object)Marshal.ReadInt32(_outs + i * sizeof(Int32)));
            }
            ArrayUtilities.UInt32Array_Destroy(_outs);
            return ret;
        }

        // various checks
        public bool validateInputs()
        {
            return UnmanagedTransactionReader.ITransactionReader_validateInputs(wrappedClass);
        }
        public bool validateOutputs()
        {
            return UnmanagedTransactionReader.ITransactionReader_validateOutputs(wrappedClass);
        }
        public bool validateSignatures()
        {
            return UnmanagedTransactionReader.ITransactionReader_validateSignatures(wrappedClass);
        }

        // serialized transaction
        public BinaryArray getTransactionData()
        {
            UnmanagedTalleo.BinaryArray ba;
            UnmanagedTransactionReader.ITransactionReader_getTransactionData(wrappedClass, out ba);
            BinaryArray _ba = ConversionTools.convertBinaryArray(ba);
            UnmanagedBinaryArray.Cleanup(ba);
            return _ba;
        }

        // IWrapper

        public static TransactionReader wrap(IntPtr reader) { return new TransactionReader(reader); }

        public IntPtr unwrap() { return wrappedClass; }

    }

    //
    // ITransactionWriter
    //
    interface ITransactionWriter : IWrapper
    {
        // transaction parameters
        void setUnlockTime(UInt64 unlockTime);

        // extra
        void setPaymentId(in Crypto.Hash paymentId);
        void setExtraNonce(in BinaryArray nonce);
        void appendExtra(in BinaryArray extraData);

        // Inputs/Outputs
        UInt64 addInput(in KeyInput input);
        UInt64 addInput(in AccountKeys senderKeys, in TransactionTypes.InputKeyInfo info, ref KeyPair ephKeys);

        UInt64 addOutput(UInt64 amount, in AccountPublicAddress to);
        UInt64 addOutput(UInt64 amount, in KeyOutput _out);

        // transaction info
        void setTransactionSecretKey(in Crypto.SecretKey key);

        // signing
        void signInputKey(UInt64 input, in TransactionTypes.InputKeyInfo info, in KeyPair ephKeys);
    };

    interface ITransaction : ITransactionReader, ITransactionWriter { };

}

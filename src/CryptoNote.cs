using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

// NOTE: BinaryArray_Cleanup() is in BinaryArray.cs

class UnmanagedSignatureArray
{
    [DllImport("TalleoWrapper", EntryPoint = "SignatureArray_Cleanup")] public static extern void Cleanup(in UnmanagedTalleo.SignatureArray signatureArray);
}

class UnmanagedBaseTransaction
{
    [DllImport("TalleoWrapper", EntryPoint = "BaseTransaction_Cleanup")] public static extern void Cleanup(in UnmanagedTalleo.BaseTransaction baseTransaction);
}

class UnmanagedBlockDetails
{
    [DllImport("TalleoWrapper", EntryPoint = "BlockDetails_Cleanup")] public static extern void Cleanup(in UnmanagedTalleo.BlockDetails blockDetails);
}

class UnmanagedTransaction
{
    [DllImport("TalleoWrapper", EntryPoint = "Transaction_Cleanup")] public static extern void Cleanup(in UnmanagedTalleo.Transaction transaction);
}

class UnmanagedBlockTemplate
{
    [DllImport("TalleoWrapper", EntryPoint = "BlockTemplate_Cleanup")] public static extern void Cleanup(in UnmanagedTalleo.BlockTemplate blockTemplate);
}

class UnmanagedParentBlock
{
    [DllImport("TalleoWrapper", EntryPoint = "ParentBlock_Cleanup")] public static extern void Cleanup(in UnmanagedTalleo.ParentBlock parentBlock);
}

namespace UnmanagedTalleo
{
    public struct KeyInput
    {
        public UInt64 amount;
        public IntPtr outputIndexes;
        public UInt64 outputIndexCount;
        public Crypto.KeyImage keyImage;
    };

    [StructLayout(LayoutKind.Explicit, Size = 64)]
    public struct TransactionInput
    {
        [FieldOffset(0)] public bool isBase;
        [FieldOffset(8)] public Talleo.BaseInput baseInput;
        [FieldOffset(8)] public KeyInput keyInput;
    };
    public struct TransactionOutput
    {
        public UInt64 amount;
        public Talleo.KeyOutput keyOutput;
        public bool hasKey;
    }

    public struct TransactionPrefix
    {
        public Byte version;
        public UInt64 unlockTime;
        public IntPtr inputs;
        public UInt64 inputCount;
        public IntPtr outputs;
        public UInt64 outputCount;
        public BinaryArray extra;
    };
    public struct BinaryArray
    {
        public IntPtr data;
        public UInt64 dataLength;
    };

    public struct SignatureArray
    {
        public IntPtr signatures;
        public UInt64 signatureCount;
    };

    public struct Transaction
    {
        public Byte version;
        public UInt64 unlockTime;
        public IntPtr inputs;
        public UInt64 inputCount;
        public IntPtr outputs;
        public UInt64 outputCount;
        public BinaryArray extra;
        public IntPtr signatures;
        public UInt64 signatureCount;
    };
    public struct BaseTransaction
    {
        public Byte version;
        public UInt64 unlockTime;
        public IntPtr inputs;
        public UInt64 inputCount;
        public IntPtr outputs;
        public UInt64 outputCount;
        public BinaryArray extra;
    };
    public struct ParentBlock
    {
        public Byte majorVersion;
        public Byte minorVersion;
        public Crypto.Hash previousBlockHash;
        public UInt16 transactionCount;
        public IntPtr baseTransactionBranch;
        public UInt64 baseTransactionBranchCount;
        public BaseTransaction baseTransaction;
        public IntPtr blockchainBranch;
        public UInt64 blockchainBranchCount;
    };

    public struct BlockTemplate
    {
        public Byte majorVersion;
        public Byte minorVersion;
        public UInt32 nonce;
        public UInt64 timestamp;
        public Crypto.Hash previousBlockHash;
        public ParentBlock parentBlock;
        public Transaction baseTransaction;
        public IntPtr transactionHashes;
        public UInt64 transactionHashCount;
    };

    public struct RawBlock
    {
        public BinaryArray block; //BlockTemplate
        public IntPtr transactions;
        public UInt64 transactionCount; 
    };
}

namespace Talleo
{

    using TransactionInput = Variant<BaseInput, KeyInput>;
    using TransactionOutputTarget = Nullable<KeyOutput>;
    using BinaryArray = List<Byte>;

    public struct BaseInput
    {
        public UInt32 blockIndex;
    };

    public struct KeyInput
    {
        public UInt64 amount;
        public List<UInt32> outputIndexes;
        public Crypto.KeyImage keyImage;
    };

    public struct KeyOutput
    {
        public Crypto.PublicKey key;
    };

    public struct TransactionOutput
    {
        public UInt64 amount;
        public TransactionOutputTarget target;
    };

    public struct TransactionPrefix
    {
        public Byte version;
        public UInt64 unlockTime;
        public List<TransactionInput> inputs;
        public List<TransactionOutput> outputs;
        public List<Byte> extra;
    };

    public struct Transaction
    {
        public Byte version;
        public UInt64 unlockTime;
        public List<TransactionInput> inputs;
        public List<TransactionOutput> outputs;
        public List<Byte> extra;
        public List<List<Crypto.Signature>> signatures;
    };

    public struct BaseTransaction
    {
        public Byte version;
        public UInt64 unlockTime;
        public List<TransactionInput> inputs;
        public List<TransactionOutput> outputs;
        public List<Byte> extra;
    };

    public struct ParentBlock
    {
        public Byte majorVersion;
        public Byte minorVersion;
        public Crypto.Hash previousBlockHash;
        public UInt16 transactionCount;
        public List<Crypto.Hash> baseTransactionBranch;
        public BaseTransaction baseTransaction;
        public List<Crypto.Hash> blockchainBranch;
    };

    public struct BlockHeader
    {
        public Byte majorVersion;
        public Byte minorVersion;
        public UInt32 nonce;
        public UInt64 timestamp;
        public Crypto.Hash previousBlockHash;
    };

    public struct BlockTemplate
    {
        public Byte majorVersion;
        public Byte minorVersion;
        public UInt32 nonce;
        public UInt64 timestamp;
        public Crypto.Hash previousBlockHash;
        public ParentBlock parentBlock;
        public Transaction baseTransaction;
        public List<Crypto.Hash> transactionHashes;
    };

    public struct AccountPublicAddress
    {
        public Crypto.PublicKey spendPublicKey;
        public Crypto.PublicKey viewPublicKey;
    };

    public struct AccountKeys
    {
        public AccountPublicAddress address;
        public Crypto.SecretKey spendSecretKey;
        public Crypto.SecretKey viewSecretKey;
    };

    public struct KeyPair
    {
        public Crypto.PublicKey publicKey;
        public Crypto.SecretKey secretKey;
    };

    public struct RawBlock
    {
        public BinaryArray block; //BlockTemplate
        public List<BinaryArray> transactions;
    };

}

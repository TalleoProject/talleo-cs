using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace UnmanagedTalleo
{
    public struct KeyInputDetails
    {
        public KeyInput input;
        public UInt64 mixin;
        public Talleo.TransactionOutputReferenceDetails output;
    };

    [StructLayout(LayoutKind.Explicit, Size = 112)]
    public struct TransactionInputDetails
    {
        [FieldOffset(0)] public bool isBase;
        [FieldOffset(8)] public Talleo.BaseInputDetails baseInputDetails;
        [FieldOffset(8)] public KeyInputDetails keyInputDetails;
    };
    public struct TransactionOutputDetails
    {
        public TransactionOutput output;
        public UInt64 globalIndex;
    };

    public struct TransactionExtraDetails
    {
        public Crypto.PublicKey publicKey;
        public BinaryArray nonce;
        public BinaryArray raw;
    };

    public struct TransactionDetails
    {
        public Crypto.Hash hash;
        public UInt64 size;
        public UInt64 fee;
        public UInt64 totalInputsAmount;
        public UInt64 totalOutputsAmount;
        public UInt64 mixin;
        public UInt64 unlockTime;
        public UInt64 timestamp;
        public Crypto.Hash paymentId;
        public bool hasPaymentId;
        public bool inBlockchain;
        public Crypto.Hash blockHash;
        public UInt32 blockIndex;
        public TransactionExtraDetails extra;
        public IntPtr signatures;
        public UInt64 signatureCount;
        public IntPtr inputs;
        public UInt64 inputCount;
        public IntPtr outputs;
        public UInt64 outputCount;
    };

    public struct BlockDetails
    {
        public Byte majorVersion;
        public Byte minorVersion;
        public UInt64 timestamp;
        public Crypto.Hash prevBlockHash;
        public UInt32 nonce;
        public bool isAlternative;
        public UInt32 index;
        public Crypto.Hash hash;
        public UInt64 difficulty;
        public UInt64 reward;
        public UInt64 baseReward;
        public UInt64 blockSize;
        public UInt64 transactionsCumulativeSize;
        public UInt64 alreadyGeneratedCoins;
        public UInt64 alreadyGeneratedTransactions;
        public UInt64 sizeMedian;
        public double penalty;
        public UInt64 totalFeeAmount;
        public IntPtr transactions;
        public UInt64 transactionCount;
    };

    public struct BlockDetailsArray
    {
        public IntPtr blockDetails;
        public UInt64 blockDetailsCount;
    };
}
namespace Talleo
{
    using TransactionInputDetails = Variant<BaseInputDetails, KeyInputDetails>;
    using BinaryArray = List<Byte>;

    enum TransactionRemoveReason : Byte
    {
        INCLUDED_IN_BLOCK = 0,
        TIMEOUT = 1
    };

    public struct TransactionOutputDetails
    {
        public TransactionOutput output;
        public UInt64 globalIndex;
    };

    public struct TransactionOutputReferenceDetails
    {
        public Crypto.Hash transactionHash;
        public UInt64 number;
    };

    public struct BaseInputDetails
    {
        public BaseInput input;
        public UInt64 amount;
    };

    public struct KeyInputDetails
    {
        public KeyInput input;
        public UInt64 mixin;
        public TransactionOutputReferenceDetails output;
    };

    public struct TransactionExtraDetails
    {
        public Crypto.PublicKey publicKey;
        public BinaryArray nonce;
        public BinaryArray raw;
    };

    public struct TransactionDetails
    {
        public Crypto.Hash hash;
        public UInt64 size;
        public UInt64 fee;
        public UInt64 totalInputsAmount;
        public UInt64 totalOutputsAmount;
        public UInt64 mixin;
        public UInt64 unlockTime;
        public UInt64 timestamp;
        public Crypto.Hash paymentId;
        public bool hasPaymentId;
        public bool inBlockchain;
        public Crypto.Hash blockHash;
        public UInt32 blockIndex;
        public TransactionExtraDetails extra;
        public List<List<Crypto.Signature>> signatures;
        public List<TransactionInputDetails> inputs;
        public List<TransactionOutputDetails> outputs;
    };

    public struct BlockDetails
    {
        public Byte majorVersion;
        public Byte minorVersion;
        public UInt64 timestamp;
        public Crypto.Hash prevBlockHash;
        public UInt32 nonce;
        public bool isAlternative;
        public UInt32 index;
        public Crypto.Hash hash;
        public UInt64 difficulty;
        public UInt64 reward;
        public UInt64 baseReward;
        public UInt64 blockSize;
        public UInt64 transactionsCumulativeSize;
        public UInt64 alreadyGeneratedCoins;
        public UInt64 alreadyGeneratedTransactions;
        public UInt64 sizeMedian;
        public double penalty;
        public UInt64 totalFeeAmount;
        public List<TransactionDetails> transactions;
    };

}

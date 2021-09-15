using System;
using System.Collections.Generic;

namespace UnmanagedTalleo
{
    public struct TransactionShortInfo
    {
        public Crypto.Hash txId;
        public TransactionPrefix txPrefix;
    };

    public struct BlockShortEntry
    {
        public Crypto.Hash blockHash;
        public bool hasBlock;
        public BlockTemplate block;
        public IntPtr txsShortInfo;
        public UInt64 txsShortInfoCount;
    };
}
namespace Talleo
{

    using Callback = Action<ErrorCode>;
    using Difficulty = UInt64;

    public interface INodeObserver : IWrapper
    {
        void peerCountUpdated(in UInt64 count);
        void localBlockchainUpdated(in UInt32 height);
        void lastKnownBlockHeightUpdated(in UInt32 height);
        void poolChanged();
        void blockchainSynchronized(in UInt32 topHeight);
        void chainSwitched(in UInt32 newTopIndex, in UInt32 commonRoot, in List<Crypto.Hash> hashes);
    };

    public struct OutEntry
    {
        public UInt32 outGlobalIndex;
        public Crypto.PublicKey outKey;
    };

    public struct OutsForAmount
    {
        public UInt64 amount;
        public List<OutEntry> outs;
    };

    public struct TransactionShortInfo
    {
        public Crypto.Hash txId;
        public TransactionPrefix txPrefix;
    };

    public struct BlockShortEntry
    {
        public Crypto.Hash blockHash;
        public bool hasBlock;
        public BlockTemplate block;
        public List<TransactionShortInfo> txsShortInfo;
    };

    public struct BlockHeaderInfo
    {
        public UInt32 index;
        public Byte majorVersion;
        public Byte minorVersion;
        public UInt64 timestamp;
        public Crypto.Hash hash;
        public Crypto.Hash prevHash;
        public UInt32 nonce;
        public bool isAlternative;
        public UInt32 depth; // last block index = current block index + depth
        public Difficulty difficulty;
        public UInt64 reward;
    };

    public struct ErrorCode
    {
        public Int32 value;
        public IntPtr ErrorCategory;
    }

    public interface INode : IWrapper
    {

        bool addObserver(in INodeObserver observer);
        bool removeObserver(in INodeObserver observer);

        //precondition: must be called in dispatcher's thread
        void init(in Callback callback);
        //precondition: must be called in dispatcher's thread
        bool shutdown();

        //precondition: all of following methods must not be invoked in dispatcher's thread
        UInt64 getPeerCount();
        UInt32 getLastLocalBlockHeight();
        UInt32 getLastKnownBlockHeight();
        UInt32 getLocalBlockCount();
        UInt32 getKnownBlockCount();
        UInt64 getLastLocalBlockTimestamp();
        String getLastFeeAddress();
        String getLastCollateralHash();

        void getBlockHashesByTimestamps(in UInt64 timestampBegin, in UInt64 secondsCount, Action<ErrorCode, List<Crypto.Hash>> callback);
        void getTransactionHashesByPaymentId(in Crypto.Hash paymentId, Action<ErrorCode, List<Crypto.Hash>> callback);

        BlockHeaderInfo getLastLocalBlockHeaderInfo();

        void relayTransaction(in Transaction transaction, Callback callback);
        void getRandomOutsByAmounts(in List<UInt64> amounts, in UInt16 outsCount, Action<ErrorCode, List<COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount>> callback);
        void getNewBlocks(in List<Crypto.Hash> knownBlockIds, Action<ErrorCode, List<RawBlock>, UInt32> callback);
        void getTransactionOutsGlobalIndices(in Crypto.Hash transactionHash, Action<ErrorCode, List<UInt32>> callback);
        void queryBlocks(in List<Crypto.Hash> knownBlockIds, in UInt64 timestamp, Action<ErrorCode, List<BlockShortEntry>, UInt32> callback);
        void getPoolSymmetricDifference(in List<Crypto.Hash> knownPoolTxIds, Crypto.Hash knownBlockId, Action<ErrorCode, bool, List<ITransactionReader>, List<Crypto.Hash>> callback);

        void getBlocks(in List<UInt32> blockHeights, Action<ErrorCode, List<List<BlockDetails>>> callback);
        void getBlocks(in List<Crypto.Hash> blockHashes, Action<ErrorCode, List<BlockDetails>> callback);
        void getBlock(in UInt32 blockHeight, Action<ErrorCode, BlockDetails> callback);
        void getTransactions(in List<Crypto.Hash> transactionHashes, Action<ErrorCode, List<TransactionDetails>> callback);
        void getFeeAddress(Action<ErrorCode, String> callback);
        void getCollateralHash(Action<ErrorCode, String> callback);
        void isSynchronized(Action<ErrorCode, bool> callback);
    }

}

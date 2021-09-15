using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

public class UnmanagedNodeRpcProxy
{
    /* Constructor */
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern IntPtr NodeRpcProxy_Create(string nodeHost, UInt16 nodePort, IntPtr logger);

    /* Destructor */
    [DllImport("TalleoWrapper")]
    public static extern void NodeRpcProxy_Destroy(IntPtr proxy);

    /* Class methods */
    [DllImport("TalleoWrapper")]
    public static extern bool NodeRpcProxy_addINodeObserver(IntPtr proxy, IntPtr observer);
    [DllImport("TalleoWrapper")]
    public static extern bool NodeRpcProxy_removeINodeObserver(IntPtr proxy, IntPtr observer);

    [DllImport("TalleoWrapper")]
    public static extern bool NodeRpcProxy_addINodeRpcProxyObserver(IntPtr proxy, IntPtr observer);
    [DllImport("TalleoWrapper")]
    public static extern bool NodeRpcProxy_removeINodeRpcProxyObserver(IntPtr proxy, IntPtr observer);

    [DllImport("TalleoWrapper")]
    public static extern void NodeRpcProxy_init(IntPtr proxy, IntPtr callback);
    [DllImport("TalleoWrapper")]
    public static extern bool NodeRpcProxy_shutdown(IntPtr proxy);

    [DllImport("TalleoWrapper")]
    public static extern UInt64 NodeRpcProxy_getPeerCount(IntPtr proxy);
    [DllImport("TalleoWrapper")]
    public static extern UInt32 NodeRpcProxy_getLastLocalBlockHeight(IntPtr proxy);
    [DllImport("TalleoWrapper")]
    public static extern UInt32 NodeRpcProxy_getLastKnownBlockHeight(IntPtr proxy);
    [DllImport("TalleoWrapper")]
    public static extern UInt32 NodeRpcProxy_getLocalBlockCount(IntPtr proxy);
    [DllImport("TalleoWrapper")]
    public static extern UInt32 NodeRpcProxy_getKnownBlockCount(IntPtr proxy);
    [DllImport("TalleoWrapper")]
    public static extern UInt64 NodeRpcProxy_getLastLocalBlockTimestamp(IntPtr proxy);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern string NodeRpcProxy_getLastFeeAddress(IntPtr proxy);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern string NodeRpcProxy_getLastCollateralHash(IntPtr proxy);

    [DllImport("TalleoWrapper")]
    public static extern void NodeRpcProxy_getBlockHashesByTimestamps(IntPtr proxy, UInt64 timestampBegin, UInt64 secondsCount, ref IntPtr blockHashes, out UInt64 blockHashesCount, in IntPtr callback);
    [DllImport("TalleoWrapper")]
    public static extern void NodeRpcProxy_getTransactionHashesByPaymentId(IntPtr proxy, in Crypto.Hash paymentId, out IntPtr transactionHashes, out UInt64 transactionHashesCount, in IntPtr callback);

    [DllImport("TalleoWrapper")]
    public static extern Talleo.BlockHeaderInfo NodeRpcProxy_getLastLocalBlockHeaderInfo(IntPtr proxy);

    [DllImport("TalleoWrapper")]
    public static extern void NodeRpcProxy_relayTransaction(IntPtr proxy, in UnmanagedTalleo.Transaction transaction, in IntPtr callback);
    [DllImport("TalleoWrapper")]
    public static extern void NodeRpcProxy_getRandomOutsByAmounts(IntPtr proxy, IntPtr amounts, in UInt64 amountsCount, UInt16 outsCount, IntPtr result, out UInt64 resultCount, in IntPtr callback);
    [DllImport("TalleoWrapper")]
    public static extern void NodeRpcProxy_getNewBlocks(IntPtr proxy, IntPtr knownBlockIds, in UInt64 knownBlockIdsCount, out IntPtr newBlocks, out UInt64 newBlocksCount, out UInt32 startHeight, in IntPtr callback);
    [DllImport("TalleoWrapper")]
    public static extern void NodeRpcProxy_getTransactionOutsGlobalIndices(IntPtr proxy, in Crypto.Hash transactionHash, out IntPtr outsGlobalIndices, out UInt64 outsGlobalIndicesCount, in IntPtr callback);
    [DllImport("TalleoWrapper")]
    public static extern void NodeRpcProxy_queryBlocks(IntPtr proxy, in IntPtr knownBlockIds, in UInt64 knownBlockIdsCount, UInt64 timestamp, out IntPtr newBlocks, out UInt64 newBlocksCount, out UInt32 startHeight, in IntPtr callback);
    [DllImport("TalleoWrapper")]
    public static extern void NodeRpcProxy_getPoolSymmetricDifference(IntPtr proxy, in IntPtr knownPoolTxIds, in UInt64 knownPoolTxIdsCount, Crypto.Hash knownBlockId, ref bool isBcActual, out IntPtr newTxs, out UInt64 newTxsCount, out IntPtr deletedTxIds, out UInt64 deletedTxIdsCount, in IntPtr callback);

    public struct BlockDetailsResults
    {
        public IntPtr details;
        public UInt64 detailsCount;
    };

    [DllImport("TalleoWrapper")]
    public static extern void NodeRpcProxy_getBlocksByHeights(IntPtr proxy, in IntPtr blockHeights, in UInt64 blockHeightsCount, out IntPtr results, out UInt64 resultsCount, in IntPtr callback);
    [DllImport("TalleoWrapper")]
    public static extern void NodeRpcProxy_getBlocksByHashes(IntPtr proxy, in IntPtr blockHashes, in UInt64 blockHashesCount, out IntPtr blocks, out UInt64 blocksCount, in IntPtr callback);
    [DllImport("TalleoWrapper")]
    public static extern void NodeRpcProxy_getBlock(IntPtr proxy, in UInt32 blockHeight, out UnmanagedTalleo.BlockDetails block, in IntPtr callback);
    [DllImport("TalleoWrapper")]
    public static extern void NodeRpcProxy_getTransactions(IntPtr proxy, in IntPtr transactionHashes, in UInt64 transactionHashesCount, out IntPtr transactions, out UInt64 transactionsCount, in IntPtr callback);
    [DllImport("TalleoWrapper")]
    public static extern void NodeRpcProxy_getFeeAddress(IntPtr proxy, out IntPtr feeAddress, in IntPtr callback);
    [DllImport("TalleoWrapper")]
    public static extern void NodeRpcProxy_getCollateralHash(IntPtr proxy, out IntPtr collateralHash, in IntPtr callback);
    [DllImport("TalleoWrapper")]
    public static extern void NodeRpcProxy_isSynchronized(IntPtr proxy, ref bool syncStatus, in IntPtr callback);

    [DllImport("TalleoWrapper")]
    public static extern UInt32 NodeRpcProxy_getRpcTimeout(IntPtr proxy);
    [DllImport("TalleoWrapper")]
    public static extern void NodeRpcProxy_setRpcTimeout(IntPtr proxy, UInt32 val);
};

namespace Talleo
{
    using Callback = Action<Talleo.ErrorCode>;

    public interface INodeRpcProxyObserver : INodeObserver
    {
        public void connectionStatusUpdated(in bool connected);
    };

    public class NodeRpcProxy : INode
    {
        private IntPtr wrappedClass;

        public NodeRpcProxy(in String nodeHost, UInt16 nodePort, in Logging.ILogger logger)
        {
            wrappedClass = UnmanagedNodeRpcProxy.NodeRpcProxy_Create(nodeHost, nodePort, logger.unwrap());
        }
        protected NodeRpcProxy(IntPtr node)
        {
            wrappedClass = node;
        }
        ~NodeRpcProxy()
        {
            UnmanagedNodeRpcProxy.NodeRpcProxy_Destroy(wrappedClass);
        }

        public bool addObserver(in INodeObserver observer)
        {
            return UnmanagedNodeRpcProxy.NodeRpcProxy_addINodeObserver(wrappedClass, observer.unwrap());
        }
        public bool removeObserver(in INodeObserver observer)
        {
            return UnmanagedNodeRpcProxy.NodeRpcProxy_removeINodeObserver(wrappedClass, observer.unwrap());
        }
        public bool addObserver(in INodeRpcProxyObserver observer)
        {
            return UnmanagedNodeRpcProxy.NodeRpcProxy_addINodeRpcProxyObserver(wrappedClass, observer.unwrap());
        }
        public bool removeObserver(in INodeRpcProxyObserver observer)
        {
            return UnmanagedNodeRpcProxy.NodeRpcProxy_removeINodeRpcProxyObserver(wrappedClass, observer.unwrap());
        }

        public void init(in Callback callback)
        {
            GCHandle gch = GCHandle.Alloc(callback);
            UnmanagedNodeRpcProxy.NodeRpcProxy_init(wrappedClass, GCHandle.ToIntPtr(gch));
        }
        public bool shutdown()
        {
            return UnmanagedNodeRpcProxy.NodeRpcProxy_shutdown(wrappedClass);
        }

        public UInt64 getPeerCount()
        {
            return UnmanagedNodeRpcProxy.NodeRpcProxy_getPeerCount(wrappedClass);
        }
        public UInt32 getLastLocalBlockHeight()
        {
            return UnmanagedNodeRpcProxy.NodeRpcProxy_getLastLocalBlockHeight(wrappedClass);
        }
        public UInt32 getLastKnownBlockHeight()
        {
            return UnmanagedNodeRpcProxy.NodeRpcProxy_getLastKnownBlockHeight(wrappedClass);
        }
        public UInt32 getLocalBlockCount()
        {
            return UnmanagedNodeRpcProxy.NodeRpcProxy_getLocalBlockCount(wrappedClass);
        }
        public UInt32 getKnownBlockCount()
        {
            return UnmanagedNodeRpcProxy.NodeRpcProxy_getKnownBlockCount(wrappedClass);
        }
        public UInt64 getLastLocalBlockTimestamp()
        {
            return UnmanagedNodeRpcProxy.NodeRpcProxy_getLastLocalBlockTimestamp(wrappedClass);
        }
        public String getLastFeeAddress()
        {
            return UnmanagedNodeRpcProxy.NodeRpcProxy_getLastFeeAddress(wrappedClass);
        }
        public String getLastCollateralHash()
        {
            return UnmanagedNodeRpcProxy.NodeRpcProxy_getLastCollateralHash(wrappedClass);
        }

        public void getBlockHashesByTimestamps(in UInt64 timestampBegin, in UInt64 secondsCount, Action<ErrorCode, List<Crypto.Hash>> callback)
        {
            // Unimplemented
        }
        public void getTransactionHashesByPaymentId(in Crypto.Hash paymentId, Action<ErrorCode, List<Crypto.Hash>> callback)
        {
            // Unimplemented
        }

        public BlockHeaderInfo getLastLocalBlockHeaderInfo()
        {
            return UnmanagedNodeRpcProxy.NodeRpcProxy_getLastLocalBlockHeaderInfo(wrappedClass);
        }


        public void relayTransaction(in Transaction transaction, Callback callback)
        {
            UnmanagedTalleo.Transaction _transaction = ConversionTools.convertTransaction(transaction);
            Callback _callback = (ec) =>
            {
                UnmanagedTransaction.Cleanup(_transaction);
                callback(ec);
            };
            GCHandle gch = GCHandle.Alloc(_callback);
            UnmanagedNodeRpcProxy.NodeRpcProxy_relayTransaction(wrappedClass, _transaction, GCHandle.ToIntPtr(gch));
        }

        private void getRandomOutsByAmounts(in IntPtr amounts, in UInt64 amountsCount, in UInt16 outsCount, Action<ErrorCode, IntPtr, UInt64> callback)
        {
            IntPtr result = IntPtr.Zero;
            UInt64 resultCount = 0;
            Callback _callback = (ec) =>
            {
                callback(ec, result, resultCount);
            };
            GCHandle gch = GCHandle.Alloc(_callback);
            UnmanagedNodeRpcProxy.NodeRpcProxy_getRandomOutsByAmounts(wrappedClass, amounts, amountsCount, outsCount, result, out resultCount, GCHandle.ToIntPtr(gch));
        }
        public void getRandomOutsByAmounts(in List<UInt64> amounts, in UInt16 outsCount, Action<ErrorCode, List<COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount>> callback)
        {
            UInt64[] _amounts = amounts.ToArray();
            GCHandle gch = GCHandle.Alloc(_amounts);
            Action<ErrorCode, IntPtr, UInt64> _callback = (ec, result, resultCount) =>
            {
                List<COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount> _result = new List<COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount>();
                unsafe
                {
                    for (int i = 0; (UInt64)i < resultCount; i++)
                    {
                        _result.Add(ConversionTools.convertOutsForAmount(Marshal.PtrToStructure<UnmanagedTalleo.COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount>(result + i * sizeof(UnmanagedTalleo.COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount))));
                    }
                };
                ArrayUtilities.outs_for_amountArray_Destroy(result);
                callback(ec, _result);
            };
            getRandomOutsByAmounts(GCHandle.ToIntPtr(gch), (UInt64)amounts.Count, outsCount, _callback);
            // Unimplemented
        }

        private void getNewBlocks(in IntPtr knownBlockIds, UInt64 knownBlockIdsCount, Action<ErrorCode, IntPtr, UInt64, UInt32> callback)
        {
            IntPtr newBlocks = IntPtr.Zero;
            UInt64 newBlocksCount = 0;
            UInt32 startHeight = 0;
            Callback _callback = (ec) =>
            {
                callback(ec, newBlocks, newBlocksCount, startHeight);
            };
            GCHandle gch = GCHandle.Alloc(_callback);
            UnmanagedNodeRpcProxy.NodeRpcProxy_getNewBlocks(wrappedClass, knownBlockIds, knownBlockIdsCount, out newBlocks, out newBlocksCount, out startHeight, GCHandle.ToIntPtr(gch));
        }
        public void getNewBlocks(in List<Crypto.Hash> knownBlockIds, Action<ErrorCode, List<RawBlock>, UInt32> callback)
        {
            Crypto.Hash[] _knownBlockIds = knownBlockIds.ToArray();
            GCHandle gch = GCHandle.Alloc(_knownBlockIds);
            Action<ErrorCode, IntPtr, UInt64, UInt32> _callback = (ec, newBlocks, newBlocksCount, startHeight) =>
            {
                List<RawBlock> _newBlocks = new List<RawBlock>();
                unsafe
                {
                    for (int i = 0; (UInt64)i < newBlocksCount; i++)
                    {
                        _newBlocks.Add(ConversionTools.convertRawBlock(Marshal.PtrToStructure<UnmanagedTalleo.RawBlock>(newBlocks + i * sizeof(UnmanagedTalleo.RawBlock))));
                    }
                }
                ArrayUtilities.RawBlockArray_Destroy(newBlocks);
                callback(ec, _newBlocks, startHeight);
            };
            getNewBlocks(GCHandle.ToIntPtr(gch), (UInt64)knownBlockIds.Count, _callback);

        }

        private void getTransactionOutsGlobalIndices(in Crypto.Hash transactionHash, Action<ErrorCode, IntPtr, UInt64> callback)
        {
            IntPtr outsGlobalIndices = IntPtr.Zero;
            UInt64 outsGlobalIndicesCount = 0;
            Callback _callback = (ec) =>
            {
                callback(ec, outsGlobalIndices, outsGlobalIndicesCount);
            };
            GCHandle gch = GCHandle.Alloc(_callback);
            UnmanagedNodeRpcProxy.NodeRpcProxy_getTransactionOutsGlobalIndices(wrappedClass, transactionHash, out outsGlobalIndices, out outsGlobalIndicesCount, GCHandle.ToIntPtr(gch));
        }
        public void getTransactionOutsGlobalIndices(in Crypto.Hash transactionHash, Action<ErrorCode, List<UInt32>> callback)
        {
            Action<ErrorCode, IntPtr, UInt64> _callback = (ec, outsGlobalIndices, outsGlobalIndicesCount) =>
            {
                List<UInt32> _outsGlobalIndices = new List<UInt32>();
                unsafe
                {
                    for (int i = 0; (UInt64)i < outsGlobalIndicesCount; i++)
                    {
                        _outsGlobalIndices.Add((UInt32)(object)Marshal.ReadInt32(outsGlobalIndices + i * sizeof(UInt32)));
                    }
                }
                ArrayUtilities.UInt32Array_Destroy(outsGlobalIndices);
                callback(ec, _outsGlobalIndices);
            };
            getTransactionOutsGlobalIndices(transactionHash, _callback);
        }

        private void queryBlocks(in IntPtr knownBlockIds, in UInt64 knownBlockIdsCount, in UInt64 timestamp, Action<ErrorCode, IntPtr, UInt64, UInt32> callback)
        {
            IntPtr newBlocks = IntPtr.Zero;
            UInt64 newBlocksCount = 0;
            UInt32 startHeight = 0;
            Callback _callback = (ec) =>
            {
                callback(ec, newBlocks, newBlocksCount, startHeight);
            };
            GCHandle gch = GCHandle.Alloc(_callback);
            UnmanagedNodeRpcProxy.NodeRpcProxy_queryBlocks(wrappedClass, knownBlockIds, knownBlockIdsCount, timestamp, out newBlocks, out newBlocksCount, out startHeight, GCHandle.ToIntPtr(gch));
        }
        public void queryBlocks(in List<Crypto.Hash> knownBlockIds, in UInt64 timestamp, Action<ErrorCode, List<BlockShortEntry>, UInt32> callback)
        {
            Crypto.Hash[] _knownBlockIds = knownBlockIds.ToArray();
            GCHandle gch = GCHandle.Alloc(_knownBlockIds);

            Action<ErrorCode, IntPtr, UInt64, UInt32> _callback = (ec, newBlocks, newBlocksCount, startHeight) =>
            {
                List<BlockShortEntry> blocks = new List<BlockShortEntry>();
                unsafe
                {
                    for (int i = 0; (UInt64)i < newBlocksCount; i++)
                    {
                        blocks.Add(ConversionTools.convertBlockShortEntry(Marshal.PtrToStructure<UnmanagedTalleo.BlockShortEntry>(newBlocks + i * sizeof(UnmanagedTalleo.BlockShortEntry))));
                    }
                }
                ArrayUtilities.BlockShortEntryArray_Destroy(newBlocks);
                callback(ec, blocks, startHeight);
            };
            queryBlocks(GCHandle.ToIntPtr(gch), (UInt64)knownBlockIds.Count, timestamp, _callback);
        }

        private void getPoolSymmetricDifference(IntPtr knownPoolTxIds, UInt64 knownPoolTxIdsCount, Crypto.Hash knownBlockId, Action<ErrorCode, bool, IntPtr, UInt64, IntPtr, UInt64> callback)
        {
            bool isBcActual = false;
            IntPtr newTxs = IntPtr.Zero;
            UInt64 newTxCount = 0;
            IntPtr deletedTxIds = IntPtr.Zero;
            UInt64 deletedTxIdCount = 0;
            Callback _callback = (ec) =>
            {
                callback(ec, isBcActual, newTxs, newTxCount, deletedTxIds, deletedTxIdCount);
            };
            GCHandle gch = GCHandle.Alloc(_callback);
            UnmanagedNodeRpcProxy.NodeRpcProxy_getPoolSymmetricDifference(wrappedClass, knownPoolTxIds, knownPoolTxIdsCount, knownBlockId, ref isBcActual, out newTxs, out newTxCount, out deletedTxIds, out deletedTxIdCount, GCHandle.ToIntPtr(gch));
        }
        public void getPoolSymmetricDifference(in List<Crypto.Hash> knownPoolTxIds, Crypto.Hash knownBlockId,
          Action<ErrorCode, bool, List<ITransactionReader>, List<Crypto.Hash>> callback)
        {
            Crypto.Hash[] _knownPoolTxIds = knownPoolTxIds.ToArray();
            GCHandle gch1 = GCHandle.Alloc(_knownPoolTxIds);
            Action<ErrorCode, bool, IntPtr, UInt64, IntPtr, UInt64> _callback = (ec, isBcActual, newTxs, newTxCount, deletedTxIds, deletedTxIdCount) =>
            {
                ITransactionReader[] _newTxs = new ITransactionReader[newTxCount];
                unsafe
                {
                    for (int i = 0; (UInt64)i < newTxCount; i++)
                    {
                        _newTxs[i] = TransactionReader.wrap(Marshal.ReadIntPtr(newTxs, i * Marshal.SizeOf(typeof(IntPtr))));
                    }
                }
                ArrayUtilities.ITransactionReaderArray_Destroy(newTxs);
                Crypto.Hash[] _deletedTxIds = new Crypto.Hash[deletedTxIdCount];
                unsafe
                {
                    for (int i = 0; (UInt64)i < deletedTxIdCount; i++)
                    {
                        _deletedTxIds[i] = Marshal.PtrToStructure<Crypto.Hash>(deletedTxIds + i * sizeof(Crypto.Hash));
                    }
                }
                ArrayUtilities.HashArray_Destroy(deletedTxIds);
                callback(ec, isBcActual, _newTxs.ToList(), _deletedTxIds.ToList());
            };
            getPoolSymmetricDifference(GCHandle.ToIntPtr(gch1), (UInt64)knownPoolTxIds.Count, knownBlockId, _callback);
        }

        private void getBlocksByHeights(IntPtr blockHeights, UInt64 blockHeightsCount, Action<ErrorCode, IntPtr, UInt64> callback)
        {
            IntPtr _results = IntPtr.Zero;
            UInt64 resultsCount = 0;
            Callback _callback = (ec) =>
            {
                callback(ec, _results, resultsCount);
            };
            GCHandle gch = GCHandle.Alloc(_callback);
            UnmanagedNodeRpcProxy.NodeRpcProxy_getBlocksByHeights(wrappedClass, blockHeights, blockHeightsCount, out _results, out resultsCount, GCHandle.ToIntPtr(gch));
        }
        public void getBlocks(in List<UInt32> blockHeights, Action<ErrorCode, List<List<BlockDetails>>> callback)
        {
            UInt32[] _blockHeights = blockHeights.ToArray();
            GCHandle gch = GCHandle.Alloc(_blockHeights);
            Action<ErrorCode, IntPtr, UInt64> _callback = (ec, blockDetailsArrays, blockDetailsArraysCount) =>
            {
                List<List<BlockDetails>> _blockDetailsList = new List<List<BlockDetails>>();
                for (int i = 0; (UInt64)i < blockDetailsArraysCount; i++)
                {
                    unsafe
                    {
                        List<BlockDetails> _blockDetails = new List<BlockDetails>();
                        UnmanagedTalleo.BlockDetailsArray _blockDetailsArray = Marshal.PtrToStructure<UnmanagedTalleo.BlockDetailsArray>(blockDetailsArrays + i * sizeof(UnmanagedTalleo.BlockDetailsArray));
                        for (int j = 0; (UInt64)j < _blockDetailsArray.blockDetailsCount; j++)
                        {
                            _blockDetails.Add(ConversionTools.convertBlockDetails(Marshal.PtrToStructure<UnmanagedTalleo.BlockDetails>(_blockDetailsArray.blockDetails + j * sizeof(UnmanagedTalleo.BlockDetails))));
                        }
                        _blockDetailsList.Add(_blockDetails);
                    }
                }
                ArrayUtilities.BlockDetailsResultsArray_Destroy(blockDetailsArrays, blockDetailsArraysCount);
                callback(ec, _blockDetailsList);
            };
            getBlocksByHeights(GCHandle.ToIntPtr(gch), (UInt64)blockHeights.Count, _callback);
        }

        private void getBlocksByHashes(IntPtr blockHashes, UInt64 blockHashesCount, Action<ErrorCode, IntPtr, UInt64> callback)
        {
            IntPtr _blocks = IntPtr.Zero;
            UInt64 blocksCount = 0;
            Callback _callback = (ec) =>
            {
                callback(ec, _blocks, blocksCount);
            };
            GCHandle gch = GCHandle.Alloc(_callback);
            UnmanagedNodeRpcProxy.NodeRpcProxy_getBlocksByHashes(wrappedClass, blockHashes, blockHashesCount, out _blocks, out blocksCount, GCHandle.ToIntPtr(gch));
        }
        public void getBlocks(in List<Crypto.Hash> blockHashes, Action<ErrorCode, List<BlockDetails>> callback)
        {
            Crypto.Hash[] _blockHashes = blockHashes.ToArray();
            UInt64 blockHashCount = (UInt64)blockHashes.Count;
            Action<ErrorCode, IntPtr, UInt64> _callback = (ec, blockDetails, blockDetailsCount) =>
            {
                BlockDetails[] _blockDetails = new BlockDetails[blockDetailsCount];

                for (int i = 0; (UInt64)i < blockDetailsCount; i++)
                {
                    unsafe
                    {
                        _blockDetails[i] = ConversionTools.convertBlockDetails(Marshal.PtrToStructure<UnmanagedTalleo.BlockDetails>(blockDetails + i * sizeof(UnmanagedTalleo.BlockDetails)));
                    }
                }
                ArrayUtilities.BlockDetailsArray_Destroy(blockDetails);
                callback(ec, _blockDetails.ToList());
            };
            GCHandle gch = GCHandle.Alloc(_blockHashes);
            getBlocksByHashes(GCHandle.ToIntPtr(gch), blockHashCount, _callback);
        }

        private void getBlock(in UInt32 blockHeight, Action<ErrorCode, UnmanagedTalleo.BlockDetails> callback)
        {
            UnmanagedTalleo.BlockDetails _blockDetails = new UnmanagedTalleo.BlockDetails();
            Callback _callback = (ec) =>
            {
                callback(ec, _blockDetails);
            };
            GCHandle gch = GCHandle.Alloc(callback);
            UnmanagedNodeRpcProxy.NodeRpcProxy_getBlock(wrappedClass, blockHeight, out _blockDetails, GCHandle.ToIntPtr(gch));
        }
        public void getBlock(in UInt32 blockHeight, Action<ErrorCode, BlockDetails> callback)
        {
            Action<ErrorCode, UnmanagedTalleo.BlockDetails> _callback = (ec, blockDetails) =>
            {
                Talleo.BlockDetails _blockDetails = ConversionTools.convertBlockDetails(blockDetails);
                UnmanagedBlockDetails.Cleanup(blockDetails);
                callback(ec, _blockDetails);
            };
            getBlock(blockHeight, _callback);
        }

        private void getTransactions(IntPtr transactionHashes, UInt64 transactionHashCount, Action<ErrorCode, IntPtr, UInt64> callback)
        {
            IntPtr _transactions = IntPtr.Zero;
            UInt64 transactionCount = 0;
            Callback _callback = (ec) =>
            {
                callback(ec, _transactions, transactionCount);
            };
            GCHandle gch = GCHandle.Alloc(_callback);
            UnmanagedNodeRpcProxy.NodeRpcProxy_getTransactions(wrappedClass, transactionHashes, transactionHashCount, out _transactions, out transactionCount, GCHandle.ToIntPtr(gch));

        }
        public void getTransactions(in List<Crypto.Hash> transactionHashes, Action<ErrorCode, List<TransactionDetails>> callback)
        {
            Crypto.Hash[] _transactionHashes = transactionHashes.ToArray();
            GCHandle gch1 = GCHandle.Alloc(_transactionHashes);
            Action<ErrorCode, IntPtr, UInt64> _callback = (ec, transactionDetails, transactionDetailCount) =>
            {
                TransactionDetails[] _transactionDetails = new TransactionDetails[transactionDetailCount];

                for (int i = 0; (UInt64)i < transactionDetailCount; i++)
                {
                    unsafe
                    {
                        _transactionDetails[i] = ConversionTools.convertTransactionDetails(Marshal.PtrToStructure<UnmanagedTalleo.TransactionDetails>(transactionDetails + i * sizeof(UnmanagedTalleo.TransactionDetails)));
                    }
                }
                ArrayUtilities.TransactionDetailsArray_Destroy(transactionDetails);
                callback(ec, _transactionDetails.ToList());
            };
            getTransactions(GCHandle.ToIntPtr(gch1), (UInt64)transactionHashes.Count, _callback);
            // Unimplemented
        }
        public void getFeeAddress(Action<ErrorCode, String> callback)
        {
            IntPtr _feeAddress = IntPtr.Zero;
            Callback _callback = (ec) =>
            {
                string feeAddress = Marshal.PtrToStringAnsi(_feeAddress);
                ArrayUtilities.String_Destroy(_feeAddress);
                callback(ec, feeAddress);
            };
            GCHandle gch = GCHandle.Alloc(_callback);
            UnmanagedNodeRpcProxy.NodeRpcProxy_getFeeAddress(wrappedClass, out _feeAddress, GCHandle.ToIntPtr(gch));
        }
        public void getCollateralHash(Action<ErrorCode, String> callback)
        {
            IntPtr _collateralHash = IntPtr.Zero;
            Callback _callback = (ec) =>
            {
                string collateralHash = Marshal.PtrToStringAnsi(_collateralHash);
                ArrayUtilities.String_Destroy(_collateralHash);
                callback(ec, collateralHash);
            };
            GCHandle gch = GCHandle.Alloc(_callback);
            UnmanagedNodeRpcProxy.NodeRpcProxy_getCollateralHash(wrappedClass, out _collateralHash, GCHandle.ToIntPtr(gch));
        }

        public void isSynchronized(Action<ErrorCode, bool> callback)
        {
            bool syncStatus = false;
            Callback _callback = (ec) =>
            {
                callback(ec, syncStatus);
            };
            GCHandle gch = GCHandle.Alloc(_callback);
            UnmanagedNodeRpcProxy.NodeRpcProxy_isSynchronized(wrappedClass, ref syncStatus, GCHandle.ToIntPtr(gch));
        }

        public UInt32 rpcTimeout()
        {
            return UnmanagedNodeRpcProxy.NodeRpcProxy_getRpcTimeout(wrappedClass);
        }
        public void rpcTimeout(UInt32 val)
        {
            UnmanagedNodeRpcProxy.NodeRpcProxy_setRpcTimeout(wrappedClass, val);
        }

        public IntPtr unwrap()
        {
            return wrappedClass;
        }

        public static NodeRpcProxy wrap(IntPtr node)
        {
            return new NodeRpcProxy(node);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace UnmanagedTalleo
{
    public struct WalletOuts
    {
        public IntPtr wallet;
        public IntPtr outs;
        public UInt64 outsCount;
    };

    public struct TransactionsInBlockInfo
    {
        public Crypto.Hash blockHash;
        public IntPtr transactions;
        public UInt64 transactionsCount;
    };

    public struct WalletTransaction
    {
        public Talleo.WalletTransactionState state;
        public UInt64 timestamp;
        public UInt32 blockHeight;
        public Crypto.Hash hash;
        public Int64 totalAmount;
        public UInt64 fee;
        public UInt64 creationTime;
        public UInt64 unlockTime;
        public IntPtr extra;
        public bool isBase;
    };

    public struct WalletTransactionWithTransfers
    {
        public WalletTransaction transaction;
        public IntPtr transfers;
        public UInt64 transfersCount;
    };

};
namespace Talleo
{
    public enum WalletTransactionState : Byte
    {
        SUCCEEDED = 0,
        FAILED,
        CANCELLED,
        CREATED,
        DELETED
    };

    public enum WalletEventType
    {
        TRANSACTION_CREATED,
        TRANSACTION_UPDATED,
        BALANCE_UNLOCKED,
        SYNC_PROGRESS_UPDATED,
        SYNC_COMPLETED,
    };

    public enum WalletSaveLevel : Byte
    {
        SAVE_KEYS_ONLY,
        SAVE_KEYS_AND_TRANSACTIONS,
        SAVE_ALL
    };

    public struct WalletTransactionCreatedData
    {
        public UInt64 transactionIndex;
    };

    public struct WalletTransactionUpdatedData
    {
        public UInt64 transactionIndex;
    };

    public struct WalletSynchronizationProgressUpdated
    {
        public UInt32 processedBlockCount;
        public UInt32 totalBlockCount;
    };

    [StructLayout(LayoutKind.Explicit)]
    public struct WalletEvent
    {
        [FieldOffset(0)] WalletEventType type;
        [FieldOffset(1)] WalletTransactionCreatedData transactionCreated;
        [FieldOffset(1)] WalletTransactionUpdatedData transactionUpdated;
        [FieldOffset(1)] WalletSynchronizationProgressUpdated synchronizationProgressUpdated;
    };

    public struct WalletTransaction
    {
        public WalletTransactionState state;
        public UInt64 timestamp;
        public UInt32 blockHeight;
        public Crypto.Hash hash;
        public Int64 totalAmount;
        public UInt64 fee;
        public UInt64 creationTime;
        public UInt64 unlockTime;
        public string extra;
        public bool isBase;
    };

    public enum WalletTransferType : Byte
    {
        USUAL = 0,
        DONATION,
        CHANGE
    };

    public struct WalletOrder
    {
        public string address;
        public UInt64 amount;
    };

    public struct WalletTransfer
    {
        public WalletTransferType type;
        public string address;
        public Int64 amount;
    };

    public struct DonationSettings
    {
        public string address;
        public UInt64 threshold;
    };

    public struct TransactionParameters
    {
        public List<string> sourceAddresses;
        public List<WalletOrder> destinations;
        public UInt64 fee;
        public UInt16 mixIn;
        public string extra;
        public UInt64 unlockTimestamp;
        public DonationSettings donation;
        public string changeDestination;
    };

    public struct WalletTransactionWithTransfers
    {
        public WalletTransaction transaction;
        public List<WalletTransfer> transfers;
    };

    public struct TransactionsInBlockInfo
    {
        public Crypto.Hash blockHash;
        public List<WalletTransactionWithTransfers> transactions;
    };
};

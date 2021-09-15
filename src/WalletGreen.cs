using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
public class UnmanagedWalletGreen
{
    /* Constructor */
    [DllImport("TalleoWrapper")]
    public static extern IntPtr WalletGreen_Create(IntPtr dispatcher, IntPtr currency, IntPtr node, IntPtr logger);
    [DllImport("TalleoWrapper")]
    public static extern IntPtr WalletGreen_CreateWithSoftLockTime(IntPtr dispatcher, IntPtr currency, IntPtr node, IntPtr logger, UInt32 transactionSoftLockTime);

    /* Destructor */
    [DllImport("TalleoWrapper")]
    public static extern void WalletGreen_Destroy(IntPtr wallet);

    /* Class methods */

    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern void WalletGreen_initialize(IntPtr wallet, string path, string password);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern void WalletGreen_initializeWithViewKey(IntPtr wallet, string path, string password, in Crypto.SecretKey viewSecretKey);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern void WalletGreen_load(IntPtr wallet, string path, string password);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern void WalletGreen_loadWithExtra(IntPtr wallet, string path, string password, string extra);
    [DllImport("TalleoWrapper")]
    public static extern void WalletGreen_shutdown(IntPtr wallet);

    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern void WalletGreen_changePassword(IntPtr wallet, string oldPassword, string newPassword);
    [DllImport("TalleoWrapper")]
    public static extern void WalletGreen_save(IntPtr wallet);
    [DllImport("TalleoWrapper")]
    public static extern void WalletGreen_saveWithLevel(IntPtr wallet, Talleo.WalletSaveLevel saveLevel);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern void WalletGreen_saveWithLevelExtra(IntPtr wallet, Talleo.WalletSaveLevel saveLevel, string extra);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern void WalletGreen_exportWallet(IntPtr wallet, string path);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern void WalletGreen_exportWalletWithEncrypt(IntPtr wallet, string path, bool encrypt);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern void WalletGreen_exportWalletWithEncryptLevel(IntPtr wallet, string path, bool encrypt, Talleo.WalletSaveLevel saveLevel);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern void WalletGreen_exportWalletWithEncryptLevelExtra(IntPtr wallet, string path, bool encrypt, Talleo.WalletSaveLevel saveLevel, string extra);

    [DllImport("TalleoWrapper")]
    public static extern UInt64 WalletGreen_getAddressCount(IntPtr wallet);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern string WalletGreen_getAddress(IntPtr wallet, UInt64 index);
    [DllImport("TalleoWrapper")]
    public static extern Talleo.KeyPair WalletGreen_getAddressSpendKeyWithIndex(IntPtr wallet, UInt64 index);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern Talleo.KeyPair WalletGreen_getAddressSpendKey(IntPtr wallet, string address);
    [DllImport("TalleoWrapper")]
    public static extern Talleo.KeyPair WalletGreen_getViewKey(IntPtr wallet);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern string WalletGreen_createAddress(IntPtr wallet);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern string WalletGreen_createAddressWithSecretKey(IntPtr wallet, in Crypto.SecretKey spendSecretKey);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern string WalletGreen_createAddressWithPublicKey(IntPtr wallet, in Crypto.PublicKey spendPublicKey);
    [DllImport("TalleoWrapper")]
    public static extern void WalletGreen_createAddressList(IntPtr wallet, in IntPtr spendSecretKeys, UInt64 keyCount, out IntPtr addresses, out UInt64 addressCount);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern void WalletGreen_deleteAddress(IntPtr wallet, string address);

    [DllImport("TalleoWrapper")]
    public static extern UInt64 WalletGreen_getActualBalance(IntPtr wallet);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern UInt64 WalletGreen_getActualBalanceWithAddress(IntPtr wallet, string address);
    [DllImport("TalleoWrapper")]
    public static extern UInt64 WalletGreen_getPendingBalance(IntPtr wallet);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern UInt64 WalletGreen_getPendingBalanceWithAddress(IntPtr wallet, string address);

    public enum WalletTransferType : Byte
    {
        USUAL = 0,
        DONATION,
        CHANGE
    };
    public struct WalletTransfer
    {
        public WalletTransferType type;
        public IntPtr address;
        public Int64 amount;
    };


    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern UnmanagedTalleo.WalletOuts WalletGreen_getUnspentOutputs(IntPtr wallet, string address);

    [DllImport("TalleoWrapper")]
    public static extern UInt64 WalletGreen_getTransactionCount(IntPtr wallet);
    [DllImport("TalleoWrapper")]
    public static extern UnmanagedTalleo.WalletTransaction WalletGreen_getTransactionWithIndex(IntPtr wallet, UInt64 transactionIndex);
    [DllImport("TalleoWrapper")]
    public static extern UInt64 WalletGreen_getTransactionTransferCount(IntPtr wallet, UInt64 transactionIndex);
    [DllImport("TalleoWrapper")]
    public static extern WalletTransfer WalletGreen_getTransactionTransfer(IntPtr wallet, UInt64 transactionIndex, UInt64 transferIndex);

    [DllImport("TalleoWrapper")]
    public static extern UnmanagedTalleo.WalletTransactionWithTransfers WalletGreen_getTransactionWithHash(IntPtr wallet, in Crypto.Hash transactionHash);
    [DllImport("TalleoWrapper")]
    public static extern void WalletGreen_getTransactionsByHash(IntPtr wallet, in Crypto.Hash blockHash, UInt64 count, out IntPtr transactions, out UInt64 transactionCount);

    [DllImport("TalleoWrapper")]
    public static extern void WalletGreen_getTransactionsByIndex(IntPtr wallet, UInt32 blockIndex, UInt64 count, out IntPtr transactions, out UInt64 transactionCount);
    [DllImport("TalleoWrapper")]
    public static extern void WalletGreen_getBlockHashes(IntPtr wallet, UInt32 blockIndex, UInt64 count, out IntPtr hashes, out UInt64 hashCount);
    [DllImport("TalleoWrapper")]
    public static extern UInt32 WalletGreen_getBlockCount(IntPtr wallet);
    [DllImport("TalleoWrapper")]
    public static extern void WalletGreen_getUnconfirmedTransactions(IntPtr wallet, out IntPtr transactions, out UInt64 transactionCount);
    [DllImport("TalleoWrapper")]
    public static extern void WalletGreen_getDelayedTransactionIds(IntPtr wallet, out IntPtr ids, out UInt64 idCount);

    public struct WalletOrder
    {
        public IntPtr address;
        public UInt64 amount;
    };

    public struct DonationSettings
    {
        public IntPtr address;
        public UInt64 threshold;
    };

    public struct TransactionParameters
    {
        public IntPtr sourceAddresses;
        public UInt64 sourceAddressCount;
        public IntPtr destinations;
        public UInt64 destinationCount;
        public UInt64 fee;
        public UInt16 mixIn;
        public IntPtr extra;
        public UInt64 unlockTimestamp;
        public DonationSettings donation;
        public IntPtr changeDestination;
    };

    [DllImport("TalleoWrapper")]
    public static extern UInt64 WalletGreen_transfer(IntPtr wallet, in TransactionParameters sendingTransaction);

    [DllImport("TalleoWrapper")]
    public static extern UInt64 WalletGreen_makeTransaction(IntPtr wallet, in TransactionParameters sendingTransaction);
    [DllImport("TalleoWrapper")]
    public static extern void WalletGreen_commitTransaction(IntPtr wallet, UInt64 transactionId);
    [DllImport("TalleoWrapper")]
    public static extern void WalletGreen_rollbackUncommitedTransaction(IntPtr wallet, UInt64 transactionId);
    [DllImport("TalleoWrapper")]
    public static extern bool WalletGreen_txIsTooLarge(IntPtr wallet, in TransactionParameters sendingTransaction);
    [DllImport("TalleoWrapper")]
    public static extern UInt64 WalletGreen_getTxSize(IntPtr wallet, in TransactionParameters sendingTransaction);
    [DllImport("TalleoWrapper")]
    public static extern void WalletGreen_updateInternalCache(IntPtr wallet);
    [DllImport("TalleoWrapper")]
    public static extern void WalletGreen_clearCaches(IntPtr wallet, bool clearTransactions, bool clearCachedData);
    [DllImport("TalleoWrapper")]
    public static extern void WalletGreen_clearCacheAndShutdown(IntPtr wallet);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern void WalletGreen_createViewWallet(IntPtr wallet, string path, string password, string address, in Crypto.SecretKey viewSecretKey);

    [DllImport("TalleoWrapper")]
    public static extern void WalletGreen_start(IntPtr wallet);
    [DllImport("TalleoWrapper")]
    public static extern void WalletGreen_stop(IntPtr wallet);

    [DllImport("TalleoWrapper")]
    public static extern Talleo.WalletEvent WalletGreen_getEvent(IntPtr wallet);

    [DllImport("TalleoWrapper")]
    public static extern UInt64 WalletGreen_createFusionTransaction(IntPtr wallet, UInt64 threshold, UInt16 mixin);
    [DllImport("TalleoWrapper")]
    public static extern UInt64 WalletGreen_createFusionTransactionWithSources(IntPtr wallet, UInt64 threshold, UInt16 mixin, IntPtr sourceAddresses, UInt64 addressCount);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)]
    public static extern UInt64 WalletGreen_createFusionTransactionWithSourcesDestination(IntPtr wallet, UInt64 threshold, UInt16 mixin, IntPtr sourceAddresses, UInt64 addressCount, string destinationAddress);
    [DllImport("TalleoWrapper")]
    public static extern bool WalletGreen_isFusionTransaction(IntPtr wallet, UInt64 transactionId);
    [DllImport("TalleoWrapper")]
    public static extern Talleo.IFusionManager.EstimateResult WalletGreen_estimate(IntPtr wallet, UInt64 threshold);
    [DllImport("TalleoWrapper")]
    public static extern Talleo.IFusionManager.EstimateResult WalletGreen_estimateWithSources(IntPtr wallet, UInt64 threshold, IntPtr sourceAddresses, UInt64 addressCount);

    /* Utility functions */
    [DllImport("TalleoWrapper")]
    public static extern void CWalletTransactionWithTransfers_Cleanup(in UnmanagedTalleo.WalletTransactionWithTransfers transaction);
    [DllImport("TalleoWrapper")]
    public static extern void CWalletTransactionWithTransfersList_Destroy(IntPtr list, UInt64 count);
    [DllImport("TalleoWrapper")]
    public static extern void CTransactionsInBlockInfo_Cleanup(in UnmanagedTalleo.TransactionsInBlockInfo transactions);
    [DllImport("TalleoWrapper")]
    public static extern void CTransactionsInBlockInfoList_Destroy(IntPtr list, UInt64 count);
    [DllImport("TalleoWrapper")]
    public static extern void AddressList_Destroy(IntPtr list, UInt64 count);
}

namespace Talleo
{
    public class WalletGreen : IWrapper
    {
        IntPtr wrappedClass;

        /* Constructor */
        public WalletGreen(Dispatcher dispatcher, Currency currency, INode node, Logging.ILogger logger)
        {
            wrappedClass = UnmanagedWalletGreen.WalletGreen_Create(dispatcher.unwrap(), currency.unwrap(), node.unwrap(), logger.unwrap());
        }
        public WalletGreen(Dispatcher dispatcher, Currency currency, INode node, Logging.ILogger logger, UInt32 transactionSoftLockTime)
        {
            wrappedClass = UnmanagedWalletGreen.WalletGreen_CreateWithSoftLockTime(dispatcher.unwrap(), currency.unwrap(), node.unwrap(), logger.unwrap(), transactionSoftLockTime);
        }
        protected WalletGreen(IntPtr wallet)
        {
            wrappedClass = wallet;
        }

        /* Destructor */
        ~WalletGreen()
        {
            UnmanagedWalletGreen.WalletGreen_Destroy(wrappedClass);
        }

        /* Class methods */
        void initialize(string path, string password)
        {
            UnmanagedWalletGreen.WalletGreen_initialize(wrappedClass, path, password);
        }
        void initialize(string path, string password, in Crypto.SecretKey viewSecretKey)
        {
            UnmanagedWalletGreen.WalletGreen_initializeWithViewKey(wrappedClass, path, password, viewSecretKey);
        }
        void load(string path, string password)
        {
            UnmanagedWalletGreen.WalletGreen_load(wrappedClass, path, password);
        }
        void load(string path, string password, string extra)
        {
            UnmanagedWalletGreen.WalletGreen_loadWithExtra(wrappedClass, path, password, extra);
        }
        void shutdown()
        {
            UnmanagedWalletGreen.WalletGreen_shutdown(wrappedClass);
        }

        void changePassword(string oldPassword, string newPassword)
        {
            UnmanagedWalletGreen.WalletGreen_changePassword(wrappedClass, oldPassword, newPassword);
        }
        void save()
        {
            UnmanagedWalletGreen.WalletGreen_save(wrappedClass);
        }
        void save(Talleo.WalletSaveLevel saveLevel)
        {
            UnmanagedWalletGreen.WalletGreen_saveWithLevel(wrappedClass, saveLevel);
        }
        void save(Talleo.WalletSaveLevel saveLevel, string extra)
        {
            UnmanagedWalletGreen.WalletGreen_saveWithLevelExtra(wrappedClass, saveLevel, extra);
        }
        void exportWallet(string path)
        {
            UnmanagedWalletGreen.WalletGreen_exportWallet(wrappedClass, path);
        }
        void exportWallet(string path, bool encrypt)
        {
            UnmanagedWalletGreen.WalletGreen_exportWalletWithEncrypt(wrappedClass, path, encrypt);
        }
        void exportWallet(string path, bool encrypt, Talleo.WalletSaveLevel saveLevel)
        {
            UnmanagedWalletGreen.WalletGreen_exportWalletWithEncryptLevel(wrappedClass, path, encrypt, saveLevel);
        }
        void exportWallet(string path, bool encrypt, Talleo.WalletSaveLevel saveLevel, string extra)
        {
            UnmanagedWalletGreen.WalletGreen_exportWalletWithEncryptLevelExtra(wrappedClass, path, encrypt, saveLevel, extra);
        }

        UInt64 getAddressCount()
        {
            return UnmanagedWalletGreen.WalletGreen_getAddressCount(wrappedClass);
        }
        string getAddress(UInt64 index)
        {
            return UnmanagedWalletGreen.WalletGreen_getAddress(wrappedClass, index);
        }
        Talleo.KeyPair getAddressSpendKey(UInt64 index)
        {
            return UnmanagedWalletGreen.WalletGreen_getAddressSpendKeyWithIndex(wrappedClass, index);
        }
        Talleo.KeyPair getAddressSpendKey(string address)
        {
            return UnmanagedWalletGreen.WalletGreen_getAddressSpendKey(wrappedClass, address);
        }
        Talleo.KeyPair getViewKey()
        {
            return UnmanagedWalletGreen.WalletGreen_getViewKey(wrappedClass);
        }
        string createAddress()
        {
            return UnmanagedWalletGreen.WalletGreen_createAddress(wrappedClass);
        }
        string createAddress(in Crypto.SecretKey spendSecretKey)
        {
            return UnmanagedWalletGreen.WalletGreen_createAddressWithSecretKey(wrappedClass, spendSecretKey);
        }
        string createAddress(in Crypto.PublicKey spendPublicKey)
        {
            return UnmanagedWalletGreen.WalletGreen_createAddressWithPublicKey(wrappedClass, spendPublicKey);
        }
        void createAddressList(List<Crypto.SecretKey> spendSecretKeys, out List<string> addresses)
        {
            Crypto.SecretKey[] _spendSecretKeys = spendSecretKeys.ToArray();
            IntPtr _addresses;
            UInt64 addressCount;
            addresses = new List<string>();
            GCHandle gch = GCHandle.Alloc(_spendSecretKeys);
            UnmanagedWalletGreen.WalletGreen_createAddressList(wrappedClass, GCHandle.ToIntPtr(gch), (UInt64)spendSecretKeys.Count, out _addresses, out addressCount);
            for (int i = 0; (UInt64)i < addressCount; i++)
            {
                IntPtr _address = Marshal.ReadIntPtr(_addresses + (i * IntPtr.Size));
                string _address2 = Marshal.PtrToStringAnsi(_address);
                addresses.Add(_address2);
            }
            UnmanagedWalletGreen.AddressList_Destroy(_addresses, addressCount);
        }
        void deleteAddress(string address)
        {
            UnmanagedWalletGreen.WalletGreen_deleteAddress(wrappedClass, address);
        }
        UInt64 getActualBalance()
        {
            return UnmanagedWalletGreen.WalletGreen_getActualBalance(wrappedClass);
        }
        UInt64 getActualBalance(string address)
        {
            return UnmanagedWalletGreen.WalletGreen_getActualBalanceWithAddress(wrappedClass, address);
        }
        UInt64 getPendingBalance()
        {
            return UnmanagedWalletGreen.WalletGreen_getPendingBalance(wrappedClass);
        }
        UInt64 getPendingBalance(string address)
        {
            return UnmanagedWalletGreen.WalletGreen_getPendingBalanceWithAddress(wrappedClass, address);
        }

        public struct WalletRecord
        {
            public Crypto.PublicKey spendPublicKey;
            public Crypto.SecretKey spendSecretKey;
            public IntPtr container;
            public UInt64 pendingBalance;
            public UInt64 actualBalance;
            public long creationTimestamp;
        };
        public struct WalletOuts
        {
            public WalletRecord wallet;
            public List<TransactionOutputInformation> outs;
        };
        WalletOuts convertWalletOuts(UnmanagedTalleo.WalletOuts outs)
        {
            WalletOuts _outs = new WalletOuts();
            WalletRecord _record = new WalletRecord();
            Marshal.PtrToStructure(outs.wallet, _record);
            _outs.wallet = _record;
            _outs.outs = new List<TransactionOutputInformation>();
            for (int i = 0; (UInt64)i < outs.outsCount; i++)
            {
                unsafe
                {
                    TransactionOutputInformation toi = new TransactionOutputInformation();
                    Marshal.PtrToStructure(outs.outs + i * sizeof(TransactionOutputInformation), toi);
                    _outs.outs.Add(toi);
                }
            }
            ArrayUtilities.TransactionOutputInformationArray_Destroy(outs.outs);
            return _outs;
        }
        WalletOuts getUnspentOutputs(string address)
        {
            UnmanagedTalleo.WalletOuts _outs = UnmanagedWalletGreen.WalletGreen_getUnspentOutputs(wrappedClass, address);
            return convertWalletOuts(_outs);
        }

        UInt64 getTransactionCount()
        {
            return UnmanagedWalletGreen.WalletGreen_getTransactionCount(wrappedClass);
        }

        WalletTransaction convertWalletTransaction(UnmanagedTalleo.WalletTransaction transaction)
        {
            WalletTransaction wt = new WalletTransaction();
            wt.state = transaction.state;
            wt.timestamp = transaction.timestamp;
            wt.blockHeight = transaction.blockHeight;
            wt.hash = transaction.hash;
            wt.totalAmount = transaction.totalAmount;
            wt.fee = transaction.fee;
            wt.creationTime = transaction.creationTime;
            wt.unlockTime = transaction.unlockTime;
            wt.extra = Marshal.PtrToStringAnsi(transaction.extra);
            wt.isBase = transaction.isBase;
            return wt;
        }
        WalletTransaction getTransaction(UInt64 transactionIndex)
        {
            UnmanagedTalleo.WalletTransaction uwt = UnmanagedWalletGreen.WalletGreen_getTransactionWithIndex(wrappedClass, transactionIndex);
            return convertWalletTransaction(uwt);
        }
        UInt64 getTransactionTransferCount(UInt64 transactionIndex)
        {
            return UnmanagedWalletGreen.WalletGreen_getTransactionTransferCount(wrappedClass, transactionIndex);
        }

        WalletTransfer convertWalletTransfer(UnmanagedWalletGreen.WalletTransfer transfer)
        {
            WalletTransfer wt = new WalletTransfer();
            wt.type = (Talleo.WalletTransferType)transfer.type;
            wt.address = Marshal.PtrToStringAnsi(transfer.address);
            wt.amount = transfer.amount;
            return wt;
        }
        WalletTransfer getTransactionTransfer(UInt64 transactionIndex, UInt64 transferIndex)
        {
            UnmanagedWalletGreen.WalletTransfer wt = UnmanagedWalletGreen.WalletGreen_getTransactionTransfer(wrappedClass, transactionIndex, transferIndex);
            return convertWalletTransfer(wt);
        }
        WalletTransactionWithTransfers convertWalletTransactionWithTransfers(UnmanagedTalleo.WalletTransactionWithTransfers transaction)
        {
            WalletTransactionWithTransfers wt = new WalletTransactionWithTransfers();
            wt.transaction = convertWalletTransaction(transaction.transaction);
            wt.transfers = new List<WalletTransfer>();
            for (int i = 0; (UInt64)i < transaction.transfersCount; i++)
            {
                unsafe
                {
                    UnmanagedWalletGreen.WalletTransfer uwt = new UnmanagedWalletGreen.WalletTransfer();
                    Marshal.PtrToStructure(transaction.transfers + i * sizeof(UnmanagedWalletGreen.WalletTransfer), uwt);
                    wt.transfers.Add(convertWalletTransfer(uwt));
                }
            }
            UnmanagedWalletGreen.CWalletTransactionWithTransfers_Cleanup(transaction);
            return wt;
        }
        WalletTransactionWithTransfers getTransaction(in Crypto.Hash transactionHash)
        {
            UnmanagedTalleo.WalletTransactionWithTransfers transfer = UnmanagedWalletGreen.WalletGreen_getTransactionWithHash(wrappedClass, transactionHash);
            return convertWalletTransactionWithTransfers(transfer);
        }

        TransactionsInBlockInfo convertTransactionsInBlockInfo(UnmanagedTalleo.TransactionsInBlockInfo tibi)
        {
            TransactionsInBlockInfo _tibi = new TransactionsInBlockInfo();
            _tibi.blockHash = tibi.blockHash;
            _tibi.transactions = new List<WalletTransactionWithTransfers>();
            for (int i = 0; (UInt64)i < tibi.transactionsCount; i++)
            {
                unsafe
                {
                    UnmanagedTalleo.WalletTransactionWithTransfers wtwt = new UnmanagedTalleo.WalletTransactionWithTransfers();
                    Marshal.PtrToStructure(tibi.transactions + i * sizeof(UnmanagedTalleo.WalletTransactionWithTransfers), wtwt);
                    _tibi.transactions.Add(convertWalletTransactionWithTransfers(wtwt));
                }
            }
            UnmanagedWalletGreen.CTransactionsInBlockInfo_Cleanup(tibi);
            return _tibi;
        }
        void getTransactions(Crypto.Hash blockHash, UInt64 count, List<TransactionsInBlockInfo> transactions)
        {
            IntPtr _transactions;
            UInt64 transactionCount;
            UnmanagedWalletGreen.WalletGreen_getTransactionsByHash(wrappedClass, blockHash, count, out _transactions, out transactionCount);
            for (int i = 0; (UInt64)i < transactionCount; i++)
            {
                unsafe
                {
                    UnmanagedTalleo.TransactionsInBlockInfo tibi = new UnmanagedTalleo.TransactionsInBlockInfo();
                    Marshal.PtrToStructure(_transactions + i * sizeof(UnmanagedTalleo.TransactionsInBlockInfo), tibi);
                    transactions.Add(convertTransactionsInBlockInfo(tibi));
                }
            }
            ArrayUtilities.TransactionsInBlockInfo_Destroy(_transactions);
        }
        void getTransactions(UInt32 blockIndex, UInt64 count, List<TransactionsInBlockInfo> transactions)
        {
            IntPtr _transactions;
            UInt64 transactionCount;
            UnmanagedWalletGreen.WalletGreen_getTransactionsByIndex(wrappedClass, blockIndex, count, out _transactions, out transactionCount);
            for (int i = 0; (UInt64)i < transactionCount; i++)
            {
                unsafe
                {
                    UnmanagedTalleo.TransactionsInBlockInfo tibi = new UnmanagedTalleo.TransactionsInBlockInfo();
                    Marshal.PtrToStructure(_transactions + i * sizeof(UnmanagedTalleo.TransactionsInBlockInfo), tibi);
                    transactions.Add(convertTransactionsInBlockInfo(tibi));
                }
            }
            ArrayUtilities.TransactionsInBlockInfo_Destroy(_transactions);
        }

        void getBlockHashes(UInt32 blockIndex, UInt64 count, List<Crypto.Hash> hashes)
        {
            IntPtr _hashes;
            UInt64 hashCount;
            UnmanagedWalletGreen.WalletGreen_getBlockHashes(wrappedClass, blockIndex, count, out _hashes, out hashCount);
            for (int i = 0; (UInt64)i < hashCount; i++)
            {
                unsafe
                {
                    Crypto.Hash hash = new Crypto.Hash();
                    Marshal.PtrToStructure(_hashes + i * sizeof(Crypto.Hash), hash);
                    hashes.Add(hash);
                }
            }
            ArrayUtilities.HashArray_Destroy(_hashes);
        }

        UInt32 getBlockCount()
        {
            return UnmanagedWalletGreen.WalletGreen_getBlockCount(wrappedClass);
        }
        void GetUnconfirmedTransactions(out List<WalletTransactionWithTransfers> transactions)
        {
            IntPtr _transactions;
            UInt64 transactionCount;
            UnmanagedWalletGreen.WalletGreen_getUnconfirmedTransactions(wrappedClass, out _transactions, out transactionCount);
            transactions = new List<WalletTransactionWithTransfers>();
            for (int i = 0; (UInt64)i < transactionCount; i++)
            {
                unsafe
                {
                    UnmanagedTalleo.WalletTransactionWithTransfers _transaction = new UnmanagedTalleo.WalletTransactionWithTransfers();
                    Marshal.PtrToStructure(_transactions + i * sizeof(UnmanagedTalleo.WalletTransactionWithTransfers), _transaction);
                    transactions.Add(convertWalletTransactionWithTransfers(_transaction));
                }
            }
            UnmanagedWalletGreen.CWalletTransactionWithTransfersList_Destroy(_transactions, transactionCount);
        }
        void getDelayedTransactionIds(List<UInt64> ids)
        {
            IntPtr _ids;
            UInt64 idCount;
            UnmanagedWalletGreen.WalletGreen_getDelayedTransactionIds(wrappedClass, out _ids, out idCount);
            ids = new List<UInt64>();
            for (int i = 0; (UInt64)i < idCount; i++)
            {
                unsafe
                {
                    UInt64[] idsArray = new UInt64[idCount];
                    Marshal.Copy(_ids + i * sizeof(UInt64), (long[])(object)idsArray, 0, (int)idCount);
                    ids = idsArray.ToList();
                }
            }
            ArrayUtilities.UInt64Array_Destroy(_ids);
        }
        UnmanagedWalletGreen.WalletOrder convertWalletOrder(in WalletOrder order)
        {
            UnmanagedWalletGreen.WalletOrder _order = new UnmanagedWalletGreen.WalletOrder();
            _order.address = Marshal.StringToHGlobalAnsi(order.address);
            _order.amount = order.amount;
            return _order;
        }

        void cleanupWalletOrder(in UnmanagedWalletGreen.WalletOrder order)
        {
            Marshal.FreeHGlobal(order.address);
        }
        UnmanagedWalletGreen.DonationSettings convertDonationSettings(in DonationSettings donation)
        {
            UnmanagedWalletGreen.DonationSettings _donation = new UnmanagedWalletGreen.DonationSettings();
            _donation.address = Marshal.StringToHGlobalAnsi(donation.address);
            _donation.threshold = donation.threshold;
            return _donation;
        }

        void cleanupDonationSettings(in UnmanagedWalletGreen.DonationSettings donation)
        {
            Marshal.FreeHGlobal(donation.address);
        }

        UnmanagedWalletGreen.TransactionParameters convertTransactionParameters(in TransactionParameters transaction)
        {
            UnmanagedWalletGreen.TransactionParameters _transaction = new UnmanagedWalletGreen.TransactionParameters();
            IntPtr[] sourceAddresses = new IntPtr[transaction.sourceAddresses.Count];
            for (int i = 0; i < transaction.sourceAddresses.Count; i++)
            {
                sourceAddresses[i] = Marshal.StringToHGlobalAnsi(transaction.sourceAddresses[i]);
            }
            GCHandle gch1 = GCHandle.Alloc(sourceAddresses);
            _transaction.sourceAddresses = GCHandle.ToIntPtr(gch1);
            IntPtr[] destinations = new IntPtr[transaction.destinations.Count];
            for (int i = 0; i < transaction.destinations.Count; i++)
            {
                UnmanagedWalletGreen.WalletOrder _destination = new UnmanagedWalletGreen.WalletOrder();
                _destination = convertWalletOrder(transaction.destinations[i]);
                GCHandle gch2 = GCHandle.Alloc(_destination);
                destinations[i] = GCHandle.ToIntPtr(gch2);
            }
            GCHandle gch3 = GCHandle.Alloc(destinations);
            _transaction.destinations = GCHandle.ToIntPtr(gch3);
            _transaction.destinationCount = (UInt64)transaction.destinations.Count;
            _transaction.fee = transaction.fee;
            _transaction.mixIn = transaction.mixIn;
            _transaction.extra = Marshal.StringToHGlobalAnsi(transaction.extra);
            _transaction.unlockTimestamp = transaction.unlockTimestamp;
            _transaction.donation = convertDonationSettings(transaction.donation);
            _transaction.changeDestination = Marshal.StringToHGlobalAnsi(transaction.changeDestination);
            return _transaction;
        }
        void cleanupTransactionParameters(in UnmanagedWalletGreen.TransactionParameters transaction)
        {
            IntPtr[] sourceAddresses = (IntPtr[])GCHandle.FromIntPtr(transaction.sourceAddresses).Target;
            for (int i = 0; (UInt64)i < transaction.sourceAddressCount; i++)
            {
                Marshal.FreeHGlobal(sourceAddresses[i]);
            }
            Marshal.FreeHGlobal(transaction.sourceAddresses);
            IntPtr[] destinations = (IntPtr[])GCHandle.FromIntPtr(transaction.destinations).Target;
            for (int i = 0; (UInt64)i < transaction.destinationCount; i++)
            {
                Marshal.FreeHGlobal(destinations[i]);
            }
            Marshal.FreeHGlobal(transaction.destinations);

        }
        UInt64 transfer(in TransactionParameters sendingTransaction)
        {
            UnmanagedWalletGreen.TransactionParameters _sendingTransaction = convertTransactionParameters(sendingTransaction);
            UInt64 ret = UnmanagedWalletGreen.WalletGreen_transfer(wrappedClass, _sendingTransaction);
            cleanupTransactionParameters(_sendingTransaction);
            return ret;
        }

        UInt64 makeTransaction(in TransactionParameters sendingTransaction)
        {
            UnmanagedWalletGreen.TransactionParameters _sendingTransaction = convertTransactionParameters(sendingTransaction);
            UInt64 ret = UnmanagedWalletGreen.WalletGreen_makeTransaction(wrappedClass, _sendingTransaction);
            cleanupTransactionParameters(_sendingTransaction);
            return ret;
        }

        void commitTransaction(UInt64 transactionId)
        {
            UnmanagedWalletGreen.WalletGreen_commitTransaction(wrappedClass, transactionId);
        }
        void rollbackUncommitedTransaction(UInt64 transactionId)
        {
            UnmanagedWalletGreen.WalletGreen_rollbackUncommitedTransaction(wrappedClass, transactionId);
        }
        bool txIsTooLarge(in TransactionParameters sendingTransaction)
        {
            UnmanagedWalletGreen.TransactionParameters _sendingTransaction = convertTransactionParameters(sendingTransaction);
            bool ret = UnmanagedWalletGreen.WalletGreen_txIsTooLarge(wrappedClass, _sendingTransaction);
            cleanupTransactionParameters(_sendingTransaction);
            return ret;
        }
        UInt64 getTxSize(in TransactionParameters sendingTransaction)
        {
            UnmanagedWalletGreen.TransactionParameters _sendingTransaction = convertTransactionParameters(sendingTransaction);
            UInt64 ret = UnmanagedWalletGreen.WalletGreen_getTxSize(wrappedClass, _sendingTransaction);
            cleanupTransactionParameters(_sendingTransaction);
            return ret;
        }
        void updateInternalCache()
        {
            UnmanagedWalletGreen.WalletGreen_updateInternalCache(wrappedClass);
        }
        void clearCaches(bool clearTransactions, bool clearCachedData)
        {
            UnmanagedWalletGreen.WalletGreen_clearCaches(wrappedClass, clearTransactions, clearCachedData);
        }
        void clearCacheAndShutdown()
        {
            UnmanagedWalletGreen.WalletGreen_clearCacheAndShutdown(wrappedClass);
        }

        void createViewWallet(string path, string password, string address, in Crypto.SecretKey viewSecretKey)
        {
            UnmanagedWalletGreen.WalletGreen_createViewWallet(wrappedClass, path, password, address, viewSecretKey);
        }
        void start()
        {
            UnmanagedWalletGreen.WalletGreen_start(wrappedClass);
        }

        void stop()
        {
            UnmanagedWalletGreen.WalletGreen_stop(wrappedClass);
        }
        WalletEvent getEvent()
        {
            return UnmanagedWalletGreen.WalletGreen_getEvent(wrappedClass);
        }
        UInt64 createFusionTransaction(UInt64 threshold, UInt16 mixin)
        {
            return UnmanagedWalletGreen.WalletGreen_createFusionTransaction(wrappedClass, threshold, mixin);
        }

        UInt64 createFusionTransaction(UInt64 threshold, UInt16 mixin, in List<string> sourceAddresses)
        {
            IntPtr[] _sourceAddresses = new IntPtr[sourceAddresses.Count];
            UInt64 sourceAddressCount = (UInt64)sourceAddresses.Count;
            for (int i = 0; i < sourceAddresses.Count; i++)
            {
                _sourceAddresses[i] = Marshal.StringToHGlobalAnsi(sourceAddresses[i]);
            }
            GCHandle gch = GCHandle.Alloc(_sourceAddresses);
            return UnmanagedWalletGreen.WalletGreen_createFusionTransactionWithSources(wrappedClass, threshold, mixin, GCHandle.ToIntPtr(gch), sourceAddressCount);
        }
        UInt64 createFusionTransaction(UInt64 threshold, UInt16 mixin, in List<string> sourceAddresses, string destinationAddress)
        {
            IntPtr[] _sourceAddresses = new IntPtr[sourceAddresses.Count];
            UInt64 sourceAddressCount = (UInt64)sourceAddresses.Count;
            for (int i = 0; i < sourceAddresses.Count; i++)
            {
                _sourceAddresses[i] = Marshal.StringToHGlobalAnsi(sourceAddresses[i]);
            }
            GCHandle gch = GCHandle.Alloc(_sourceAddresses);
            return UnmanagedWalletGreen.WalletGreen_createFusionTransactionWithSourcesDestination(wrappedClass, threshold, mixin, GCHandle.ToIntPtr(gch), sourceAddressCount, destinationAddress);
        }
        bool isFusionTransaction(UInt64 transactionId)
        {
            return UnmanagedWalletGreen.WalletGreen_isFusionTransaction(wrappedClass, transactionId);
        }
        IFusionManager.EstimateResult estimate(UInt64 threshold)
        {
            return UnmanagedWalletGreen.WalletGreen_estimate(wrappedClass, threshold);
        }
        IFusionManager.EstimateResult estimate(UInt64 threshold, List<string> sourceAddresses)
        {
            IntPtr[] _sourceAddresses = new IntPtr[sourceAddresses.Count];
            UInt64 sourceAddressCount = (UInt64)sourceAddresses.Count;
            for (int i = 0; i < sourceAddresses.Count; i++)
            {
                _sourceAddresses[i] = Marshal.StringToHGlobalAnsi(sourceAddresses[i]);
            }
            GCHandle gch = GCHandle.Alloc(_sourceAddresses);
            return UnmanagedWalletGreen.WalletGreen_estimateWithSources(wrappedClass, threshold, GCHandle.ToIntPtr(gch), sourceAddressCount);
        }
        //IWrapper
        public IntPtr unwrap()
        {
            return wrappedClass;
        }

        static WalletGreen wrap(IntPtr stream)
        {
            return new WalletGreen(stream);
        }

    }
}
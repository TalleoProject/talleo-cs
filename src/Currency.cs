using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Difficulty = System.UInt64;
class UnmanagedCurrencyBuilder
{
    [DllImport("TalleoWrapper")]
    public static extern IntPtr CurrencyBuilder_Create(IntPtr logger);
    [DllImport("TalleoWrapper")]
    public static extern void CurrencyBuilder_Destroy(IntPtr builder);
}

class UnmanagedCurrency
{
    /* Pseudo constructor, as constructor is private */
    [DllImport("TalleoWrapper")] public static extern IntPtr Currency_Get(IntPtr builder);

    /* Destructor */
    [DllImport("TalleoWrapper")] public static extern void Currency_Destroy(IntPtr currency);

    /* Class methods */

    [DllImport("TalleoWrapper")] public static extern UInt32 Currency_maxBlockHeight(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_maxBlockBlobSize(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_maxTxSize(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_publicAddressBase58Prefix(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt32 Currency_minedMoneyUnlockWindow(IntPtr currency);

    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_timestampCheckWindow(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_blockFutureTimeLimit(IntPtr currency, UInt32 height);

    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_moneySupply(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt32 Currency_emissionSpeedFactor(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_genesisBlockReward(IntPtr currency);

    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_rewardBlocksWindow(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt32 Currency_zawyDifficultyBlockIndex(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_zawyDifficultyV2(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern Byte Currency_zawyDifficultyBlockVersion(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_blockGrantedFullRewardZone(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_blockGrantedFullRewardZoneByBlockVersion(IntPtr currency, Byte blockMajorVersion);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_minerTxBlobReservedSize(IntPtr currency);

    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_numberOfDecimalPlaces(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_coin(IntPtr currency);

    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_minimumFee(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_defaultDustThreshold(IntPtr currency);

    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_difficultyTarget(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_difficultyWindow(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_difficultyWindowByBlockVersion(IntPtr currency, Byte blockMajorVersion);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_difficultyLag(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_difficultyLagByBlockVersion(IntPtr currency, Byte blockMajorVersion);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_difficultyCut(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_difficultyCutByBlockVersion(IntPtr currency, Byte blockMajorVersion);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_difficultyBlocksCount(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_difficultyBlocksCountByBlockVersion(IntPtr currency, Byte blockMajorVersion, UInt32 height);

    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_maxBlockSizeInitial(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_maxBlockSizeGrowthSpeedNumerator(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_maxBlockSizeGrowthSpeedDenominator(IntPtr currency);

    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_lockedTxAllowedDeltaSeconds(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_lockedTxAllowedDeltaBlocks(IntPtr currency);

    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_mempoolTxLiveTime(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_mempoolTxFromAltBlockLiveTime(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_numberOfPeriodsToForgetTxDeletedFromPool(IntPtr currency);

    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_fusionTxMaxSize(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_fusionTxMinInputCount(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_fusionTxMinInOutCountRatio(IntPtr currency);

    [DllImport("TalleoWrapper")] public static extern UInt32 Currency_upgradeHeight(IntPtr currency, Byte majorVersion);
    [DllImport("TalleoWrapper")] public static extern UInt32 Currency_upgradeVotingThreshold(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt32 Currency_upgradeVotingWindow(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt32 Currency_upgradeWindow(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt32 Currency_minNumberVotingBlocks(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt32 Currency_maxUpgradeDistance(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern UInt32 Currency_calculateUpgradeHeight(IntPtr currency, UInt32 voteCompleteHeight);

    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)] public static extern string Currency_blocksFileName(IntPtr currency);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)] public static extern string Currency_blockIndexesFileName(IntPtr currency);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)] public static extern string Currency_txPoolFileName(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern bool Currency_isBlockexplorer(IntPtr currency);
    [DllImport("TalleoWrapper")] public static extern bool Currency_isTestnet(IntPtr currency);

    [DllImport("TalleoWrapper")] public static extern void Currency_genesisBlock(IntPtr currency, out UnmanagedTalleo.BlockTemplate blockTemplate);
    [DllImport("TalleoWrapper")] public static extern void Currency_genesisBlockHash(IntPtr currency, out Crypto.Hash hash);

    [DllImport("TalleoWrapper")]
    public static extern bool Currency_getBlockReward(IntPtr currency, Byte blockMajorVersion, UInt64 medianSize, UInt64 currentBlockSize, UInt64 alreadyGeneratedCoins, UInt64 fee,
    out UInt64 reward, out Int64 emissionChange);
    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_maxBlockCumulativeSize(IntPtr currency, UInt64 height);

    [DllImport("TalleoWrapper")]
    public static extern bool Currency_constructMinerTx(IntPtr currency, Byte blockMajorVersion, UInt32 height, UInt64 medianSize, UInt64 alreadyGeneratedCoins, UInt64 currentBlockSize,
    UInt64 fee, in Talleo.AccountPublicAddress minerAddress, ref UnmanagedTalleo.Transaction tx);
    [DllImport("TalleoWrapper")]
    public static extern bool Currency_constructMinerTxWithExtraNonce(IntPtr currency, Byte blockMajorVersion, UInt32 height, UInt64 medianSize, UInt64 alreadyGeneratedCoins, UInt64 currentBlockSize,
    UInt64 fee, in Talleo.AccountPublicAddress minerAddress, ref UnmanagedTalleo.Transaction tx, in UnmanagedTalleo.BinaryArray extraNonce);
    [DllImport("TalleoWrapper")]
    public static extern bool Currency_constructMinerTxWithExtraNonceMaxOuts(IntPtr currency, Byte blockMajorVersion, UInt32 height, UInt64 medianSize, UInt64 alreadyGeneratedCoins, UInt64 currentBlockSize,
    UInt64 fee, in Talleo.AccountPublicAddress minerAddress, ref UnmanagedTalleo.Transaction tx, in UnmanagedTalleo.BinaryArray extraNonce, UInt64 maxOuts);

    [DllImport("TalleoWrapper")] public static extern bool Currency_isFusionTransaction(IntPtr currency, in UnmanagedTalleo.Transaction transaction);
    [DllImport("TalleoWrapper")] public static extern bool Currency_isFusionTransactionWithSize(IntPtr currency, in UnmanagedTalleo.Transaction transaction, UInt64 size);
    [DllImport("TalleoWrapper")] public static extern bool Currency_isFusionTransactionWithInputsOutputs(IntPtr currency, IntPtr inputsAmounts, UInt64 inputsAmountsCount, IntPtr outputsAmounts, UInt64 outputsAmountsCount, UInt64 size);
    [DllImport("TalleoWrapper")] public static extern bool Currency_isAmountApplicableInFusionTransactionInput(IntPtr currency, UInt64 amount, UInt64 threshold);
    [DllImport("TalleoWrapper")] public static extern bool Currency_isAmountApplicableInFusionTransactionInputWithAmount(IntPtr currency, UInt64 amount, UInt64 threshold, out Byte amountPowerOfTen);

    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)] public static extern string Currency_accountAddressAsString(IntPtr currency, IntPtr account);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)] public static extern string Currency_accountAddressAsStringFromAddress(IntPtr currency, in Talleo.AccountPublicAddress accountPublicAddress);
    [DllImport("TalleoWrapper")] public static extern bool Currency_parseAccountAddressString(IntPtr currency, string str, out Talleo.AccountPublicAddress addr);

    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)] public static extern string Currency_formatUnsignedAmount(IntPtr currency, UInt64 amount);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)] public static extern string Currency_formatAmount(IntPtr currency, Int64 amount);
    [DllImport("TalleoWrapper", CharSet = CharSet.Ansi)] public static extern bool Currency_parseAmount(IntPtr currency, string str, out UInt64 amount);

    [DllImport("TalleoWrapper")] public static extern Difficulty Currency_nextDifficulty(IntPtr currency, Byte version, UInt32 blockIndex, IntPtr timestamps, UInt64 timestampsCount, IntPtr cumulativeDifficulties, UInt64 cumulateDifficultiesCount);

    [DllImport("TalleoWrapper")] public static extern bool Currency_checkProofOfWorkV1(IntPtr currency, IntPtr block, Difficulty currentDifficulty);
    [DllImport("TalleoWrapper")] public static extern bool Currency_checkProofOfWorkV2(IntPtr currency, IntPtr block, Difficulty currentDifficulty);
    [DllImport("TalleoWrapper")] public static extern bool Currency_checkProofOfWork(IntPtr currency, IntPtr block, Difficulty currentDifficulty);

    [DllImport("TalleoWrapper")] public static extern UInt64 Currency_getApproximateMaximumInputCount(IntPtr currency, UInt64 transactionSize, UInt64 outputCount, UInt64 mixinCount);
}

namespace Talleo
{
    using BinaryArray = List<Byte>;

    class CurrencyBuilder : IWrapper
    {
        IntPtr wrappedClass;
        public CurrencyBuilder(Logging.ILogger logger)
        {
            wrappedClass = UnmanagedCurrencyBuilder.CurrencyBuilder_Create(logger.unwrap());
        }

        protected CurrencyBuilder(IntPtr currency)
        {
            wrappedClass = currency;
        }

        ~CurrencyBuilder()
        {
            UnmanagedCurrencyBuilder.CurrencyBuilder_Destroy(wrappedClass);
        }

        Currency currency()
        {
            return Currency.wrap(UnmanagedCurrency.Currency_Get(wrappedClass));
        }

        public IntPtr unwrap()
        {
            return wrappedClass;
        }

        public static CurrencyBuilder wrap(IntPtr currency)
        {
            return new CurrencyBuilder(currency);
        }
    }

    public class Currency : IWrapper
    {
        IntPtr wrappedClass;

        protected Currency(IntPtr currency)
        {
            wrappedClass = currency;
        }
        ~Currency()
        {
            UnmanagedCurrency.Currency_Destroy(wrappedClass);
        }

        UInt32 maxBlockHeight()
        {
            return UnmanagedCurrency.Currency_maxBlockHeight(wrappedClass);
        }
        UInt64 maxBlockBlobSize()
        {
            return UnmanagedCurrency.Currency_maxBlockBlobSize(wrappedClass);
        }
        UInt64 maxTxSize()
        {
            return UnmanagedCurrency.Currency_maxTxSize(wrappedClass);
        }
        UInt64 publicAddressBase58Prefix()
        {
            return UnmanagedCurrency.Currency_publicAddressBase58Prefix(wrappedClass);
        }
        UInt32 minedMoneyUnlockWindow()
        {
            return UnmanagedCurrency.Currency_minedMoneyUnlockWindow(wrappedClass);
        }
        UInt64 timestampCheckWindow()
        {
            return UnmanagedCurrency.Currency_timestampCheckWindow(wrappedClass);
        }

        UInt64 blockFutureTimeLimit(UInt32 height)
        {
            return UnmanagedCurrency.Currency_blockFutureTimeLimit(wrappedClass, height);
        }
        UInt64 moneySupply()
        {
            return UnmanagedCurrency.Currency_moneySupply(wrappedClass);
        }

        UInt32 emissionSpeedFactor()
        {
            return UnmanagedCurrency.Currency_emissionSpeedFactor(wrappedClass);
        }
        UInt64 genesisBlockReward()
        {
            return UnmanagedCurrency.Currency_genesisBlockReward(wrappedClass);
        }
        UInt64 rewardBlocksWindow()
        {
            return UnmanagedCurrency.Currency_rewardBlocksWindow(wrappedClass);
        }

        UInt32 zawyDifficultyBlockIndex()
        {
            return UnmanagedCurrency.Currency_zawyDifficultyBlockIndex(wrappedClass);
        }
        UInt64 zawyDifficultyV2()
        {
            return UnmanagedCurrency.Currency_zawyDifficultyV2(wrappedClass);
        }
        Byte zawyDifficultyBlockVersion()
        {
            return UnmanagedCurrency.Currency_zawyDifficultyBlockVersion(wrappedClass);
        }
        UInt64 blockGrantedFullRewardZone()
        {
            return UnmanagedCurrency.Currency_blockGrantedFullRewardZone(wrappedClass);
        }
        UInt64 blockGrantedFullRewardZoneByBlockVersion(Byte blockMajorVersion)
        {
            return UnmanagedCurrency.Currency_blockGrantedFullRewardZoneByBlockVersion(wrappedClass, blockMajorVersion);
        }
        UInt64 minerTxBlobReservedSize()
        {
            return UnmanagedCurrency.Currency_minerTxBlobReservedSize(wrappedClass);
        }
        UInt64 numberOfDecimalPlaces()
        {
            return UnmanagedCurrency.Currency_numberOfDecimalPlaces(wrappedClass);
        }

        UInt64 coin()
        {
            return UnmanagedCurrency.Currency_coin(wrappedClass);
        }
        UInt64 minimumFee()
        {
            return UnmanagedCurrency.Currency_minimumFee(wrappedClass);
        }

        UInt64 defaultDustThreshold()
        {
            return UnmanagedCurrency.Currency_defaultDustThreshold(wrappedClass);
        }
        UInt64 difficultyTarget()
        {
            return UnmanagedCurrency.Currency_difficultyTarget(wrappedClass);
        }

        UInt64 difficultyWindow()
        {
            return UnmanagedCurrency.Currency_difficultyWindow(wrappedClass);
        }
        UInt64 difficultyWindowByBlockVersion(Byte blockMajorVersion)
        {
            return UnmanagedCurrency.Currency_difficultyWindowByBlockVersion(wrappedClass, blockMajorVersion);
        }
        UInt64 difficultyLag()
        {
            return UnmanagedCurrency.Currency_difficultyLag(wrappedClass);
        }
        UInt64 difficultyLagByBlockVersion(Byte blockMajorVersion)
        {
            return UnmanagedCurrency.Currency_difficultyLagByBlockVersion(wrappedClass, blockMajorVersion);
        }
        UInt64 difficultyCut()
        {
            return UnmanagedCurrency.Currency_difficultyCut(wrappedClass);
        }
        UInt64 difficultyCutByBlockVersion(Byte blockMajorVersion)
        {
            return UnmanagedCurrency.Currency_difficultyCutByBlockVersion(wrappedClass, blockMajorVersion);
        }
        UInt64 difficultyBlocksCount()
        {
            return UnmanagedCurrency.Currency_difficultyBlocksCount(wrappedClass);
        }
        UInt64 difficultyBlocksCountByBlockVersion(Byte blockMajorVersion, UInt32 height)
        {
            return UnmanagedCurrency.Currency_difficultyBlocksCountByBlockVersion(wrappedClass, blockMajorVersion, height);
        }
        UInt64 maxBlockSizeInitial()
        {
            return UnmanagedCurrency.Currency_maxBlockSizeInitial(wrappedClass);
        }

        UInt64 maxBlockSizeGrowthSpeedNumerator()
        {
            return UnmanagedCurrency.Currency_maxBlockSizeGrowthSpeedNumerator(wrappedClass);
        }
        UInt64 maxBlockSizeGrowthSpeedDenominator()
        {
            return UnmanagedCurrency.Currency_maxBlockSizeGrowthSpeedDenominator(wrappedClass);
        }
        UInt64 lockedTxAllowedDeltaSeconds()
        {
            return UnmanagedCurrency.Currency_lockedTxAllowedDeltaSeconds(wrappedClass);
        }

        UInt64 lockedTxAllowedDeltaBlocks()
        {
            return UnmanagedCurrency.Currency_lockedTxAllowedDeltaBlocks(wrappedClass);
        }
        UInt64 mempoolTxLiveTime()
        {
            return UnmanagedCurrency.Currency_mempoolTxLiveTime(wrappedClass);
        }

        UInt64 mempoolTxFromAltBlockLiveTime()
        {
            return UnmanagedCurrency.Currency_mempoolTxFromAltBlockLiveTime(wrappedClass);
        }
        UInt64 numberOfPeriodsToForgetTxDeletedFromPool()
        {
            return UnmanagedCurrency.Currency_numberOfPeriodsToForgetTxDeletedFromPool(wrappedClass);
        }
        UInt64 fusionTxMaxSize()
        {
            return UnmanagedCurrency.Currency_fusionTxMaxSize(wrappedClass);
        }

        UInt64 fusionTxMinInputCount()
        {
            return UnmanagedCurrency.Currency_fusionTxMinInputCount(wrappedClass);
        }
        UInt64 fusionTxMinInOutCountRatio()
        {
            return UnmanagedCurrency.Currency_fusionTxMinInOutCountRatio(wrappedClass);
        }
        UInt32 upgradeHeight(Byte majorVersion)
        {
            return UnmanagedCurrency.Currency_upgradeHeight(wrappedClass, majorVersion);
        }

        UInt32 upgradeVotingThreshold()
        {
            return UnmanagedCurrency.Currency_upgradeVotingThreshold(wrappedClass);
        }
        UInt32 upgradeVotingWindow()
        {
            return UnmanagedCurrency.Currency_upgradeVotingWindow(wrappedClass);
        }
        UInt32 upgradeWindow()
        {
            return UnmanagedCurrency.Currency_upgradeWindow(wrappedClass);
        }
        UInt32 minNumberVotingBlocks()
        {
            return UnmanagedCurrency.Currency_minNumberVotingBlocks(wrappedClass);
        }
        UInt32 maxUpgradeDistance()
        {
            return UnmanagedCurrency.Currency_maxUpgradeDistance(wrappedClass);
        }
        UInt32 calculateUpgradeHeight(UInt32 voteCompleteHeight)
        {
            return UnmanagedCurrency.Currency_calculateUpgradeHeight(wrappedClass, voteCompleteHeight);
        }
        string blocksFileName()
        {
            return UnmanagedCurrency.Currency_blocksFileName(wrappedClass);
        }

        string blockIndexesFileName()
        {
            return UnmanagedCurrency.Currency_blockIndexesFileName(wrappedClass);
        }
        string txPoolFileName()
        {
            return UnmanagedCurrency.Currency_txPoolFileName(wrappedClass);
        }
        bool isBlockexplorer()
        {
            return UnmanagedCurrency.Currency_isBlockexplorer(wrappedClass);
        }
        bool isTestnet()
        {
            return UnmanagedCurrency.Currency_isTestnet(wrappedClass);
        }

        void genesisBlock(out BlockTemplate blockTemplate)
        {
            UnmanagedTalleo.BlockTemplate _blockTemplate;
            UnmanagedCurrency.Currency_genesisBlock(wrappedClass, out _blockTemplate);
            blockTemplate = ConversionTools.convertBlockTemplate(_blockTemplate);
            UnmanagedBlockTemplate.Cleanup(_blockTemplate);
        }

        void genesisBlockHash(out Crypto.Hash hash)
        {
            UnmanagedCurrency.Currency_genesisBlockHash(wrappedClass, out hash);
        }
        bool getBlockReward(Byte blockMajorVersion, UInt64 medianSize, UInt64 currentBlockSize, UInt64 alreadyGeneratedCoins, UInt64 fee,
        out UInt64 reward, out Int64 emissionChange)
        {
            return UnmanagedCurrency.Currency_getBlockReward(wrappedClass, blockMajorVersion, medianSize, currentBlockSize, alreadyGeneratedCoins, fee, out reward, out emissionChange);
        }

        UInt64 maxBlockCumulativeSize(UInt64 height)
        {
            return UnmanagedCurrency.Currency_maxBlockCumulativeSize(wrappedClass, height);
        }
        bool constructMinerTx(Byte blockMajorVersion, UInt32 height, UInt64 medianSize, UInt64 alreadyGeneratedCoins, UInt64 currentBlockSize,
        UInt64 fee, in Talleo.AccountPublicAddress minerAddress, ref Transaction tx)
        {
            UnmanagedTalleo.Transaction _tx = ConversionTools.convertTransaction(tx);
            bool ret = UnmanagedCurrency.Currency_constructMinerTx(wrappedClass, blockMajorVersion, height, medianSize, alreadyGeneratedCoins, currentBlockSize, fee, minerAddress, ref _tx);
            tx = ConversionTools.convertTransaction(_tx);
            return ret;
        }

        bool constructMinerTx(Byte blockMajorVersion, UInt32 height, UInt64 medianSize, UInt64 alreadyGeneratedCoins, UInt64 currentBlockSize, UInt64 fee, in Talleo.AccountPublicAddress minerAddress, ref Transaction tx, in List<Byte> extraNonce)
        {
            UnmanagedTalleo.BinaryArray _extraNonce = ConversionTools.convertBinaryArray(extraNonce);
            UnmanagedTalleo.Transaction _tx = ConversionTools.convertTransaction(tx);
            bool ret = UnmanagedCurrency.Currency_constructMinerTxWithExtraNonce(wrappedClass, blockMajorVersion, height, medianSize, alreadyGeneratedCoins, currentBlockSize, fee, minerAddress, ref _tx, _extraNonce);
            tx = ConversionTools.convertTransaction(_tx);
            return ret;
        }
        bool constructMinerTxWithExtraNonceMaxOuts(IntPtr currency, Byte blockMajorVersion, UInt32 height, UInt64 medianSize, UInt64 alreadyGeneratedCoins, UInt64 currentBlockSize, UInt64 fee, in Talleo.AccountPublicAddress minerAddress, ref Transaction tx, in BinaryArray extraNonce, UInt64 maxOuts)
        {
            UnmanagedTalleo.BinaryArray _extraNonce = ConversionTools.convertBinaryArray(extraNonce);
            UnmanagedTalleo.Transaction _tx = ConversionTools.convertTransaction(tx);
            bool ret = UnmanagedCurrency.Currency_constructMinerTxWithExtraNonceMaxOuts(wrappedClass, blockMajorVersion, height, medianSize, alreadyGeneratedCoins, currentBlockSize, fee, minerAddress, ref _tx, _extraNonce, maxOuts);
            tx = ConversionTools.convertTransaction(_tx);
            return ret;
        }
        bool isFusionTransaction(in Transaction transaction)
        {
            UnmanagedTalleo.Transaction _transaction = ConversionTools.convertTransaction(transaction);
            return UnmanagedCurrency.Currency_isFusionTransaction(wrappedClass, _transaction);
        }

        bool isFusionTransaction(in Transaction transaction, UInt64 size)
        {
            UnmanagedTalleo.Transaction _transaction = ConversionTools.convertTransaction(transaction);
            return UnmanagedCurrency.Currency_isFusionTransactionWithSize(wrappedClass, _transaction, size);
        }
        bool isFusionTransaction(List<UInt64> inputsAmounts, List<UInt64> outputsAmounts, UInt64 size)
        {
            UInt64[] _inputsAmounts = inputsAmounts.ToArray();
            GCHandle gch1 = GCHandle.Alloc(_inputsAmounts);
            UInt64[] _outputsAmounts = outputsAmounts.ToArray();
            GCHandle gch2 = GCHandle.Alloc(_outputsAmounts);
            return UnmanagedCurrency.Currency_isFusionTransactionWithInputsOutputs(wrappedClass, GCHandle.ToIntPtr(gch1), (UInt64) inputsAmounts.Count, GCHandle.ToIntPtr(gch2), (UInt64) outputsAmounts.Count, size);
        }
        bool isAmountApplicableInFusionTransactionInput(UInt64 amount, UInt64 threshold)
        {
            return UnmanagedCurrency.Currency_isAmountApplicableInFusionTransactionInput(wrappedClass, amount, threshold);
        }
        bool isAmountApplicableInFusionTransactionInput(UInt64 amount, UInt64 threshold, out Byte amountPowerOfTen)
        {
            return UnmanagedCurrency.Currency_isAmountApplicableInFusionTransactionInputWithAmount(wrappedClass, amount, threshold, out amountPowerOfTen);
        }
        string accountAddressAsString(AccountBase account)
        {
            return UnmanagedCurrency.Currency_accountAddressAsString(wrappedClass, account.unwrap());
        }

        string accountAddressAsString(in Talleo.AccountPublicAddress accountPublicAddress)
        {
            return UnmanagedCurrency.Currency_accountAddressAsStringFromAddress(wrappedClass, accountPublicAddress);
        }
        bool parseAccountAddressString(string str, out Talleo.AccountPublicAddress addr)
        {
            return UnmanagedCurrency.Currency_parseAccountAddressString(wrappedClass, str, out addr);
        }
        string formatAmount(UInt64 amount)
        {
            return UnmanagedCurrency.Currency_formatUnsignedAmount(wrappedClass, amount);
        }

        string formatAmount(Int64 amount)
        {
            return UnmanagedCurrency.Currency_formatAmount(wrappedClass, amount);
        }
        bool parseAmount(string str, out UInt64 amount)
        {
            return UnmanagedCurrency.Currency_parseAmount(wrappedClass, str, out amount);
        }
        Difficulty nextDifficulty(Byte version, UInt32 blockIndex, List<UInt64> timestamps, List<Difficulty> cumulativeDifficulties)
        {
            UInt64[] _timestamps = timestamps.ToArray();
            GCHandle gch1 = GCHandle.Alloc(_timestamps);
            Difficulty[] _cumulativeDifficulties = cumulativeDifficulties.ToArray();
            GCHandle gch2 = GCHandle.Alloc(_cumulativeDifficulties);
            return UnmanagedCurrency.Currency_nextDifficulty(wrappedClass, version, blockIndex, GCHandle.ToIntPtr(gch1), (UInt64) timestamps.Count, GCHandle.ToIntPtr(gch2), (UInt64)cumulativeDifficulties.Count);
        }

        bool checkProofOfWorkV1(CachedBlock block, Difficulty currentDifficulty)
        {
            return UnmanagedCurrency.Currency_checkProofOfWorkV1(wrappedClass, block.unwrap(), currentDifficulty);
        }

        bool checkProofOfWorkV2(CachedBlock block, Difficulty currentDifficulty)
        {
            return UnmanagedCurrency.Currency_checkProofOfWorkV2(wrappedClass, block.unwrap(), currentDifficulty);
        }
        bool checkProofOfWork(CachedBlock block, Difficulty currentDifficulty)
        {
            return UnmanagedCurrency.Currency_checkProofOfWork(wrappedClass, block.unwrap(), currentDifficulty);
        }
        UInt64 getApproximateMaximumInputCount(UInt64 transactionSize, UInt64 outputCount, UInt64 mixinCount)
        {
            return UnmanagedCurrency.Currency_getApproximateMaximumInputCount(wrappedClass, transactionSize, outputCount, mixinCount);
        }

        // IWrapper
        public IntPtr unwrap()
        {
            return wrappedClass;
        }

        public static Currency wrap(IntPtr currency)
        {
            return new Currency(currency);
        }

    }
}
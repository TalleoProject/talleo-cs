using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
class ConversionTools
{
    public static UnmanagedTalleo.KeyInput convertKeyInput(in Talleo.KeyInput input)
    {
        UnmanagedTalleo.KeyInput _input = new UnmanagedTalleo.KeyInput();
        _input.amount = input.amount;
        UInt32[] _outputIndexes = new UInt32[input.outputIndexes.Count];
        _input.outputIndexCount = (UInt64)input.outputIndexes.Count;
        for (int i = 0; i < input.outputIndexes.Count; i++)
        {
            _outputIndexes[i] = input.outputIndexes[i];
        }
        GCHandle gch = GCHandle.Alloc(_outputIndexes);
        _input.outputIndexes = GCHandle.ToIntPtr(gch);
        _input.keyImage = input.keyImage;
        return _input;
    }

    public static Talleo.KeyInput convertKeyInput(in UnmanagedTalleo.KeyInput input)
    {
        Talleo.KeyInput _input = new Talleo.KeyInput();
        _input.amount = input.amount;
        _input.outputIndexes = new List<UInt32>((int)input.outputIndexCount);
        for (int i = 0; (UInt64)i < input.outputIndexCount; i++)
        {
            UInt32 _outputIndex = (UInt32)(object)Marshal.ReadInt32(input.outputIndexes + i * sizeof(Int32));
            _input.outputIndexes.Add(_outputIndex);
        }
        _input.keyImage = input.keyImage;
        return _input;
    }
    public static UnmanagedTalleo.TransactionInput convertTransactionInput(in Variant<Talleo.BaseInput, Talleo.KeyInput> input)
    {
        UnmanagedTalleo.TransactionInput _input = new UnmanagedTalleo.TransactionInput();
        _input.isBase = input.Is<Talleo.BaseInput>();
        if (_input.isBase)
        {
            _input.baseInput = input.Get<Talleo.BaseInput>();
        }
        else
        {
            _input.keyInput = convertKeyInput(input.Get<Talleo.KeyInput>());
        }
        return _input;
    }

    public static UnmanagedTalleo.TransactionOutput convertTransactionOutput(in Talleo.TransactionOutput output)
    {
        UnmanagedTalleo.TransactionOutput _output = new UnmanagedTalleo.TransactionOutput();
        _output.amount = output.amount;
        _output.hasKey = output.target.HasValue;
        if (_output.hasKey)
        {
            _output.keyOutput = output.target.Value;
        }
        return _output;
    }

    public static Variant<Talleo.BaseInput, Talleo.KeyInput> convertTransactionInput(in UnmanagedTalleo.TransactionInput input)
    {
        Variant<Talleo.BaseInput, Talleo.KeyInput> _input;
        if (input.isBase)
        {
            _input = input.baseInput;
        }
        else
        {
            _input = convertKeyInput(input.keyInput);
        }
        return _input;
    }
    public static Talleo.TransactionOutput convertTransactionOutput(in UnmanagedTalleo.TransactionOutput output)
    {
        Talleo.TransactionOutput _output;
        _output.amount = output.amount;
        _output.target = output.hasKey ? output.keyOutput : null;
        return _output;
    }
    public static UnmanagedTalleo.Transaction convertTransaction(in Talleo.Transaction transaction)
    {
        UnmanagedTalleo.Transaction _transaction = new UnmanagedTalleo.Transaction();
        _transaction.version = transaction.version;
        _transaction.unlockTime = transaction.unlockTime;
        // Inputs
        UnmanagedTalleo.TransactionInput[] _inputs = new UnmanagedTalleo.TransactionInput[transaction.inputs.Count];
        _transaction.inputCount = (UInt64)transaction.inputs.Count;
        for (int i = 0; i < transaction.inputs.Count; i++)
        {
            _inputs[i] = convertTransactionInput(transaction.inputs[i]);
        }
        GCHandle gch2 = GCHandle.Alloc(_inputs);
        _transaction.inputs = GCHandle.ToIntPtr(gch2);
        // Outputs
        UnmanagedTalleo.TransactionOutput[] _outputs = new UnmanagedTalleo.TransactionOutput[transaction.outputs.Count];
        _transaction.outputCount = (UInt64)transaction.outputs.Count;
        for (int i = 0; i < transaction.outputs.Count; i++)
        {
            _outputs[i] = convertTransactionOutput(transaction.outputs[i]);
        }
        GCHandle gch4 = GCHandle.Alloc(_outputs);
        _transaction.outputs = GCHandle.ToIntPtr(gch4);
        // Extra
        Byte[] _extra = transaction.extra.ToArray();
        GCHandle gch5 = GCHandle.Alloc(_extra);
        _transaction.extra.data = GCHandle.ToIntPtr(gch5);
        _transaction.extra.dataLength = (UInt64)_extra.Length;
        // Signatures
        UnmanagedTalleo.SignatureArray[] _signatures = new UnmanagedTalleo.SignatureArray[transaction.signatures.Count];
        _transaction.signatureCount = (UInt64)transaction.signatures.Count;
        for (int i = 0; i < transaction.signatures.Count; i++)
        {
            UnmanagedTalleo.SignatureArray signatureArray = new UnmanagedTalleo.SignatureArray();
            Crypto.Signature[] _signatures2 = new Crypto.Signature[transaction.signatures[i].Count];
            for (int j = 0; j < transaction.signatures[i].Count; j++)
            {
                _signatures2[j] = transaction.signatures[i][j];
            }
            GCHandle gch6 = GCHandle.Alloc(_signatures2);
            signatureArray.signatures = GCHandle.ToIntPtr(gch6);
            signatureArray.signatureCount = (UInt64)transaction.signatures[i].Count;
            _signatures[i] = signatureArray;
        }
        GCHandle gch7 = GCHandle.Alloc(_signatures);
        _transaction.signatures = GCHandle.ToIntPtr(gch7);
        // All done
        return _transaction;
    }

    public static List<List<Crypto.Signature>> convertSignatures(in IntPtr signatures, in UInt64 signatureCount)
    {
        List<List<Crypto.Signature>> ret = new List<List<Crypto.Signature>>();
        for (int i = 0; (UInt64)i < signatureCount; i++)
        {
            unsafe
            {
                UnmanagedTalleo.SignatureArray _array = Marshal.PtrToStructure<UnmanagedTalleo.SignatureArray>(signatures + i * sizeof(UnmanagedTalleo.SignatureArray));
                List<Crypto.Signature> _signatures = new List<Crypto.Signature>((int)_array.signatureCount);
                for (int j = 0; (UInt64)j < _array.signatureCount; j++)
                {
                    Crypto.Signature hash = Marshal.PtrToStructure<Crypto.Signature>(_array.signatures + j * sizeof(Crypto.Signature));
                    _signatures.Add(hash);
                }
                ret.Add(_signatures);
            }
        }
        return ret;
    }
    public static Talleo.Transaction convertTransaction(in UnmanagedTalleo.Transaction transaction)
    {
        Talleo.Transaction _transaction = new Talleo.Transaction();
        _transaction.version = transaction.version;
        _transaction.unlockTime = transaction.unlockTime;
        // Inputs
        for (int i = 0; (UInt64)i < transaction.inputCount; i++)
        {
            unsafe
            {
                UnmanagedTalleo.TransactionInput input = Marshal.PtrToStructure<UnmanagedTalleo.TransactionInput>(transaction.inputs + i * sizeof(UnmanagedTalleo.TransactionInput));
                _transaction.inputs.Add(convertTransactionInput(input));
            }
        }
        // Outputs
        for (int i = 0; (UInt64)i < transaction.outputCount; i++)
        {
            unsafe
            {
                UnmanagedTalleo.TransactionOutput output = Marshal.PtrToStructure<UnmanagedTalleo.TransactionOutput>(transaction.outputs + i * sizeof(UnmanagedTalleo.TransactionOutput));
                _transaction.outputs.Add(convertTransactionOutput(output));
            }
        }
        // extra
        Byte[] extra = new byte[transaction.extra.dataLength];
        Marshal.Copy(transaction.extra.data, extra, 0, (int)transaction.extra.dataLength);
        _transaction.extra = extra.ToList();
        // Signatures
        _transaction.signatures = convertSignatures(transaction.signatures, transaction.signatureCount);
        // All done
        return _transaction;
    }

    public static Talleo.BaseTransaction convertBaseTransaction(in UnmanagedTalleo.BaseTransaction transaction)
    {
        Talleo.BaseTransaction _transaction = new Talleo.BaseTransaction();
        _transaction.version = transaction.version;
        _transaction.unlockTime = transaction.unlockTime;
        _transaction.inputs = new List<Variant<Talleo.BaseInput, Talleo.KeyInput>>();
        for (int i = 0; (UInt64)i < transaction.inputCount; i++)
        {
            unsafe
            {
                UnmanagedTalleo.TransactionInput input = Marshal.PtrToStructure<UnmanagedTalleo.TransactionInput>(transaction.inputs + i * sizeof(UnmanagedTalleo.TransactionInput));
                _transaction.inputs.Add(convertTransactionInput(input));
            }
        }
        _transaction.outputs = new List<Talleo.TransactionOutput>();
        for (int i = 0; (UInt64)i < transaction.outputCount; i++)
        {
            unsafe
            {
                UnmanagedTalleo.TransactionOutput baseOutput = Marshal.PtrToStructure<UnmanagedTalleo.TransactionOutput>(transaction.outputs + i * sizeof(UnmanagedTalleo.TransactionOutput));
                _transaction.outputs.Add(convertTransactionOutput(baseOutput));
            }
        }
        _transaction.extra = convertBinaryArray(transaction.extra);
        return _transaction;
    }
    public static UnmanagedTalleo.BaseTransaction convertBaseTransaction(in Talleo.BaseTransaction transaction)
    {
        UnmanagedTalleo.BaseTransaction _transaction = new UnmanagedTalleo.BaseTransaction();
        _transaction.version = transaction.version;
        _transaction.unlockTime = transaction.unlockTime;
        //
        UnmanagedTalleo.TransactionInput[] _inputs = new UnmanagedTalleo.TransactionInput[transaction.inputs.Count];
        for (int i = 0; i < transaction.inputs.Count; i++)
        {
            _inputs[i] = convertTransactionInput(transaction.inputs[i]);
        }
        GCHandle gch1 = GCHandle.Alloc(_inputs);
        _transaction.inputs = GCHandle.ToIntPtr(gch1);
        _transaction.inputCount = (UInt64)transaction.inputs.Count;
        //
        UnmanagedTalleo.TransactionOutput[] _outputs = new UnmanagedTalleo.TransactionOutput[transaction.outputs.Count];
        for (int i = 0; i < transaction.outputs.Count; i++)
        {
            _outputs[i] = convertTransactionOutput(transaction.outputs[i]);
        }
        GCHandle gch2 = GCHandle.Alloc(_outputs);
        _transaction.outputs = GCHandle.ToIntPtr(gch2);
        _transaction.outputCount = (UInt64)transaction.outputs.Count;
        //
        _transaction.extra = convertBinaryArray(transaction.extra);
        // All done
        return _transaction;
    }
    public static Talleo.ParentBlock convertParentBlock(in UnmanagedTalleo.ParentBlock block)
    {
        Talleo.ParentBlock _block = new Talleo.ParentBlock();
        _block.majorVersion = block.majorVersion;
        _block.minorVersion = block.minorVersion;
        _block.previousBlockHash = block.previousBlockHash;
        _block.transactionCount = block.transactionCount;
        _block.baseTransactionBranch = new List<Crypto.Hash>();
        for (int i = 0; (UInt64)i < block.baseTransactionBranchCount; i++)
        {
            unsafe
            {
                Crypto.Hash hash = Marshal.PtrToStructure<Crypto.Hash>(block.baseTransactionBranch + i * sizeof(Crypto.Hash));
                _block.baseTransactionBranch.Add(hash);
            }
        }
        _block.baseTransaction = convertBaseTransaction(block.baseTransaction);
        _block.blockchainBranch = new List<Crypto.Hash>();
        for (int i = 0; (UInt64)i < block.blockchainBranchCount; i++)
        {
            unsafe
            {
                Crypto.Hash hash = Marshal.PtrToStructure<Crypto.Hash>(block.blockchainBranch + i * sizeof(Crypto.Hash));
                _block.blockchainBranch.Add(hash);
            }
        }
        return _block;
    }

    public static UnmanagedTalleo.ParentBlock convertParentBlock(in Talleo.ParentBlock block)
    {
        UnmanagedTalleo.ParentBlock _block = new UnmanagedTalleo.ParentBlock();
        _block.majorVersion = block.majorVersion;
        _block.minorVersion = block.minorVersion;
        _block.previousBlockHash = block.previousBlockHash;
        _block.transactionCount = block.transactionCount;
        //
        Crypto.Hash[] _baseTransactionBranch = block.baseTransactionBranch.ToArray();
        GCHandle gch1 = GCHandle.Alloc(_baseTransactionBranch);
        _block.baseTransactionBranch = GCHandle.ToIntPtr(gch1);
        _block.baseTransactionBranchCount = (UInt64)block.baseTransactionBranch.Count;
        //
        _block.baseTransaction = convertBaseTransaction(block.baseTransaction);
        //
        Crypto.Hash[] _blockchainBranch = block.blockchainBranch.ToArray();
        GCHandle gch2 = GCHandle.Alloc(_blockchainBranch);
        _block.blockchainBranch = GCHandle.ToIntPtr(gch2);
        _block.blockchainBranchCount = (UInt64)block.blockchainBranch.Count;
        // All done
        return _block;
    }
    public static Talleo.BlockTemplate convertBlockTemplate(in UnmanagedTalleo.BlockTemplate block)
    {
        Talleo.BlockTemplate _block = new Talleo.BlockTemplate();
        _block.majorVersion = block.majorVersion;
        _block.minorVersion = block.minorVersion;
        _block.nonce = block.nonce;
        _block.timestamp = block.timestamp;
        _block.previousBlockHash = block.previousBlockHash;
        _block.parentBlock = convertParentBlock(block.parentBlock);
        Talleo.Transaction baseTransaction = convertTransaction(block.baseTransaction);
        _block.transactionHashes = new List<Crypto.Hash>();
        for (int i = 0; (UInt64)i < block.transactionHashCount; i++)
        {
            unsafe
            {
                Crypto.Hash hash = Marshal.PtrToStructure<Crypto.Hash>(block.transactionHashes + i * sizeof(Crypto.Hash));
                _block.transactionHashes.Add(hash);
            }
        }
        return _block;
    }

    public static UnmanagedTalleo.BlockTemplate convertBlockTemplate(in Talleo.BlockTemplate blockTemplate)
    {
        UnmanagedTalleo.BlockTemplate _blockTemplate = new UnmanagedTalleo.BlockTemplate();
        _blockTemplate.majorVersion = blockTemplate.majorVersion;
        _blockTemplate.minorVersion = blockTemplate.minorVersion;
        _blockTemplate.nonce = blockTemplate.nonce;
        _blockTemplate.timestamp = blockTemplate.timestamp;
        _blockTemplate.previousBlockHash = blockTemplate.previousBlockHash;
        _blockTemplate.parentBlock = convertParentBlock(blockTemplate.parentBlock);
        _blockTemplate.baseTransaction = convertTransaction(blockTemplate.baseTransaction);
        Crypto.Hash[] _transactionHashes = blockTemplate.transactionHashes.ToArray();
        _blockTemplate.transactionHashCount = (UInt64)blockTemplate.transactionHashes.Count;
        GCHandle gch = GCHandle.Alloc(_transactionHashes);
        _blockTemplate.transactionHashes = GCHandle.ToIntPtr(gch);
        return _blockTemplate;
    }


    public static UnmanagedTalleo.BinaryArray convertBinaryArray(in List<Byte> array)
    {
        UnmanagedTalleo.BinaryArray _array = new UnmanagedTalleo.BinaryArray();
        Byte[] _data = array.ToArray();
        GCHandle gch = GCHandle.Alloc(_data);
        _array.data = GCHandle.ToIntPtr(gch);
        _array.dataLength = (UInt64)array.Count;
        return _array;
    }

    public static List<Byte> convertBinaryArray(in UnmanagedTalleo.BinaryArray array)
    {
        GCHandle gch = GCHandle.FromIntPtr(array.data);
        Byte[] _data = (Byte[])gch.Target;
        return _data.ToList();
    }

    public static Talleo.COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount convertOutsForAmount(in UnmanagedTalleo.COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount outs)
    {
        Talleo.COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount _outs = new Talleo.COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount();
        _outs.amount = outs.amount;
        _outs.outs = new List<Talleo.COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_out_entry>();
        for (int i = 0; (UInt64)i < outs.outsCount; i++)
        {
            unsafe
            {
                _outs.outs.Add(Marshal.PtrToStructure<Talleo.COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_out_entry>(outs.outs + i * sizeof(Talleo.COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_out_entry)));
            }
        }
        return _outs;
    }

    public static Talleo.RawBlock convertRawBlock(in UnmanagedTalleo.RawBlock block)
    {
        Talleo.RawBlock _rawBlock = new Talleo.RawBlock();
        _rawBlock.block = convertBinaryArray(block.block);
        for (int i = 0; (UInt64)i < block.transactionCount; i++)
        {
            unsafe
            {
                _rawBlock.transactions.Add(convertBinaryArray(Marshal.PtrToStructure<UnmanagedTalleo.BinaryArray>(block.transactions + i * sizeof(UnmanagedTalleo.BinaryArray))));
            }
        }
        return _rawBlock;
    }

    public static Talleo.TransactionPrefix convertTransactionPrefix(in UnmanagedTalleo.TransactionPrefix prefix)
    {
        Talleo.TransactionPrefix _prefix = new Talleo.TransactionPrefix();
        _prefix.version = prefix.version;
        _prefix.unlockTime = prefix.unlockTime;
        for (int i = 0; (UInt64)i < prefix.inputCount; i++)
        {
            unsafe
            {
                _prefix.inputs.Add(convertTransactionInput(Marshal.PtrToStructure<UnmanagedTalleo.TransactionInput>(prefix.inputs + i * sizeof(UnmanagedTalleo.TransactionInput))));
            }
        }
        for (int i = 0; (UInt64)i < prefix.outputCount; i++)
        {
            unsafe
            {
                _prefix.outputs.Add(convertTransactionOutput(Marshal.PtrToStructure<UnmanagedTalleo.TransactionOutput>(prefix.outputs + i * sizeof(UnmanagedTalleo.TransactionOutput))));
            }
        }
        _prefix.extra = convertBinaryArray(prefix.extra);
        return _prefix;
    }
    public static Talleo.TransactionShortInfo convertTransactionShortInfo(in UnmanagedTalleo.TransactionShortInfo info)
    {
        Talleo.TransactionShortInfo _info = new Talleo.TransactionShortInfo();
        _info.txId = info.txId;
        _info.txPrefix = convertTransactionPrefix(info.txPrefix);
        return _info;
    }
    public static Talleo.BlockShortEntry convertBlockShortEntry(in UnmanagedTalleo.BlockShortEntry entry)
    {
        Talleo.BlockShortEntry _entry = new Talleo.BlockShortEntry();
        _entry.blockHash = entry.blockHash;
        _entry.hasBlock = entry.hasBlock;
        _entry.block = convertBlockTemplate(entry.block);
        for (int i = 0; (UInt64)i < entry.txsShortInfoCount; i++)
        {
            unsafe
            {
                _entry.txsShortInfo.Add(convertTransactionShortInfo(Marshal.PtrToStructure<UnmanagedTalleo.TransactionShortInfo>(entry.txsShortInfo + i * sizeof(UnmanagedTalleo.TransactionShortInfo))));
            }
        }
        return _entry;
    }

    public static Talleo.TransactionExtraDetails convertTransactionExtraDetails(in UnmanagedTalleo.TransactionExtraDetails details)
    {
        Talleo.TransactionExtraDetails _details;
        _details.publicKey = details.publicKey;
        _details.nonce = convertBinaryArray(details.nonce);
        _details.raw = convertBinaryArray(details.raw);
        return _details;
    }

    public static Talleo.KeyInputDetails convertKeyInputDetails(in UnmanagedTalleo.KeyInputDetails details)
    {
        Talleo.KeyInputDetails _details = new Talleo.KeyInputDetails();
        _details.input = convertKeyInput(details.input);
        _details.mixin = details.mixin;
        _details.output = details.output;
        return _details;
    }
    public static Variant<Talleo.BaseInputDetails, Talleo.KeyInputDetails> convertTransactionInputDetails(in UnmanagedTalleo.TransactionInputDetails details)
    {
        Variant<Talleo.BaseInputDetails, Talleo.KeyInputDetails> _details;
        if (details.isBase)
        {
            _details = details.baseInputDetails;
        }
        else
        {
            _details = convertKeyInputDetails(details.keyInputDetails);
        }
        return _details;
    }

    public static List<Variant<Talleo.BaseInputDetails, Talleo.KeyInputDetails>> convertTransactionInputDetails(in IntPtr details, in UInt64 detailsCount)
    {
        List<Variant<Talleo.BaseInputDetails, Talleo.KeyInputDetails>> _details = new List<Variant<Talleo.BaseInputDetails, Talleo.KeyInputDetails>>();
        for (int i = 0; (UInt64)i < detailsCount; i++)
        {
            unsafe {
                _details.Add(convertTransactionInputDetails(Marshal.PtrToStructure<UnmanagedTalleo.TransactionInputDetails>(details + i * sizeof(UnmanagedTalleo.TransactionInputDetails))));
            }
        }
        return _details;
    }

    public static Talleo.TransactionOutputDetails convertTransactionOutputDetails(in UnmanagedTalleo.TransactionOutputDetails details)
    {
        Talleo.TransactionOutputDetails _details = new Talleo.TransactionOutputDetails();
        _details.output = convertTransactionOutput(details.output);
        _details.globalIndex = details.globalIndex;
        return _details;
    }
    public static List<Talleo.TransactionOutputDetails> convertTransactionOutputDetails(in IntPtr details, in UInt64 detailsCount)
    {
        List<Talleo.TransactionOutputDetails> _details = new List<Talleo.TransactionOutputDetails>();
        for (int i = 0; (UInt64)i < detailsCount; i++)
        {
            unsafe
            {
                _details.Add(convertTransactionOutputDetails(Marshal.PtrToStructure<UnmanagedTalleo.TransactionOutputDetails>(details + i * sizeof(UnmanagedTalleo.TransactionOutputDetails))));
            }
        }
        return _details;
    }
    public static Talleo.TransactionDetails convertTransactionDetails(in UnmanagedTalleo.TransactionDetails details)
    {
        Talleo.TransactionDetails _details = new Talleo.TransactionDetails();
        _details.hash = details.hash;
        _details.size = details.size;
        _details.fee = details.fee;
        _details.totalInputsAmount = details.totalInputsAmount;
        _details.totalOutputsAmount = details.totalOutputsAmount;
        _details.mixin = details.mixin;
        _details.unlockTime = details.unlockTime;
        _details.timestamp = details.timestamp;
        _details.paymentId = details.paymentId;
        _details.inBlockchain = details.inBlockchain;
        _details.blockHash = details.blockHash;
        _details.extra = convertTransactionExtraDetails(details.extra);
        _details.signatures = convertSignatures(details.signatures, details.signatureCount);
        _details.inputs = convertTransactionInputDetails(details.inputs, details.inputCount);
        _details.outputs = convertTransactionOutputDetails(details.outputs, details.outputCount);
        return _details;
    }
    public static Talleo.BlockDetails convertBlockDetails(in UnmanagedTalleo.BlockDetails details)
    {
        Talleo.BlockDetails _details = new Talleo.BlockDetails();
        _details.majorVersion = details.majorVersion;
        _details.minorVersion = details.minorVersion;
        _details.timestamp = details.timestamp;
        _details.prevBlockHash = details.prevBlockHash;
        _details.nonce = details.nonce;
        _details.isAlternative = details.isAlternative;
        _details.index = details.index;
        _details.hash = details.hash;
        _details.difficulty = details.difficulty;
        _details.reward = details.reward;
        _details.baseReward = details.baseReward;
        _details.blockSize = details.blockSize;
        _details.transactionsCumulativeSize = details.transactionsCumulativeSize;
        _details.alreadyGeneratedCoins = details.alreadyGeneratedCoins;
        _details.alreadyGeneratedTransactions = details.alreadyGeneratedTransactions;
        _details.sizeMedian = details.sizeMedian;
        _details.penalty = details.penalty;
        _details.totalFeeAmount = details.totalFeeAmount;
        for (int i = 0; (UInt64)i < details.transactionCount; i++)
        {
            unsafe
            {
                _details.transactions.Add(convertTransactionDetails(Marshal.PtrToStructure<UnmanagedTalleo.TransactionDetails>(details.transactions + i * sizeof(UnmanagedTalleo.TransactionDetails))));
            }
        }
        return _details;
    }
}
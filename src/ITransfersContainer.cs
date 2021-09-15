using System;

namespace Talleo
{
    public struct TransactionOutputInformation
    {
        // output info
        public TransactionTypes.OutputType type;
        public UInt64 amount;
        public UInt32 globalOutputIndex;
        public UInt32 outputInTransaction;

        // transaction info
        public Crypto.Hash transactionHash;
        public Crypto.PublicKey transactionPublicKey;

        public Crypto.PublicKey outputKey;
    };
}
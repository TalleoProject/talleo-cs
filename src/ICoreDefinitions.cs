using System.Collections.Generic;
using BinaryArray = System.Collections.Generic.List<System.Byte>;

namespace Talleo
{

    public struct BlockFullInfo
    {
        public BinaryArray block; //BlockTemplate
        public List<BinaryArray> transactions;
        public Crypto.Hash block_id;

        public void serialize(ref ISerializer s)
        {
            s.serialize(ref block, "block");
            s.serialize(ref transactions, "transactions");
            s.serialize(ref block_id, "block_id");
        }
    };

    public struct TransactionPrefixInfo
    {
        public Crypto.Hash txHash;
        public TransactionPrefix txPrefix;

        public void serialize(ref ISerializer s)
        {
            s.serialize(ref txHash, "txHash");
            s.serialize(ref txPrefix, "txPrefix");
        }
    };

    public struct BlockShortInfo
    {
        public Crypto.Hash blockId;
        public BinaryArray block;
        public List<TransactionPrefixInfo> txPrefixes;

        public void serialize(ref ISerializer s)
        {
            s.serialize(ref blockId, "blockId");
            s.serialize(ref block, "block");
            s.serialize(ref txPrefixes, "txPrefixes");
        }
    };

}

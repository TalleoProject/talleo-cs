using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using BinaryArray = System.Collections.Generic.List<System.Byte>;
using Difficulty = System.UInt64;

namespace UnmanagedTalleo
{
    public struct COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount
    {
        public UInt64 amount;
        public IntPtr outs;
        public UInt64 outsCount;
    };
};

namespace Talleo
{
    //-----------------------------------------------

    public struct EMPTY_STRUCT
    {
        void serialize(ref ISerializer s) { }
    };

    public struct STATUS_STRUCT
    {
        public String status;

        void serialize(ref ISerializer s)
        {
            s.serialize(ref status, "status");
        }
    };

    public struct COMMAND_RPC_GET_HEIGHT
    {
        public EMPTY_STRUCT request;

        public struct response
        {
            UInt64 height;
            UInt32 network_height;
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref height, "height");
                s.serialize(ref network_height, "network_height");
                s.serialize(ref status, "status");
            }
        };
    };

    struct COMMAND_RPC_GET_BLOCKS_FAST
    {

        struct request
        {
            List<Crypto.Hash> block_ids; //*first 10 blocks id goes sequential, next goes in pow(2,n) offset, like 2, 4, 8, 16, 32, 64 and so on, and the last one is always genesis block */

            void serialize(ref ISerializer s)
            {
                s.serializeAsBinary(ref block_ids, "block_ids");
            }
        };

        struct response
        {
            List<RawBlock> blocks;
            UInt64 start_height;
            UInt64 current_height;
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref blocks, "blocks");
                s.serialize(ref start_height, "start_height");
                s.serialize(ref current_height, "current_height");
                s.serialize(ref status, "status");
            }
        };
    };
    //-----------------------------------------------
    struct COMMAND_RPC_GET_TRANSACTIONS
    {
        struct request
        {
            List<String> txs_hashes;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref txs_hashes, "txs_hashes");
            }
        };

        struct response
        {
            List<String> txs_as_hex; //transactions blobs as hex
            List<String> missed_tx;  //not found transactions
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref txs_as_hex, "txs_as_hex");
                s.serialize(ref missed_tx, "missed_tx");
                s.serialize(ref status, "status");
            }
        };
    };
    //-----------------------------------------------
    struct COMMAND_RPC_GET_POOL_CHANGES
    {
        struct request
        {
            Crypto.Hash tailBlockId;
            List<Crypto.Hash> knownTxsIds;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref tailBlockId, "tailBlockID");
                s.serializeAsBinary(ref knownTxsIds, "knownTxsIds");
            }
        };

        struct response
        {
            bool isTailBlockActual;
            List<BinaryArray> addedTxs;      // Added transactions blobs
            List<Crypto.Hash> deletedTxsIds; // IDs of not found transactions
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref isTailBlockActual, "isTailBlockActual");
                s.serialize(ref addedTxs, "addedTxs");
                s.serializeAsBinary(ref deletedTxsIds, "deletedTxsIds");
                s.serialize(ref status, "status");
            }
        };
    };

    struct COMMAND_RPC_GET_POOL_CHANGES_LITE
    {
        struct request
        {
            Crypto.Hash tailBlockId;
            List<Crypto.Hash> knownTxsIds;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref tailBlockId, "tailBlockId");
                s.serializeAsBinary(ref knownTxsIds, "knownTxsIds");
            }
        };

        struct response
        {
            bool isTailBlockActual;
            List<TransactionPrefixInfo> addedTxs;          // Added transactions blobs
            List<Crypto.Hash> deletedTxsIds;               // IDs of not found transactions
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref isTailBlockActual, "isTailBlockActual");
                s.serialize(ref addedTxs, "addedTxs");
                s.serializeAsBinary(ref deletedTxsIds, "deletedTxsIds");
                s.serialize(ref status, "status");
            }
        };
    };

    //-----------------------------------------------
    struct COMMAND_RPC_GET_TX_GLOBAL_OUTPUTS_INDEXES
    {

        struct request
        {
            Crypto.Hash txid;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref txid, "txid");
            }
        };

        struct response
        {
            List<UInt64> o_indexes;
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref o_indexes, "o_indexes");
                s.serialize(ref status, "status");
            }
        };
    };
    //-----------------------------------------------
    public struct COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_request
    {
        public List<UInt64> amounts;
        public UInt16 outs_count;

        void serialize(ref ISerializer s)
        {
            s.serialize(ref amounts, "amounts");
            s.serialize(ref outs_count, "outs_count");
        }
    };

    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public struct COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_out_entry
    {
        public UInt32 global_amount_index;
        public Crypto.PublicKey out_key;

        void serialize(ref ISerializer s)
        {
            s.serialize(ref global_amount_index, "global_amount_index");
            s.serialize(ref out_key, "out_key");
        }
    };

    public struct COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount
    {
        public UInt64 amount;
        public List<COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_out_entry> outs;

        void serialize(ref ISerializer s)
        {
            s.serialize(ref amount, "amount");
            s.serializeAsBinary(ref outs, "outs");
        }
    };

    public struct COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_response
    {
        public List<COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount> outs;
        public String status;

        void serialize(ref ISerializer s)
        {
            s.serialize(ref outs, "outs");
            s.serialize(ref status, "status");
        }
    };

    public struct COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS
    {
        public COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_request request;
        public COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_response response;

        public COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_out_entry out_entry;
        public COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount outs_for_amount;
    };

    //-----------------------------------------------
    struct COMMAND_RPC_SEND_RAW_TX
    {
        struct request
        {
            String tx_as_hex;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref tx_as_hex, "tx_as_hex");
            }
        };

        struct response
        {
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref status, "status");
            }
        };
    };
    //-----------------------------------------------
    struct COMMAND_RPC_START_MINING
    {
        struct request
        {
            String miner_address;
            UInt64 threads_count;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref miner_address, "miner_address");
                s.serialize(ref threads_count, "threads_count");
            }
        };

        struct response
        {
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref status, "status");
            }
        };
    };
    //-----------------------------------------------
    public struct COMMAND_RPC_GET_INFO
    {
        public EMPTY_STRUCT request;

        public struct response
        {
            public String status;
            public UInt64 height;
            public UInt64 difficulty;
            public UInt64 tx_count;
            public UInt64 tx_pool_size;
            public UInt64 alt_blocks_count;
            public UInt64 outgoing_connections_count;
            public UInt64 incoming_connections_count;
            public UInt64 white_peerlist_size;
            public UInt64 grey_peerlist_size;
            public UInt32 last_known_block_index;
            public UInt32 network_height;
            public UInt32 hashrate;
            public String version;
            public bool synced;
            public String fee_address;
            public UInt64 max_block_size;
            public UInt64 max_tx_size;
            public UInt64 genesis_time;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref status, "status");
                s.serialize(ref height, "height");
                s.serialize(ref difficulty, "difficulty");
                s.serialize(ref tx_count, "tx_count");
                s.serialize(ref tx_pool_size, "tx_pool_size");
                s.serialize(ref alt_blocks_count, "alt_blocks_count");
                s.serialize(ref outgoing_connections_count, "outgoing_connections_count");
                s.serialize(ref incoming_connections_count, "incoming_connections_count");
                s.serialize(ref white_peerlist_size, "white_peerlist_size");
                s.serialize(ref grey_peerlist_size, "grey_peerlist_size");
                s.serialize(ref last_known_block_index, "last_known_block_index");
                s.serialize(ref network_height, "network_height");
                s.serialize(ref hashrate, "hashrate");
                s.serialize(ref synced, "synced");
                s.serialize(ref version, "version");
                s.serialize(ref fee_address, "fee_address");
                s.serialize(ref max_block_size, "max_block_size");
                s.serialize(ref max_tx_size, "max_tx_size");
                s.serialize(ref genesis_time, "genesis_time");
            }
        };
    };

    //-----------------------------------------------
    public struct COMMAND_RPC_STOP_MINING
    {
        public EMPTY_STRUCT request;
        public STATUS_STRUCT response;
    };

    //-----------------------------------------------
    public struct COMMAND_RPC_STOP_DAEMON
    {
        public EMPTY_STRUCT request;
        public STATUS_STRUCT response;
    };

    //-----------------------------------------------
    public struct COMMAND_RPC_GET_FEE_ADDRESS
    {
        public EMPTY_STRUCT request;

        public struct response
        {
            public String fee_address;
            public String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref fee_address, "fee_address");
                s.serialize(ref status, "status");
            }
        };
    };

    //
    public struct COMMAND_RPC_GETBLOCKCOUNT
    {
        public List<String> request;

        public struct response
        {
            public UInt64 count;
            public String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref count, "count");
                s.serialize(ref status, "status");
            }
        };
    };

    public struct COMMAND_RPC_GETBLOCKHASH
    {
        public List<UInt64> request;
        public String response;
    };

    struct COMMAND_RPC_GETBLOCKTEMPLATE
    {
        struct request
        {
            UInt64 reserve_size; //max 255 bytes
            String wallet_address;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref reserve_size, "reserve_size");
                s.serialize(ref wallet_address, "wallet_address");
            }
        };

        struct response
        {
            UInt64 difficulty;
            UInt32 height;
            UInt32 num_transactions;
            UInt64 reserved_offset;
            String blocktemplate_blob;
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref difficulty, "difficulty");
                s.serialize(ref height, "height");
                s.serialize(ref num_transactions, "num_transactions");
                s.serialize(ref reserved_offset, "reserved_offset");
                s.serialize(ref blocktemplate_blob, "blocktemplate_blob");
                s.serialize(ref status, "status");
            }
        };
    };

    public struct COMMAND_RPC_GET_CURRENCY_ID
    {
        public EMPTY_STRUCT request;

        public struct response
        {
            public String currency_id_blob;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref currency_id_blob, "currency_id_blob");
            }
        };
    };

    public struct COMMAND_RPC_SUBMITBLOCK
    {
        public List<String> request;
        public STATUS_STRUCT response;
    };

    public struct block_header_response
    {
        public Byte major_version;
        public Byte minor_version;
        public UInt64 timestamp;
        public String prev_hash;
        public UInt32 nonce;
        bool orphan_status;
        public UInt32 height;
        public UInt32 depth;
        public String hash;
        public Difficulty difficulty;
        public UInt64 reward;
        public UInt32 num_txes;
        public UInt64 block_size;

        public void serialize(ref ISerializer s)
        {
            s.serialize(ref major_version, "major_version");
            s.serialize(ref minor_version, "minor_version");
            s.serialize(ref timestamp, "timestamp");
            s.serialize(ref prev_hash, "prev_hash");
            s.serialize(ref nonce, "nonce");
            s.serialize(ref orphan_status, "orphan_status");
            s.serialize(ref height, "height");
            s.serialize(ref depth, "depth");
            s.serialize(ref hash, "hash");
            s.serialize(ref difficulty, "difficulty");
            s.serialize(ref reward, "reward");
            s.serialize(ref num_txes, "num_txes");
            s.serialize(ref block_size, "block_size");
        }
    };

    public struct BLOCK_HEADER_RESPONSE
    {
        public String status;
        public block_header_response block_header;

        public void serialize(ref ISerializer s)
        {
            s.serialize(ref block_header, "block_header");
            s.serialize(ref status, "status");
        }
    };


    struct COMMAND_RPC_GET_BLOCK_HEADERS_RANGE
    {
        struct request
        {
            UInt64 start_height;
            UInt64 end_height;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref start_height, "start_height");
                s.serialize(ref end_height, "end_height");
            }
        };

        struct response
        {
            String status;
            List<block_header_response> headers;
            bool untrusted;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref status, "status");
                s.serialize(ref headers, "headers");
                s.serialize(ref untrusted, "untrusted");
            }
        };
    };

    public struct f_transaction_short_response
    {
        public String hash;
        public UInt64 fee;
        public UInt64 amount_out;
        public UInt64 size;

        public void serialize(ref ISerializer s)
        {
            s.serialize(ref hash, "hash");
            s.serialize(ref fee, "fee");
            s.serialize(ref amount_out, "amount_out");
            s.serialize(ref size, "size");
        }
    };

    struct f_transaction_details_response
    {
        String hash;
        UInt64 size;
        String paymentId;
        UInt64 mixin;
        UInt64 fee;
        UInt64 amount_out;

        void serialize(ref ISerializer s)
        {
            s.serialize(ref hash, "hash");
            s.serialize(ref size, "size");
            s.serialize(ref paymentId, "paymentId");
            s.serialize(ref mixin, "mixin");
            s.serialize(ref fee, "fee");
            s.serialize(ref amount_out, "amount_out");
        }
    };

    struct f_block_short_response
    {
        UInt64 difficulty;
        UInt64 timestamp;
        UInt32 height;
        String hash;
        UInt64 tx_count;
        UInt64 cumul_size;
        UInt64 reward;

        void serialize(ref ISerializer s)
        {
            s.serialize(ref difficulty, "difficulty");
            s.serialize(ref timestamp, "timestamp");
            s.serialize(ref height, "height");
            s.serialize(ref hash, "hash");
            s.serialize(ref tx_count, "tx_count");
            s.serialize(ref cumul_size, "cumul_size");
            s.serialize(ref reward, "reward");
        }
    };

    struct f_block_details_response
    {
        Byte major_version;
        Byte minor_version;
        UInt64 timestamp;
        String prev_hash;
        UInt32 nonce;
        bool orphan_status;
        UInt32 height;
        UInt64 depth;
        String hash;
        UInt64 difficulty;
        UInt64 reward;
        UInt64 blockSize;
        UInt64 sizeMedian;
        UInt64 effectiveSizeMedian;
        UInt64 transactionsCumulativeSize;
        String alreadyGeneratedCoins;
        UInt64 alreadyGeneratedTransactions;
        UInt64 baseReward;
        double penalty;
        UInt64 totalFeeAmount;
        List<f_transaction_short_response> transactions;

        void serialize(ref ISerializer s)
        {
            s.serialize(ref major_version, "major_version");
            s.serialize(ref minor_version, "minor_version");
            s.serialize(ref timestamp, "timestamp");
            s.serialize(ref prev_hash, "prev_hash");
            s.serialize(ref nonce, "nonce");
            s.serialize(ref orphan_status, "orphan_status");
            s.serialize(ref height, "height");
            s.serialize(ref depth, "depth");
            s.serialize(ref hash, "hash");
            s.serialize(ref difficulty, "difficulty");
            s.serialize(ref reward, "reward");
            s.serialize(ref blockSize, "blockSize");
            s.serialize(ref sizeMedian, "sizeMedian");
            s.serialize(ref effectiveSizeMedian, "effectiveSizeMedian");
            s.serialize(ref transactionsCumulativeSize, "transactionCumulativeSize");
            s.serialize(ref alreadyGeneratedCoins, "alreadyGeneratedCoins");
            s.serialize(ref alreadyGeneratedTransactions, "alreadyGeneratedTransactions");
            s.serialize(ref baseReward, "baseReward");
            s.serialize(ref penalty, "penalty");
            s.serialize(ref transactions, "transactions");
            s.serialize(ref totalFeeAmount, "totalFeeAmount");
        }
    };

    public struct COMMAND_RPC_GET_LAST_BLOCK_HEADER
    {
        public EMPTY_STRUCT request;
        public BLOCK_HEADER_RESPONSE response;
    };

    public struct COMMAND_RPC_GET_BLOCK_HEADER_BY_HASH
    {
        public struct request
        {
            public String hash;

            public void serialize(ref ISerializer s)
            {
                s.serialize(ref hash, "hash");
            }
        };

        public BLOCK_HEADER_RESPONSE response;
    };

    public struct COMMAND_RPC_GET_BLOCK_HEADER_BY_HEIGHT
    {
        public struct request
        {
            public UInt64 height;

            public void serialize(ref ISerializer s)
            {
                s.serialize(ref height, "height");
            }
        };

        public BLOCK_HEADER_RESPONSE response;
    };

    public struct COMMAND_RPC_GET_ALTERNATE_CHAINS
    {
        public EMPTY_STRUCT request;

        public struct chain_info
        {
            public String block_hash;
            public UInt32 height;
            public UInt32 length;
            public Difficulty difficulty;

            public void serialize(ref ISerializer s)
            {
                s.serialize(ref block_hash, "block_hash");
                s.serialize(ref height, "height");
                s.serialize(ref length, "length");
                s.serialize(ref difficulty, "difficulty");
            }
        };

        public struct response
        {
            public String status;
            public List<chain_info> chains;

            public void serialize(ref ISerializer s)
            {
                s.serialize(ref status, "status");
                s.serialize(ref chains, "chains");
            }
        };
    };

    struct F_COMMAND_RPC_GET_BLOCKS_LIST
    {
        struct request
        {
            UInt64 height;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref height, "height");
            }
        };

        struct response
        {
            List<f_block_short_response> blocks; //transactions blobs as hex
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref blocks, "blocks");
                s.serialize(ref status, "status");
            }
        };
    };

    struct F_COMMAND_RPC_GET_BLOCK_DETAILS
    {
        struct request
        {
            String hash;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref hash, "hash");
            }
        };

        struct response
        {
            f_block_details_response block;
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref block, "block");
                s.serialize(ref status, "status");
            }
        };
    };

    struct F_COMMAND_RPC_GET_TRANSACTION_DETAILS
    {
        struct request
        {
            String hash;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref hash, "hash");
            }
        };

        struct response
        {
            Transaction tx;
            f_transaction_details_response txDetails;
            f_block_short_response block;
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref tx, "tx");
                s.serialize(ref txDetails, "txDetails");
                s.serialize(ref block, "block");
                s.serialize(ref status, "status");
            }
        };
    };

    public struct F_COMMAND_RPC_GET_POOL
    {
        public EMPTY_STRUCT request;

        public struct response
        {
            public List<f_transaction_short_response> transactions; //transactions blobs as hex
            public String status;

            public void serialize(ref ISerializer s)
            {
                s.serialize(ref transactions, "transactions");
                s.serialize(ref status, "status");
            }
        };
    };
    public struct COMMAND_RPC_QUERY_BLOCKS
    {
        public struct request
        {
            public List<Crypto.Hash> block_ids; //*first 10 blocks id goes sequential, next goes in pow(2,n) offset, like 2, 4, 8, 16, 32, 64 and so on, and the last one is always genesis block */
            public UInt64 timestamp;

            public void serialize(ref ISerializer s)
            {
                s.serializeAsBinary(ref block_ids, "block_ids");
                s.serialize(ref timestamp, "timestamp");
            }
        };

        public struct response
        {
            public String status;
            public UInt64 start_height;
            public UInt64 current_height;
            public UInt64 full_offset;
            public List<BlockFullInfo> items;

            public void serialize(ISerializer s)
            {
                s.serialize(ref status, "status");
                s.serialize(ref start_height, "start_height");
                s.serialize(ref current_height, "current_height");
                s.serialize(ref full_offset, "full_offset");
                s.serialize(ref items, "items");
            }
        };
    };

    struct COMMAND_RPC_QUERY_BLOCKS_LITE
    {
        struct request
        {
            List<Crypto.Hash> blockIds;
            UInt64 timestamp;

            void serialize(ref ISerializer s)
            {
                s.serializeAsBinary(ref blockIds, "block_ids");
                s.serialize(ref timestamp, "timestamp");
            }
        };

        struct response
        {
            String status;
            UInt64 startHeight;
            UInt64 currentHeight;
            UInt64 fullOffset;
            List<BlockShortInfo> items;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref status, "status");
                s.serialize(ref startHeight, "startHeight");
                s.serialize(ref currentHeight, "currentHeight");
                s.serialize(ref fullOffset, "fullOffset");
                s.serialize(ref items, "items");
            }
        };
    };

    struct COMMAND_RPC_GET_BLOCKS_DETAILS_BY_HASHES
    {
        struct request
        {
            List<Crypto.Hash> blockHashes;

            void serialize(ref ISerializer s)
            {
                s.serializeAsBinary(ref blockHashes, "blockHashes");
            }
        };

        struct response
        {
            List<BlockDetails> blocks;
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref status, "status");
                s.serialize(ref blocks, "blocks");
            }
        };
    };

    struct COMMAND_RPC_GET_BLOCKS_DETAILS_BY_HASHES_JSON
    {
        struct request
        {
            List<Crypto.Hash> blockHashes;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref blockHashes, "blockHashes");
            }
        };

        struct response
        {
            List<BlockDetails> blocks;
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref status, "status");
                s.serialize(ref blocks, "blocks");
            }
        };
    };


    struct COMMAND_RPC_GET_BLOCK_DETAILS_BY_HEIGHT
    {
        struct request
        {
            UInt32 blockHeight;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref blockHeight, "blockHeight");
            }
        };

        struct response
        {
            BlockDetails block;
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref status, "status");
                s.serialize(ref block, "block");
            }
        };
    };

    struct COMMAND_RPC_GET_BLOCKS_HASHES_BY_TIMESTAMPS
    {
        struct request
        {
            UInt64 timestampBegin;
            UInt64 secondsCount;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref timestampBegin, "timestampBegin");
                s.serialize(ref secondsCount, "secondsCount");
            }
        };

        struct response
        {
            List<Crypto.Hash> blockHashes;
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref status, "status");
                s.serialize(ref blockHashes, "blockHashes");
            }
        };
    };

    struct COMMAND_RPC_GET_TRANSACTION_HASHES_BY_PAYMENT_ID
    {
        struct request
        {
            Crypto.Hash paymentId;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref paymentId, "paymentId");
            }
        };

        struct response
        {
            List<Crypto.Hash> transactionHashes;
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref status, "status");
                s.serializeAsBinary(ref transactionHashes, "transactionHashes");
            }
        };
    };

    struct COMMAND_RPC_GET_TRANSACTION_HASHES_BY_PAYMENT_ID_JSON
    {
        struct request
        {
            Crypto.Hash paymentId;
            UInt32 startIndex;
            UInt32 endIndex;
            bool includeUnconfirmed;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref paymentId, "paymentId");
                s.serialize(ref startIndex, "startIndex");
                s.serialize(ref endIndex, "endIndex");
                s.serialize(ref includeUnconfirmed, "includeUnconfirmed");
            }
        };

        struct response
        {
            List<Crypto.Hash> transactionHashes;
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref status, "status");
                s.serialize(ref transactionHashes, "transactionHashes");
            }
        };
    };

    struct COMMAND_RPC_GET_BLOCK_HASHES_BY_TRANSACTION_HASHES
    {
        struct request
        {
            List<Crypto.Hash> transactionHashes;
            UInt32 startIndex;
            UInt32 endIndex;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref transactionHashes, "transactionHashes");
                s.serialize(ref startIndex, "startIndex");
                s.serialize(ref endIndex, "endIndex");
            }
        };

        struct response
        {
            List<Crypto.Hash> blockHashes;
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref blockHashes, "blockHashes");
                s.serialize(ref status, "status");
            }
        };
    };

    struct COMMAND_RPC_GET_BLOCK_INDEXES_BY_TRANSACTION_HASHES
    {
        struct request
        {
            List<Crypto.Hash> transactionHashes;
            UInt32 startIndex;
            UInt32 endIndex;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref transactionHashes, "transactionHashes");
                s.serialize(ref startIndex, "startIndex");
                s.serialize(ref endIndex, "endIndex");
            }
        };

        struct response
        {
            List<UInt32> blockIndexes;
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref blockIndexes, "blockIndexes");
                s.serialize(ref status, "status");
            }
        };
    };

    struct COMMAND_RPC_GET_BLOCK_HASHES_BY_PAYMENT_ID_JSON
    {
        struct request
        {
            Crypto.Hash paymentId;
            UInt32 startIndex;
            UInt32 endIndex;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref paymentId, "paymentId");
                s.serialize(ref startIndex, "startIndex");
                s.serialize(ref endIndex, "endIndex");
            }
        };

        struct response
        {
            List<Crypto.Hash> blockHashes;
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref status, "status");
                s.serialize(ref blockHashes, "blockHashes");
            }
        };
    };

    struct COMMAND_RPC_GET_TRANSACTION_DETAILS_BY_HASHES
    {
        struct request
        {
            List<Crypto.Hash> transactionHashes;

            void serialize(ref ISerializer s)
            {
                s.serializeAsBinary(ref transactionHashes, "transactionHashes");
            }
        };

        struct response
        {
            List<TransactionDetails> transactions;
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref status, "status");
                s.serialize(ref transactions, "transactions");
            }
        };
    };

    struct COMMAND_RPC_GET_TRANSACTION_DETAILS_BY_HASHES_JSON
    {
        struct request
        {
            List<Crypto.Hash> transactionHashes;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref transactionHashes, "transactionHashes");
            }
        };

        struct response
        {
            List<TransactionDetails> transactions;
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref status, "status");
                s.serialize(ref transactions, "transactions");
            }
        };
    };

    public struct COMMAND_RPC_GET_PEERS
    {
        public EMPTY_STRUCT request;

        public struct response
        {
            public String status;
            public List<String> peers;

            public void serialize(ref ISerializer s)
            {
                s.serialize(ref status, "status");
                s.serialize(ref peers, "peers");
            }
        };
    };

    public struct COMMAND_RPC_GET_PEERSGRAY
    {
        public EMPTY_STRUCT request;

        public struct response
        {
            public String status;
            public List<String> peers;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref status, "status");
                s.serialize(ref peers, "peers");
            }
        };
    };

    public struct COMMAND_RPC_GET_ISSUED_COINS
    {
        public EMPTY_STRUCT request;

        public struct response
        {
            public String alreadyGeneratedCoins;
            public String status;

            public void serialize(ref ISerializer s)
            {
                s.serialize(ref alreadyGeneratedCoins, "alreadyGeneratedCoins");
                s.serialize(ref status, "status");
            }
        };
    };

    public struct COMMAND_RPC_GET_TOTAL_COINS
    {
        public EMPTY_STRUCT request;

        public struct response
        {
            public String totalCoins;
            public String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref totalCoins, "totalCoins");
                s.serialize(ref status, "status");
            }
        };
    };

    struct COMMAND_RPC_GET_TRANSACTION_OUT_AMOUNTS_FOR_ACCOUNT
    {
        struct request
        {
            String transaction;
            String account;
            String viewKey;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref transaction, "transaction");
                s.serialize(ref account, "account");
                s.serialize(ref viewKey, "viewKey");
            }
        };

        struct response
        {
            UInt64 amount;
            String status;

            void serialize(ref ISerializer s)
            {
                s.serialize(ref amount, "amount");
                s.serialize(ref status, "status");
            }
        };
    };

    public struct COMMAND_RPC_GET_COLLATERAL_HASH
    {
        public EMPTY_STRUCT request;

        public struct response
        {
            public String collateralHash;
            public String status;

            public void serialize(ref ISerializer s)
            {
                s.serialize(ref collateralHash, "collateralHash");
                s.serialize(ref status, "status");
            }
        };
    };
}

namespace Crypto
{

    public unsafe struct Hash
    {
        public fixed byte data[32];
    };

    public unsafe struct PublicKey
    {
        public fixed byte data[32];
    };

    public unsafe struct SecretKey
    {
        public fixed byte data[32];
    };

    public unsafe struct KeyDerivation
    {
        public fixed byte data[32];
    };

   public unsafe struct KeyImage
    {
        public fixed byte data[32];
    };

    public unsafe struct Signature
    {
        public fixed byte data[64];
    };

}

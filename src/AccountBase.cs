using System;
using System.Runtime.InteropServices;
class UnmanagedAccountBase
{
    [DllImport("TalleoWrapper")] public static extern IntPtr AccountBase_Create();

    [DllImport("TalleoWrapper")] public static extern void AccountBase_Destroy(IntPtr account);
    [DllImport("TalleoWrapper")] public static extern void AccountBase_generate(IntPtr account);
    [DllImport("TalleoWrapper")] public static extern void generateViewKeysFromSpend(IntPtr account, in Crypto.SecretKey spendSecretKey, out Crypto.SecretKey viewSecretKey, out Crypto.PublicKey viewPublic);
    [DllImport("TalleoWrapper")] public static extern void generateViewKeyFromSpend(IntPtr account, in Crypto.SecretKey spendSecretKey, out Crypto.SecretKey viewSecretKey);

    [DllImport("TalleoWrapper")] public static extern ref Talleo.AccountKeys AccountBase_getAccountKeys(IntPtr account);
    [DllImport("TalleoWrapper")] public static extern void AccountBase_setAccountKeys(IntPtr account, in Talleo.AccountKeys keys);
    [DllImport("TalleoWrapper")] public static extern UInt64 AccountBase_get_createtime(IntPtr account);
    [DllImport("TalleoWrapper")] public static extern void AccountBase_set_createtime(IntPtr account, UInt64 val);
}

namespace Talleo
{
    class AccountBase : IWrapper
    {
        IntPtr wrappedClass;
        public AccountBase()
        {
            wrappedClass = UnmanagedAccountBase.AccountBase_Create();
        }

        protected AccountBase(IntPtr account)
        {
            wrappedClass = account;
        }
        ~AccountBase()
        {
            UnmanagedAccountBase.AccountBase_Destroy(wrappedClass);
        }
        public void generate()
        {
            UnmanagedAccountBase.AccountBase_generate(wrappedClass);
        }
        public void generateViewFromSpend(in Crypto.SecretKey spendSecretKey, out Crypto.SecretKey viewSecretKey, out Crypto.PublicKey viewPublicKey)
        {
            UnmanagedAccountBase.generateViewKeysFromSpend(wrappedClass, spendSecretKey, out viewSecretKey, out viewPublicKey);
        }
        public void generateViewFromSpend(in Crypto.SecretKey spendSecretKey, out Crypto.SecretKey viewSecretKey)
        {
            UnmanagedAccountBase.generateViewKeyFromSpend(wrappedClass, spendSecretKey, out viewSecretKey);
        }

        public ref AccountKeys getAccountKeys()
        {
            return ref UnmanagedAccountBase.AccountBase_getAccountKeys(wrappedClass);
        }
        public void setAccountKeys(in AccountKeys keys)
        {
            UnmanagedAccountBase.AccountBase_setAccountKeys(wrappedClass, keys);
        }
        public UInt64 get_createtime()
        {
            return UnmanagedAccountBase.AccountBase_get_createtime(wrappedClass);
        }
        public void set_createtime(UInt64 val)
        {
            UnmanagedAccountBase.AccountBase_set_createtime(wrappedClass, val);
        }
        // IWrapper
        public IntPtr unwrap()
        {
            return wrappedClass;
        }

        public static AccountBase wrap(IntPtr account)
        {
            return new AccountBase(account);
        }
    }
}
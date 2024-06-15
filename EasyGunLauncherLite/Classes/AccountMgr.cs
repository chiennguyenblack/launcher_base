using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyGunLauncherLite
{
    internal class AccountMgr
    {
        private static Dictionary<string, UserInfo> accounts;

        private static void initAccounts()
        {
            if (accounts == null)
            {
                accounts = new Dictionary<string, UserInfo>();
            }
        }

        public static void addAccount(string account, UserInfo userInfo)
        {
            initAccounts();
            if (accounts.ContainsKey(account))
            {
                accounts.Remove(account);
            }
            accounts.Add(account, userInfo);
        }

        public static Dictionary<string, UserInfo> getAllAccounts()
        {
            initAccounts();
            return accounts;
        }

        public static void removeAccount(string account)
        {
            initAccounts();
            if (accounts.ContainsKey(account))
            {
                accounts.Remove(account);
            }
        }

        public static UserInfo getUserInfoByUsername(string username)
        {
            initAccounts();
            if (accounts.ContainsKey(username))
            {
                return accounts[username];
            }
            return null;
        }
    }
}

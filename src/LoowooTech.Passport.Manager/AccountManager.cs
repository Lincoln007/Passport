using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Manager
{
    public class AccountManager
    {
        public Account GetAccount(string username, string password, string agentUsername = null)
        {
            throw new NotImplementedException();
        }

        public Account GetAccount(int accountId)
        {
            throw new NotImplementedException();
        }

        public Account GetAgentAccount(string username, string angentUsername)
        {
            throw new NotImplementedException();
        }

        public Account Create(Account account)
        {
            throw new NotImplementedException();
        }


        public void Delete(int accountId)
        {
            throw new NotImplementedException();
        }

        public string ResetPassword(int accountId)
        {
            throw new NotImplementedException();
        }
    }
}
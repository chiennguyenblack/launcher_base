using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyGunLauncherLite
{
    class ResponseObject
    {
        public bool success;
        public string message;
        public DataObject data;
    }

    class DataObject
    {
        public string PlayUrl;
        public bool TwoFactor;
        public string token;

        //charge
        public string rateCard;
        public string rateMomo;
        public string rateATM;
        public string src;
        public string comment;
        public AccInfo accinfo;

        //convert coin
        public float rate;
        public int coin;
    }

    class AccInfo
    {
        public string name;
        public string acc_num;
        public string bank_name;
    }

    class ServerListResponseObject : ResponseObject
    {
        public new List<ServerObject> data;
    }

    class ServerObject
    {
        public int ServerID;
        public string ServerName;
    }

    class PlayerListResponseObject : ResponseObject
    {
        public new List<Player> data;
    }

    class Player
    {
        public int UserID;
        public string NickName;
    }
    
    class ChargeHistoryResponseObject : ResponseObject
    {
        public new List<ChargeHistory> data;
    }

    class ChargeHistory
    {
        public string TimeCreate;
        public int Value;
        public string Content;
    }

    class LoginResponseObject : ResponseObject
    {
        public new LoginResponseData data;
    }

    class LoginResponseData
    {
        public string playUrl;
        public string token;
        public UserInfo userInfo;
        public bool TwoFactor;
    }

    public class UserInfo
    {
        public int UserID;
        public string UserName;
        public string Email;
        public int Money;
        public bool TwoFactor;
        public bool VerifiedEmail;
        public string Date;

        public string Token;
    }
}

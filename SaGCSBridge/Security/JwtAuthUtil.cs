using Jose;
using SaGCSBridge.Utils;
using SaGModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SaGCSBridge.Security
{
    public class JwtAuthUtil
    {
        public string GenerateToken(ApLoginRequest loginRequest)
        {
            string secret = AppSettings.TokenSecretKey();//加解密的key,如果不一樣會無法成功解密
            Dictionary<string, Object> claim = new Dictionary<string, Object>();//payload 需透過token傳遞的資料
            claim.Add("Account", loginRequest.App);
            claim.Add("IP", loginRequest.ApMachine.IP);
            claim.Add("MachineName", loginRequest.ApMachine.MachineName);
            claim.Add("Expire", DateTime.Now.AddSeconds(10).ToString());//Token 時效設定60*60*60秒
            var payload = claim;
            var token = Jose.JWT.Encode(payload, Encoding.UTF8.GetBytes(secret), JwsAlgorithm.HS512);//產生token
            return token;
        }
    }
}
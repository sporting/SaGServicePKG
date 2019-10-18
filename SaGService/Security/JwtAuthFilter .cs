using Jose;
using SaGService.Utils;
using SaGUtil.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace SaGService.Security
{
    public class JwtAuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            string secret =AppSettings.TokenSecretKey();//加解密的key,如果不一樣會無法成功解密
            var request = actionContext.Request;

            if (!WithoutVerifyToken(request.RequestUri.ToString()))
            {
                if (request.Headers.Authorization == null || request.Headers.Authorization.Scheme != "Bearer")
                {
                    MyLog.Error(this, "Lost Token");
                    throw new System.Exception("Lost Token");
                }
                else
                {
                    try
                    {
                        //解密後會回傳Json格式的物件(即加密前的資料)
                        var jwtObject = Jose.JWT.Decode<Dictionary<string, Object>>(
                        request.Headers.Authorization.Parameter,
                        Encoding.UTF8.GetBytes(secret),
                        JwsAlgorithm.HS512);

                        if (IsTokenExpired(jwtObject["Expire"].ToString()))
                        {
                            MyLog.Error(this, "Token Expired");
                            throw new System.Exception("Token Expired");
                        }
                    }
                    catch (Exception ex)
                    {
                        MyLog.Fatal(this, ex.Message);
                        throw new System.Exception(ex.Message);
                    }
                }
            }

            base.OnActionExecuting(actionContext);
        }

        //Login不需要驗證因為還沒有token
        public bool WithoutVerifyToken(string requestUri)
        {
            if (requestUri.EndsWith("/Login") || requestUri.EndsWith("/Login/"))
                return true;
            return false;
        }

        //驗證token時效
        public bool IsTokenExpired(string dateTime)
        {
            return false;
            //return Convert.ToDateTime(dateTime) < DateTime.Now;
        }

    }
}
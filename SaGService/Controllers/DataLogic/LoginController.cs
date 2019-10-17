using SaGLogic;
using SaGModel;
using SaGService.Security;
using SaGUtil.System;
using System;
using System.Net;
using System.Web.Http;

namespace SaGService.Controllers
{
    public class LoginController : ApiController
    {
        public IHttpActionResult Post(ApLoginRequest loginRequest)
        {
            SaLogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"{loginRequest.App} {loginRequest.LoginUser} {loginRequest.ApMachine.IP} {loginRequest.ApMachine.MachineName}");

            try
            {
                if (UserValidate.CheckUser(loginRequest))
                {
                    JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
                    string jwtToken = jwtAuthUtil.GenerateToken(loginRequest);
                    loginRequest.Token = jwtToken;
                    loginRequest.LoginDate = SaDate.Today();

                    if (LoginLog(loginRequest, true))
                    {
                        return Ok(loginRequest);
                    }
                    else
                    {
                        return InternalServerError();
                    }
                }
                else
                {
                    SaLogMan.Instance.Info(GlobalVars.LOGGER_NAME, "Unauthorized");
                    LoginLog(loginRequest, false);

                    return StatusCode(HttpStatusCode.Unauthorized);
                }
            }
            catch (Exception ex)
            {
                SaLogMan.Instance.Error(GlobalVars.LOGGER_NAME, ex.Message);
                LoginLog(loginRequest, false,ex.Message);
                return InternalServerError();
            }
        }

        private bool LoginLog(ApLoginRequest loginRequest,bool v,string msg = "")
        {
            try
            {
                SysLog syslog = new SysLog();
                syslog.Add(new SysLogM() { EventName = "Login", Params = $"App='{loginRequest.App}',User='{loginRequest.LoginUser}',IP='{loginRequest.ApMachine.IP}',Name='{loginRequest.ApMachine.MachineName}',result={v.ToString()},msg='{msg}'" });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

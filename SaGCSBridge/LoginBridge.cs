using SaGCSBridge.Security;
using SaGLogic;
using SaGModel;
using SaGUtil.System;
using SaGUtil.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGCSBridge
{
    public class LoginBridge : BaseCSBridge<ApLoginRequest>
    {
        protected override string Api
        {
            get
            {
                return "Login";
            }
        }

        public LoginBridge(string token) : base(token)
        {

        }

        public async Task<BridgeResult<ApLoginRequest>> Login(ApLoginRequest loginRequest)
        {
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
                        return await Task.FromResult(new BridgeResult<ApLoginRequest>
                        {
                            status = true,
                            message = string.Empty,
                            result = loginRequest
                        });
                    }
                    else
                    {
                        return await Task.FromResult(new BridgeResult<ApLoginRequest>
                        {
                            status = false,
                            message = "Login Log Error",
                            result = null
                        });
                    }
                }
                else
                {
                    MyLog.Info(this, "Unauthorized");
                    LoginLog(loginRequest, false);

                    return await Task.FromResult(new BridgeResult<ApLoginRequest>
                    {
                        status = false,
                        message = "Unauthorized",
                        result = loginRequest
                    });
                }
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                LoginLog(loginRequest, false, ex.Message);
                return await Task.FromResult(new BridgeResult<ApLoginRequest>
                {
                    status = false,
                    message = "Exception",
                    result = loginRequest
                });
            }            
        }

        private bool LoginLog(ApLoginRequest loginRequest, bool v, string msg = "")
        {
            try
            {
                SysLog syslog = new SysLog();
                return syslog.Add(new SysLogM() { EventName = "Login", Params = $"App='{loginRequest.App}',User='{loginRequest.LoginUser}',IP='{loginRequest.ApMachine.IP}',Name='{loginRequest.ApMachine.MachineName}',result={v.ToString()},msg='{msg}'" });
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return false;
            }
        }
    }
}

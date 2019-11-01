using SaGLogic;
using SaGModel;
using SaGUtil.Utils;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SaGCSBridge
{
    public class SysParamsBridge : BaseCSBridge<SysParamsM>
    {
        protected override string Api
        {
            get
            {
                return "SysParams";
            }
        }

        public SysParamsBridge(string token):base(token)
        {
            
        }

        public async Task<BridgeResult<SysParamsM[]>> GetValues(string name)
        {
            try
            {
                SysParams sysparams = new SysParams();
                
                return await Task.FromResult(new BridgeResult<SysParamsM[]>
                {
                    status = true,
                    message = string.Empty,
                    result = sysparams.GetValues(name)
                });
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return await Task.FromResult(new BridgeResult<SysParamsM[]>
                {
                    status = false,
                    message = ex.Message,
                    result = new SysParamsM[] { }
                });
            }
        }

        public async Task<BridgeResult<bool>> Delete(int id)
        {
            try
            {
                SysParams sysparams = new SysParams();

                if (sysparams.Delete(new int[] { id }))
                {
                    return await Task.FromResult(new BridgeResult<bool>
                    {
                        status = true,
                        message = string.Empty,
                        result = true
                    });
                }
                else
                {
                    return await Task.FromResult(new BridgeResult<bool>
                    {
                        status = true,
                        message = "SysParams Delete failed",
                        result = false
                    });
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new BridgeResult<bool>
                {
                    status = false,
                    message = ex.Message,
                    result = false
                });
            }
        }

        public async Task<BridgeResult<bool>> AddSysParams(SysParamsM[] spms)
        {
            try
            {
                SysParams sysparams = new SysParams();

                if (sysparams.Update(spms))
                {
                    return await Task.FromResult(new BridgeResult<bool>
                    {
                        status = true,
                        message = string.Empty,
                        result = true
                    });
                }
                else
                {
                    return await Task.FromResult(new BridgeResult<bool>
                    {
                        status = true,
                        message = "SysParams Add failed",
                        result = false
                    });
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new BridgeResult<bool>
                {
                    status = false,
                    message = ex.Message,
                    result = false
                });
            }
        }
    }
}

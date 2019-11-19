using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaGModel;
using SaGLogic;
using SaGUtil.Utils;

namespace SaGCSBridge
{
    public class SysUserBridge : BaseCSBridge<SysUserM>
    {
        protected override string Api
        {
            get
            {
                return "SysUser";
            }
        }
        public SysUserBridge(string token) : base(token)
        {
        }

        public async Task<BridgeResult<SysUserM[]>> GetAll()
        {
            try
            {
                SysUser ssUser = new SysUser();
                return await Task.FromResult(new BridgeResult<SysUserM[]>
                {
                    status = true,
                    message = string.Empty,
                    result = ssUser.GetAll()
                });
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return await Task.FromResult(new BridgeResult<SysUserM[]>
                {
                    status = false,
                    message = ex.Message,
                    result = new SysUserM[] { }
                });
            }
        }

        public async Task<BridgeResult<bool>> Delete(int id)
        {
            try
            {
                SysUser ssUser = new SysUser();
                if (ssUser.Delete(new int[] { id }))
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
                        message = "SysUser Delete failed",
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

        public async Task<BridgeResult<bool>> AddSysUser(SysUserM[] sums)
        {
            try
            {
                SysUser ssUser = new SysUser();
                if (ssUser.Updates(sums))
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
                        message = "SysUser Add failed",
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

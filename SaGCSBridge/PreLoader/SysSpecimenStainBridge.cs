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
    public class SysSpecimenStainBridge : BaseCSBridge<SysSpecimenStainM>
    {
        protected override string Api
        {
            get
            {
                return "SysSpecimenStain";
            }
        }
        public SysSpecimenStainBridge(string token) : base(token)
        {
        }

        public async Task<BridgeResult<SysSpecimenStainM[]>> GetAll()
        {
            try
            {
                SysSpecimenStain ssStain = new SysSpecimenStain();
                return await Task.FromResult(new BridgeResult<SysSpecimenStainM[]>
                {
                    status = true,
                    message = string.Empty,
                    result = ssStain.GetAll()
                });
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return await Task.FromResult(new BridgeResult<SysSpecimenStainM[]>
                {
                    status = false,
                    message = ex.Message,
                    result = new SysSpecimenStainM[] { }
                });
            }
        }

        public async Task<BridgeResult<bool>> Delete(int id)
        {
            try
            {
                SysSpecimenStain ssStain = new SysSpecimenStain();

                if (ssStain.Delete(new int[] { id }))
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
                        message = "SysSpecimenStain Delete failed",
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

        public async Task<BridgeResult<bool>> AddSpecimenStain(SysSpecimenStainM[] ssms)
        {
            try
            {
                SysSpecimenStain ssStain = new SysSpecimenStain();

                if (ssStain.Update(ssms))
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
                        message = "SysSpecimenStain Add failed",
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

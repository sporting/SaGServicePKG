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
    public class SysSlideSlotBridge : BaseCSBridge<SysSlideSlotM>
    {
        protected override string Api
        {
            get
            {
                return "SysSlideSlot";
            }
        }
        public SysSlideSlotBridge(string token) : base(token)
        {
        }

        public async Task<BridgeResult<SysSlideSlotM[]>> GetAll()
        {
            try
            {
                SysSlideSlot ssSlot = new SysSlideSlot();
                return await Task.FromResult(new BridgeResult<SysSlideSlotM[]>
                {
                    status = true,
                    message = string.Empty,
                    result = ssSlot.GetValues()
                });
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return await Task.FromResult(new BridgeResult<SysSlideSlotM[]>
                {
                    status = false,
                    message = ex.Message,
                    result = new SysSlideSlotM[] { }
                });
            }
        }

        public async Task<BridgeResult<bool>> Delete(int id)
        {
            try
            {
                SysSlideSlot ssSlot = new SysSlideSlot();
                if (ssSlot.Delete(new int[] { id }))
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
                        message = "SysSlideSlot Delete failed",
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

        public async Task<BridgeResult<bool>> AddSlideSlot(SysSlideSlotM[] ssms)
        {
            try
            {
                SysSlideSlot ssSlot = new SysSlideSlot();
                if (ssSlot.Updates(ssms))
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
                        message = "SysSlideSlot Add failed",
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

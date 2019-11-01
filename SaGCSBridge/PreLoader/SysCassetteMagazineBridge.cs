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
    public class SysCassetteMagazineBridge : BaseCSBridge<SysCassetteMagazineM>
    {
        protected override string Api
        {
            get
            {
                return "SysCassetteMagazine";
            }
        }
        public SysCassetteMagazineBridge(string token) : base(token)
        {
        }

        public async Task<BridgeResult<SysCassetteMagazineM[]>> GetAll()
        {
            try
            {
                SysCassetteMagazine scmm = new SysCassetteMagazine();
                 
                return await Task.FromResult(new BridgeResult<SysCassetteMagazineM[]>
                {
                    status = true,
                    message = string.Empty,
                    result = scmm.GetValues()
                });
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return await Task.FromResult(new BridgeResult<SysCassetteMagazineM[]>
                {
                    status = false,
                    message = ex.Message,
                    result = new SysCassetteMagazineM[] { }
                });
            }
        }

        public async Task<BridgeResult<bool>> Delete(int id)
        {
            try
            {
                SysCassetteMagazine scm = new SysCassetteMagazine();
                if (scm.Delete(new int[] { id }))
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
                        message = "SysCassetteMagazine Delete failed",
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


        public async Task<BridgeResult<bool>> AddCassetteMagazine(SysCassetteMagazineM[] scms)
        {
            try
            {
                SysCassetteMagazine scm = new SysCassetteMagazine();

                if (scm.Update(scms))
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
                        message = "SysCassetteMagazine Add failed",
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

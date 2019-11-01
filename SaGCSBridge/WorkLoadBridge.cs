using SaGLogic;
using SaGModel;
using SaGUtil.System;
using SaGUtil.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SaGCSBridge
{
    public class WorkLoadBridge : BaseCSBridge<WorkLoadMV[]>
    {
        protected override string Api
        {
            get
            {
                return "WorkLoad";
            }
        }

        public WorkLoadBridge(string token):base(token)
        {
            
        }

        public async Task<BridgeResult<SlideWorkLoadMV[]>> GetSlideWorkLoad(string type, string begDate, string endDate, string user)
        {
            try
            {
                if (type == "slide")
                {
                    SlideWorkLoadV work = new SlideWorkLoadV();
                    SlideWorkLoadMV[] wv = work.Get(begDate, endDate, user);
                    return await Task.FromResult(new BridgeResult<SlideWorkLoadMV[]>
                    {
                        status = true,
                        message = string.Empty,
                        result = wv
                    });
                }
                else if (type == "doctor")
                {
                    DoctorSlideWorkLoadV work = new DoctorSlideWorkLoadV();
                    SlideWorkLoadMV[] wv = work.Get(begDate, endDate, user);
                    return await Task.FromResult(new BridgeResult<SlideWorkLoadMV[]>
                    {
                        status = true,
                        message = string.Empty,
                        result = wv
                    });
                }
                else
                {
                    return await Task.FromResult(new BridgeResult<SlideWorkLoadMV[]>
                    {
                        status = false,
                        message = "please give argument 'type' in ('doctor' or 'slide')",
                        result = new SlideWorkLoadMV[] { }
                    });
                }                
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return await Task.FromResult(new BridgeResult<SlideWorkLoadMV[]>
                {
                    status = false,
                    message = ex.Message,
                    result = new SlideWorkLoadMV[] { }
                });
            }
        }
        public async Task<BridgeResult<WorkLoadMV[]>> GetWorkLoad(string type, string begDate, string endDate, string user)
        {
            try
            {
                if (type == "gross")
                {
                    GrossWorkLoadV work = new GrossWorkLoadV();
                    WorkLoadMV[] wv = work.Get(begDate, endDate, user);
                    return await Task.FromResult(new BridgeResult<WorkLoadMV[]>
                    {
                        status = true,
                        message = string.Empty,
                        result = wv
                    });
                }
                else if (type == "embed")
                {
                    EmbedWorkLoadV work = new EmbedWorkLoadV();
                    WorkLoadMV[] wv = work.Get(begDate, endDate, user);
                    return await Task.FromResult(new BridgeResult<WorkLoadMV[]>
                    {
                        status = true,
                        message = string.Empty,
                        result = wv
                    });
                }
                else
                {
                    return await Task.FromResult(new BridgeResult<WorkLoadMV[]>
                    {
                        status = false,
                        message = "please give argument 'type' in ('gross' or 'embed')",
                        result = new WorkLoadMV[] { }
                    });
                }
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return await Task.FromResult(new BridgeResult<WorkLoadMV[]>
                {
                    status = false,
                    message = ex.Message,
                    result = new WorkLoadMV[] { }
                });
            }
        }
    }
}

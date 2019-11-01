using SaGCSBridge;
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
    public class OrderSlideBridge : BaseCSBridge<OrderSlideM>
    {
        protected override string Api
        {
            get
            {
                return "OrderSlide";
            }
        }

        public OrderSlideBridge(string token) : base(token)
        {

        }

        public async Task<BridgeResult<OrderSlideM[]>> GetByOpDate(string date)
        {
            try
            {
                OrderSlide order = new OrderSlide();
                OrderSlideM[] osm = order.GetSlidesByOpDate(date);

                return await Task.FromResult(new BridgeResult<OrderSlideM[]>
                {
                    status = true,
                    message = string.Empty,
                    result = osm
                });                
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return await Task.FromResult(new BridgeResult<OrderSlideM[]>
                {
                    status = false,
                    message = ex.Message,
                    result = new OrderSlideM[] { }
                });
            }
        }

        public async Task<BridgeResult<OrderSlideM>> GetByOrdNoSeq(string ordNo, int cassetteSequence, int slideSequence)
        {
            try
            {
                OrderSlide order = new OrderSlide();
                OrderSlideM osm = order.GetSlidesByOrdNoSeq(ordNo, cassetteSequence, slideSequence);

                return await Task.FromResult(new BridgeResult<OrderSlideM>
                {
                    status = true,
                    message = string.Empty,
                    result = osm
                });
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return await Task.FromResult(new BridgeResult<OrderSlideM>
                {
                    status = false,
                    message = ex.Message,
                    result = null
                });
            }
        }

        public async Task<BridgeResult<OrderSlideM[]>> GetDetail(string type, string begDate, string endDate, string user)
        {
            try
            {
                OrderSlide order = new OrderSlide();

                if (type == "slide")
                {
                    return await Task.FromResult(new BridgeResult<OrderSlideM[]>
                    {
                        status = true,
                        message = string.Empty,
                        result = order.GetSlideDetail(begDate, endDate, user)
                    });
                }
                else if (type == "doctor")
                {
                    return await Task.FromResult(new BridgeResult<OrderSlideM[]>
                    {
                        status = true,
                        message = string.Empty,
                        result = order.GetDoctorSlideDetail(begDate, endDate, user)
                    });
                }

                return await Task.FromResult(new BridgeResult<OrderSlideM[]>
                {
                    status = false,
                    message = "please give argument 'type' in ('doctor' or 'slide')",
                    result = new OrderSlideM[] { }
                });
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return await Task.FromResult(new BridgeResult<OrderSlideM[]>
                {
                    status = false,
                    message = ex.Message,
                    result = new OrderSlideM[] { }
                });
            }
        }

        public async Task<BridgeResult<OrderSlideM>> AddOrderSlide(OrderSlideM log)
        {
            try
            {
                OrderSlide slide = new OrderSlide();
                int newSlideSeq = 1;
                if (slide.Add(log, out newSlideSeq))
                {
                    log.SlideSequence = newSlideSeq;

                    return await Task.FromResult(new BridgeResult<OrderSlideM>
                    {
                        status = true,
                        message = string.Empty,
                        result = log
                    });
                }
                else
                {
                    return await Task.FromResult(new BridgeResult<OrderSlideM>
                    {
                        status = false,
                        message = "OrderSlide Add Error",
                        result = log
                    });
                }                
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return await Task.FromResult(new BridgeResult<OrderSlideM>
                {
                    status = false,
                    message = ex.Message,
                    result = log
                });
            }
        }
    }
}

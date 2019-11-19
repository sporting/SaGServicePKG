using SaGLogic;
using SaGModel;
using SaGUtil.System;
using SaGUtil.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SaGCSBridge
{
    public class OrderBarcodeBridge : BaseCSBridge<OrderBarcodeM>
    {
        protected override string Api
        {
            get
            {
                return "OrderBarcode";
            }
        }

        public OrderBarcodeBridge(string token):base(token)
        {
            
        }

        public async Task<BridgeResult<bool>> OrdNoExist(string ordNo)
        {
            try
            {
                OrderBarcode orderbarcode = new OrderBarcode();
                    return await Task.FromResult(new BridgeResult<bool>
                    {
                        status = true,
                        message = string.Empty,
                        result = orderbarcode.OrdNoExist(ordNo)
                    });
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return await Task.FromResult(new BridgeResult<bool>
                {
                    status = false,
                    message = ex.Message,
                    result = false
                });
            }
        }


        public async Task<BridgeResult<OrderBarcodeM[]>> GetByOrdNo(string ordNo)
        {
            try
            {
                OrderBarcode orderbarcode = new OrderBarcode();
                return await Task.FromResult(new BridgeResult<OrderBarcodeM[]>
                {
                    status = true,
                    message = string.Empty,
                    result = orderbarcode.GetByOrdNo(ordNo)
                });
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return await Task.FromResult(new BridgeResult<OrderBarcodeM[]>
                {
                    status = false,
                    message = ex.Message,
                    result = new OrderBarcodeM[] { }
                });
            }
        }


        public async Task<BridgeResult<DataTable>> GetGroupByDate(string begDate, string endDate)
        {
            try
            {
                OrderBarcode order = new OrderBarcode();

                return await Task.FromResult(new BridgeResult<DataTable>
                {
                    status = true,
                    message = string.Empty,
                    result = order.GetBarcodeGroup(begDate, endDate)
                });
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return await Task.FromResult(new BridgeResult<DataTable>
                {
                    status = false,
                    message = ex.Message,
                    result = null
                });
            }
        }
    }
}

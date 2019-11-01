using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using SaGUtil.Utils;

namespace SaGCSBridge
{
    public class BridgeResult<T>
    {
        public bool status;
        public string message;
        public T result;
    }



    public abstract class BaseCSBridge<T>
    {
        protected abstract string Api { get; }
        const string _bridge = "Bridge";
        const string _apiSection = "api";
        private string _url = "";
        protected string MainUrl
        {
            get
            {
                return _url;
            }
        }

        protected string ApiUrl
        {
            get
            {
                return string.Concat(MainUrl, "/", _apiSection, "/", Api, "/");
            }
        }

        public BaseCSBridge(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                MyLog.Error(this,"token is lost");
            }

            try
            {
                //_url = AppSettings.SaGServiceUrl();
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this,$"{_bridge}: {ex.Message}");
            }
        }

    }
}

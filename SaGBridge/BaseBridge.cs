using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using SaGBridge.Utils;
using System.Net.Http.Headers;
using SaGUtil.System;
using SaGUtil.Utils;
using SaGUtil.Json;

namespace SaGBridge
{
    public class BridgeResult<T>
    {
        public bool status;
        public string message;
        public T result;
    }



    public abstract class BaseBridge<T>
    {
        HttpClient _client;
        protected HttpClient Client { get { return _client; } }
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

        public BaseBridge(string token)
        {            
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
                _url = AppSettings.SaGServiceUrl();
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this,$"{_bridge}: {ex.Message}");
                //LogMan.Instance.Info(_bridge,$"Load AppSettings SaGServiceUrl Exception: {ex.Message}");
            }
        }

        //Get All Data
        // http://host/api/[Api]/
        public async Task<BridgeResult<T[]>> GetAll()
        {
            MyLog.Info(this, $"{Api}: {ApiUrl}");
            //LogMan.Instance.Info(Api, $"{Api}: Get: {ApiUrl}");

            HttpResponseMessage response = await _client.GetAsync(ApiUrl);

            try
            {
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                List<T> result = SaJson.DeserializeObject<List<T>>(responseBody);
                return new BridgeResult<T[]> { status = true,message=string.Empty, result = result.ToArray() };
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, $"{Api}: {ApiUrl}: {ex.Message}");
                //LogMan.Instance.Error(Api, $"{Api}: {ApiUrl}: {ex.Message}");
                return new BridgeResult<T[]> { status = false,message=ex.Message, result = new T[] { } };
            }
        }

        //Get Data by id
        // http://host/api/[Api]/id
        public async Task<BridgeResult<T>> Get(int id)
        {
            string url = $"{ApiUrl}/{id.ToString()}";

            MyLog.Info(this, $"{Api}: {ApiUrl}: {id}");
            //LogMan.Instance.Info(Api, $"{Api}: Get: {url}: {id}");

            HttpResponseMessage response = await _client.GetAsync(url);
            T res = default(T);
            try
            {
                response.EnsureSuccessStatusCode();
                
                try
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    res = SaJson.DeserializeObject<T>(responseBody);
                }
                catch
                {
                }

                return new BridgeResult<T> { status = true, message = string.Empty,result = res };
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, $"{Api}: {url}: {ex.Message}");
                //LogMan.Instance.Error(Api, $"{Api}: {url}: {ex.Message}");
                return new BridgeResult<T> { status = false, message = ex.Message, result = res };
            }
        }


        //Delete Data by id
        // http://host/api/[Api]/id
        public async Task<BridgeResult<T>> Delete(int id)
        {
            string url = $"{ApiUrl}/{id.ToString()}";
            MyLog.Info(this, $"{Api}: {url}: {id}");
            //LogMan.Instance.Info(Api, $"{Api}: Delete: {url}: {id}");

            HttpResponseMessage response = await _client.DeleteAsync(url);
            T res = default(T);
            try
            {
                response.EnsureSuccessStatusCode();
                try
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    res = SaJson.DeserializeObject<T>(responseBody);
                }
                catch
                {
                }

                return new BridgeResult<T> { status = true, message = string.Empty, result =res };
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, $"{Api}: {url}: {ex.Message}");
                //LogMan.Instance.Error(Api, $"{Api}: {url}: {ex.Message}");
                return new BridgeResult<T> { status = false, message = ex.Message, result = res };
            }
        }


        //Update Data by id, data
        // http://host/api/[Api]/id
        public async Task<BridgeResult<T>> Put(int id, T data)
        {
            string url = $"{ApiUrl}/{id.ToString()}";
            string js = SaJson.SerializeObject(data);
            StringContent content = new StringContent(js, Encoding.UTF8, "application/json");

            MyLog.Info(this, $"{Api}: {url}: {id}");
            //LogMan.Instance.Info(Api, $"{Api}: Put: {url}: {js}");

            HttpResponseMessage response = await _client.PutAsync(url, content);
            T res = default(T);
            try
            {
                response.EnsureSuccessStatusCode();
                try
                {
                    string responseBody = await response.Content.ReadAsStringAsync();                    
                    res = SaJson.DeserializeObject<T>(responseBody);
                }
                catch
                {
                }

                return new BridgeResult<T> { status = true, message = string.Empty, result = res };
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, $"{Api}: {ApiUrl}:{js}: {ex.Message}");
                //LogMan.Instance.Error(Api, $"{Api}: {ApiUrl}: {js}: {ex.Message}");
                return new BridgeResult<T> { status = false, message = ex.Message, result = res };
            }
        }


        //Post Data by id, data
        // http://host/api/[Api]/
        public async Task<BridgeResult<T>> Post(T data)
        {
            string js = SaJson.SerializeObject(data);
            StringContent content = new StringContent(js, Encoding.UTF8, "application/json");

            MyLog.Info(this, $"{Api}: {ApiUrl}: {js}");
            //LogMan.Instance.Info(Api, $"{Api}: Post: {ApiUrl}: {js}");

            HttpResponseMessage response = await _client.PostAsync(ApiUrl, content);
            T res = default(T);
            try
            {
                response.EnsureSuccessStatusCode();

                try
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    res = SaJson.DeserializeObject<T>(responseBody);
                }
                catch
                {
                }

                return new BridgeResult<T> { status = true,message=string.Empty, result = res };
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, $"{Api}: {ApiUrl}:{js}: {ex.Message}");
                //LogMan.Instance.Error(Api, $"{Api}: {ApiUrl}: {js}: {ex.Message}");
                return new BridgeResult<T> { status = false, message = ex.Message, result = res };
            }
        }

        //Post Data by id, data
        // http://host/api/[Api]/
        public async Task<BridgeResult<T[]>> Post(T[] data)
        {
            string js = SaJson.SerializeObject(data);
            StringContent content = new StringContent(js, Encoding.UTF8, "application/json");

            MyLog.Info(this, $"{Api}: {ApiUrl}: {js}");
            //LogMan.Instance.Info(Api, $"{Api}: Post: {ApiUrl}: {js}");

            HttpResponseMessage response = await _client.PostAsync(ApiUrl, content);
            T[] res = default(T[]);
            try
            {
                response.EnsureSuccessStatusCode();

                try
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    res = SaJson.DeserializeObject<T[]>(responseBody);
                }
                catch
                {
                }

                return new BridgeResult<T[]> { status = true, message = string.Empty, result = res };
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, $"{Api}: {ApiUrl}:{js}: {ex.Message}");
                //LogMan.Instance.Error(Api, $"{Api}: {ApiUrl}: {js}: {ex.Message}");
                return new BridgeResult<T[]> { status = false, message = ex.Message, result = res };
            }
        }
    }
}

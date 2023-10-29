using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using FootBallProject.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FootBallProject.Service
{
    public class APIService
    {
       
        public readonly string BASE_URL = "https://localhost:7257";
        private static APIService _ins;
        public static APIService ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new APIService();
                }
                return _ins;

            }
            set { _ins = value; }
        }
        public APIService() { }

      
        //SUPPLIER
        public async Task<List<SUPPLIER>> getSuppliers()
        {
            List<SUPPLIER> data = new List<SUPPLIER>();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{BASE_URL}/api/Suppliers");
                if (response.IsSuccessStatusCode)
                {
                    string temp = await response.Content.ReadAsStringAsync();
                  Response<SUPPLIER> responses= JsonConvert.DeserializeObject<Response<SUPPLIER>>(temp);
                    data = responses.data;
                }
            }
            return data;
        }
        public async Task<SingleSupplierResponse> getSuppliersById(int idSupplier)
        {
           
            SingleSupplierResponse res =new SingleSupplierResponse();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{BASE_URL}/api/Suppliers/{idSupplier}");
                if (response.IsSuccessStatusCode)
                {
                    string temp = await response.Content.ReadAsStringAsync();
                    SingleSupplierResponse responses = JsonConvert.DeserializeObject<SingleSupplierResponse>(temp);
                    res = responses;
                  
                }
            }
            return res;
        }
        public async Task<List<DOIBONGSUPPLIER>> getDoiBongSuppliers()
        {
            List<DOIBONGSUPPLIER> data = new List<DOIBONGSUPPLIER>();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{BASE_URL}/api/DoibongSuppliers");
                if (response.IsSuccessStatusCode)
                {
                    string temp = await response.Content.ReadAsStringAsync();
                  Response<DOIBONGSUPPLIER> responses= JsonConvert.DeserializeObject<Response<DOIBONGSUPPLIER>>(temp);
                    data = responses.data;
                }
            }
            return data;
        }
        //SUPPLIER
  
    }

    public class Response<T>
    {
        public int statusCode { get; set; }
        public List<T> data { get; set; }
    }

    public class ResponseSingle<T>
    {
        public int statusCode { get; set; }
        public T data { get; set; }
    }


    public class SingleSupplierResponse
    {
        public int statusCode { get; set; }
        public SUPPLIER data { get; set; }
        public List<DOIBONG> footBallTeamsUnCooperate { get; set; }
    }



}

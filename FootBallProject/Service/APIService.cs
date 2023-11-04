using FootBallProject.Model;
using FootBallProject.RequestModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FootBallProject.Service
{
    public class APIService
    {
       
        public readonly string BASE_URL = "https://localhost:7257";
        public readonly string TOKEN = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Ik5ndXnhu4VuIFRo4buLIMOBdCBNaW5oIiwiZW1haWwiOiJoaWVubGVwaGFuMjAwM0BnbWFpbC5jb20iLCJqdGkiOiI4YmRlNWI2Ni02YWIwLTQwNmUtOGE5Mi0xYTgyMmZlZmE0ZjgiLCJVc2VyTmFtZSI6ImFkbWluIiwiSWQiOiIxIiwiUm9sZSI6IkFkbWluIiwidG9rZW5JZCI6Ijc5OThiZGQ0LTRmYTQtNDhjYS1iMGNmLWI0ZmU0NWVmODhhOSIsIm5iZiI6MTY5ODU4ODc2NywiZXhwIjoxNzAxMjY3MTY3LCJpYXQiOjE2OTg1ODg3Njd9.ZmYgmXgEtOFHyITVZGmkOcYw-5al8EIf3Y5qmhjwuWTPs8ZAuHyMZWqOCz5-X8QtlkBsd3-AqErkJq7RWUHQaw";
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
        public async Task updateSupplier(int idSupplier,SUPPLIER value)
        {
            var temp = new
            {
                idSupplier = idSupplier,
                supplierName = value.supplierName,
                addresss = value.addresss,
                phoneNumber = value.phoneNumber,
                representativeName = value.representativeName,
                establishDate = value.establishDate,
                images = value.images
            };
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Put,
                        RequestUri = new Uri($"{BASE_URL}/api/Suppliers/updates/{idSupplier}"),
                        Headers =
                {
                    { "accept", "application/json" },
                     { "Authorization", "Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Ik5ndXnhu4VuIFRo4buLIMOBdCBNaW5oIiwiZW1haWwiOiJoaWVubGVwaGFuMjAwM0BnbWFpbC5jb20iLCJqdGkiOiI4YmRlNWI2Ni02YWIwLTQwNmUtOGE5Mi0xYTgyMmZlZmE0ZjgiLCJVc2VyTmFtZSI6ImFkbWluIiwiSWQiOiIxIiwiUm9sZSI6IkFkbWluIiwidG9rZW5JZCI6Ijc5OThiZGQ0LTRmYTQtNDhjYS1iMGNmLWI0ZmU0NWVmODhhOSIsIm5iZiI6MTY5ODU4ODc2NywiZXhwIjoxNzAxMjY3MTY3LCJpYXQiOjE2OTg1ODg3Njd9.ZmYgmXgEtOFHyITVZGmkOcYw-5al8EIf3Y5qmhjwuWTPs8ZAuHyMZWqOCz5-X8QtlkBsd3-AqErkJq7RWUHQaw" },
                },
                        Content = new StringContent(JsonConvert.SerializeObject(temp), Encoding.UTF8, "application/json")
                        {
                            Headers =
                {
                    ContentType = new MediaTypeHeaderValue("application/json")
                }
                        }
                    };
                    using (var response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();
                        var body = await response.Content.ReadAsStringAsync();
                    }


                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        //SUPPLIER

        //DOIBONGSUPPLER
        public async Task<List<DOIBONGSUPPLIER>> getDoiBongSuppliers()
        {
            List<DOIBONGSUPPLIER> data = new List<DOIBONGSUPPLIER>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"{BASE_URL}/api/DoibongSuppliers");
                    if (response.IsSuccessStatusCode)
                    {
                        string temp = await response.Content.ReadAsStringAsync();
                        Response<DOIBONGSUPPLIER> responses = JsonConvert.DeserializeObject<Response<DOIBONGSUPPLIER>>(temp);
                        data = responses.data;
                    }
                }
                return data;
            }
            catch (Exception)
            {

               return null;
            }
           
        }


        public async Task postNewDoiBongSupplier(DoiBongSupplierRequest value, string token="")
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri($"{BASE_URL}/api/DoibongSuppliers/create"),
                        Headers =
                {
                    { "accept", "application/json" },
                     { "Authorization", "Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Ik5ndXnhu4VuIFRo4buLIMOBdCBNaW5oIiwiZW1haWwiOiJoaWVubGVwaGFuMjAwM0BnbWFpbC5jb20iLCJqdGkiOiI4YmRlNWI2Ni02YWIwLTQwNmUtOGE5Mi0xYTgyMmZlZmE0ZjgiLCJVc2VyTmFtZSI6ImFkbWluIiwiSWQiOiIxIiwiUm9sZSI6IkFkbWluIiwidG9rZW5JZCI6Ijc5OThiZGQ0LTRmYTQtNDhjYS1iMGNmLWI0ZmU0NWVmODhhOSIsIm5iZiI6MTY5ODU4ODc2NywiZXhwIjoxNzAxMjY3MTY3LCJpYXQiOjE2OTg1ODg3Njd9.ZmYgmXgEtOFHyITVZGmkOcYw-5al8EIf3Y5qmhjwuWTPs8ZAuHyMZWqOCz5-X8QtlkBsd3-AqErkJq7RWUHQaw" },
                },
                        Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json")
                        {
                            Headers =
                {
                    ContentType = new MediaTypeHeaderValue("application/json")
                }
                        }
                    };
                    using (var response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();
                        var body = await response.Content.ReadAsStringAsync();
                    }


                }

            }
            catch (Exception )
            {
                throw;
            }


        }


        public async Task patchDoiBongSupplier(DoiBongSupplierRequest value, string token = "")
        {
            List<object> patchObjects = new List<object>();
            patchObjects.Add(new PatchObject<string>() { op="replace",path= "endDate",value= value.endDate.ToString() });
            patchObjects.Add(new PatchObject<string>() { op="replace",path= "duration", value= value.duration.ToString() });
            patchObjects.Add(new PatchObject<string>() { op="replace",path= "status", value= value.status.ToString() });
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = new HttpMethod("PATCH"),
                        RequestUri = new Uri($"{BASE_URL}/api/DoibongSuppliers/patch/{value.idSupplier}/{value.idDoiBong}"),
                        Headers =
                {
                    { "accept", "application/json" },
                     { "Authorization", "Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Ik5ndXnhu4VuIFRo4buLIMOBdCBNaW5oIiwiZW1haWwiOiJoaWVubGVwaGFuMjAwM0BnbWFpbC5jb20iLCJqdGkiOiI4YmRlNWI2Ni02YWIwLTQwNmUtOGE5Mi0xYTgyMmZlZmE0ZjgiLCJVc2VyTmFtZSI6ImFkbWluIiwiSWQiOiIxIiwiUm9sZSI6IkFkbWluIiwidG9rZW5JZCI6Ijc5OThiZGQ0LTRmYTQtNDhjYS1iMGNmLWI0ZmU0NWVmODhhOSIsIm5iZiI6MTY5ODU4ODc2NywiZXhwIjoxNzAxMjY3MTY3LCJpYXQiOjE2OTg1ODg3Njd9.ZmYgmXgEtOFHyITVZGmkOcYw-5al8EIf3Y5qmhjwuWTPs8ZAuHyMZWqOCz5-X8QtlkBsd3-AqErkJq7RWUHQaw" },
                },
                        Content = new StringContent(JsonConvert.SerializeObject(patchObjects), Encoding.UTF8, "application/json")
                        {
                            Headers =
                {
                    ContentType = new MediaTypeHeaderValue("application/json")
                }
                        }
                    };
                    using (var response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();
                        var body = await response.Content.ReadAsStringAsync();
                    }


                }

            }
            catch (Exception )
            {
                throw;
            }


        }


        public async Task UpdateStatusDoiBongSupplier(DOIBONGSUPPLIER value, string token = "")
        {
            List<object> patchObjects = new List<object>();
            patchObjects.Add(new PatchObject<string>() { op = "replace", path = "status", value = value.status.ToString() });
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = new HttpMethod("PATCH"),
                        RequestUri = new Uri($"{BASE_URL}/api/DoibongSuppliers/patch/{value.idSupplier}/{value.idDoiBong}"),
                        Headers =
                {
                    { "accept", "application/json" },
                     { "Authorization", "Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Ik5ndXnhu4VuIFRo4buLIMOBdCBNaW5oIiwiZW1haWwiOiJoaWVubGVwaGFuMjAwM0BnbWFpbC5jb20iLCJqdGkiOiI4YmRlNWI2Ni02YWIwLTQwNmUtOGE5Mi0xYTgyMmZlZmE0ZjgiLCJVc2VyTmFtZSI6ImFkbWluIiwiSWQiOiIxIiwiUm9sZSI6IkFkbWluIiwidG9rZW5JZCI6Ijc5OThiZGQ0LTRmYTQtNDhjYS1iMGNmLWI0ZmU0NWVmODhhOSIsIm5iZiI6MTY5ODU4ODc2NywiZXhwIjoxNzAxMjY3MTY3LCJpYXQiOjE2OTg1ODg3Njd9.ZmYgmXgEtOFHyITVZGmkOcYw-5al8EIf3Y5qmhjwuWTPs8ZAuHyMZWqOCz5-X8QtlkBsd3-AqErkJq7RWUHQaw" },
                },
                        Content = new StringContent(JsonConvert.SerializeObject(patchObjects), Encoding.UTF8, "application/json")
                        {
                            Headers =
                {
                    ContentType = new MediaTypeHeaderValue("application/json")
                }
                        }
                    };
                    using (var response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();
                        var body = await response.Content.ReadAsStringAsync();
                    }


                }

            }
            catch (Exception)
            {
                throw;
            }


        }
        //DOIBONGSUPPLER


        //DOIBONG
        public async Task<DOIBONG> getDoiBongById(string idDoiBong)
        {

            DOIBONG res = new DOIBONG();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{BASE_URL}/api/Doibongs/{idDoiBong}");
                if (response.IsSuccessStatusCode)
                {
                    string temp = await response.Content.ReadAsStringAsync();
                    DOIBONG responses = JsonConvert.DeserializeObject<DOIBONG>(temp);
                    res = responses;

                }
            }
            return res;
        }


        //DOIBONG

        //SERVICES
        public async Task<List<SERVICE>> getAllServices()
        {
            List<SERVICE> data = new List<SERVICE>();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{BASE_URL}/api/Services");
                if (response.IsSuccessStatusCode)
                {
                    string temp = await response.Content.ReadAsStringAsync();
                    Response<SERVICE> responses = JsonConvert.DeserializeObject<Response<SERVICE>>(temp);
                    data = responses.data;
                }
            }
            return data;
        }

        public async Task<SERVICE> postNewServices(SERVICE value, string token = "")
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri($"{BASE_URL}/api/Services/create"),
                        Headers =
                {
                    { "accept", "application/json" },
                     { "Authorization", "Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Ik5ndXnhu4VuIFRo4buLIMOBdCBNaW5oIiwiZW1haWwiOiJoaWVubGVwaGFuMjAwM0BnbWFpbC5jb20iLCJqdGkiOiI4YmRlNWI2Ni02YWIwLTQwNmUtOGE5Mi0xYTgyMmZlZmE0ZjgiLCJVc2VyTmFtZSI6ImFkbWluIiwiSWQiOiIxIiwiUm9sZSI6IkFkbWluIiwidG9rZW5JZCI6Ijc5OThiZGQ0LTRmYTQtNDhjYS1iMGNmLWI0ZmU0NWVmODhhOSIsIm5iZiI6MTY5ODU4ODc2NywiZXhwIjoxNzAxMjY3MTY3LCJpYXQiOjE2OTg1ODg3Njd9.ZmYgmXgEtOFHyITVZGmkOcYw-5al8EIf3Y5qmhjwuWTPs8ZAuHyMZWqOCz5-X8QtlkBsd3-AqErkJq7RWUHQaw" },
                },
                        Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json")
                        {
                            Headers =
                {
                    ContentType = new MediaTypeHeaderValue("application/json")
                }
                        }
                    };
                    using (var response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();
                        var body = await response.Content.ReadAsStringAsync();
                        SERVICE responses = JsonConvert.DeserializeObject<SERVICE>(body);
                        return responses;
                    }


                }

            }
            catch (Exception)
            {
                throw;
            }


        }
        public async Task deleteServices(int idService, string token = "")
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri($"{BASE_URL}/api/Services/delete/{idService}"),
                        Headers =
                {
                    { "accept", "application/json" },
                     { "Authorization", "Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Ik5ndXnhu4VuIFRo4buLIMOBdCBNaW5oIiwiZW1haWwiOiJoaWVubGVwaGFuMjAwM0BnbWFpbC5jb20iLCJqdGkiOiI4YmRlNWI2Ni02YWIwLTQwNmUtOGE5Mi0xYTgyMmZlZmE0ZjgiLCJVc2VyTmFtZSI6ImFkbWluIiwiSWQiOiIxIiwiUm9sZSI6IkFkbWluIiwidG9rZW5JZCI6Ijc5OThiZGQ0LTRmYTQtNDhjYS1iMGNmLWI0ZmU0NWVmODhhOSIsIm5iZiI6MTY5ODU4ODc2NywiZXhwIjoxNzAxMjY3MTY3LCJpYXQiOjE2OTg1ODg3Njd9.ZmYgmXgEtOFHyITVZGmkOcYw-5al8EIf3Y5qmhjwuWTPs8ZAuHyMZWqOCz5-X8QtlkBsd3-AqErkJq7RWUHQaw" },
                }
                       
                    };
                    using (var response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();
                    }


                }

            }
            catch (Exception)
            {
                throw;
            }


        }
        public async Task updateService(int idService, SERVICE value)
        {
            var temp = new
            {
                idService = value.idService,
                serviceName = value.serviceName,
                detail = value.detail,
                images = value.images,
            };
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Put,
                        RequestUri = new Uri($"{BASE_URL}/api/Services/update/{idService}"),
                        Headers =
                {
                    { "accept", "application/json" },
                     { "Authorization", "Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Ik5ndXnhu4VuIFRo4buLIMOBdCBNaW5oIiwiZW1haWwiOiJoaWVubGVwaGFuMjAwM0BnbWFpbC5jb20iLCJqdGkiOiI4YmRlNWI2Ni02YWIwLTQwNmUtOGE5Mi0xYTgyMmZlZmE0ZjgiLCJVc2VyTmFtZSI6ImFkbWluIiwiSWQiOiIxIiwiUm9sZSI6IkFkbWluIiwidG9rZW5JZCI6Ijc5OThiZGQ0LTRmYTQtNDhjYS1iMGNmLWI0ZmU0NWVmODhhOSIsIm5iZiI6MTY5ODU4ODc2NywiZXhwIjoxNzAxMjY3MTY3LCJpYXQiOjE2OTg1ODg3Njd9.ZmYgmXgEtOFHyITVZGmkOcYw-5al8EIf3Y5qmhjwuWTPs8ZAuHyMZWqOCz5-X8QtlkBsd3-AqErkJq7RWUHQaw" },
                },
                        Content = new StringContent(JsonConvert.SerializeObject(temp), Encoding.UTF8, "application/json")
                        {
                            Headers =
                {
                    ContentType = new MediaTypeHeaderValue("application/json")
                }
                        }
                    };
                    using (var response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();
                        var body = await response.Content.ReadAsStringAsync();
                    }


                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        //SERVICES


        //SUPPLIERSERVICES
        public async Task<List<SUPPLIERSERVICE>> getAllSupplierServices()
        {
            List<SUPPLIERSERVICE> data = new List<SUPPLIERSERVICE>();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{BASE_URL}/api/SupplierServices");
                if (response.IsSuccessStatusCode)
                {
                    string temp = await response.Content.ReadAsStringAsync();
                    Response<SUPPLIERSERVICE> responses = JsonConvert.DeserializeObject<Response<SUPPLIERSERVICE>>(temp);
                    data = responses.data;
                }
            }
            return data;
        }

        public async Task<SUPPLIERSERVICE> postNewSupplierService(SUPPLIERSERVICE value, string token = "")
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri($"{BASE_URL}/api/SupplierServices/create"),
                        Headers =
                {
                    { "accept", "application/json" },
                     { "Authorization", "Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Ik5ndXnhu4VuIFRo4buLIMOBdCBNaW5oIiwiZW1haWwiOiJoaWVubGVwaGFuMjAwM0BnbWFpbC5jb20iLCJqdGkiOiI4YmRlNWI2Ni02YWIwLTQwNmUtOGE5Mi0xYTgyMmZlZmE0ZjgiLCJVc2VyTmFtZSI6ImFkbWluIiwiSWQiOiIxIiwiUm9sZSI6IkFkbWluIiwidG9rZW5JZCI6Ijc5OThiZGQ0LTRmYTQtNDhjYS1iMGNmLWI0ZmU0NWVmODhhOSIsIm5iZiI6MTY5ODU4ODc2NywiZXhwIjoxNzAxMjY3MTY3LCJpYXQiOjE2OTg1ODg3Njd9.ZmYgmXgEtOFHyITVZGmkOcYw-5al8EIf3Y5qmhjwuWTPs8ZAuHyMZWqOCz5-X8QtlkBsd3-AqErkJq7RWUHQaw" },
                },
                        Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json")
                        {
                            Headers =
                {
                    ContentType = new MediaTypeHeaderValue("application/json")
                }
                        }
                    };
                    using (var response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();
                        var body = await response.Content.ReadAsStringAsync();
                        SUPPLIERSERVICE responses = JsonConvert.DeserializeObject<SUPPLIERSERVICE>(body);
                        return responses;
                    }


                }

            }
            catch (Exception)
            {
                throw;
            }


        }

        public async Task deleteSupplerService(int idService,int idSupplier, string token = "")
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri($"{BASE_URL}/api/SupplierServices/delete/{idService}/{idSupplier}"),
                        Headers =
                {
                    { "accept", "application/json" },
                     { "Authorization", "Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Ik5ndXnhu4VuIFRo4buLIMOBdCBNaW5oIiwiZW1haWwiOiJoaWVubGVwaGFuMjAwM0BnbWFpbC5jb20iLCJqdGkiOiI4YmRlNWI2Ni02YWIwLTQwNmUtOGE5Mi0xYTgyMmZlZmE0ZjgiLCJVc2VyTmFtZSI6ImFkbWluIiwiSWQiOiIxIiwiUm9sZSI6IkFkbWluIiwidG9rZW5JZCI6Ijc5OThiZGQ0LTRmYTQtNDhjYS1iMGNmLWI0ZmU0NWVmODhhOSIsIm5iZiI6MTY5ODU4ODc2NywiZXhwIjoxNzAxMjY3MTY3LCJpYXQiOjE2OTg1ODg3Njd9.ZmYgmXgEtOFHyITVZGmkOcYw-5al8EIf3Y5qmhjwuWTPs8ZAuHyMZWqOCz5-X8QtlkBsd3-AqErkJq7RWUHQaw" },
                }

                    };
                    using (var response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();
                    }


                }

            }
            catch (Exception)
            {
                throw;
            }


        }

        //SUPPLIERSERVICES
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

    public class PatchObject<T>
    {
        public string op { get; set; }
        public string path { get; set; }
        public T value { get; set; }
    }

}

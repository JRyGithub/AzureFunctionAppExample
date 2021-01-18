using System;
using Models;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Services
{
    public class APICallService
    {
        public string Cn { get; set; }
        public string VSDCAddress { get; set; }
        public ValidationService validation = new ValidationService();
        public async Task<string> ExecuteRequest(JsonModel request, ILogger log)
        {
            try
            {
                using (var handler = new HttpClientHandler())
                {                 
                    handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                    handler.ClientCertificates.Add(LoadMyCertificate(Cn));

                    handler.ServerCertificateCustomValidationCallback +=
                                    (sender, certificate, chain, errors) =>
                                    {
                                        log.LogInformation($"Ssl Policy sender: {sender}");
                                        log.LogInformation($"Ssl Policy certificate: {certificate}");
                                        log.LogInformation($"Ssl Policy chain: {chain}");
                                        log.LogInformation($"Ssl Policy Errors: {errors}");

                                        return true;
                                    };


                    using (var client = new HttpClient(handler))
                    {
                        client.BaseAddress = new Uri(VSDCAddress);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var serializedJson = JsonConvert.SerializeObject(request);

                        var response = await client.PostAsync($"{VSDCAddress}", new StringContent(
                        serializedJson, Encoding.UTF8, "application/json"));

                        var responseContent = response.Content.ReadAsStringAsync().Result;

                        if (validation.ValidateResponse(responseContent))
                        {
                            return response.IsSuccessStatusCode ? responseContent : "Status code fail reason:" + response.ReasonPhrase + " = " + responseContent;
                        }
                        else
                        {
                            throw new Exception("API fail:" + response.Content);
                        }   
                        
                    }
                }
            }
            catch (Exception e)
            {
                log.LogInformation($"Error: {e.InnerException}.");
                throw;
            }
        }

        public static X509Certificate2 LoadMyCertificate(string cn)
        {
            try
            {
                var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                
                store.Open(OpenFlags.MaxAllowed | OpenFlags.ReadOnly);
                var result = store.Certificates.Find(X509FindType.FindByThumbprint, cn, false);
         
                return (result.Count > 0) ? result[0] : throw new Exception("Certificate failure");
                
            }
            catch(Exception e) 
            {
                throw new Exception("Certificate failure:" + e.Message);
            }
        }
    }
}

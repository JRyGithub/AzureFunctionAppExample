using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Models;
using Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Functions
{
    public class PdfGenerator
    {
        public readonly AppSettingsOptions _options;

        public PdfGenerator(IOptions<AppSettingsOptions> options)
        {
            _options = options.Value;
        }
        [FunctionName("pdfgenerator")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP Trigger function processed a request.");


            IActionResult result;

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject<RequestModel>(requestBody);

                PdfGeneratorOrchestratorService pdfGeneratorOrchestrator = new PdfGeneratorOrchestratorService(_options, data);

                result = await pdfGeneratorOrchestrator.ExecutePdfGeneration(log);
                
            }
            catch (Exception e)
            {
                return new OkObjectResult("Error recieving request:" + e.Message);
            }


            return result;
        }
     }
}

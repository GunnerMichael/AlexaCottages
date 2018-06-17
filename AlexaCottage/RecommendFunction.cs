using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace AlexaCottage
{
    public static class RecommendFunction
    {
        [FunctionName("Recommend")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP Recommend function processed a request.");


            dynamic data = await req.Content.ReadAsAsync<object>();

            log.Info($"Content={data}");


            if (data.request.type == "LaunchRequest")
            {
                // default launch request, let's just let them know what you can do
                log.Info($"Default LaunchRequest made");

                return DefaultRequest(req);

            }
            else if (data.request.type == "IntentRequest")
            {
                // Set name to query string or body data
                string intentName = data.request.intent.name;
                log.Info($"intentName={intentName}");

                switch (intentName)
                {
                    case "AddIntent":
                        return new RecommendIntent().Run(data.request.intent, req);
                        

         // Add more intents and default responses
                    default:
                        return DefaultRequest(req);
                }

            }
            else
            {
                return DefaultRequest(req);
            }
        }

        private static HttpResponseMessage DefaultRequest(HttpRequestMessage req)
        {
            return req.CreateResponse(HttpStatusCode.OK, new
            {
                version = "1.0",
                sessionAttributes = new { },
                response = new
                {
                    outputSpeech = new
                    {
                        type = "PlainText",
                        text = "Welcome to Michael Cottages recommendation"
                    },
                    card = new
                    {
                        type = "Simple",
                        title = "Michael Cottages recommendation",
                        content = "Welcome to Michael Cottages recommendation"
                    },
                    shouldEndSession = true
                }
            });
        }
    }
}

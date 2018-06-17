using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AlexaCottage
{
    public class RecommendIntent
    {
        public  HttpResponseMessage Run(dynamic intent, HttpRequestMessage req)
        {
            var location = intent.slots["Location"].value;

            string name = $"This weeks recommendation is Michael Farm Cottage. Located in {location}, It sleeps 5 people and has a rating of 9 out of 10";
             return req.CreateResponse(HttpStatusCode.OK, new
            {
                version = "1.0",
                sessionAttributes = new { },
                response = new
                {
                    outputSpeech = new
                    {
                        type = "PlainText",
                        text = $"{name}."
                    },
                    card = new
                    {
                        type = "Simple",
                        title = "Michael Cottage",
                        content = $"{name}"
                    },
                    shouldEndSession = true
                }
            });

        }
    }
}

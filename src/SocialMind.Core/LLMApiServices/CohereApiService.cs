using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocialMind.Core.LLMApiServices
{
    public class CohereApiService : LLMApiService
    {
        public CohereApiService(HttpClient httpClient, string apiEndpoint, string apiKey)
            : base(httpClient, apiEndpoint, apiKey) { }

        protected override object CreatePayload(string? message)
        {
            return new
                   {
                       model = "command-r-plus-08-2024",
                       messages = new[]
                                  {
                                      new
                                      {
                                          role = "user",
                                          content = message
                                      }
                                  }
                   };
        }

        public override string? ResponseParser(string response)
        {
            using JsonDocument jsonDoc = JsonDocument.Parse(response);
            return jsonDoc.RootElement.GetProperty("message")
                          .GetProperty("content")[0]
                          .GetProperty("text")
                          .GetString();
        }
    }
}

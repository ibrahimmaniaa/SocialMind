using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMind.Core.LLMApiServices
{
    public interface ILanguageModelService
    {
        Task<string> GetResponseAsync(string? message);

        string? ResponseParser(string response);
    }
}

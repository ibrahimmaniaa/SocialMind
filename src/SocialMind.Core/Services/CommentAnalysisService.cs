using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using SocialMind.Core.Domain;

namespace SocialMind.Core.Services;

internal class CommentAnalysisService
{
    private readonly ILanguageModelClient _languageModelClient;
    private readonly ILogger<CommentAnalysisService> _logger;

    public CommentAnalysisService(ILanguageModelClient languageModelClient, ILogger<CommentAnalysisService> logger)
    {
        _languageModelClient = languageModelClient;
        _logger = logger;
    }


    public Task<IList<Sentiment>> JudgeComments(IPost post)
    {
        throw new NotImplementedException();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMind.Core.Domain;

/// <summary>
/// Enumeration representing sentiment classifications
/// </summary>
public enum Sentiment
{
    /// <summary>Indicates a positive sentiment.</summary>
    Positive,

    /// <summary>Indicates a negative sentiment.</summary>
    Negative,

    /// <summary>Indicates a neutral sentiment.</summary>
    Neutral,

    /// <summary>Indicates an unknown sentiment.</summary>
    Unknown
}
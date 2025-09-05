using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMind.Core.Domain;

public interface IPost
{
    /// <summary>
    /// Unique identifier for the post
    /// </summary>
    string Id { get; set; }

    /// <summary>
    /// Date and time when the post was created
    /// </summary>
    DateTime CreatedAt { get; set; }

    /// <summary>
    /// Comments associated with the post
    /// </summary>
    IList<IComment> Comments { get; set; }
}
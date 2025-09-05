using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMind.Core.Domain;

public interface IComment
{
    /// <summary>
    /// Date and time when the comment was created
    /// </summary>
    DateTime CreatedAt { get; set; }

    /// <summary>
    /// Text content of the comment
    /// </summary>
    string Content { get; set; }
}
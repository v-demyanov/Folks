using Folks.ChatService.Domain.Common;

namespace Folks.ChatService.Domain.Entities;

public class Group : BaseEntity
{
    public required string Title { get; set; }

    public ICollection<string> UserIds { get; set; } = new HashSet<string>();
}

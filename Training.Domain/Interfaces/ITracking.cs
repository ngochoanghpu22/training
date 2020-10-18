using System;
using System.Collections.Generic;
using System.Text;

namespace Training.Domain.Interfaces
{
    public interface ITracking
    {
        DateTime CreationTime { get; set; }

        DateTime? LastModificationTime { get; set; }

        Guid CreatorId { get; set; }

        Guid? LastModifierId { get; set; }
    }
}

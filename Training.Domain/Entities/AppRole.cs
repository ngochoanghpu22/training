using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Training.Domain.Interfaces;

namespace Training.Domain.Entities
{
    [Table("AppRoles")]
    public class AppRole: IdentityRole<Guid>, ITracking
    {
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid CreatorId { get; set; }
        public Guid? LastModifierId { get; set; }

        [StringLength(255)]
        public string Description { get; set; }
    }
}

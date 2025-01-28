using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManagement.Domain.Common
{
    public class EntityBase
    {
        public string? CreatorId { get; set; }
        public string? CreatorName { get; set; }
        public DateTime CreationTime { get; set; }
        public string? LastmodifierId { get; set; }
        public string? LastmodifierName { get; set; }
        public DateTime? LastmodificationTime { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string? DeletedBy { get; set; }

        protected EntityBase()
        {
            CreationTime = DateTime.UtcNow;
            LastmodificationTime = DateTime.UtcNow;
        }
    }
}

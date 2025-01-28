

using MaintenanceManagement.Domain.Common;

namespace MaintenanceManagement.Domain.Entities
{
    public class Service :EntityBase
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public DateTime ServiceDate { get; set; }

        public ICollection<Task> Tasks { get; set; }  

    }
}

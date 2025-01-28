using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManagement.Application.Features.Dtos
{
    public class ServiceDto
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public DateTime ServiceDate { get; set; }
        public List<TaskDto> Tasks { get; set; }
    }
}

using MaintenanceManagement.Application.Contracts;
using MaintenanceManagement.Application.Features.Dtos;
using MaintenanceManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceManagement.Application.Features.MaintenanceService
{
    public class GetServicesQuery : IRequest<List<ServiceDto>>
    {
    }


    public class GetServicesQueryHandler : IRequestHandler<GetServicesQuery, List<ServiceDto>>
    {
        private readonly IRepository<Service> _repository;

        public GetServicesQueryHandler(IRepository<Service> repository)
        {
            _repository = repository;
        }

        public async Task<List<ServiceDto>> Handle(GetServicesQuery request, CancellationToken cancellationToken)
        {
            var servicesQuery = await _repository.GetAllAsync(service => !service.IsDeleted);

            var services = await servicesQuery
                .Include(service => service.Tasks) // Include related tasks
                .ToListAsync(cancellationToken);

            var serviceDtos = services.Select(service => new ServiceDto
            {
                ServiceId = service.ServiceId,
                ServiceName = service.ServiceName,
                ServiceDate = service.ServiceDate,
                Tasks = service.Tasks.Select(task => new TaskDto
                {
                    TaskName = task.TaskName,
                    Description = task.Description,
                    Remarks = task.Remarks
                }).ToList()
            }).ToList();

            return serviceDtos;
        }
    }
}
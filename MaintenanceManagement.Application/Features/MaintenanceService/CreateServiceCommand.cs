using AutoWrapper.Wrappers;
using MaintenanceManagement.Application.Contracts;
using MaintenanceManagement.Application.Features.Dtos;
using MaintenanceManagement.Domain.Entities;
using MediatR;
using Task = MaintenanceManagement.Domain.Entities.Task;

namespace MaintenanceManagement.Application.Features.MaintenanceService
{
    public class CreateServiceCommand : IRequest<ApiResponse>
    {
        public string ServiceName { get; set; }
        public DateTime ServiceDate { get; set; }
        public List<TaskDto> Tasks { get; set; }
    }

    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, ApiResponse>
    {
        private readonly IRepository<Service> _repository;

        public CreateServiceCommandHandler(IRepository<Service> repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            // Create the service object
            var service = new Service
            {
                ServiceName = request.ServiceName,
                ServiceDate = request.ServiceDate,
                Tasks = request.Tasks.Select(t => new Task
                {
                    TaskName = t.TaskName,
                    Description = t.Description,
                    Remarks = t.Remarks
                }).ToList()
            };

            // Add the service and its associated tasks to the database
            await _repository.AddAsync(service);
            await _repository.SaveChangesAsync(cancellationToken);

            return new ApiResponse(200, "Successfully submitted");
        }
    }
}

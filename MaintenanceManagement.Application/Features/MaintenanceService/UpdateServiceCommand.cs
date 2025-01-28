using AutoWrapper.Wrappers;
using MaintenanceManagement.Application.Contracts;
using MaintenanceManagement.Application.Features.Dtos;
using MaintenanceManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = MaintenanceManagement.Domain.Entities.Task;

namespace MaintenanceManagement.Application.Features.MaintenanceService
{
    public class UpdateServiceCommand : IRequest<ApiResponse>
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public DateTime ServiceDate { get; set; }
        public List<TaskDto> Tasks { get; set; }
    }

        public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, ApiResponse>
        {
            private readonly IRepository<Service> _repository;

            public UpdateServiceCommandHandler(IRepository<Service> repository)
            {
                _repository = repository;
            }

            public async Task<ApiResponse> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
            {


            var servicesQuery = await _repository.GetAllAsync( x=>x.ServiceId == request.ServiceId);

            var service = await servicesQuery
                .Include(service => service.Tasks) // Include related tasks
                .FirstOrDefaultAsync();

            if (service == null)
                {
                    return new ApiResponse(404, "Service not found");
                }

                service.ServiceName = request.ServiceName;
                service.ServiceDate = request.ServiceDate;

            // Update tasks
            // Clear existing tasks and add new ones
            service.Tasks.Clear();
            foreach (var taskDto in request.Tasks)
            {
                service.Tasks.Add(new Task
                {
                    TaskName = taskDto.TaskName,
                    Description = taskDto.Description,
                    Remarks = taskDto.Remarks
                });
            }


            await _repository.SaveChangesAsync(cancellationToken);

                return new ApiResponse(200, "Data updated successfully");
            }
        }
    }


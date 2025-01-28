using AutoWrapper.Wrappers;
using MaintenanceManagement.Application.Contracts;
using MaintenanceManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManagement.Application.Features.MaintenanceService
{
    public class DeleteServiceCommand : IRequest<ApiResponse>
    {
        public int ServiceId { get; set; }

        public DeleteServiceCommand(int serviceId)
        {
            ServiceId = serviceId;
        }
    }


    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, ApiResponse>
    {
        private readonly IRepository<Service> _repository;

        public DeleteServiceCommandHandler(IRepository<Service> repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _repository.GetByIdAsync(request.ServiceId);

            if (service == null)
            {
                return new ApiResponse(404, "Service not found");
            }

            service.IsDeleted = true;
            await _repository.SaveChangesAsync(cancellationToken);


            return new ApiResponse(200, "Data Deleted Successfully!");
        }
    }
}
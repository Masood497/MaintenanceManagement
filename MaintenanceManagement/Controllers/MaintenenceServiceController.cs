using Microsoft.AspNetCore.Mvc;
using AutoWrapper.Wrappers;
using MaintenanceManagement.Application.Features.MaintenanceService;
using MaintenanceManagement.Application.Features.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace MaintenanceManagement.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class MaintenenceServiceController : BaseApiController
    {

        [HttpPost("addservice")]

        public async Task<ApiResponse> PostService([FromBody] CreateServiceCommand request)
        {
            var result = await Mediator.Send(request);

            return new ApiResponse(200,"successfully saved");
        }


        [HttpGet("getservices")]

        public async Task<List<ServiceDto>> GetServices()
        {
            var query = new GetServicesQuery();
            var services = await Mediator.Send(query);

            return services;
        }

        [HttpPut("updateservice")]

        public async Task<ApiResponse> PutService([FromBody] UpdateServiceCommand request)
        {
            await Mediator.Send(request);

            return new ApiResponse(200,"Data updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> DeleteService(int id)
        {
            var command = new DeleteServiceCommand(id);
            await Mediator.Send(command);

            return new ApiResponse(200,"Data Deleted Successfully!");
        }

    }
}

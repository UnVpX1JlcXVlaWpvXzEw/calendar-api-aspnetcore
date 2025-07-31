using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Response;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CreateCalendar;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Exception.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICreateCalendar createCalendar;

        public CalendarController(IServiceProvider provider)
        {
            createCalendar = provider.GetRequiredService<ICreateCalendar>();
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateCalendarResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.InternalServerError)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateCalendarAsync(
            [FromBody] CreateCalendarRequest request,
            CancellationToken cancellationToken)
        {
            var ownerIdClaim = User.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier ||
                c.Type == "sub");

            if (ownerIdClaim == null || !Guid.TryParse(
                ownerIdClaim.Value,
                out var ownerId))
                return BadRequest("Invalid or missing ownerId");

            var response = await createCalendar.CreateAsync(
                request.Name,
                ownerId,
                cancellationToken);

            return Created(string.Empty, response);
        }
    }
}

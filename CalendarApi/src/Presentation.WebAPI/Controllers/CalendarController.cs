using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CreateCalendar;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Exception.Common;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateCalendarAsync(
            [FromBody] CreateCalendarRequest request,
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var id = await createCalendar.CreateAsync(
                request,
                cancellationToken);

            return Created(string.Empty, id);
        }
    }
}

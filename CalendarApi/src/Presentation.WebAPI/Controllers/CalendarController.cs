using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Response;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CreateCalendar;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CreateEvent;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.DeleteCalendar;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.GetCalendar;
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
        private readonly IGetCalendars getCalendars;
        private readonly ICreateEvent createEvent;
        private readonly IDeleteCalendar deleteCalendar;

        public CalendarController(IServiceProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider, nameof(provider));

            createCalendar = provider.GetRequiredService<ICreateCalendar>();
            getCalendars = provider.GetRequiredService<IGetCalendars>();
            createEvent = provider.GetRequiredService<ICreateEvent>();
            deleteCalendar = provider.GetRequiredService<IDeleteCalendar>();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateCalendarAsync(
            [FromBody] CreateCalendarRequest request,
            CancellationToken cancellationToken = default)
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

        [HttpGet]
        [ProducesResponseType(typeof(CalendarSummary), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetCalendarsAsync(CancellationToken cancellationToken = default)
        {
            var response = await getCalendars.GetAsync(cancellationToken);

            return this.Ok(response);
        }

        [HttpPost("calendars{calendarId}/event")]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateEventAsync(
            Guid calendarId,
            [FromBody] CreateEventRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var eventId = await createEvent.CreateAsync(
                calendarId,
                request,
                cancellationToken);

            return Created(string.Empty, eventId);
        }

        [HttpDelete("/calendars/{calendarId}")]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteEventAsync(
            Guid calendarId,
            CancellationToken cancellationToken = default)
        {

            await deleteCalendar.DeleteAsync(calendarId, cancellationToken);

            return Ok();
        }
    }
}


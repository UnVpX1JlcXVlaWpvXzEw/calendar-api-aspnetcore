using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Response;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CreateCalendar;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CreateEvent;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.DeleteCalendar;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.DeleteEvent;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.DeleteReminder;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.GetCalendar;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.GetEvent;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.UpdateEvent;
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
        private readonly IDeleteEvent deleteEvent;
        private readonly IUpdateEvent updateEvent;
        private readonly IDeleteReminder deleteReminder;
        private readonly IGetEventOccurrences getEventOccurrences;

        public CalendarController(IServiceProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider, nameof(provider));

            createCalendar = provider.GetRequiredService<ICreateCalendar>();
            getCalendars = provider.GetRequiredService<IGetCalendars>();
            createEvent = provider.GetRequiredService<ICreateEvent>();
            deleteCalendar = provider.GetRequiredService<IDeleteCalendar>();
            deleteEvent = provider.GetRequiredService<IDeleteEvent>();
            updateEvent = provider.GetRequiredService<IUpdateEvent>();
            deleteReminder = provider.GetRequiredService<IDeleteReminder>();
            getEventOccurrences = provider.GetRequiredService<IGetEventOccurrences>();
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
        public async Task<IActionResult> DeleteCalendarAsync(
            Guid calendarId,
            CancellationToken cancellationToken = default)
        {
            await deleteCalendar.DeleteCalendarAsync(calendarId, cancellationToken);

            return Ok();
        }

        [HttpDelete("/calendars/{calendarId}/event/{eventId}")]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteEventAsync(
            Guid calendarId,
            Guid eventId,
            CancellationToken cancellationToken = default)
        {
            await deleteEvent.DeleteEventAsync(calendarId, eventId, cancellationToken);

            return Ok();
        }

        [HttpPut("/calendars/{calendarId}/events/{eventId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateEventAsync(
            Guid calendarId,
            Guid eventId,
            [FromBody] UpdateEventRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request is null)
                return BadRequest();

            await updateEvent.UpdateEventAsync(
                calendarId,
                eventId,
                request,
                cancellationToken);

            return Ok();
        }

        [HttpDelete("/calendars/{calendarId}/events/{eventId}/reminders/{reminderId}")]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteReminderAsync(
            Guid calendarId,
            Guid eventId,
            Guid reminderId,
            CancellationToken cancellationToken = default)
        {
            await deleteReminder.DeleteReminderAsync(
                calendarId,
                eventId,
                reminderId,
                cancellationToken);

            return Ok();
        }

        [HttpGet("calendars/{calendarId}/occurrences")]
        [ProducesResponseType(typeof(EventSummary), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetEventsAsync(Guid calendarId,
            DateTime from,
            DateTime after,
            CancellationToken cancellationToken = default)
        {
            var response = await getEventOccurrences.GetEventSummariesAsync(
                calendarId,
                from,
                after,
                cancellationToken);

            return this.Ok(response);
        }
    }
}


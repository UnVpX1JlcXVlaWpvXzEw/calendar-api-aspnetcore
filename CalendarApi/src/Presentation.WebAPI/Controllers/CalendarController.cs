namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Controllers
{
    using HustleAddiction.Platform.CalendarApi.Domain.Services.EventOccurrenceService;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Response;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CreateCalendar;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CreateEvent;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.DeleteCalendar;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.DeleteEvent;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.DeleteReminder;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.GetCalendar;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.GetEventByCalendar;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.RescheduleNotificationJob;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.UpdateEvent;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Exception.Common;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;


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
        private readonly IGetEventByCalendar getEventOccurrences;
        private readonly IRescheduleNotificationJob rescheduleNotificationJob;

        public CalendarController(IServiceProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider);

            createCalendar = provider.GetRequiredService<ICreateCalendar>();
            getCalendars = provider.GetRequiredService<IGetCalendars>();
            createEvent = provider.GetRequiredService<ICreateEvent>();
            deleteCalendar = provider.GetRequiredService<IDeleteCalendar>();
            deleteEvent = provider.GetRequiredService<IDeleteEvent>();
            updateEvent = provider.GetRequiredService<IUpdateEvent>();
            deleteReminder = provider.GetRequiredService<IDeleteReminder>();
            getEventOccurrences = provider.GetRequiredService<IGetEventByCalendar>();
            rescheduleNotificationJob = provider.GetRequiredService<IRescheduleNotificationJob>();
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
                return BadRequest();

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

        [HttpPost("calendars/{calendarId}/event")]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateEventAsync(
            [FromRoute] Guid calendarId,
            [FromBody] CreateEventRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request is null)
                return BadRequest();

            var eventId = await createEvent.CreateAsync(
                calendarId,
                request,
                cancellationToken);

            return Created(string.Empty, eventId);
        }

        [HttpDelete("calendars/{calendarId}")]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteCalendarAsync(
            [FromRoute] Guid calendarId,
            CancellationToken cancellationToken = default)
        {
            await deleteCalendar.DeleteCalendarAsync(calendarId, cancellationToken);

            return Ok();
        }

        [HttpDelete("calendars/{calendarId}/event/{eventId}")]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteEventAsync(
            [FromRoute] Guid calendarId,
            [FromRoute] Guid eventId,
            CancellationToken cancellationToken = default)
        {
            await deleteEvent.DeleteEventAsync(calendarId, eventId, cancellationToken);

            return Ok();
        }

        [HttpPut("calendars/{calendarId}/events/{eventId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateEventAsync(
            [FromRoute] Guid calendarId,
            [FromRoute] Guid eventId,
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

        [HttpDelete("calendars/{calendarId}/events/{eventId}/reminders/{reminderId}")]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteReminderAsync(
            [FromRoute] Guid calendarId,
            [FromRoute] Guid eventId,
            [FromRoute] Guid reminderId,
            CancellationToken cancellationToken = default)
        {
            await deleteReminder.DeleteReminderAsync(
                calendarId,
                eventId,
                reminderId,
                cancellationToken);

            return Ok();
        }

        [HttpGet("calendars/{calendarId}/events")]
        [ProducesResponseType(typeof(EventDetails), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetEventsAsync(
            [FromRoute] Guid calendarId,
            [FromQuery] GetEventOcurrencesRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var response = await getEventOccurrences.GetEventSummariesAsync(
                calendarId,
                request,
                cancellationToken);

            return this.Ok(response);
        }
    }
}
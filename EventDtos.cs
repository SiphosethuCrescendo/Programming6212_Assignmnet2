using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceDay.Core.DTOs
{
    // What the client sends to create an event
    public class CreateEventDto
{
    public string EventName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public DateTime RegistrationStartDate { get; set; }
    public DateTime RegistrationEndDate { get; set; }
    public string Location { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public int MaxParticipants { get; set; }
    public string Status { get; set; } = string.Empty;
    public string RouteDescription { get; set; } = string.Empty;
}

// What the client sends to update an event
public class UpdateEventDto
{
    public string EventName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public DateTime RegistrationStartDate { get; set; }
    public DateTime RegistrationEndDate { get; set; }
    public string Location { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public int MaxParticipants { get; set; }
    public string Status { get; set; } = string.Empty;
    public string RouteDescription { get; set; } = string.Empty;
}
}

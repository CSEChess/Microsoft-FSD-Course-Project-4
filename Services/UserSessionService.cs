using System.Collections.Generic;
using System.Linq;

namespace EventEase.Services;

public class UserSessionService
{
    // 🔒 Current active session (existing logic)
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? SelectedEvent { get; set; }

    public bool IsRegistered =>
        !string.IsNullOrWhiteSpace(Name) &&
        !string.IsNullOrWhiteSpace(Email) &&
        !string.IsNullOrWhiteSpace(SelectedEvent);

    public void Reset()
    {
        Name = null;
        Email = null;
        SelectedEvent = null;
    }

    // 📦 New logic: all registrations
    public List<RegistrationRecord> AllRegistrations { get; } = new();

    public void AddRegistration(string name, string email, string selectedEvent)
    {
        AllRegistrations.Add(new RegistrationRecord
        {
            Name = name,
            Email = email,
            SelectedEvent = selectedEvent
        });

        // Optionally update current session too
        Name = name;
        Email = email;
        SelectedEvent = selectedEvent;
    }

    public Dictionary<string, int> GetAttendanceStats()
    {
        return AllRegistrations
            .GroupBy(r => r.SelectedEvent ?? "Unknown")
            .ToDictionary(g => g.Key, g => g.Count());
    }

    public class RegistrationRecord
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? SelectedEvent { get; set; }
    }
}
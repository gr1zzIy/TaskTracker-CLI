using System;
using System.Text.Json.Serialization;

namespace TaskTrackerCLI.Models;

public class TaskItem
{
    public int Id { get; init; }
    public string Description { get; set; } = string.Empty;
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Status Status { get; set; } = Status.Todo;
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; } = null;
}

public enum Status
{
    Todo,
    InProgress,
    Done
}
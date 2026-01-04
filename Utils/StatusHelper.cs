using TaskTrackerCLI.Models;

namespace TaskTrackerCLI.Utils;

public class StatusHelper
{
    /// <summary>
    /// Парсимо стасус зі стрінга
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public static Status ParseStatus(string status)
    {
        return status.ToLower() switch
        {
            "todo" or "t" => Status.Todo,
            "in-progress" or "inprogress" or "ip" => Status.InProgress,
            "completed" or "done" or "finished" => Status.Done,
            _ => Status.Todo
        };
    }

    /// <summary>
    /// Повертаємо статус в стрінг
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public static string StatusToString(Status status)
    {
        return status switch
        {
            Status.Todo => "todo",
            Status.InProgress => "in-progress",
            Status.Done => "done",
            _ => "todo"
        };
    }
}
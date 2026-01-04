using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using TaskTrackerCLI.Models;

namespace TaskTrackerCLI.Services;

public class TaskFileService
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonOptions;

    public TaskFileService(string filePath = "tasks.json")
    {
        _filePath = filePath;
        
        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,                    // Красиве форматування
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // camelCase в JSON
            PropertyNameCaseInsensitive = true,      // Нечутливість до регістру
            AllowTrailingCommas = true,              // Дозвіл кінцевих ком
            ReadCommentHandling = JsonCommentHandling.Skip, // Пропускати коментарі
            Converters = { new JsonStringEnumConverter() }, // Додаємо конвертер
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic), // Фікс для кирилиці
        };
        
        EnsureFileExists();
    }

    private void EnsureFileExists()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                SaveTasks(new List<TaskItem>());
            }
        }
        catch (Exception e)
        {
            throw new Exception($"Не вдалося створити файл {_filePath}: {e.Message}", e);
        }
    }

    public void SaveTasks(List<TaskItem> tasks)
    {
        string json = JsonSerializer.Serialize(tasks, _jsonOptions);
        File.WriteAllText(_filePath, json);
    }

    public List<TaskItem> LoadTasks()
    {
        if (!File.Exists(_filePath))
        {
            return new List<TaskItem>();
        }
        
        string json = File.ReadAllText(_filePath);

        if (string.IsNullOrEmpty(json))
        {
            return new List<TaskItem>();
        }

        try
        {
            return JsonSerializer.Deserialize<List<TaskItem>>(json, _jsonOptions) ?? new List<TaskItem>();
        }
        catch (JsonException)
        {
            return new List<TaskItem>();
        }
    }
}
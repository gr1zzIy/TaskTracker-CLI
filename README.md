# TaskTracker CLI

TaskTracker CLI is a simple command-line application for managing tasks. It allows you to add, update, delete, and track tasks directly from the terminal.

This project was built as a solution to the **Task Tracker CLI** project from [roadmap.sh](https://roadmap.sh/projects/task-tracker) and follows all the required specifications.

---

## Features

* Add new tasks
* Update existing tasks
* Delete tasks
* Mark tasks as `in-progress` or `done`
* List all tasks
* Filter tasks by status (`todo`, `in-progress`, `done`)
* Persistent storage using a JSON file

---

## Project Requirements (from roadmap.sh)

* Command-line interface application
* Uses command-line arguments for all actions
* Tasks are stored in a JSON file
* JSON file is created automatically if it does not exist
* Handles invalid input and edge cases
* No external libraries used (standard language features only)

---

## Installation & Running

### 1. Clone the repository

```
git clone https://github.com/gr1zzIy/TaskTracker-CLI.git
cd TaskTracker-CLI
```

### 2. Build the project

```
dotnet build
```

### 3. Run the application

You can run the application using `dotnet run` followed by command-line arguments.

```
dotnet run -- add "Task description"
dotnet run -- list
dotnet run -- mark-in-progress 1
dotnet run -- mark-done 1
```

Alternatively, after publishing or configuring a global tool, commands can be used directly:

```
task-cli add "Task description"
task-cli list
task-cli list done
```

---


## Usage

All commands must be executed using `dotnet run --`. The `task-cli` prefix is optional.

### Add a new task

```
dotnet run -- add "Task description"
```

### Update a task

```
dotnet run -- update <id> "Updated description"
```

### Delete a task

```
dotnet run -- delete <id>
```

### Mark task as in progress

```
dotnet run -- mark-in-progress <id>
```

### Mark task as done

```
dotnet run -- mark-done <id>
```

### List all tasks

```
dotnet run -- list
```

### List tasks by status

```
dotnet run -- list todo
dotnet run -- list in-progress
dotnet run -- list done
```

---

## Task Data Format

Each task is stored in JSON format with the following fields:

* `id` — unique task identifier
* `description` — task description
* `status` — task status (`todo`, `in-progress`, `done`)
* `createdAt` — task creation timestamp
* `updatedAt` — last update timestamp

---

## Technologies Used

* C#
* .NET
* JSON for data persistence

---

## Purpose

The goal of this project is to practice:

* Working with command-line arguments
* File I/O and JSON serialization
* Clean code structure
* Basic error handling
* CLI application design

---
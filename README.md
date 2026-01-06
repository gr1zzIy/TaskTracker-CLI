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

## Usage

### Add a new task

```
task-cli add "Task description"
```

### Update a task

```
task-cli update <id> "Updated description"
```

### Delete a task

```
task-cli delete <id>
```

### Mark task as in progress

```
task-cli mark-in-progress <id>
```

### Mark task as done

```
task-cli mark-done <id>
```

### List all tasks

```
task-cli list
```

### List tasks by status

```
task-cli list todo
task-cli list in-progress
task-cli list done
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


# Task Management Workflow

Tasks are tracked within the repository for convenient offline access, and so as to not tie us to a particular repository hosting provider.

## Creating
Tasks are created as YAML files in the `planning/tasks/` directory.  A task file's name should be of the form `T42.yml`.

When creating new tasks, consult and update `planning/tasks/_meta.yml` to use the correct next task number.

### Contents
Every task file should contain at least a `title` item (a short string).
A `milestone` identifier can be added in later planning sessions if not decided yet.  These are of the form `M2` and correspond to milestone planning items.

## Working
If you start working on a task, please first update the task file to say so and commit it to `unstable`.

If this is your first time helping, you will also need to update `planning/people.yml` with some sort of contact details and a unique ID for you.

If you run into problems and have to stop working on the task, please remove your ID from the task file so that others know it is not being worked on.

## Completion
When a task is completed, its file is deleted.  Ideally this should be in the same commit that completes the task, and should be done before the `task/*` branch is merged back into `unstable`.

All tests must pass before a task is considered complete.

## Reopening
Do not reuse task numbers, even if we need to "reopen" a task that was completed wrongly.  In this case we just create a new task with a new number, and reference the old task in its description.

## Example
    title: "do some stuff"
    milestone: M2
    workers:
    - dchampion
    description: >
        In this task we need to do some serious stuff.
        Don't forget about the thing and the whatchamacallit.

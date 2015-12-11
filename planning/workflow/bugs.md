# Bugfixing Workflow

Bugs are tracked in a very similar way to tasks (see [tasks.md](tasks.md)).

Bugs are tracked within the repository for convenient offline access, and so as to not tie us to a particular repository hosting provider.

## Reporting
You can report bugs through the github issue tracker or via any other contact method (see [README.md](../../README.md)).

The issue tracker is only really used as a reporting mechanism; the in-repository bug item is considered canonical.

## Creating
Bug planning items are created as YAML files in the `planning/bugs/` directory, with file names of the form `B999.yml`.

When creating new bugs, consult and update `planning/bugs/_meta.yml` to use the correct next bug number.

## Working
As with tasks, please update the bug file to say that you are working on a bugfix and commit the change to `unstable`.  If the bugfix is to be applied to a `milestone/*` branch, please commit the bug file change there too.

## Completion
As with tasks, the file should be deleted when the bug is fixed.

## Reopening
As with tasks, do not reuse bug numbers or "undelete" bugs which have been closed.

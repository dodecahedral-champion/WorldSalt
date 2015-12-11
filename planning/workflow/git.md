# Git workflow
Work should __NOT__ be committed directly to `master`.

Most work is instead done on side branches which are merged into the `unstable` branch.

Work from `unstable` is merged into `master` via a `milestone/*` branch each time a milestone is hit; `master` is tagged at this point with the milestone number such as e.g. `release/M2`.


## Branches
### Master
Do not commit directly to this branch.

Only `milestone/*` and `hotfix/*` branches should be merged into this branch.
Only `hotfix/*` branches should be forked from this branch.

This branch has tags of the form `release/M2` and `release/hotfix/B99` for released milestones and bug hotfixes, respectively.

### Unstable
Do not commit code directly to this branch.
Planning items (tasks, bugs) are created on this branch.

Most side branches are forked from and merged into this branch.

This branch has tags of the form `freeze/M2` to mark where testing for a milestone release began.

### Tasks
Work on regular functionality (e.g. task T197) should be made on branches with names of the form `task/T197`.
These branches should fork off from `unstable`.
Once work is complete and tested, the completed task planning item should be deleted.
Now the branch is merged back into `unstable`.

### Bugfixes
Work on bug fixing (e.g. bug B42) should be made on branches with names of the form `bugfix/B42`.
These branches should fork off from `unstable` or a `milestone/*` branch.

Once work is complete and tested, the completed bug planning item should be deleted.
Now the branch is merged back into the source branch (`unstable` or `milestone/*`).
Bugfixes merged into a `milestone/*` branch can also be merged into `unstable` at this point.

### Milestones
When all tasks on a milestone are completed, we can begin testing it.
First ensure that all milestone-related `task/*` branches are merged into `unstable`.
A new branch with name like `milestone/M2` is forked off from `unstable` (which is tagged with `freeze/M2` at that point).

Bugfixes (both `bugfix/*` and `hotfix/*`) can be merged into this milestone branch during testing.
Task branches should _not_ be merged into a milestone branch.

Once the milestone is happy, it is merged into `master` and back into `unstable`.
The `master` branch is now tagged with `release/M2`.

We don't need to delete milestone planning items, but of course the planning item should be up-to-date before release.

### Hotfixes
If a very urgent hotfix cannot wait for the next milestone, it must be addressed now.
Of course, we prefer to work slowly, so this should really be urgent.
Work on a hotfix bug should be done on a branch with name of the form `hotfix/B99`.
This branch should fork off from `master`.

When done, it is merged into both `master` and `unstable`.
If a `milestone/*` branch is currently active, the hotfix can also be merged into it.
The `master` branch is tagged with `release/hotfix/B99` at this point.

## Continuous Integration
Commits to `unstable` and `master` automatically trigger a test run in [travis-ci](https://travis-ci.org/dodecahedral-champion/WorldSalt).

Breakages are announced in [#worldsalt-dev](irc://irc.freenode.net/#worldsalt-dev) at irc.freenode.net as well as via email.

## Experiments
Experimental work should please be kept in personal repositories.
Naming such local branches as `experimental/*` will help them be spotted if accidentally pushed. :-)

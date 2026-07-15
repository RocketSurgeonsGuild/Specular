# Feature Specification: Secure Lint Workflow (drop `pull_request_target`, sign commits without a PAT)

**Feature Branch**: `005-secure-lint-workflow`

**Created**: 2026-06-29

**Status**: Draft

**Input**: User description: "I want to fix pull_request_target to use pull_request instead of pull_request_target. Can we find a way so that it can push signed commits back for the specific pull request, without needing secrets access?"

## Problem

`.github/workflows/lint.yml` runs on `pull_request_target`. That trigger executes in the **base
repository's trusted context**, exposing repository secrets (`RSG_BOT_TOKEN`, a personal access
token) to a workflow that then checks out and runs tooling (`hk fix`) against **untrusted PR head
code**. This is the canonical `pull_request_target` security anti-pattern: any malicious fork PR can
exfiltrate the PAT.

The PAT exists for one reason: to push the auto-formatted commit ("Automatically linting code") back
onto the PR branch. The maintainer wants to:

1. Switch the trigger to `pull_request` (untrusted code never sees secrets).
2. Still push **signed/verified** commits back to the originating PR branch.
3. Do so **without a stored secret / PAT** — using only the workflow's built-in `GITHUB_TOKEN`.

## User Scenarios

### US-1 — Same-repo PR gets auto-linted (primary)

A maintainer opens a PR from a branch in `RocketSurgeonsGuild/Specular`. CI runs the formatter, and if
files change, a **verified** commit authored by the GitHub Actions bot is pushed back to the PR
branch. No PAT is involved.

### US-2 — Fork PR is checked, never trusted (primary)

An external contributor opens a PR from their fork. CI runs the formatter as a **read-only check**.
If formatting is needed, the check fails with a clear message (and/or a diff comment) telling the
contributor to run `hk fix` locally. No secret is ever exposed to fork code, and nothing is pushed
to the fork branch.

### US-3 — Commits are verified (primary)

Any commit the workflow creates shows GitHub's **"Verified"** badge, satisfying signed-commit
branch-protection rules, without the repo storing or rotating any signing key.

## Requirements

- **FR-001**: `lint.yml` MUST trigger on `pull_request` (not `pull_request_target`); `merge_group`
  behavior is preserved.
- **FR-002**: The workflow MUST NOT reference `secrets.RSG_BOT_TOKEN` (or any PAT) for the lint flow.
- **FR-003**: For same-repo PRs the workflow MUST push the formatting fix as a commit that GitHub
  reports as **Verified**.
- **FR-004**: Verified status MUST be achieved via commits created through the GitHub API using the
  built-in `GITHUB_TOKEN` (GitHub signs these server-side) — no GPG key import, no stored secret.
- **FR-005**: For fork PRs the workflow MUST NOT attempt to push and MUST instead fail/report the
  formatting check, because `GITHUB_TOKEN` cannot (and must not) write to a fork branch.
- **FR-006**: The infinite-loop guard (skip when the head commit is already the bot lint commit) MUST
  be preserved or replaced with an equivalent.
- **FR-007**: `permissions:` MUST be least-privilege; `contents: write` is only meaningful for
  same-repo PRs and is automatically downgraded to read for forks.

## Success Criteria

- **SC-001**: No workflow that runs untrusted PR head code references a PAT or repository secret.
- **SC-002**: A same-repo PR with a formatting violation receives exactly one verified lint commit;
  a second run on the now-clean tree pushes nothing (no loop).
- **SC-003**: A fork PR with a formatting violation produces a failing/among-checks signal and pushes
  nothing; the job log contains no secret-bearing step.
- **SC-004**: `RSG_BOT_TOKEN` is no longer required by `lint.yml`.

## Out of Scope

- `dependabot-merge.yml` and `update-milestone.yml`: these also use `pull_request_target` but only
  comment / read; converting them is tracked separately. (Noted in research for follow-up.)
- Auto-pushing fixes onto fork branches (impossible without a PAT scoped to the fork; intentionally
  excluded by FR-005).

## Clarifications

### Session 2026-06-29

- Q: Forks can't receive pushed commits from `GITHUB_TOKEN` — how should fork PRs behave? →
  A: Check-only. Run `hk check` (read-only) for forks and fail with guidance; only same-repo PRs get
  an auto-pushed verified commit. This is the only secret-free option.

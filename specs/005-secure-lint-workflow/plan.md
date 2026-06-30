# Implementation Plan: Secure Lint Workflow (drop `pull_request_target`, sign commits without a PAT)

**Branch**: `005-secure-lint-workflow` | **Date**: 2026-06-29 | **Spec**: [spec.md](./spec.md)

**Input**: Feature specification from `/specs/005-secure-lint-workflow/spec.md`

## Summary

Replace the `pull_request_target` trigger in `.github/workflows/lint.yml` with `pull_request`, and
remove the `RSG_BOT_TOKEN` PAT. Same-repo PRs get auto-formatted by `hk fix`, and the resulting
change is committed back as a **GitHub-Verified** commit created through the GraphQL
`createCommitOnBranch` mutation using the built-in `GITHUB_TOKEN` (GitHub signs API commits
server-side — no key, no secret). Fork PRs run `hk check` read-only and fail with guidance, since
`GITHUB_TOKEN` is read-only for forks and must never write to a fork branch.

## Technical Context

**Language/Version**: GitHub Actions workflow YAML; `actions/github-script@v7` (Node 20 inline JS) for
the GraphQL mutation.

**Primary Dependencies**: `actions/checkout@v7`, `jdx/mise-action@v4`, `hk` (formatter, via mise),
`actions/github-script@v7`. No third-party commit action; no PAT.

**Storage**: N/A (CI configuration change only).

**Testing**: Manual/CI validation per [quickstart.md](./quickstart.md) — a same-repo PR with a
deliberate formatting violation, plus a fork PR scenario. No unit/snapshot tests apply (no library
code changes).

**Target Platform**: GitHub Actions `ubuntu-latest` runners.

**Project Type**: CI/CD workflow (infrastructure-as-config), not application code.

**Performance Goals**: N/A — must not measurably slow PR CI; single extra API call on same-repo PRs.

**Constraints**: No stored secrets/PAT in any job that executes untrusted PR head code; commits must
be GitHub-Verified; least-privilege `permissions:`.

**Scale/Scope**: One workflow file (`lint.yml`) rewritten; two related workflows flagged as
out-of-scope follow-ups.

## Constitution Check

_GATE: Must pass before Phase 0 research. Re-check after Phase 1 design._

This change touches CI tooling only — no generator output, no public API, no runtime code. Mapped
against the constitution:

- **I. AOT & Trim Safety** — N/A. No generator/runtime code changes; no reflection introduced.
- **II. Test-First w/ Snapshot Verification** — N/A for snapshots (no generated code). Validation is
  the `quickstart.md` scenarios; no `.verified.cs` is added or removed, so snapshot coverage is
  unchanged (no reduction).
- **III. Minimal & Stable Public API Surface** — Honored. No public API change; chose the simplest
  single-workflow design and rejected the heavier `workflow_run` split (YAGNI).
- **IV. Code Quality & Strict Analysis** — Applies as formatting only: the workflow YAML MUST pass
  `prettier` (YAML formatter) before commit, per the same `hk` gate the workflow itself runs.
- **V. Documentation as First-Class** — Honored. This feature **improves** the contributor-facing CI
  story; the change and its rationale are documented here and in `contracts/`. No `docs/` site page is
  required (internal CI), but the `quickstart.md` documents how to validate.

**Result**: PASS — no violations, no Complexity Tracking entries required.

## Project Structure

### Documentation (this feature)

```text
specs/005-secure-lint-workflow/
├── plan.md              # This file
├── research.md          # Phase 0 output
├── data-model.md        # Phase 1 output (workflow trigger/permission entities)
├── quickstart.md        # Phase 1 output (validation scenarios)
├── contracts/           # Phase 1 output
│   ├── lint-workflow.contract.md     # Trigger + permission + behavior contract
│   └── lint.yml.proposed             # Reference implementation of the new workflow
└── tasks.md             # Phase 2 output (/speckit-tasks — NOT created here)
```

### Source Code (repository root)

```text
.github/
└── workflows/
    └── lint.yml          # REWRITTEN: pull_request trigger, GITHUB_TOKEN-only, API-signed commit
```

Out-of-scope (flagged for separate review, not modified here):

```text
.github/workflows/
├── dependabot-merge.yml  # pull_request_target → comment-only (follow-up)
└── update-milestone.yml  # pull_request_target → read/milestone (follow-up)
```

**Structure Decision**: Single-file CI change. All behavior lives in `.github/workflows/lint.yml`;
the reference implementation is captured in `contracts/lint.yml.proposed` and copied/adapted during
implementation.

## Implementation Approach (high level)

1. Change `on:` from `pull_request_target` to `pull_request` (keep `merge_group`).
2. Drop `token: ${{ secrets.RSG_BOT_TOKEN }}` and the `repository:`/`ref:` cross-repo overrides;
   checkout the PR head branch with default `GITHUB_TOKEN`, `fetch-depth: 0`.
3. Add a `is_fork` computed condition.
4. Same-repo path: `hk fix` → if `git diff` non-empty, build `fileChanges` and call
   `createCommitOnBranch` via `actions/github-script` (Verified commit, no secret).
5. Fork path: `hk check` (read-only) → non-zero exit fails the job; no push.
6. Preserve the `Automatically linting code` message guard; rely on empty-diff fixpoint as the second
   loop stop.
7. Tighten `permissions:` to the minimum (`contents: write`, `pull-requests: write` for optional
   comment, `checks: write`/`statuses: write` as today; remove unused scopes).

See [contracts/lint-workflow.contract.md](./contracts/lint-workflow.contract.md) for the exact
trigger/permission/behavior contract and [contracts/lint.yml.proposed](./contracts/lint.yml.proposed)
for the full reference YAML.

## Complexity Tracking

> No Constitution violations — table intentionally empty.

| Violation | Why Needed | Simpler Alternative Rejected Because |
| --------- | ---------- | ------------------------------------ |
| (none)    | —          | —                                    |

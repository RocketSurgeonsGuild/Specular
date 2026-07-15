# Quickstart: Validating the Secure Lint Workflow

This validates that `lint.yml` (a) never exposes secrets to untrusted code, (b) pushes a **Verified**
commit on same-repo PRs, and (c) only checks (never pushes) on fork PRs — all without a PAT.

## Prerequisites

- The new `.github/workflows/lint.yml` is on `main`/`next` (the trigger reads the workflow from the
  base branch for `pull_request`).
- `RSG_BOT_TOKEN` is **not** referenced by `lint.yml` (it may still exist for other workflows).
- Branch protection (optional) requiring "Verified" commits — to prove FR-003 end-to-end.

## Scenario 1 — Same-repo PR gets a Verified auto-lint commit (SC-002, FR-003/004)

1. From a branch in `RocketSurgeonsGuild/Specular`, introduce a deliberate formatting violation
   (e.g. mis-indent a YAML/JSON file that `hk` formats).
2. Open a PR targeting `main`.
3. **Expect**: the Lint job runs `hk fix`, then the "Commit verified lint fix" step creates a commit
   `Automatically linting code`.
4. Open the new commit in the GitHub UI → it shows a green **Verified** badge.
5. The `synchronize` event re-runs Lint; `hk fix` finds no diff → **no second commit** (no loop).

**Pass**: exactly one verified lint commit; tree now clean; no `RSG_BOT_TOKEN` in any step log.

## Scenario 2 — Fork PR is checked, not pushed (SC-003, FR-005)

1. From a fork, open a PR with a formatting violation targeting `main`.
2. **Expect**: the "Lint (hk check) — fork PR" step runs and **fails** the job (read-only token).
3. No commit is pushed to the fork branch; the job log shows no secret-bearing steps.
4. Contributor runs `hk fix` locally, pushes; the check passes.

**Pass**: failing check with guidance, zero pushes, zero secrets exposed.

## Scenario 3 — `merge_group` unchanged

1. Queue a PR into the merge queue.
2. **Expect**: `hk check` runs against the merge-group SHAs; no push.

**Pass**: behavior identical to before this change.

## Negative check — secret leakage guard (SC-001)

- Inspect the rendered job for any fork PR: confirm the only `secrets.*` reference reachable is
  `GITHUB_TOKEN`. Grep the workflow: `grep -n "secrets\\." .github/workflows/lint.yml` must show only
  `secrets.GITHUB_TOKEN`.

## Formatting gate

Before committing the workflow change itself:

```bash
prettier --write .github/workflows/lint.yml
hk check --all   # or the repo's standard pre-commit gate
```

Contracts: [contracts/lint-workflow.contract.md](./contracts/lint-workflow.contract.md) ·
Reference YAML: [contracts/lint.yml.proposed](./contracts/lint.yml.proposed) ·
Data model: [data-model.md](./data-model.md)

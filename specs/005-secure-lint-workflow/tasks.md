---
description: Task list for Secure Lint Workflow
---

# Tasks: Secure Lint Workflow (drop `pull_request_target`, sign commits without a PAT)

**Input**: Design documents from `/specs/005-secure-lint-workflow/`

**Prerequisites**: [plan.md](./plan.md), [spec.md](./spec.md), [research.md](./research.md),
[data-model.md](./data-model.md), [contracts/](./contracts/)

**Tests**: No automated unit/snapshot tests apply (CI-config change, no library code). Validation is
manual via [quickstart.md](./quickstart.md) — those validation runs are captured as explicit tasks in
Phase 6.

**Organization**: Tasks are grouped by the user stories in spec.md. NOTE: all three stories are
facets of a single file, `.github/workflows/lint.yml`. They are **independently testable** (different
PR scenarios) but **not independently editable** (same file), so most tasks are sequential, not `[P]`.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: US1 (same-repo verified auto-lint), US2 (fork check-only), US3 (verified status)
- Exact file paths included.

## Path Conventions

Single CI file at repository root: `.github/workflows/lint.yml`. Reference implementation:
`specs/005-secure-lint-workflow/contracts/lint.yml.proposed`.

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Establish a safe edit baseline before touching the live workflow.

- [x] T001 Confirm current `RSG_BOT_TOKEN` consumers with `grep -rn "RSG_BOT_TOKEN" .github/` and record which workflows other than `lint.yml` still need it (so removal from `lint.yml` does not break `dependabot-merge.yml` / `draft-release.yml` / `close-milestone.yml`)
- [x] T002 [P] Verify the GraphQL approach is available on the runner: confirm `actions/github-script@v7`, `jq`, and `base64` are usable on `ubuntu-latest` (all are; document in a one-line note in the PR description)
- [ ] T003 [P] Open a scratch same-repo branch with a deliberate formatting violation to use as the Phase 6 validation fixture (do not push the workflow yet)

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Rewrite the workflow skeleton — trigger, permissions, checkout, fork detection — that
all three stories build on. **⚠️ No story behavior can be added until this is complete.**

- [x] T004 In `.github/workflows/lint.yml`, change `on:` from `pull_request_target` to `pull_request` (keep the same `branches: [main, next]`) and keep the `merge_group` trigger unchanged
- [x] T005 In `.github/workflows/lint.yml`, replace the `permissions:` block with the least-privilege set from [contracts/lint-workflow.contract.md](./contracts/lint-workflow.contract.md): `contents: write`, `pull-requests: write`, `checks: write`, `statuses: write`; remove all unused scopes (`issues`, `deployments`, `id-token`, `packages`, `pages`, `repository-projects`, `security-events`, `discussions`)
- [x] T006 In `.github/workflows/lint.yml` Checkout step, remove `token: ${{ secrets.RSG_BOT_TOKEN }}` and the cross-repo `repository:` override; set `ref` to `github.event.pull_request.head.ref` for `pull_request` (fall back to `github.sha`), keep `fetch-depth: '0'` and `clean: 'false'`
- [x] T007 In `.github/workflows/lint.yml`, add a `Compute context` step (id `ctx`) that sets `is_fork=true|false` from `github.event.pull_request.head.repo.full_name != github.repository`
- [x] T008 In `.github/workflows/lint.yml`, preserve the `Get Head Commit Message` step (id `commit-message`) and the `Setup mise` step (mise `github_token` may keep `secrets.GITHUB_TOKEN` — rate-limit only, not a write credential)

**Checkpoint**: Workflow parses, runs on `pull_request`, exposes no PAT, and knows fork vs. same-repo.

---

## Phase 3: User Story 1 - Same-repo PR gets a Verified auto-lint commit (Priority: P1) 🎯 MVP

**Goal**: A same-repo PR with a formatting violation receives exactly one verified `Automatically
linting code` commit, with no PAT.

**Independent Test**: Push the Phase 1 fixture branch as a same-repo PR → one Verified commit lands;
re-run finds clean tree and pushes nothing (quickstart Scenario 1).

- [x] T009 [US1] In `.github/workflows/lint.yml`, add the `hk fix` step gated on `github.event_name == 'pull_request' && steps.ctx.outputs.is_fork == 'false' && !contains(steps.commit-message.outputs.message, 'Automatically linting code')`, using `--from-ref ${{ github.event.pull_request.base.sha }} --to-ref ${{ github.event.pull_request.head.sha }}`
- [x] T010 [US1] In `.github/workflows/lint.yml`, add the `Collect changes` step (id `changes`) that sets `dirty=true|false` from `git status --porcelain` and, when dirty, emits `additions` (base64 of each non-deleted changed file) and `deletions` JSON via `git diff`/`jq` per [contracts/lint.yml.proposed](./contracts/lint.yml.proposed)
- [x] T011 [US1] In `.github/workflows/lint.yml`, add the loop-guard condition so neither `hk fix`, `Collect changes`, nor the commit step run when the head commit message already contains `Automatically linting code` (FR-006)

**Checkpoint**: Same-repo PR is auto-fixed and a commit is produced; verification (signing) is US3.

---

## Phase 4: User Story 2 - Fork PR is checked, never pushed (Priority: P1)

**Goal**: Fork PRs run a read-only `hk check` that fails on formatting issues and never pushes; no
secret is reachable by fork code.

**Independent Test**: Open a fork PR with a violation → job fails with guidance, nothing pushed
(quickstart Scenario 2).

- [x] T012 [US2] In `.github/workflows/lint.yml`, add the fork `hk check` step gated on `github.event_name == 'pull_request' && steps.ctx.outputs.is_fork == 'true'`, using base/head SHAs; a non-zero exit fails the job (FR-005)
- [x] T013 [US2] In `.github/workflows/lint.yml`, keep the existing `merge_group` `hk check` step (gated on `github.event_name == 'merge_group'`, using `merge_group.base_sha`/`head_sha`) so queue behavior is unchanged
- [x] T014 [US2] Confirm no push/commit step has a condition that can evaluate true when `is_fork == 'true'` (enforces invariant I2 — never write a fork branch)

**Checkpoint**: Forks are gated read-only; same-repo path from US1 is untouched.

---

## Phase 5: User Story 3 - Commits are Verified (Priority: P1)

**Goal**: The commit US1 produces is created via the GitHub API with `GITHUB_TOKEN`, so it shows
**Verified** — no GPG key, no PAT.

**Independent Test**: Inspect the lint commit from Scenario 1 in the GitHub UI → green **Verified**
badge (quickstart Scenario 1, step 4).

- [x] T015 [US3] In `.github/workflows/lint.yml`, add the `actions/github-script@v7` commit step gated on `is_fork == 'false' && steps.changes.outputs.dirty == 'true'`, calling the GraphQL `createCommitOnBranch` mutation with `branch.repositoryNameWithOwner`, `branch.branchName = head.ref`, `message.headline = 'Automatically linting code'`, `expectedHeadOid = head.sha`, and `fileChanges` from `ADDITIONS`/`DELETIONS` env (per [contracts/lint.yml.proposed](./contracts/lint.yml.proposed))
- [x] T016 [US3] Verify the old `stefanzweifel/git-auto-commit-action` step is fully removed from `.github/workflows/lint.yml` (it pushes via git and would be unsigned — FR-004)

**Checkpoint**: Same-repo lint commits are Verified, produced with only `GITHUB_TOKEN`.

---

## Phase 6: Polish & Validation (Cross-Cutting)

**Purpose**: Format, prove the contracts, and confirm no secret regressions.

- [x] T017 [P] Format the workflow: `prettier --write .github/workflows/lint.yml` and run the repo's `hk` pre-commit gate so the new file passes its own linter (Constitution IV)
- [x] T018 Secret-leak guard: `grep -n "secrets\." .github/workflows/lint.yml` must return only `secrets.GITHUB_TOKEN` (SC-001)
- [ ] T019 Execute [quickstart.md](./quickstart.md) Scenario 1 (same-repo verified commit + no-loop fixpoint) using the Phase 1 fixture; capture the Verified badge as evidence (SC-002/SC-003-verified)
- [ ] T020 Execute [quickstart.md](./quickstart.md) Scenario 2 (fork PR check-only, no push, no secret) — use a throwaway fork (SC-003)
- [ ] T021 Execute [quickstart.md](./quickstart.md) Scenario 3 (`merge_group` `hk check` unchanged)
- [x] T022 Confirm `RSG_BOT_TOKEN` is no longer referenced by `lint.yml` and is still intact for the other workflows recorded in T001 (SC-004)
- [ ] T023 [P] Add a short note to the PR description flagging `dependabot-merge.yml` and `update-milestone.yml` as out-of-scope `pull_request_target` follow-ups (per research.md Open Risks)

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies — start immediately.
- **Foundational (Phase 2)**: Depends on Setup — **BLOCKS all stories** (creates the file skeleton all stories edit).
- **User Stories (Phase 3–5)**: All depend on Phase 2. They edit the **same file** in related regions, so run **sequentially** US1 → US2 → US3 (US3 depends on US1's `changes` step output).
- **Polish (Phase 6)**: Depends on Phases 3–5.

### Story Dependencies

- **US1 (P1)**: After Foundational. Produces the `changes` output US3 consumes.
- **US2 (P1)**: After Foundational. Independent of US1 (separate `if` branch), but same file → land after US1 to avoid merge churn.
- **US3 (P1)**: After **US1** (needs `steps.changes.outputs.dirty`/`additions`/`deletions`).

### Parallel Opportunities

- Phase 1: T002 and T003 are `[P]` (independent of T001 and each other).
- Phase 6: T017 and T023 are `[P]`.
- Story phases: **not parallel** — single shared file, and US3 depends on US1.

---

## Implementation Strategy

### MVP (Phases 1–2 + US1 + US3)

The minimum that satisfies the user's ask is: secure trigger (Phase 2) + same-repo auto-lint (US1) +
Verified commit (US3). US2 (fork gating) is required for correctness/safety and should ship in the
same PR, but US1+US3 is the demonstrable core.

1. Phase 1 Setup → Phase 2 Foundational.
2. US1 (same-repo fix) → US3 (sign it) → validate quickstart Scenario 1. **This is the headline result: signed commit, no PAT.**
3. Add US2 (fork gating) → validate Scenario 2.
4. Phase 6 polish + Scenario 3 + secret-leak guard.

### Notes

- All implementation edits target one file: `.github/workflows/lint.yml`.
- Commit after each phase; keep the reference YAML in `contracts/lint.yml.proposed` as the source of truth.
- Avoid reintroducing any `secrets.*` other than `GITHUB_TOKEN` into steps reachable by fork PRs.

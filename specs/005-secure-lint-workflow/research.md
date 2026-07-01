# Phase 0 Research: Secure Lint Workflow

## Decision 1 — Trigger: `pull_request` (single workflow), not the split `pull_request` + `workflow_run` pattern

**Decision**: Convert `lint.yml` to a single `pull_request`-triggered workflow that branches its
behavior on whether the PR originates from the same repository or a fork.

**Rationale**:

- `pull_request` runs the head SHA's workflow **without secrets** and with a **read-only**
  `GITHUB_TOKEN` for fork PRs — this is exactly the isolation we want for running a formatter over
  untrusted code.
- For **same-repo** PRs, `pull_request` still honors the `permissions:` block, so `contents: write`
  is granted and the job can push back.
- The popular "split" mitigation (untrusted `pull_request` job uploads a patch artifact; a trusted
  `workflow_run` job applies it) exists to give the apply-step write access **and** secrets. We
  explicitly do **not** want secrets, and `workflow_run`'s `GITHUB_TOKEN` **still cannot push to a
  fork branch**. So the split adds real complexity (artifact passing, run correlation, a second YAML
  file) while buying nothing for the no-secret/fork case. Rejected as over-engineering per
  Constitution Principle III (YAGNI) — revisit only if fork auto-fix becomes a hard requirement
  (which then _requires_ a PAT and reintroduces the very risk we're removing).

**Alternatives considered**:

- _Keep `pull_request_target` + PAT_: rejected — this is the vulnerability being fixed.
- _`pull_request` + `workflow_run` split_: rejected — see above; complexity without payoff under the
  no-secret constraint.

## Decision 2 — Signed commits via the GitHub GraphQL `createCommitOnBranch` mutation using `GITHUB_TOKEN`

**Decision**: Create the lint commit through the GitHub API (GraphQL
`createCommitOnBranch`, or an equivalent REST Git Data flow) authenticated with the built-in
`GITHUB_TOKEN`. Commits created server-side via the API are **automatically signed by GitHub** and
display the **"Verified"** badge.

**Rationale**:

- `git commit && git push` produces an **unsigned** commit; making it verified would require
  importing a GPG/SSH signing key — i.e., a stored secret. That violates FR-004.
- Commits authored through the GitHub API are signed with GitHub's internal (web-flow) key, so the
  resulting commit is Verified with **no key material in the repo**. This is the only way to satisfy
  "signed commits without secrets."
- `GITHUB_TOKEN` has `contents: write` for same-repo PRs, which is sufficient for
  `createCommitOnBranch` against the PR head branch (a ref in the same repo).

**Implementation options (all equivalent on the signing guarantee)**:

| Option                                                          | Notes                                                                     |
| --------------------------------------------------------------- | ------------------------------------------------------------------------- |
| `actions/github-script` running the GraphQL mutation            | Zero extra deps; full control; build the `fileChanges` from `git diff`.   |
| `qoomon/actions--create-commit` / `planetscale/ghcommit-action` | Thin wrappers over the same mutation; less YAML, adds a 3rd-party action. |

**Recommended (updated)**: `planetscale/ghcommit-action` — a thin Docker action that runs
`git add <file_pattern>` and commits the working-tree diff (adds **and** deletions) via
`createCommitOnBranch` using `GITHUB_TOKEN`. It collapses the manual "collect changes (jq/base64) +
github-script GraphQL" into one step (~40 fewer lines), and with `empty: false` (default) it no-ops
on a clean tree, giving the empty-diff fixpoint for free. The Indago workflows already depend on
several third-party actions (`stefanzweifel`, `WyriHaximus`, `jdx`), so this is consistent with
existing practice; pin to a specific tag (`@v0.2.22`). The `actions/github-script` inline-mutation
approach remains the dependency-free fallback if pinning a Docker action is undesirable.

**Alternatives considered**:

- _`stefanzweifel/git-auto-commit-action`_ (current): pushes via git → unsigned unless a GPG key
  secret is imported. Rejected — cannot meet FR-003/FR-004 without a secret.
- _REST `PUT /repos/{o}/{r}/contents/{path}`_: signs commits but is one-file-per-call and clumsy for
  multi-file formatter output. Rejected in favor of the single-call GraphQL mutation.

## Decision 3 — Fork detection & gating

**Decision**: Compute `is_fork = github.event.pull_request.head.repo.full_name != github.repository`.

- `is_fork == false` → run `hk fix`, and if `git status --porcelain` shows changes, push a verified
  commit via the API to `github.event.pull_request.head.ref`.
- `is_fork == true` → run `hk check` (read-only) and let a non-zero exit fail the job; optionally
  post a PR comment with the diff. Never push.

**Rationale**: `GITHUB_TOKEN` is read-only for fork PRs and cannot write to the fork's branch
anyway; attempting a push would just fail. Gating up front gives a clean, intentional UX (FR-005).

## Decision 4 — Loop prevention

**Decision**: Preserve the existing guard — skip the commit step when the head commit message is the
bot's `Automatically linting code`. With the API-commit approach, also rely on the natural fixpoint:
after a verified fix lands, the re-triggered `synchronize` run finds a clean tree (`hk fix` produces
no diff) and pushes nothing.

**Rationale**: Two independent stops (message guard + empty-diff guard) satisfy SC-002 without a PAT
or special bot-author filtering.

## Decision 5 — Checkout & ref handling

**Decision**: With `pull_request`, `actions/checkout` defaults to the PR **merge ref**; pin
`ref: ${{ github.event.pull_request.head.ref }}` and `fetch-depth: 0` so the working tree matches the
branch we will commit to, and `hk fix --from-ref <base.sha> --to-ref <head.sha>` scopes to changed
files. No `repository:`/`token:` override is needed (default `GITHUB_TOKEN`, default repo).

**Rationale**: Committing via API targets the head **branch ref**; the checked-out tree must
correspond to that branch tip so the generated `fileChanges` are correct.

## Open Risks / Notes

- **Running a formatter over untrusted code** is still code execution, but under `pull_request` it
  has no secrets and a read-only token — the blast radius is a throwaway runner. Acceptable and the
  standard posture.
- **`merge_group`** path keeps `hk check` only (no push), unchanged.
- **Follow-up (out of scope)**: `dependabot-merge.yml` and `update-milestone.yml` use
  `pull_request_target` but only comment/read with `GITHUB_TOKEN`/secrets in a reusable workflow.
  They don't run untrusted code in a secret context the same way, but should be reviewed separately.

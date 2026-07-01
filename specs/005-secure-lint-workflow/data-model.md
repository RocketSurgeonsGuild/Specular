# Phase 1 Data Model: Secure Lint Workflow

No application/runtime entities. The "data model" here is the set of CI configuration entities and the
state transitions of a lint run.

## Entities

### Workflow Trigger

| Field          | Value (new)                    | Notes                                             |
| -------------- | ------------------------------ | ------------------------------------------------- |
| `event`        | `pull_request`, `merge_group`  | was `pull_request_target` — the change under test |
| `branches`     | `main`, `next`                 | unchanged                                         |
| `pr.head.ref`  | branch the commit is pushed to | only writable when same-repo                      |
| `pr.head.repo` | used to derive `is_fork`       | `full_name != github.repository` ⇒ fork           |

### Token / Permissions

| Field               | Value (new)             | Notes                                                      |
| ------------------- | ----------------------- | ---------------------------------------------------------- |
| auth                | built-in `GITHUB_TOKEN` | **no** `RSG_BOT_TOKEN`                                     |
| `contents`          | `write`                 | effective only for same-repo PRs; auto-read for forks      |
| `pull-requests`     | `write`                 | optional diff comment on fork PRs                          |
| `checks`/`statuses` | `write`                 | preserved                                                  |
| everything else     | `none`/`read`           | least-privilege; drop unused `issues`, `deployments`, etc. |

### Commit (the artifact produced)

| Field      | Value                          | Notes                                   |
| ---------- | ------------------------------ | --------------------------------------- |
| created by | GraphQL `createCommitOnBranch` | server-side ⇒ **Verified**              |
| author     | `github-actions[bot]`          | implicit when using `GITHUB_TOKEN`      |
| message    | `Automatically linting code`   | doubles as the loop-prevention sentinel |
| target     | `pr.head.ref` (same-repo only) | never a fork branch                     |
| signature  | GitHub web-flow key            | no GPG/SSH key stored in the repo       |

## Run State Machine

```text
PR event (pull_request)
   │
   ├─ is_fork == true ───► hk check (read-only)
   │                          ├─ clean  ► job success, no push
   │                          └─ dirty  ► job FAIL + (optional) diff comment, no push
   │
   └─ is_fork == false ──► hk fix
                              ├─ no diff ► job success, no push        (fixpoint / SC-002)
                              └─ diff    ► head message == sentinel?
                                              ├─ yes ► skip push       (loop guard)
                                              └─ no  ► createCommitOnBranch (Verified) ► push
```

## Validation Rules

- **R1**: No step that runs on fork PRs may reference any `secrets.*` other than the auto-provided
  `GITHUB_TOKEN`.
- **R2**: A push step may run **only** when `is_fork == false`.
- **R3**: The commit MUST be created via the GitHub API (not `git push`) to guarantee Verified status.
- **R4**: `permissions:` must not grant scopes unused by the workflow.

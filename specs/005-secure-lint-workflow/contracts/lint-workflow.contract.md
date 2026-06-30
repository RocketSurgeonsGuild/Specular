# Contract: `lint.yml` workflow

## Trigger contract

- MUST fire on `pull_request` for base branches `main`, `next`.
- MUST fire on `merge_group` for `main`, `next` (unchanged `hk check` behavior).
- MUST NOT use `pull_request_target`.

## Secret contract

- MUST NOT reference `secrets.RSG_BOT_TOKEN` or any PAT.
- The ONLY credential is the auto-provided `secrets.GITHUB_TOKEN`.
- `mise-action`'s `github_token` MAY use `GITHUB_TOKEN` (rate-limit only; not a write credential).

## Permission contract (least privilege)

| scope           | value   |
| --------------- | ------- |
| `contents`      | `write` |
| `pull-requests` | `write` |
| `checks`        | `write` |
| `statuses`      | `write` |
| all others      | `none`  |

## Behavior contract

| Condition                                   | Action                                             | Push? |
| ------------------------------------------- | -------------------------------------------------- | ----- |
| `merge_group`                               | `hk check` (from/to merge_group SHAs)              | no    |
| `pull_request`, fork, formatting clean      | `hk check` passes                                  | no    |
| `pull_request`, fork, formatting dirty      | `hk check` fails job (+ optional diff comment)     | no    |
| `pull_request`, same-repo, no diff          | `hk fix` yields nothing; success                   | no    |
| `pull_request`, same-repo, head == sentinel | skip (loop guard)                                  | no    |
| `pull_request`, same-repo, diff present     | `createCommitOnBranch` (Verified) to `pr.head.ref` | yes   |

`is_fork := github.event.pull_request.head.repo.full_name != github.repository`.

## Commit contract

- Created via GitHub GraphQL `createCommitOnBranch` (or REST Git Data equivalent) using
  `GITHUB_TOKEN`.
- MUST appear as **Verified** in the GitHub UI.
- Message: `Automatically linting code` (also the loop sentinel).
- `expectedHeadOid` MUST be the current branch tip to avoid clobbering concurrent pushes.

## Invariants

- I1: No job executing untrusted PR head code has access to any non-`GITHUB_TOKEN` secret.
- I2: No push targets a fork-owned branch.
- I3: A clean tree never produces a commit (idempotent / no loop).

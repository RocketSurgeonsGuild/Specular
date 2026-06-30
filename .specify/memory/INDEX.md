# Memory Index

Compact routing for durable project memory. Each row points to a source entry; it does not duplicate
the lesson.

## Bugs

| ID  | Title                                                    | Source  | Tags                            |
| --- | -------------------------------------------------------- | ------- | ------------------------------- |
| B1  | IndagoProviderAttribute emitted IL2077 under Native AOT  | BUGS.md | aot, trim, il2077, principle-i  |
| B2  | Pipeline DocsModule recursion via a delegating mise task | BUGS.md | pipeline, mise, docs, recursion |

## Decisions

| ID  | Title                                                                          | Source       | Tags                    |
| --- | ------------------------------------------------------------------------------ | ------------ | ----------------------- |
| D1  | Native AOT zero-warning enforcement is csproj-scoped; modules skip-with-reason | DECISIONS.md | aot, ci, xmldocmd, docs |

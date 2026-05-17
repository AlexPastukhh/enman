from __future__ import annotations

import sys
from pathlib import Path

repo = Path(sys.argv[1])

def read(path: str) -> str:
    p = repo / path
    return p.read_text(encoding="utf-8") if p.exists() else ""

def write(path: str, text: str) -> None:
    p = repo / path
    p.parent.mkdir(parents=True, exist_ok=True)
    p.write_text(text, encoding="utf-8", newline="\n")

def ensure_section(path: str, marker: str, section: str) -> None:
    text = read(path)
    if not text:
        text = f"# {Path(path).stem}\n\n"
    if marker in text:
        return
    if not text.endswith("\n"):
        text += "\n"
    text += "\n" + section.strip() + "\n"
    write(path, text)

def replace_or_append_register_row(path: str, old_prefix: str, new_rows: str, marker: str, fallback_section: str) -> None:
    text = read(path)
    if marker in text:
        return
    if old_prefix in text:
        lines = text.splitlines()
        out = []
        replaced = False
        for line in lines:
            if line.strip().startswith(old_prefix):
                if not replaced:
                    out.extend(new_rows.strip().splitlines())
                    replaced = True
                continue
            out.append(line)
        text = "\n".join(out) + "\n"
        write(path, text)
    else:
        ensure_section(path, marker, fallback_section)

# planning/README.md
ensure_section(
    "planning/README.md",
    "planning/slices/drafting-current-state-source-rule.md",
    """
## Current State Source Rule

When answering questions about current implementation status, existing code, routes, generated contracts or tests, use the current GitHub repository state as source of truth.

```text
planning/slices/drafting-current-state-source-rule.md
```

Handoff archives and uploaded zip packages are inputs for proposed changes, not proof of current repo state.
"""
)

# planning/slices/README.md
ensure_section(
    "planning/slices/README.md",
    "planning/slices/slice-draft-file-naming-and-placement.md",
    """
## Slice Draft Naming / Placement

Use the canonical naming and storage rules:

```text
planning/slices/slice-draft-file-naming-and-placement.md
planning/slices/drafting-current-state-source-rule.md
planning/slices/drafting-implementation-checklist-rule.md
```

Current project-state questions must be answered from the GitHub/current branch state, not from handoff archives.
"""
)

ensure_section(
    "planning/slices/README.md",
    "SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md",
    """
## Current Agreement Exchange Drafts

```text
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
SL-AGR-EXCH-003 — Read Agreement Exchange
SL-AGR-EXCH-004 — Accept Active Agreement Proposal
SL-AGR-EXCH-005 — Final Refuse Agreement Exchange
```

Canonical server draft files live directly under `planning/slices/`.
"""
)

# L1 drafting guide
ensure_section(
    "planning/slices/l1-slice-drafting-guide.md",
    "Implementation Checklist Section",
    """
## Implementation Checklist Section

Full server and full client drafts must include an explicit `Implementation Checklist` near the end.

Use:

```text
planning/slices/drafting-implementation-checklist-rule.md
```

For command slices that use the project Result/Error model, do not add per-command status enums by default. Prefer `UnitResult<IReadOnlyList<Error>>` or the current project result type and map failures through existing Error/ProblemDetails mapping.
"""
)

# l2 README
ensure_section(
    "planning/slices/l2/README.md",
    "Agreement Exchange Slice Chain",
    """
## Agreement Exchange Slice Chain

Canonical server draft files:

```text
planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md
planning/slices/SL-AGR-EXCH-002-send-agreement-counter-proposal-version.md
planning/slices/SL-AGR-EXCH-003-read-agreement-exchange.md
planning/slices/SL-AGR-EXCH-004-accept-active-agreement-proposal.md
planning/slices/SL-AGR-EXCH-005-final-refuse-agreement-exchange.md
```

Split decision:

```text
SL-AGR-EXCH-001:
  creates exchange + first Employee proposal version.

SL-AGR-EXCH-002:
  one counter-proposal slice with two actor branches:
  Client sends own version and Employee sends new version.

SL-AGR-EXCH-003:
  read exchange state/details.

SL-AGR-EXCH-004:
  accept active proposal.

SL-AGR-EXCH-005:
  final refusal.
```
"""
)

# Scenario flow behavior register: replace SL-AGR-* row if present.
new_rows = """
| `SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-13A..E`, agreement behavior files, `CC-CSRF-001` | Start AgreementProposalExchange with initial Employee proposal version | drafted |
| `SL-AGR-EXCH-002-send-agreement-counter-proposal-version.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-13A..E`, agreement behavior files, `CC-CSRF-001` | Client and Employee counter-proposal version send branches inside existing exchange | drafted |
| `SL-AGR-EXCH-003-read-agreement-exchange.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-13A..E`, agreement DATA/behavior files | Read AgreementProposalExchange state/details | drafted |
| `SL-AGR-EXCH-004-accept-active-agreement-proposal.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-13A..E`, agreement behavior files, `CC-CSRF-001` | Accept active agreement proposal | drafted |
| `SL-AGR-EXCH-005-final-refuse-agreement-exchange.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-13E`, agreement behavior files, `CC-CSRF-001` | Final refusal of AgreementProposalExchange by Employee | drafted |
"""
replace_or_append_register_row(
    "planning/slices/slice-scenario-flow-behavior-register.md",
    "| `SL-AGR-*`",
    new_rows,
    "SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md",
    f"""
## Agreement Exchange Source Map

{new_rows}
"""
)

# Questions register
ensure_section(
    "planning/slices/slice-questions-register.md",
    "SL-AGR-EXCH-001-Q001",
    """
## Agreement Exchange Questions / Decisions

| ID | Local file(s) | Area | Status | Question | Decision / current direction | Impact |
|---|---|---|---|---|---|---|
| `SL-AGR-EXCH-001-Q001` | `SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md` | exchange lifecycle | accepted | Does approve automatically start exchange? | No. Exchange starts by explicit employee command. | Keeps review and agreement lifecycle separated. |
| `SL-AGR-EXCH-001-Q002` | same | exchange lifecycle | accepted | Is this empty start or start with first proposal? | Start with initial Employee proposal. | Body requires document ref. |
| `SL-AGR-EXCH-001-Q012` | same | application result | accepted | Should command have per-command status enum? | No. Use `UnitResult<IReadOnlyList<Error>>` / current Result+Error model. | Avoids enum noise and centralizes ProblemDetails mapping. |
| `SL-AGR-EXCH-001-Q013` | same | API placement | accepted | Controller placement? | Separate `EmployeeAgreementExchangeController`. | Prevents request/review controller bloat. |
| `SL-AGR-EXCH-001-Q015` | same | proposal comment | accepted | Comment behavior? | Optional; null/blank means no comment. | Create `ProposalComment` only for meaningful text. |
| `SL-AGR-EXCH-001-Q017` | same | persistence | accepted | Repository shape? | Agreement exchange repository with `GetByRequestIdAsync` and `Add`. | Supports duplicate guard and aggregate persistence. |
"""
)

# Extension points
ensure_section(
    "planning/slices/slice-extension-points-register.md",
    "CP-AGR-EXCH-001",
    """
## Agreement Exchange Extension Points

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-AGR-EXCH-001` | start exchange | Start exchange only with initial Employee proposal; no empty exchange start. | accepted |
| `CP-AGR-EXCH-002` | counter-proposal | Client and Employee counter-proposal branches stay in one slice unless UI/permissions/document handling diverge. | accepted |
| `CP-AGR-EXCH-003` | duplicate handling | Prefer unique exchange per request; exact 409 vs 422 mapping can be cleanup if mapper lacks conflict support. | accepted/future cleanup |
| `CP-AGR-EXCH-004` | documents | Store `AgreementDocumentRef` only; binary upload/storage/generation is future. | future |
| `CP-AGR-EXCH-005` | controller growth | Agreement exchange uses dedicated EmployeeAgreementExchangeController from first slice. | accepted |
"""
)

# Implementation notes
ensure_section(
    "planning/slices/slice-implementation-notes-register.md",
    "IMPL-AGR-EXCH-001",
    """
## Agreement Exchange Implementation Notes

| ID | Applies to | Note | Status |
|---|---|---|---|
| `IMPL-AGR-EXCH-001` | `SL-AGR-EXCH-001` | Command returns `UnitResult<IReadOnlyList<Error>>` or current project result type; do not add per-command status enum. | accepted |
| `IMPL-AGR-EXCH-002` | `SL-AGR-EXCH-001` | Use separate `EmployeeAgreementExchangeController` for agreement exchange endpoints. | accepted |
| `IMPL-AGR-EXCH-003` | `SL-AGR-EXCH-001` | Add agreement exchange repository abstraction and EF repository if missing. | accepted |
| `IMPL-AGR-EXCH-004` | `SL-AGR-EXCH-001` | Enforce unique exchange per request in application handler; add DB uniqueness if practical. | accepted |
| `IMPL-AGR-EXCH-005` | all API-changing slices | Regenerate OpenAPI and TS types through tools only. | accepted |
| `IMPL-DRAFTING-STATE-001` | status/inventory answers | Current project state must be checked in GitHub/current branch, not inferred from archives. | accepted |
"""
)

# Optional project planning agent protocol if exists
ensure_section(
    "planning/planning-agent-protocol.md",
    "Current Project State Source",
    """
## Current Project State Source

When the user asks about current project state, implementation status, existing code, routes, generated artifacts or tests, inspect the current GitHub repository state.

```text
Do not treat uploaded archives or old handoff zips as proof of what is currently implemented.
```

Archives are proposed changes unless the current repository state confirms them.
"""
)

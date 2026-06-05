# SC-19 — Document Template Management Behavior Items

Status: future behavior items draft
Doc version: v0.1.0
Source scenario: `SC-19`

## 1. Purpose

This file contains behavior items owned by the future document template management scenario.

Behavior items describe required/observable behavior, not implementation details.

## 2. Source Set

```text
planning/diagrams/scenario-text-specs/SC-19-document-template-management.md
planning/diagrams/scenario-data/DOCGEN-document-template-data.md
```

## 3. Coverage Items Registry

| ID | Category | Short name | Source | Migration note |
|---|---|---|---|---|
| DOCGEN-CMD-TPL-CREATE-001 | CMD | Employee creates template in MVP | SC-19 | Future candidate. |
| DOCGEN-CMD-TPL-ACTIVATE-001 | CMD | Template becomes active | SC-19 | Future candidate. |
| DOCGEN-READ-TPL-LIST-001 | READ | Actor sees templates | SC-19 | Future candidate. |
| DOCGEN-VI-TPL-001 | VI | Template has required name/content/kind | SC-19 | Future candidate. |
| DOCGEN-IBS-TPL-001 | IBS | Client cannot create templates | SC-19 / SC-21 | Accepted direction. |
| DOCGEN-UCQ-TPL-001 | UCQ | Template creation can be opened from proposal-send flow | SC-19 / SC-20 | Future UX coordination. |

## 4. Grouped Behavior Items

### 4.CMD — Command Behavior Cards

#### DOCGEN-CMD-TPL-CREATE-001 — Employee creates template in MVP

Source: `SC-19`

Required behavior / guarantee:

```text
Employee with template-management permission can create a document template with name, document kind and template content/source.
```

Failure / no-write / preservation guarantee:

```text
Invalid template input creates no active template.
```

#### DOCGEN-CMD-TPL-ACTIVATE-001 — Template becomes active

Source: `SC-19`

Required behavior / guarantee:

```text
Created template can become active so it is available for proposal document generation.
```

Failure / no-write / preservation guarantee:

```text
Template with missing required data cannot become active.
```

### 4.READ — Read / Listing Behavior

#### DOCGEN-READ-TPL-LIST-001 — Actor sees templates

Source: `SC-19`

Required behavior / guarantee:

```text
Employee/Admin can see relevant templates and their status in template management.
```

Failure / no-write / preservation guarantee:

```text
Unauthorized actor does not receive template management access.
```

### 4.VI — Value Integrity Items

#### DOCGEN-VI-TPL-001 — Template has required name/content/kind

Source: `SC-19`

Required behavior / guarantee:

```text
A template must have a name, document kind and template content/source before it can be used for generation.
```

Failure / no-write / preservation guarantee:

```text
Missing required template data blocks creation/activation.
```

### 4.IBS — Impossible Business State Candidates

#### DOCGEN-IBS-TPL-001 — Client cannot create templates

Source: `SC-19 / SC-21`

Required behavior / guarantee:

```text
Client must not create or edit document templates.
```

Failure / no-write / preservation guarantee:

```text
Client template-management attempt changes no templates.
```

### 4.UCQ — Use-Case Coordination

#### DOCGEN-UCQ-TPL-001 — Template creation can be opened from proposal-send flow

Source: `SC-19 / SC-20`

Required behavior / guarantee:

```text
When employee does not see a suitable template in proposal-send form, employee can open template creation in a separate tab/window and then refresh/refetch the template list.
```

Failure / no-write / preservation guarantee:

```text
If template is not created or not active, proposal-send template list remains without that template.
```

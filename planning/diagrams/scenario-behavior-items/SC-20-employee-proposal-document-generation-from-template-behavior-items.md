# SC-20 — Employee Proposal Document Generation From Template Behavior Items

Status: future behavior items draft
Doc version: v0.1.0
Source scenario: `SC-20`

## 1. Purpose

This file contains behavior items for employee generation of proposal documents from active templates.

## 2. Source Set

```text
planning/diagrams/scenario-text-specs/SC-20-employee-proposal-document-generation-from-template.md
planning/diagrams/scenario-data/DOCGEN-document-template-data.md
planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md
```

## 3. Coverage Items Registry

| ID | Category | Short name | Source | Migration note |
|---|---|---|---|---|
| DOCGEN-CMD-PROP-GEN-001 | CMD | Employee generates proposal document from template | SC-20 | Future candidate. |
| DOCGEN-CMD-PROP-SEND-001 | CMD | Proposal version uses generated document | SC-20 / SC-13D | Future extension of proposal-send. |
| DOCGEN-READ-TPL-ACTIVE-001 | READ | Active templates appear in proposal-send form | SC-20 | Future candidate. |
| DOCGEN-VI-GEN-001 | VI | Required placeholder data is available | SC-20 | Future candidate. |
| DOCGEN-LC-GEN-001 | LC | Generation follows proposal-send lifecycle | SC-20 / SC-13D | SC-13D lifecycle remains authoritative. |
| DOCGEN-NW-GEN-001 | NW | Failed generation creates no proposal version | SC-20 | Future candidate. |
| DOCGEN-UCQ-GEN-001 | UCQ | Upload flow remains available | SC-20 / SC-13D | Future extension must not remove upload MVP unless explicitly decided. |

## 4. Grouped Behavior Items

### 4.CMD — Command Behavior Cards

#### DOCGEN-CMD-PROP-GEN-001 — Employee generates proposal document from template

Source: `SC-20`

Required behavior / guarantee:

```text
Employee can select an active template and generate a proposal document from current request/applicant/exchange data.
```

Failure / no-write / preservation guarantee:

```text
If selected template is inactive or required data is missing, no generated document is stored.
```

#### DOCGEN-CMD-PROP-SEND-001 — Proposal version uses generated document

Source: `SC-20 / SC-13D`

Required behavior / guarantee:

```text
Generated document can be attached as the document of a proposal version sent by employee.
```

Failure / no-write / preservation guarantee:

```text
If proposal-send lifecycle does not allow employee version, generated document does not create a proposal version.
```

### 4.READ — Read / Listing Behavior

#### DOCGEN-READ-TPL-ACTIVE-001 — Active templates appear in proposal-send form

Source: `SC-20`

Required behavior / guarantee:

```text
Proposal-send form shows active templates relevant to the document kind when employee selects generate-from-template mode.
```

Failure / no-write / preservation guarantee:

```text
Inactive/unavailable templates are not offered for new generation.
```

### 4.VI — Value Integrity Items

#### DOCGEN-VI-GEN-001 — Required placeholder data is available

Source: `SC-20`

Required behavior / guarantee:

```text
System validates that template-required data can be resolved from request/applicant/exchange context before generation.
```

Failure / no-write / preservation guarantee:

```text
Missing required data blocks generation and reports missing-data feedback.
```

### 4.LC — Scenario State / Condition Matrices

#### DOCGEN-LC-GEN-001 — Generation follows proposal-send lifecycle

Source: `SC-20 / SC-13D`

Required behavior / guarantee:

```text
Template generation is allowed only when employee can send a proposal version according to agreement exchange lifecycle rules.
```

Failure / no-write / preservation guarantee:

```text
Wrong request/exchange state blocks proposal generation/send and leaves exchange unchanged.
```

### 4.NW — No-Write / Failure Preservation

#### DOCGEN-NW-GEN-001 — Failed generation creates no proposal version

Source: `SC-20`

Required behavior / guarantee:

```text
Generation failure does not create partial proposal version.
```

Failure / no-write / preservation guarantee:

```text
Existing proposal versions and exchange status remain unchanged after blocked generation.
```

### 4.UCQ — Use-Case Coordination

#### DOCGEN-UCQ-GEN-001 — Upload flow remains available

Source: `SC-20 / SC-13D`

Required behavior / guarantee:

```text
Employee can still choose upload-ready-file mode when template generation is unavailable or unsuitable.
```

Failure / no-write / preservation guarantee:

```text
Adding template generation must not break existing proposal upload/send behavior.
```

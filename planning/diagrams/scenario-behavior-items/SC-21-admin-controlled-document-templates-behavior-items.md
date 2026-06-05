# SC-21 — Admin-Controlled Document Templates Behavior Items

Status: future/deferred behavior items draft
Doc version: v0.1.0
Source scenario: `SC-21`

## 1. Purpose

This file contains future behavior items for admin-controlled template governance and later client-visible document options.

## 2. Source Set

```text
planning/diagrams/scenario-text-specs/SC-21-admin-controlled-document-templates.md
planning/diagrams/scenario-data/DOCGEN-document-template-data.md
```

## 3. Coverage Items Registry

| ID | Category | Short name | Source | Migration note |
|---|---|---|---|---|
| DOCGEN-CMD-ADMIN-PUBLISH-001 | CMD | Admin publishes template version | SC-21 | Future governance. |
| DOCGEN-CMD-ADMIN-ARCHIVE-001 | CMD | Admin archives template/version | SC-21 | Future governance. |
| DOCGEN-READ-EMP-AVAILABLE-001 | READ | Employee sees active employee-available templates | SC-21 / SC-20 | Future governance. |
| DOCGEN-READ-CLIENT-OPTIONS-001 | READ | Client sees only permitted options | SC-21 | Future client-visible stage. |
| DOCGEN-IBS-ADMIN-001 | IBS | Unauthorized user cannot manage templates | SC-21 | Future governance. |
| DOCGEN-VI-AUDIT-001 | VI | Generated document remains traceable to template version | SC-21 / SC-20 | Audit/history. |

## 4. Grouped Behavior Items

### 4.CMD — Command Behavior Cards

#### DOCGEN-CMD-ADMIN-PUBLISH-001 — Admin publishes template version

Source: `SC-21`

Required behavior / guarantee:

```text
Admin can publish/activate a template version for employee use.
```

Failure / no-write / preservation guarantee:

```text
Invalid template version or unauthorized actor does not publish template.
```

#### DOCGEN-CMD-ADMIN-ARCHIVE-001 — Admin archives template/version

Source: `SC-21`

Required behavior / guarantee:

```text
Admin can archive/deactivate template so it is not used for new proposal generation.
```

Failure / no-write / preservation guarantee:

```text
Archiving does not rewrite old generated documents.
```

### 4.READ — Read / Listing Behavior

#### DOCGEN-READ-EMP-AVAILABLE-001 — Employee sees active employee-available templates

Source: `SC-21 / SC-20`

Required behavior / guarantee:

```text
Employee sees only active templates published for employee use.
```

Failure / no-write / preservation guarantee:

```text
Inactive or not-available templates are not shown for new generation.
```

#### DOCGEN-READ-CLIENT-OPTIONS-001 — Client sees only permitted options

Source: `SC-21`

Required behavior / guarantee:

```text
Client sees only document options explicitly allowed by admin for client visibility.
```

Failure / no-write / preservation guarantee:

```text
Client cannot access internal template definitions or non-client-visible templates.
```

### 4.IBS — Impossible Business State Candidates

#### DOCGEN-IBS-ADMIN-001 — Unauthorized user cannot manage templates

Source: `SC-21`

Required behavior / guarantee:

```text
Only admin/governance-authorized actor can create, publish, archive or change official templates in the admin-governed stage.
```

Failure / no-write / preservation guarantee:

```text
Unauthorized template-management attempt makes no changes.
```

### 4.VI — Value Integrity Items

#### DOCGEN-VI-AUDIT-001 — Generated document remains traceable to template version

Source: `SC-21 / SC-20`

Required behavior / guarantee:

```text
Generated document keeps reference to the template version used even if admin later changes or archives template.
```

Failure / no-write / preservation guarantee:

```text
Template edits/archives do not mutate historical generated documents.
```

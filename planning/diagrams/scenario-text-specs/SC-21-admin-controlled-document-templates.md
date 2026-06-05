# SC-21 — Admin-Controlled Document Templates And Client-Visible Options

Status: future/deferred scenario draft
Doc version: v0.1.0
Source type: core/business scenario text spec
Actors:
- Admin
- Employee
- Client, only in later client-visible option stage

Related artifacts:
- Reusable DATA concepts: `planning/diagrams/scenario-data/DOCGEN-document-template-data.md`
- UI scenario: not created yet
- Behavior items: `planning/diagrams/scenario-behavior-items/SC-21-admin-controlled-document-templates-behavior-items.md`
- Related scenarios: SC-19, SC-20
- Domain map: future template governance/publication model
- Slice map: future `FUT-DOCGEN-2`, `FUT-DOCGEN-3`

## 1. Purpose

This scenario describes the later governance model where official document templates are created and controlled by an administrator, while employees use active approved templates and clients may only see permitted document options.

This scenario is not MVP. It should be planned after employee template generation is stable.

## 2. Statement Status / Source

### Direct requirements

- Client should not create templates.
- It may make sense later for client to choose from options, but not to create templates.
- Admin should eventually control what templates/options are available.

### Accepted directions / accepted decisions

- MVP is employee-only template creation/use.
- Later, admin creates/versions/activates/templates and controls availability.
- Employee uses active templates but does not edit official templates after admin governance exists.
- Client may later choose only admin-approved document options, not internal template definitions.

### Assumptions

- Admin is a future role or permission set.
- Template visibility/audience can be represented separately from template existence.
- Client-facing options may need different names/descriptions from internal template names.

### Future / deferred

- Admin template creation workflow.
- Template version publication.
- Employee-only vs client-visible audience rules.
- Client selection of permitted document options.

### Open questions

- Is Admin a separate account type, employee role, permission, or policy?
- Are client-visible options selected during request creation or during agreement exchange?
- Can employee override a client-selected document option?
- Which rules decide option availability by request type/applicant type/document kind?

### Deprecated / stale context

- Client-created templates are not accepted for current/future direction.

## 3. Actor / Context

Actor:
- Admin manages official templates.
- Employee uses active templates for proposal generation.
- Client may later select permitted document options.

Business context:
- Company document templates can be legal or organizational forms.
- Official templates should be governed and auditable.
- Client-facing choices should be controlled by company configuration.

User goal:
- Admin controls template availability and publication.
- Employee chooses approved active templates.
- Client sees only safe, permitted choices if client-visible options are implemented.

Related role/account state:
- Admin is authenticated and has template governance permission.
- Employee has proposal send permission but not template governance permission in this future stage.
- Client has account access but no template-management permission.

## 4. Entry Points

- Admin opens template management/governance section.
- Employee opens proposal-send form and sees only active employee-available templates.
- Future: client opens a request/agreement step with document option selection.

## 5. Preconditions

Business preconditions:
- Template generation MVP exists or is planned.
- Company wants stronger control over official document forms.

State preconditions:
- Template version exists before it can be published to audience.

Permission/access preconditions:
- Admin has template management permission.
- Employee has template usage permission.
- Client only sees client-visible options.

Out of scope:
- Client-created templates.
- Unrestricted template editing by employees.
- Legal compliance guarantees.

## 6. DATA / Scenario Information

### 6.1 Entered by actor

| DATA item | Meaning | Required? | Source/status | UI/UX note |
|---|---|---:|---|---|
| Admin template metadata | Name, description, document kind. | yes | future | form |
| Publication/audience | Employee-only or client-visible availability. | yes | future | select/toggle |
| Client display name | Name shown to client if option is client-visible. | if client-visible | future | form field |
| Client description | Explanation shown to client. | no | future | textarea |
| Availability rule | Conditions for showing option. | future | future | rule editor/simple fields |

### 6.2 Seen by actor

| DATA item | Meaning | Source/status | UI/UX note |
|---|---|---|---|
| Template version history | Admin sees previous versions and status. | future | table/details |
| Employee-available templates | Employee sees active templates only. | future | selector/list |
| Client-visible options | Client sees only allowed options. | future | list/select |

### 6.3 Selected / referenced

| DATA item | Meaning | Source/status | UI/UX note |
|---|---|---|---|
| Template version | Version admin publishes or employee uses. | future | selector/action |
| Audience | Who can use/see the template version. | future | option |
| Client document option | Business option selected by client. | future | client-facing select/card |

### 6.4 Filtered / searched

| DATA item | Meaning | Source/status | UI/UX note |
|---|---|---|---|
| Template status | Active/archived/draft. | future | filter |
| Audience | Employee-only/client-visible. | future | filter |
| Document kind | Template kind. | future | filter |

### 6.5 Attached / uploaded

| DATA item | Meaning | Source/status | UI/UX note |
|---|---|---|---|
| Template source | Official template content/source. | future | file/body input |

### 6.6 Result / feedback information

| DATA item | Meaning | Source/status | UI/UX note |
|---|---|---|---|
| Template published | Admin sees that version is available. | future | success/status |
| Client option saved | Client preference is stored. | future | success/selection state |
| Access blocked | User lacks permission or option not available. | future | feedback/error |

### 6.7 Not DATA

DTO/API contract:
- Exact admin endpoint names are future slice design.

UI layout:
- Exact admin/client UI is not defined here.

Domain invariant:
- Template publication and audit rules belong to future domain/slice design.

Test assertion:
- Exact tests belong to future test plan.

## 7. Extracted / Reusable DATA Concepts

| Concept | File | Used by | Reason for extraction |
|---|---|---|---|
| Document template DATA | `planning/diagrams/scenario-data/DOCGEN-document-template-data.md` | SC-19, SC-20, SC-21 | shared template concept |

## 8. Main Flow

1. Admin opens template management.
2. Admin creates or edits a template version.
3. Admin activates/publishes the version for employees.
4. Employee later sees active employee-available template in proposal-send form.
5. Employee generates proposal document from active template in SC-20.

## 9. Branches / Alternatives

### Branch A — Template archived by admin

Condition:
- Template should no longer be used for new proposal generation.

Flow:
1. Admin archives/deactivates template or version.
2. Employee template lists no longer show it for new generation.
3. Old generated documents remain traceable to the old template version.

Outcome:
- Template is unavailable for new use but historical documents remain unchanged.

### Branch B — Client-visible option after admin governance

Condition:
- Admin decides that a document option may be shown to clients.

Flow:
1. Admin marks template option as client-visible and configures client display data.
2. Client sees the option in the relevant client scenario.
3. Client chooses the option.
4. Employee/system uses that preference during document generation, subject to employee/business confirmation.

Outcome:
- Client can influence document option but cannot create/edit internal templates.

### Branch C — Unauthorized user attempts template management

Condition:
- Employee/client without admin permission opens admin template management.

Flow:
1. System blocks access.
2. No template changes are made.

Outcome:
- Template governance remains restricted.

## 10. Business Rules / Invariants

| Rule | Source/status | Notes |
|---|---|---|
| Admin controls official template availability in future governance stage. | accepted direction | Employee-only creation is MVP, not final governance model. |
| Employee uses active templates but does not edit official templates after admin governance exists. | accepted direction | Role separation. |
| Client cannot create or edit templates. | accepted | Strong invariant. |
| Client can only see options explicitly allowed by admin. | future | Not MVP. |
| Old generated documents remain traceable to used template version. | accepted | Audit/history. |

## 11. Observable Outcomes

Successful outcome:
- Admin publishes template/version for employee use.
- Employee uses only active employee-available templates.
- Future client sees only allowed options.

Failure/blocked outcomes:
- Unauthorized template management is blocked.
- Archived/inactive templates cannot be used for new generation.

No-write / preservation outcomes:
- Unauthorized action changes no templates.
- Archiving template does not rewrite old generated documents.

User-visible outcome/feedback:
- Admin sees publication/archive status.
- Employee sees available active templates.
- Future client sees only allowed options.

## 12. UI / UX Alignment

This scenario does not define full UI layout.

UI scenario:
- Not created yet.

Presentation/UX summary:
- Admin template management should expose status, audience and version history.
- Employee selector should hide inactive/unavailable templates.
- Client option UI should use business labels, not internal template IDs.

Consistency questions:
- Should client preference be binding or only advisory for employee?
- Where should client option selection happen: request creation or agreement exchange?

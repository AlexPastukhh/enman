# SC-19 — Document Template Management

Status: future scenario draft / document generation candidate
Doc version: v0.1.0
Source type: core/business scenario text spec
Actors:
- Employee in MVP.
- Admin in future governance stage.

Related artifacts:
- Reusable DATA concepts: `planning/diagrams/scenario-data/DOCGEN-document-template-data.md`
- UI scenario: not created yet
- Behavior items: `planning/diagrams/scenario-behavior-items/SC-19-document-template-management-behavior-items.md`
- Clarifications: future DOCGEN track in VKR goal map / conversation decisions
- Domain map: future `DocumentTemplate`, `DocumentTemplateVersion`, `TemplateField`
- Slice map: future `FUT-DOCGEN-1`, `FUT-DOCGEN-2`

## 1. Purpose

This scenario describes how an employee can create and maintain document templates so that proposal documents can later be generated from company-controlled reusable forms instead of being prepared only outside the system and uploaded as files.

This scenario is future/planned. It does not mean that template generation is already implemented.

## 2. Statement Status / Source

### Direct requirements

- Employee should be able to create document templates in a separate workflow.
- Employee should be able to use created templates later when sending proposal versions to the client.
- Template creation should be reachable from agreement exchange/proposal-send flow through a link or button that opens template creation in a new tab/window.

### Accepted directions / accepted decisions

- In the MVP, employee can create templates and use templates.
- In a later stage, template creation and governance should move to an administrator role.
- Client must not create templates.
- Client template selection is deferred; the client may later choose only admin-approved document options, not internal template definitions.
- Uploading a ready-made proposal document remains available as an alternative to generation.

### Assumptions

- A template can be represented as HTML, DOCX or another template source, but the first implementation should choose one simple engine.
- A template can declare required placeholders/fields.
- Template changes must not rewrite old generated documents.

### Future / deferred

- Admin-only template management.
- Template version approval workflow.
- Client-visible document options.
- Visual template designer.
- External EDMS / e-signature / legal compliance validation.

### Open questions

- Which template engine is first: HTML-to-PDF or DOCX placeholder replacement?
- Does MVP need `DocumentTemplateVersion` immediately, or can first pass use a single active version model?
- Should employee-created templates become active immediately in MVP?
- Which employee roles may create templates before Admin exists?

### Deprecated / stale context

- A proposal document is not only an uploaded file in this future direction; it can also be generated from a template.

## 3. Actor / Context

Actor:
- Employee in MVP.
- Admin in future governance stage.

Business context:
- The company needs reusable document forms for agreement proposal exchange.
- Different proposal documents may be needed for different legal, business or process cases.
- Templates should not be hardcoded in application code.

User goal:
- Create a reusable document template that can be selected during proposal version sending.

Related role/account state:
- Actor is authenticated and has permission to manage templates in the current stage.

## 4. Entry Points

- Employee opens a document templates page/section.
- Employee clicks "Create new template" from the proposal-send form in an agreement exchange; template creation opens in a new tab/window.
- Future: Admin opens a template-management section.

## 5. Preconditions

Business preconditions:
- Company wants to generate proposal documents from reusable templates.

State preconditions:
- No existing exchange is required to create a template.
- Template can be created before it is used in a proposal.

Permission/access preconditions:
- MVP: employee has template-management permission.
- Future: admin has template-management permission; ordinary employee only selects active templates.

Out of scope:
- Client-created templates.
- Legal guarantee that a template is compliant with current law.
- Visual designer.
- E-signature.

## 6. DATA / Scenario Information

### 6.1 Entered by actor

| DATA item | Meaning | Required? | Source/status | UI/UX note |
|---|---|---:|---|---|
| Template name | Human-readable name of the template. | yes | accepted | form field |
| Template description | Optional explanation of when template is used. | no | accepted | textarea |
| Document kind | Business category of generated document. | yes | accepted | select |
| Template content/source | Template body or uploaded template source. | yes | accepted | textarea or file input depending on engine |
| Template fields/placeholders | Data keys used by the template. | yes if template has placeholders | accepted | may be detected or entered manually |
| Activation decision | Whether template becomes available for proposal generation. | yes | accepted | checkbox/action |

### 6.2 Seen by actor

| DATA item | Meaning | Source/status | UI/UX note |
|---|---|---|---|
| Existing templates | Templates already available to actor. | accepted | list/table |
| Template status | Draft/Active/Archived or equivalent. | accepted | status badge/general pattern |
| Template version | Version used for audit/history. | future/accepted direction | version label |
| Validation feedback | Missing name/content/placeholders. | accepted | field errors/feedback |

### 6.3 Selected / referenced

| DATA item | Meaning | Source/status | UI/UX note |
|---|---|---|---|
| Existing template | Template selected for viewing/editing/activation. | accepted | row/action |
| Template engine | Method used to render the template. | assumption | may be hidden in MVP |

### 6.4 Filtered / searched

| DATA item | Meaning | Source/status | UI/UX note |
|---|---|---|---|
| Document kind filter | Show templates by document kind. | future | filter |
| Status filter | Show active/draft/archived templates. | future | filter |

### 6.5 Attached / uploaded

| DATA item | Meaning | Source/status | UI/UX note |
|---|---|---|---|
| Template source file | DOCX/HTML/source template file if file-based templates are used. | engine-dependent | file input |

### 6.6 Result / feedback information

| DATA item | Meaning | Source/status | UI/UX note |
|---|---|---|---|
| Template created | Confirmation that template exists. | accepted | success feedback |
| Template active | Confirmation that template can be used in proposal-send flow. | accepted | success/status |
| Template validation errors | Required fields/content/placeholders missing or invalid. | accepted | field/server errors |

### 6.7 Not DATA

DTO/API contract:
- Concrete request/response names are implementation details.

UI layout:
- Exact placement and styling are not owned by this scenario.

Domain invariant:
- Template generation/domain validation belongs to downstream domain/slice drafts.

Test assertion:
- Exact test names are not owned by this scenario.

## 7. Extracted / Reusable DATA Concepts

| Concept | File | Used by | Reason for extraction |
|---|---|---|---|
| Document template DATA | `planning/diagrams/scenario-data/DOCGEN-document-template-data.md` | SC-19, SC-20, SC-21 | reused by template management, proposal generation and admin governance |

## 8. Main Flow

1. Employee opens document template management.
2. Employee chooses to create a new template.
3. Employee enters template name, document kind and template content/source.
4. Employee defines or confirms template placeholders/fields.
5. Employee saves the template.
6. System validates that required template data is present.
7. System creates the template.
8. Employee activates the template or the system makes it active in MVP.
9. Active template becomes available for proposal generation scenario SC-20.

## 9. Branches / Alternatives

### Branch A — Template creation opened from proposal-send form

Condition:
- Employee is preparing a proposal version and does not see a suitable template.

Flow:
1. Employee clicks "Create new template".
2. Template creation opens in a new tab/window.
3. Employee creates and activates the template.
4. Employee returns to the proposal-send window.
5. Original window refreshes or refetches active templates.

Outcome:
- Newly created template can be selected in proposal-send form.

### Branch B — Invalid template

Condition:
- Required template name/content/kind/fields are missing or invalid.

Flow:
1. System rejects template creation or activation.
2. Employee sees validation feedback.
3. No active template is created.

Outcome:
- Employee can correct template data and retry.

### Branch C — Future admin governance

Condition:
- Admin-controlled templates are implemented.

Flow:
1. Admin creates/activates templates.
2. Employee no longer creates official templates.
3. Employee uses active templates only.

Outcome:
- Template ownership moves to admin-controlled workflow SC-21.

## 10. Business Rules / Invariants

| Rule | Source/status | Notes |
|---|---|---|
| Template must have a name. | accepted | MVP validation. |
| Template must have content/source. | accepted | Engine-specific details downstream. |
| Active template can be used for proposal generation. | accepted | SC-20 depends on this. |
| Client cannot create templates. | accepted | Client-created templates out of scope. |
| Template changes must not rewrite already generated documents. | accepted | Requires generated document traceability. |
| Template versioning should preserve historical generation context. | accepted direction | Strongly recommended even if MVP is simplified. |

## 11. Observable Outcomes

Successful outcome:
- A template is created and can become active.
- Active template appears in proposal generation scenario.

Failure/blocked outcomes:
- Invalid template input blocks creation/activation.
- User without permission cannot create templates.

No-write / preservation outcomes:
- Invalid template does not become active.
- Existing generated documents are not changed by template edits.

User-visible outcome/feedback:
- Employee sees created/activated template or validation error.

## 12. UI / UX Alignment

This scenario does not define full UI layout.

UI scenario:
- Not created yet.

Presentation/UX summary:
- Use a template management page with a create/edit form.
- From proposal-send form, use a link/button to open template creation in a new tab/window.
- Proposal-send window should support manual refresh and preferably refetch on window focus.

Consistency questions:
- Should template creation be modal, new page or new tab in MVP?
- Should activation be automatic in MVP or explicit?

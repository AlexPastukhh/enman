# SC-20 — Employee Proposal Document Generation From Template

Status: future scenario draft / document generation candidate
Doc version: v0.1.0
Source type: core/business scenario text spec
Actors:
- Employee

Related artifacts:
- Reusable DATA concepts: `planning/diagrams/scenario-data/DOCGEN-document-template-data.md`
- UI scenario: not created yet
- Behavior items: `planning/diagrams/scenario-behavior-items/SC-20-employee-proposal-document-generation-from-template-behavior-items.md`
- Related current scenario: `planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md`
- Domain map: future `DocumentTemplate`, `DocumentTemplateVersion`, generated document metadata
- Slice map: future `FUT-DOCGEN-1`

## 1. Purpose

This scenario describes how an employee can generate a proposal document from an active document template while sending an agreement proposal version to the client.

The scenario extends the existing employee proposal send-version flow. It does not replace file upload in the MVP.

## 2. Statement Status / Source

### Direct requirements

- When sending a proposal version, employee can choose an existing active template.
- Employee can still upload a ready-made file.
- Employee can open template creation from the proposal-send window and return/refetch to choose the new template.
- Generated document is used as the file/document sent to the client.

### Accepted directions / accepted decisions

- Template generation is employee-side in MVP.
- Client does not choose internal templates in MVP.
- Client does not create templates.
- Generated document must be traceable to the template/version used.
- Upload flow remains available as an alternative document origin.

### Assumptions

- Proposal exchange already exists or can be started according to SC-13D.
- Related request/applicant/exchange data can provide template placeholder values.
- If required data is missing, generation is blocked with clear feedback.

### Future / deferred

- Client-visible document options.
- Admin-controlled template availability.
- Preview before generation.
- Complex conditions/clauses in templates.
- E-signature/EDMS/legal validation.

### Open questions

- Should proposal generation create preview first or create/send in one command?
- Should the generated file be PDF-only or should DOCX also be stored?
- Should employee be allowed to edit generated content before sending?
- Which placeholder keys are supported in MVP?

### Deprecated / stale context

- Treating agreement proposal document as upload-only becomes incomplete after this scenario is implemented.

## 3. Actor / Context

Actor:
- Employee.

Business context:
- Employee sends agreement proposal documents to client as part of agreement proposal exchange.
- The company may need different proposal document forms.
- Template-based generation reduces manual file preparation and hardcoded document forms.

User goal:
- Select an active template and generate a proposal document for the current exchange/request.

Related role/account state:
- Employee is authenticated and has permission to manage/send agreement proposals.

## 4. Entry Points

- Employee starts first proposal version from an approved request.
- Employee sends a new proposal version in response to a client-sent version.
- Employee opens proposal-send form and chooses document preparation mode.

## 5. Preconditions

Business preconditions:
- Employee can send a proposal version according to SC-13D.
- At least one active template exists for the relevant document kind, or employee can open template creation.

State preconditions:
- Initial proposal: request is Approved and no existing exchange blocks start.
- New version: exchange is in state that allows employee response.

Permission/access preconditions:
- Employee has access to the request/exchange and active templates.

Out of scope:
- Client selecting internal template.
- Client creating template.
- Admin governance.
- Legal compliance guarantee.

## 6. DATA / Scenario Information

### 6.1 Entered by actor

| DATA item | Meaning | Required? | Source/status | UI/UX note |
|---|---|---:|---|---|
| Document preparation mode | Upload file or generate from template. | yes | accepted | radio/select |
| Proposal comment/details | Optional or required message attached to proposal version, depending on current SC-13D rules. | scenario-dependent | existing SC-13D | textarea |

### 6.2 Seen by actor

| DATA item | Meaning | Source/status | UI/UX note |
|---|---|---|---|
| Approved request / exchange summary | Context for generated document. | existing SC-13D | detail summary |
| Active templates | Templates available for selected document kind. | accepted | dropdown/list |
| Template description | Helps employee choose correct template. | accepted | option details |
| Missing data feedback | Required placeholder data missing. | accepted | feedback/errors |

### 6.3 Selected / referenced

| DATA item | Meaning | Source/status | UI/UX note |
|---|---|---|---|
| Document template | Active template selected for generation. | accepted | selector |
| Template version | Specific version used for generation. | accepted direction | may be hidden but stored |
| Current request/applicant/exchange data | Source values for placeholders. | accepted | not directly editable here |

### 6.4 Filtered / searched

| DATA item | Meaning | Source/status | UI/UX note |
|---|---|---|---|
| Document kind | Limits active templates to relevant proposal document kind. | accepted | implicit or explicit filter |

### 6.5 Attached / uploaded

| DATA item | Meaning | Source/status | UI/UX note |
|---|---|---|---|
| Ready-made proposal file | Used when employee chooses upload mode. | existing SC-13D | file input |
| Generated document | System-created file attached to proposal version when template mode is selected. | accepted | result of generation, not employee upload |

### 6.6 Result / feedback information

| DATA item | Meaning | Source/status | UI/UX note |
|---|---|---|---|
| Generated document reference | Stored file/document created from template. | accepted | success/result |
| Proposal version sent | Proposal version created with uploaded or generated document. | existing + accepted | success feedback |
| Generation blocked feedback | Required template data missing or template inactive. | accepted | error feedback |

### 6.7 Not DATA

DTO/API contract:
- Exact endpoint shape belongs to slice/API design.

UI layout:
- Exact form layout belongs to UI scenario/client slice.

Domain invariant:
- Exchange lifecycle rules remain owned by SC-13D/domain drafts.

Test assertion:
- Test names/assertions belong to test plans.

## 7. Extracted / Reusable DATA Concepts

| Concept | File | Used by | Reason for extraction |
|---|---|---|---|
| Document template DATA | `planning/diagrams/scenario-data/DOCGEN-document-template-data.md` | SC-19, SC-20, SC-21 | shared template concept |

## 8. Main Flow

1. Employee opens proposal-send form for an approved request or existing exchange.
2. Employee chooses document preparation mode.
3. Employee selects "Generate from template".
4. System shows active templates for the relevant document kind.
5. Employee selects a template.
6. System checks that required placeholder data is available.
7. System generates a document/PDF from the selected template and current request/applicant/exchange data.
8. System stores generated document.
9. Employee submits/sends proposal version with the generated document.
10. Proposal version becomes visible to the client like a normal employee-sent proposal.

## 9. Branches / Alternatives

### Branch A — Employee uploads ready-made file

Condition:
- Employee selects upload mode.

Flow:
1. Employee uploads ready-made proposal file.
2. Existing SC-13D upload/send flow continues.

Outcome:
- Proposal version is created with uploaded document origin.

### Branch B — Employee creates missing template from proposal-send form

Condition:
- Suitable template does not exist or is not visible.

Flow:
1. Employee clicks "Create new template".
2. Template creation opens in a new tab/window.
3. Employee creates and activates template in SC-19.
4. Employee returns to original proposal-send window.
5. Original window refetches active templates on focus or employee clicks "Refresh templates".
6. Employee selects newly created template.

Outcome:
- New template can be used without restarting the whole proposal-send scenario.

### Branch C — Required data missing

Condition:
- Template requires data that is unavailable for the current request/applicant/exchange.

Flow:
1. System blocks generation.
2. Employee sees missing-data feedback.
3. No generated document and no proposal version is created from that attempt.

Outcome:
- Employee can choose another template, correct available data if possible, or upload a ready-made file.

### Branch D — Template becomes inactive before submit

Condition:
- Selected template is no longer active/available.

Flow:
1. System blocks generation or send.
2. Employee refreshes template list and chooses an active template.

Outcome:
- Inactive template is not used for new generated documents.

## 10. Business Rules / Invariants

| Rule | Source/status | Notes |
|---|---|---|
| Employee can generate proposal document only when employee can send proposal version. | accepted | SC-13D lifecycle still applies. |
| Generated document must be attached to proposal version. | accepted | Generated document replaces upload as document origin. |
| Upload flow remains available in MVP. | accepted | Avoids blocking users if template generation fails. |
| Generated document must reference template version used. | accepted | Audit/history. |
| Template generation must validate required placeholder data. | accepted | No invalid generated document. |
| Client cannot create templates. | accepted | Client receives document only in MVP. |

## 11. Observable Outcomes

Successful outcome:
- Employee sends proposal version with generated document.
- Client can view/download/respond to generated document like other proposal documents.
- Generated document is traceable to template version.

Failure/blocked outcomes:
- Missing template or missing required data blocks generation.
- Inactive template cannot be used.
- Wrong exchange/request lifecycle blocks proposal send.

No-write / preservation outcomes:
- Failed generation creates no proposal version.
- Existing exchange/proposals remain unchanged after blocked generation.
- Existing generated documents are not changed by template edits.

User-visible outcome/feedback:
- Employee sees generated/sent success or clear generation error.

## 12. UI / UX Alignment

This scenario does not define full UI layout.

UI scenario:
- Not created yet.

Presentation/UX summary:
- Proposal-send form should provide a document preparation mode: upload or generate from template.
- Template selector appears only for generate-from-template mode.
- "Create new template" opens template workflow in a new tab/window.
- Original proposal-send window supports refetch on focus and/or manual refresh.
- Preview is useful but deferred unless selected for MVP.

Consistency questions:
- Should generation happen before send as a preview step or inside send command?
- Should employee be able to download generated document before sending?

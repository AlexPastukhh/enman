# Scenario DATA Concept — Document Template / Generated Proposal Document

Status: future reusable DATA concept / document generation candidate
Doc version: v0.1.0
Scope: shared template and generated-document information used by SC-19, SC-20 and SC-21

## 1. Purpose

This DATA concept describes information that participates in document template management and template-based proposal document generation.

DATA here means scenario information seen, entered, selected, referenced or received by actors. It is not DTO/API contract, DB schema, domain design or UI layout.

## 2. Used By Scenarios

| Scenario | How used | Local variation |
|---|---|---|
| SC-19 Document Template Management | Employee/Admin creates and activates templates. | Template creation/editing data. |
| SC-20 Employee Proposal Document Generation From Template | Employee selects active template and generates proposal document. | Template selected as document origin. |
| SC-21 Admin-Controlled Document Templates | Admin publishes templates and later exposes permitted options to clients. | Governance/audience/client-facing fields. |

## 3. Business Information

Entered:
- Template name.
- Template description.
- Document kind.
- Template content/source.
- Placeholder/field definitions if not detected automatically.
- Publication/audience settings in future admin stage.
- Client display name/description in future client-visible stage.

Seen:
- Template list.
- Template status.
- Template version.
- Template description.
- Template document kind.
- Required placeholder fields.
- Generation result/feedback.

Selected / referenced:
- Active template.
- Template version.
- Document preparation mode: upload or generate from template.
- Template audience: employee-only or client-visible in future admin stage.

Filtered / searched:
- By document kind.
- By status.
- By audience in future admin stage.

Attached / uploaded:
- Template source file if file-based template engine is used.
- Ready-made proposal document if upload mode is used.
- Generated proposal document as system-created attachment/result.

Outcome / feedback:
- Template created/activated.
- Template unavailable/inactive.
- Missing required placeholder data.
- Generated document created and attached to proposal version.
- Generation blocked without changing existing exchange/proposals.

## 4. Scenario Variations

Employee MVP:
- Employee can create templates and use active templates.
- Employee can open template creation from proposal-send form.

Admin future:
- Admin creates, versions, activates and archives templates.
- Employee only uses active approved templates.

Client future:
- Client sees only admin-approved document options.
- Client cannot create or edit templates.
- Client-facing labels may differ from internal template names.

## 5. UI / UX Presentation Notes

No scenario-specific UI requirement by default.

General patterns:
- Template list/table.
- Template create/edit form.
- Active template selector in proposal-send form.
- Document preparation mode selector.
- Manual refresh button and refetch-on-focus after creating a template in a new tab/window.
- Status badge for active/draft/archived.
- Feedback block for missing placeholder data.

Specific UI requirements:
- See future UI scenarios when created.

## 6. Downstream Notes

Domain:
- Candidate classes: DocumentTemplate, DocumentTemplateVersion, TemplateField, GeneratedAgreementDocument.
- AgreementProposalVersion or related metadata should distinguish Uploaded vs GeneratedFromTemplate document origin.
- Generated document should reference the template version used.

Slice:
- FUT-DOCGEN-1: employee template generation MVP.
- FUT-DOCGEN-2: admin-controlled templates.
- FUT-DOCGEN-3: client-visible document options.

Client/testing:
- Proposal-send form should prove upload flow still works.
- Template generation should prove selected active template produces a stored document reference.
- Missing data should block generation without creating proposal version.

## 7. Questions / Decisions

- Which template engine should be first: HTML-to-PDF or DOCX placeholder replacement?
- Should preview be required before send?
- Should employee be able to edit generated content before sending?
- Should template versioning be implemented in the first MVP or only modeled for future?
- Which placeholder keys are available in the first implementation?

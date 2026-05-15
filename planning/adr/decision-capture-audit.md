# ADR Decision Capture Audit

Status: current audit summary  
Scope: pass over current planning docs to collect decisions used as guidance

## 1. Purpose

This file records the result of the current decision-capture audit.

The audit did not create full numbered ADRs.

It updated:

```text
planning/adr/architecture-decision-notes.md
planning/adr/adr-candidates.md
planning/adr/adr-workflow.md
planning/adr/README.md
```

## 2. Sources Checked

Main sources checked:

```text
planning/adr/README.md
planning/adr/adr-candidates.md
planning/adr/architecture-decision-notes.md
planning/tables/domain-drafts/domain-draft-01.md
planning/l1-domain-implementation-cut.md
planning/l1-domain-testing-rules.md
planning/slices/SL-REQ-001-create-connection-request.md
planning/slices/SL-REVIEW-001-approve-request-and-verify-applicant.md
planning/slices/SL-REVIEW-002-reject-request.md
planning/slices/slice-extension-points-register.md
planning/slices/client-architecture-principles.md
planning/client/cross-cutting/CL-STYLING-001-css-modules-tokens.md
planning/client/cross-cutting/CL-A11Y-001-accessibility-and-aria.md
planning/client/cross-cutting/CL-FORM-VALIDATION-001-deferred-validation.md
```

## 3. Result

No blocking contradictions were found.

Several open questions were kept as open questions and were not converted into accepted decisions.

## 4. Decisions Added / Clarified

Added or clarified accepted decision notes for:

```text
- first L1 cut narrower than domain draft;
- progressive file splitting;
- no-write testing behavior;
- scenario UI specs;
- no .client.md in advance;
- entity query hooks generic-only;
- entity display components only;
- widgets only after real reuse;
- CSS Modules/tokens styling decision;
- accessibility / ARIA contract;
- deferred validation;
- client/server error mapping;
- FormValues vs API DTO;
- extension pressure register as workflow gate;
- account email vs applicant contact email;
- current Active account registration with activation flow future;
- request creation Client/UI as dependent slice;
- approval does not create agreement proposal;
- empty feedback normalization as open question, not accepted decision;
- failed approve/reject no-write behavior;
- request/agreement public numbers as future.
```

## 5. Open Questions Preserved

Open questions remain listed in `architecture-decision-notes.md` section 5.

They must not be treated as accepted decisions.

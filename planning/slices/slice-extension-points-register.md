# Slice Extension Points Register

Status: active register  
Scope: cross-slice extension points, change points, extension pressure, anti-coupling decisions and related extension questions

## 1. Purpose

This register makes planned extension points and extension pressure visible across slices.

It must be checked before starting a parent slice or `.client.md` sidecar.

It does not replace local slice sections.

It also does not replace:

```text
planning/slices/slice-questions-register.md
```

Use the slice questions register as the shared overview of currently relevant local slice questions.

Use this extension register when a question is tied to extension/change pressure, anti-coupling or future slices.

## 2. Intake Rule

Before starting a slice/client sidecar:

```text
1. Search this register by parent slice, future slice, scenario and layer.
2. Search `slice-questions-register.md` for related local questions.
3. Check whether relevant extension pressure affects current implementation.
4. Decide whether current work needs explicit seam, anti-coupling only, convention-first, ignore, or revisit.
5. Record the decision locally in the slice/client file.
6. Update this register when the decision can affect future slices.
7. Update `slice-questions-register.md` when a local/cross-slice question status changes.
```

## 3. Extension Points Coverage

| ID | Parent slice | Future extension slice | Layer | Current seam | Certainty | Time horizon | Covered locally? | Status |
|---|---|---|---|---|---|---|---|---|
| EP-REQ-APPROVED-001 | SL-REVIEW-001 | Agreement proposal creation | Server + Client | Approved request status / approved request details | high | next layer / near future | to confirm in slice | planned |
| EP-REQ-DOCS-001 | SL-REQ-001 | Request documents | Server + Client | RequestId / request context | medium | later | to confirm in slice | planned |
| EP-APPL-VERIFY-001 | SL-REVIEW-001 | External ApplicantParty verification provider | Server | ApplicantParty + request review context | medium | later / plugin | to confirm in slice | planned |
| EP-NOTIFY-001 | SL-REVIEW-001 / SL-REVIEW-002 | Notifications after approve/reject | Server + Client | review decision result | medium | later | to confirm in slice | planned |
| EP-REVIEW-START-001 | Review read/client slices | Start review lock/session/assignment | Server + Client | review page entry | low/medium | maybe later | not yet | candidate |
| EP-DASH-FILTER-001 | Employee request dashboard read slice | Saved dashboard filter preference | Client + Server | dashboard filters | low/medium | later | not yet | candidate |

## 4. Extension Pressure / Anti-Coupling Decisions

| ID | Related extension point | Affected current slice | Layer | Pressure importance | Probability | Time horizon | Discussed? | Decision | Anti-coupling constraint | Trade-off | Revisit when |
|---|---|---|---|---|---|---|---|---|---|---|---|
| EPRESS-APPROVAL-001 | EP-REQ-APPROVED-001 | SL-REVIEW-001 | Server + Client | high | high | next layer / near future | yes | Anti-coupling only now | Approval must not auto-create agreement proposal; approve client must not import/couple to agreement proposal feature | User will need separate future action; current flow stays simpler and clearer | before agreement proposal slice |
| EPRESS-REQ-DOCS-001 | EP-REQ-DOCS-001 | SL-REQ-001 | Server + Client | medium | medium | later | yes | Anti-coupling only now | Request creation must not require documents in L1 and should leave request context usable for later documents slice | Later document workflow may need extra UI/API | before documents slice |
| EPRESS-APPL-VERIFY-001 | EP-APPL-VERIFY-001 | SL-REVIEW-001 | Server | medium | medium | later / plugin | yes | Avoid provider coupling | Current applicant verification should not hard-code assumptions that block external provider later | Provider abstraction may still wait until provider slice | before external verification slice |
| EPRESS-REVIEW-START-001 | EP-REVIEW-START-001 | Employee review client/read planning | Client + Server | low/medium | low/medium | maybe later | yes | Convention-first / navigation-only for now | Do not create start-review feature unless server-side lock/session is introduced | If lock appears later, review entry flow may be refactored | if review lock/session is requested |
| EPRESS-DASH-FILTER-001 | EP-DASH-FILTER-001 | Employee dashboard client/read planning | Client + Server | low/medium | low/medium | later | yes | Treat filters as read query state now | Do not create command feature for filters unless preferences are persisted | Saved preference slice may add feature later | if saved filters are requested |

## 5. Change Points Coverage

| ID | Affected slice | Layer | Behavior aspect | Change point owner | Current decision | Configurable now? | Tests affected | Status |
|---|---|---|---|---|---|---|---|---|
| CP-REJECT-FEEDBACK-001 | SL-REVIEW-002 | Server + Client | Rejection feedback required/optional | domain/app/client policy | optional in domain; UI warning/confirmation for empty | no, documented decision | reject domain/client tests | active |
| CP-REVIEW-ENTRY-001 | Review read/client slices | Server + Client | Review page entry vs startReview command | page routing / possible application command | navigation/read context only | no | route/read tests | active |
| CP-ERROR-MAPPING-001 | multiple slices | Server + Client | Domain/application error to HTTP/client display | response mapper + client error mapper | stable problem/error mapping | later | integration/client error tests | active |
| CP-FORM-VALIDATION-001 | form sidecars | Client | Deferred validation behavior | CL-FORM-VALIDATION-001 / form hooks | deferred after input, immediate on submit | maybe | client form tests | active |
| CP-STYLING-001 | client sidecars | Client | Project styling theme/tokens | CSS variables / CSS Modules + tokens | plain CSS + CSS Modules + tokens | yes | visual/component checks | active |
| CP-A11Y-001 | client sidecars | Client | Accessibility/ARIA choices | native semantics + ARIA only when needed | role/label-first | no | Testing Library semantic queries | active |

## 6. Questions Across Slices

This section contains extension/change-related questions.

For the broader shared overview of local slice questions, use:

```text
planning/slices/slice-questions-register.md
```

| ID | Related slice(s) | Related EP/CP | Question | Assumption | Why it matters | Blocks current work? | Status |
|---|---|---|---|---|---|---|---|
| Q-EP-001 | SL-REVIEW-001, agreement proposal slice | EP-REQ-APPROVED-001 / EPRESS-APPROVAL-001 | Should approval create agreement proposal automatically? | No. Agreement proposal starts by separate employee action on Approved request. | Prevents coupling approval to agreement proposal. | no for current docs; yes if agreement behavior changes | accepted direction |
| Q-EP-002 | Review read/client slices | EP-REVIEW-START-001 / CP-REVIEW-ENTRY-001 | Does entering review create server-side lock/session/assignment? | No for now; review page is navigation/read context. | Determines whether start-review command/feature exists. | no | open for future |
| Q-EP-003 | Dashboard read/client slices | EP-DASH-FILTER-001 | Are filters persisted user preferences? | No for now; filters are read query state. | Determines whether filtering is read state or command feature. | no | open for future |
| Q-EP-004 | SL-REVIEW-001 | EP-APPL-VERIFY-001 | Is external verification provider needed in current L1? | No; keep provider assumptions out where reasonable. | Avoids premature provider abstraction but protects future plugin. | no | open for future |

## 7. Status Values

```text
planned
candidate
active
accepted direction
open for future
resolved
superseded
ignored
```

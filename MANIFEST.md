# L2-REVIEW-REJECT-001.client — Reject Request Review

## Summary

Client-only implementation for the Employee Reject Review command sidecar.

## Added files

- `energymanagement.client/src/features/employee-request/reject-review/ui/RejectReviewForm.tsx`
- `energymanagement.client/src/features/employee-request/reject-review/ui/RejectReviewForm.test.tsx`
- `energymanagement.client/src/features/employee-request/reject-review/ui/rejectReviewForm.css`
- `energymanagement.client/src/features/employee-request/reject-review/ui/rejectReviewFormConst.ts`

## Replaced files

- `energymanagement.client/src/features/employee-request/reject-review/api/rejectRequestReview.ts`
- `energymanagement.client/src/features/employee-request/reject-review/api/rejectRequestReview.test.ts`
- `energymanagement.client/src/features/employee-request/reject-review/model/useRejectRequestReviewMutation.ts`
- `energymanagement.client/src/pages/employee/requests/details/EmployeeRequestDetailsPage.tsx`
- `energymanagement.client/src/pages/employee/requests/details/EmployeeRequestDetailsPage.test.tsx`

## Deleted files

None.

## Generated artifacts

None changed in this client-only archive.

Note: the uploaded project already contains server source for `POST /api/employee/requests/{requestId}/review/reject`, but `Shared/openapi.json` / generated TypeScript types appear stale for this endpoint. If `npm run check:api` reports stale artifacts, run the repo generation workflow and include:

- `Shared/openapi.json`
- `energymanagement.client/src/shared/api/generated/openapi-types.ts`

in the backend/API handoff.

## Tests changed

- `energymanagement.client/src/features/employee-request/reject-review/api/rejectRequestReview.test.ts`
- `energymanagement.client/src/features/employee-request/reject-review/ui/RejectReviewForm.test.tsx`
- `energymanagement.client/src/pages/employee/requests/details/EmployeeRequestDetailsPage.test.tsx`

## Commands run and results

- `npm --prefix ./energymanagement.client install` — success; npm reported existing audit vulnerabilities.
- `npm --prefix ./energymanagement.client run test -- --run --reporter=verbose src/features/employee-request/reject-review/api/rejectRequestReview.test.ts src/features/employee-request/reject-review/ui/RejectReviewForm.test.tsx src/pages/employee/requests/details/EmployeeRequestDetailsPage.test.tsx` — success: 3 test files passed, 13 tests passed.
- `npm --prefix ./energymanagement.client run build` — success; Vite emitted existing chunk-size warning.
- `npm --prefix ./energymanagement.client run test -- --run --reporter=dot` — timed out in sandbox after startup output.
- `npm --prefix ./energymanagement.client run lint` — failed on existing `react-refresh/only-export-components` errors outside this slice.

## Non-goals respected

- No server/backend changes.
- No Domain.EnergyManagement changes.
- No planning docs changes.
- No database/migration changes.
- No generated artifact manual edits.
- No StartReview changes.
- No ApproveReview changes.
- No AgreementProposalExchange implementation.
- No dashboard/list reject entry point.
- No local CSRF mechanics.
- No `shared/api` business wrapper.
- No unrelated cleanup.
- No GitHub write.

## Risks / handoff notes

- First-pass Reject placement is details-only under `pages/employee/requests/details`.
- Feedback is treated as optional on the client: missing feedback does not block submit.
- The client sends a JSON body with `feedback: ""` when no feedback is provided, which keeps the current server binding shape stable.
- The uploaded server validator currently appears to reject blank feedback; if optional feedback is the final target, the server validator/docs/generated contract need a backend/API follow-up.
- Generated OpenAPI/types may be stale for the reject endpoint and should be regenerated in the backend/API handoff.

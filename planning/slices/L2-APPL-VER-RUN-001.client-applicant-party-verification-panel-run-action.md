L2-APPL-VER-RUN-001.client — ApplicantParty Verification Panel / Run Action

Status: planned client sidecar draft / deferred verification extension / blocked until server endpoint, read DTO extension and generated OpenAPI contract exist
Parent server slice: SL-APPL-VER-001 — Run Mock ApplicantParty Verification From Employee Request Review
Host read sidecars: L2-EMP-DASH-001.client, L2-EMP-DETAILS-001.client
Actor: Employee
Slice type: client command sidecar + request/read-surface verification display
Placement: Employee request dashboard row, Employee request details applicant/review area

Architecture direction:

Read state:
  Employee request dashboard/details DTOs expose applicantVerification summary.

Display:
  Employee request read UI renders compact verification state.

Command action:
  feature-owned wrapper/mutation/button calls:
    POST /api/employee/requests/{requestId}/applicant-party/verification/run

Authority:
  client sends requestId only.
  server resolves ApplicantParty through request.ApplicantPartyId.
0. Scenario Sources

Business scenario / extension source:

Deferred ApplicantParty / client data verification during Employee request review.

Related scenario context:

SC-07A — Employee Request Details
SC-07B — Employee Request Review

Parent server slice:

SL-APPL-VER-001 — Run Mock ApplicantParty Verification From Employee Request Review

Behavior items:

VER-CMD-START-001 — Employee starts ApplicantParty verification in request/review context.
VER-UCQ-001 — Verification is not triggered by standalone ApplicantParty create/edit.
VER-STATE-001 — ApplicantParty owns persisted verification state.
VER-READ-001 — Employee dashboard/details/review surfaces show current ApplicantParty verification state.

Source note:

ApplicantParty owns verification state.

Employee request/review UI owns the action placement.

Client never chooses ApplicantParty directly.
0.1 Source / Domain / Slice Coverage Snapshot

Source versions:

SC-07A: current Employee request details scenario
SC-07B: current Employee request review scenario
ApplicantParty verification extension source: pending canonical source-sync

Server/domain baseline:

ApplicantParty.VerificationStatus:
  Unverified
  Verified

Mock command first pass:
  returns Passed
  persists Verified

Client baseline:

Current Employee request dashboard/details DTOs do not expose applicantVerification yet.

Current UI already has:
  EmployeeDashboardPage
  EmployeeRequestDashboardList
  EmployeeRequestDashboardRow
  EmployeeRequestDetailsPage
  EmployeeRequestDetailsView
  renderRowActions on dashboard list/row
  renderReviewActions on details view

Coverage snapshot:

Behavior item / provisional behavior	Server/domain disposition	This client sidecar responsibility	Notes
Employee sees verification state on dashboard/details	read DTO extension required	render status when applicantVerification exists	do not infer from unrelated fields
Employee starts verification from request context	server command by requestId	render run action and call feature mutation	no applicantPartyId in client request
Server resolves ApplicantParty	server uses request.ApplicantPartyId	client sends only requestId	prevents arbitrary verification
Mock result is visible	server returns 200 with result	show immediate success/error and refetch reads	no optimistic verified state
ApplicantParty becomes Verified	domain-owned	refetch dashboard/details after success	UI displays refreshed read model
Standalone ApplicantParty edit does not verify	out of sidecar	no wiring into ApplicantParty create/edit pages	request/review only
ApproveReview is not gated	review command unchanged	do not disable approve based on verification	later optional guard slice
0.2 Implementation Sync Status

Implementation status:

planned / blocked until:
  server endpoint exists;
  Employee request read DTOs expose applicantVerification;
  OpenAPI/types are regenerated.

Current implementation evidence:

client:
  EmployeeDashboardPage composes row actions with renderRowActions.
  EmployeeRequestDashboardRow renders request summary and actions area.
  EmployeeRequestDetailsPage composes review command actions.
  EmployeeRequestDetailsView renders applicant data, review state and review actions.

server/generated:
  EmployeeRequestListItemDto currently has no applicantVerification.
  EmployeeRequestDetailsDto currently has no applicantVerification.
  EmployeeRequestApplicantSummaryDto currently has no verification summary.

Known limitation:

Client implementation should not start until read DTO extension exists.

If applicantVerification is missing from DTO, UI should not guess:
  do not show run button;
  do not infer status from applicantPartyId or displayName.
1. Key Decision

Use request surfaces, not ApplicantParty standalone surfaces.

Dashboard:
  show compact verification badge in row;
  show run action through existing renderRowActions area when canRun=true.

Details:
  show full verification panel near applicant/review data;
  action belongs to verification feature.

Standalone ApplicantParty create/edit:
  no verification action.

Client sends:

requestId

Client never sends:

applicantPartyId
employeeId
target verification status
mock result
2. Required Read Model Contract

Preferred generated DTO direction:

type EmployeeRequestApplicantVerificationDto = {
  required: boolean;
  status: "NotRequired" | "Unverified" | "Verified";
  canRun: boolean;
  message?: string | null;
};

Add to dashboard row DTO:

type EmployeeRequestListItemDto = {
  requestId: number;
  requestType: string;
  status: string;
  applicantDisplayName: string;
  objectAddress: string;
  createdAt: string;
  reviewState: string;
  applicantVerification?: EmployeeRequestApplicantVerificationDto | null;
};

Add to details DTO:

type EmployeeRequestDetailsDto = {
  requestId: number;
  requestType: string;
  status: string;
  applicant: EmployeeRequestApplicantSummaryDto;
  objectAddress: string;
  details: string;
  createdAt: string;
  reviewState: string;
  applicantVerification?: EmployeeRequestApplicantVerificationDto | null;
};

Meaning:

required=false:
  show "Проверка не требуется";
  canRun=false.

status=Unverified:
  show "Данные не проверены";
  canRun=true only if server says so.

status=Verified:
  show "Данные проверены";
  canRun=false.

Important:

NotRequired is read-model state, not ApplicantParty persisted enum.

ApplicantParty persisted enum remains:
  Unverified / Verified
3. Scope

This client sidecar owns:

- compact verification state display on Employee request dashboard row;
- full verification panel on Employee request details page;
- run verification action from request/review context;
- feature-owned API wrapper for server command;
- feature-owned mutation hook;
- pending state while command is running;
- visible success feedback after mock verification result;
- visible error feedback when command is rejected;
- invalidation/refetch of Employee request details after success;
- invalidation/refetch of Employee request dashboard/list after success;
- stale-state refetch after command error;
- no ApplicantParty standalone verification page;
- no approve/reject gating;
- no real external verification UI;
- no random result handling.

Primary command:

POST /api/employee/requests/{requestId}/applicant-party/verification/run

Success:

200 OK RunApplicantPartyVerificationResponseDto
4. Out of Scope
- backend endpoint implementation -> SL-APPL-VER-001;
- Employee request read DTO implementation;
- ApplicantParty domain state expansion;
- Failed / Unavailable persisted states;
- verification history UI;
- external provider UI;
- ApplicantParty create/edit verification;
- blocking ApproveReview;
- changing StartReview / ApproveReview / RejectReview actions;
- changing Agreement Exchange actions;
- changing Employee dashboard/details base layout broadly;
- manual generated OpenAPI/type edits;
- decorative styling pass.
5. Related Slices / Owners
SL-APPL-VER-001
  Owns server command, mock service and ApplicantParty.MarkVerified() call.

L2-APPL-VER-RUN-001.client
  Owns Employee-facing verification panel/action.

SL-EMP-REQ-001 / L2-EMP-DASH-001.client
  Own Employee request dashboard/list read surface.
  Must expose applicantVerification for dashboard row.

SL-EMP-REQ-002 / L2-EMP-DETAILS-001.client
  Own Employee request details read surface.
  Must expose applicantVerification for details panel.

SL-EMP-REQ-003 / L2-REVIEW-START-001.client
  StartReview remains separate.

SL-EMP-REQ-004
  ApproveReview is not gated here.

SL-EMP-REQ-005
  RejectReview is not changed here.

ApplicantParty domain
  Owns persisted verification state.
6. Visual UI / Scenario Flow
[Employee Dashboard]
Employee sees request row
        ↓
Row shows compact verification status:
  Проверка не требуется / Данные не проверены / Данные проверены
        ↓
If canRun:
  row actions include "Проверить данные"
        ↓
Employee clicks action
        ↓
Command pending
        ↓
Command succeeds
        ↓
Dashboard/details queries refetch
        ↓
Row shows verified state


[Employee Request Details]
Employee opens request details
        ↓
Details page shows applicant data
        ↓
Verification panel shows current status and message
        ↓
If canRun:
  panel shows "Проверить данные"
        ↓
Employee clicks action
        ↓
Command pending
        ↓
Server returns mock result Passed
        ↓
Panel shows success message and refetches details/list
7. Current-code Integration Plan
Dashboard row

Current code already supports:

EmployeeRequestDashboardList.renderRowActions
EmployeeRequestDashboardRow.renderActions

Use that for command action.

Dashboard read-only status:

EmployeeRequestDashboardRow should render ApplicantVerificationStatusBadge
when request.applicantVerification exists.

Dashboard action:

EmployeeDashboardPage renderRowActions should compose:
  StartReviewButton
  RunApplicantPartyVerificationButton
depending on availability.

Direction:

renderRowActions={(request) => (
  <>
    {canStartReviewFromDashboardRow(request) ? (
      <StartReviewButton requestId={request.requestId} surface="dashboard" />
    ) : null}

    {request.applicantVerification?.canRun ? (
      <RunApplicantPartyVerificationButton
        requestId={request.requestId}
        surface="dashboard"
      />
    ) : null}
  </>
)}
Details page

Current EmployeeRequestDetailsView has only:

renderReviewActions

Better update details view with a separate verification slot:

type EmployeeRequestDetailsViewProps = {
  details: EmployeeRequestDetails;
  renderApplicantVerification?: (details: EmployeeRequestDetails) => ReactNode;
  renderReviewActions?: (details: EmployeeRequestDetails) => ReactNode;
};

Placement inside view:

<EmployeeApplicantReviewData details={details} />
{renderApplicantVerification?.(details)}
<EmployeeReviewStatePanel details={details} />
<EmployeeReviewActionAvailabilityPanel ... />

Reason:

Verification belongs near Applicant data and review context.
It should not be mixed into approve/reject/start command list only.

Details page usage:

<EmployeeRequestDetailsView
  details={detailsQuery.data}
  renderApplicantVerification={(details) =>
    details.applicantVerification ? (
      <ApplicantPartyVerificationPanel
        requestId={details.requestId}
        verification={details.applicantVerification}
        surface="details"
      />
    ) : null
  }
  renderReviewActions={(details) => (...existing actions...)}
/>
8. Visual Client Implementation Flow
Entity/request read type layer
employeeRequestTypes.ts
  add EmployeeRequestApplicantVerification type alias from generated DTO;
  extend EmployeeRequestDashboardItem with applicantVerification;
  extend EmployeeRequestDetails with applicantVerification.
Entity/read UI layer
ApplicantVerificationStatusBadge
  from: entities/employee-request/ui/ApplicantVerificationStatusBadge.tsx
  needed to: render compact read-only verification state.
  visual: small badge/text inside request row/details panel.

EmployeeRequestDashboardRow
  add compact badge if request.applicantVerification exists.

EmployeeRequestDetailsView
  add renderApplicantVerification slot after applicant data.
Feature API layer
runApplicantPartyVerification
  from: features/employee-request/applicant-verification/api/runApplicantPartyVerification.ts
  needed to: call server command by requestId.
Feature model layer
useRunApplicantPartyVerificationMutation
  from: features/employee-request/applicant-verification/model/useRunApplicantPartyVerificationMutation.ts
  needed to: own mutation, pending/error state and query invalidation.
Feature UI layer
ApplicantPartyVerificationPanel
  from: features/employee-request/applicant-verification/ui/ApplicantPartyVerificationPanel.tsx
  needed to: render details-page status, message and run action.
  visual: simple panel, not modal.

RunApplicantPartyVerificationButton
  from: features/employee-request/applicant-verification/ui/RunApplicantPartyVerificationButton.tsx
  needed to: submit command from dashboard/details.
  visual: small secondary button on dashboard, normal action button in details.
9. Client API / Server Contract

Endpoint:

POST /api/employee/requests/{requestId}/applicant-party/verification/run

Request body:

none

Response:

type RunApplicantPartyVerificationResponseDto = {
  requestId: number;
  applicantPartyId: number;
  verificationStatus: "Verified";
  mockResult: "Passed";
  message?: string | null;
};

Generated alias:

// features/employee-request/applicant-verification/api/applicantVerificationApiTypes.ts
import type { components } from "../../../../shared/api/generated/openapi-types";

export type RunApplicantPartyVerificationResponse =
  components["schemas"]["RunApplicantPartyVerificationResponseDto"];

export type EmployeeRequestApplicantVerification =
  components["schemas"]["EmployeeRequestApplicantVerificationDto"];

API wrapper:

// features/employee-request/applicant-verification/api/runApplicantPartyVerification.ts
import { fetchJson } from "../../../../shared/api/fetchJson";
import type { RunApplicantPartyVerificationResponse } from "./applicantVerificationApiTypes";

const runApplicantPartyVerificationPath = (requestId: number | string) =>
  `/api/employee/requests/${encodeURIComponent(String(requestId))}/applicant-party/verification/run`;

export const runApplicantPartyVerification = (
  requestId: number,
): Promise<RunApplicantPartyVerificationResponse> =>
  fetchJson<RunApplicantPartyVerificationResponse>(
    runApplicantPartyVerificationPath(requestId),
    { method: "POST" },
  );

Do not add:

shared/api/applicantVerificationApi.ts
entities/applicant-party/api/runVerification.ts
10. Mutation / Query Invalidation

Current StartReview invalidates:

employeeRequestQueryKeys.details(requestId)
employeeRequestQueryKeys.all

Use same direction.

Mutation:

export const useRunApplicantPartyVerificationMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<
    RunApplicantPartyVerificationResponse,
    unknown,
    number
  >({
    mutationFn: runApplicantPartyVerification,
    onSuccess: async (_data, requestId) => {
      await Promise.all([
        queryClient.invalidateQueries({
          queryKey: employeeRequestQueryKeys.details(requestId),
        }),
        queryClient.invalidateQueries({
          queryKey: employeeRequestQueryKeys.all,
        }),
      ]);
    },
    onError: async (_error, requestId) => {
      await Promise.all([
        queryClient.invalidateQueries({
          queryKey: employeeRequestQueryKeys.details(requestId),
        }),
        queryClient.invalidateQueries({
          queryKey: employeeRequestQueryKeys.all,
        }),
      ]);
    },
  });
};

Reason:

Even failed command can mean stale read state.
Follow current StartReview mutation pattern.
11. UI State Rules

Verification display:

required=false:
  label: "Проверка не требуется"
  action: hidden

status=Unverified:
  label: "Данные не проверены"
  action: shown only if canRun=true

status=Verified:
  label: "Данные проверены"
  action: hidden

Pending:

Button disabled.
Text: "Проверяем..."
No optimistic Verified state.

Success:

Show response.message if available.
Invalidate/refetch details/list.
Final visible state should come from refreshed DTO.

Error:

Show form/action error near panel/button.
Keep previous read state visible.
Do not pretend verification succeeded.

Missing DTO field:

If applicantVerification is absent:
  do not show action;
  do not guess status;
  do not infer from applicantPartyId.
12. Security / Protection

Client sends only:

requestId

Client never sends:

applicantPartyId
employeeId
target verification status
mock result
checkedAt
checkedByEmployeeId

Server remains authoritative:

Employee role
request context
request visibility
ApplicantParty resolution
ApplicantParty.MarkVerified()
13. Questions / Decisions
ID	Status	Question	Current direction
Q-L2-APPL-VER-CLIENT-001	blocked	Exact generated response DTO name?	Expected RunApplicantPartyVerificationResponseDto.
Q-L2-APPL-VER-CLIENT-002	blocked	Exact generated verification summary DTO name?	Preferred EmployeeRequestApplicantVerificationDto.
Q-L2-APPL-VER-CLIENT-003	accepted	Does client send applicantPartyId?	No. Sends requestId only.
Q-L2-APPL-VER-CLIENT-004	accepted	Does client send body?	No body first pass.
Q-L2-APPL-VER-CLIENT-005	accepted	Does success return result?	Yes, 200 with compact result.
Q-L2-APPL-VER-CLIENT-006	accepted	Does client optimistically mark verified?	No. Wait for server success/refetch.
Q-L2-APPL-VER-CLIENT-007	accepted	Where does dashboard action wire?	Existing renderRowActions.
Q-L2-APPL-VER-CLIENT-008	accepted	Where does details panel wire?	Add dedicated renderApplicantVerification slot.
Q-L2-APPL-VER-CLIENT-009	accepted	Is ApproveReview gated?	No.
Q-L2-APPL-VER-CLIENT-010	future	Failed / Unavailable UI?	Only after domain/read model extension.
14. Behavior Coverage
Behavior	How client sidecar covers it
Employee sees verification state on dashboard	dashboard row renders compact badge from request.applicantVerification.
Employee sees verification state on request details	details page renders verification panel from details.applicantVerification.
Employee can run verification from request context	run button calls command with requestId.
Client cannot choose ApplicantParty	no applicantPartyId is sent.
Mock result is visible	response message/result shown and reads refetched.
ApplicantParty becomes Verified	refetch shows Verified.
NotRequired is visible	read DTO maps required=false to visible state.
Standalone ApplicantParty edit does not verify	no wiring outside request/review surfaces.
ApproveReview is not gated	approve/reject actions unchanged.
Missing verification DTO	action hidden / feature blocked.
15. Test / Verification Plan

Component tests:

- ApplicantVerificationStatusBadge renders NotRequired.
- ApplicantVerificationStatusBadge renders Unverified.
- ApplicantVerificationStatusBadge renders Verified.
- EmployeeRequestDashboardRow shows badge when applicantVerification exists.
- EmployeeRequestDashboardRow does not guess state when missing.
- RunApplicantPartyVerificationButton calls mutation with requestId.
- Button disables while pending.
- Button shows error on failure.
- ApplicantPartyVerificationPanel shows status/message/action.
- Panel hides action when required=false.
- Panel hides action when status=Verified.
- Panel shows pending and error states.
- EmployeeRequestDetailsView renders renderApplicantVerification slot near applicant data.

API/model tests:

- runApplicantPartyVerification posts to:
  /api/employee/requests/{requestId}/applicant-party/verification/run

- wrapper sends no body;
- wrapper returns RunApplicantPartyVerificationResponse;
- mutation invalidates employeeRequestQueryKeys.details(requestId);
- mutation invalidates employeeRequestQueryKeys.all;
- no shared/api business wrapper exists.

E2E planned:

Employee session:
  open Employee request details
  applicant verification state is Unverified
  click "Проверить данные"
  assert mock result/success feedback visible
  assert panel shows "Данные проверены" after refetch

Non-goals:

- no Client verification flow;
- no ApplicantParty standalone verification;
- no approve blocking test here;
- no Failed/Unavailable UI until domain/read model supports it;
- no React Query cache internals in E2E.
16. Suggested File Placement
src/features/employee-request/applicant-verification/api/
  runApplicantPartyVerification.ts
  applicantVerificationApiTypes.ts

src/features/employee-request/applicant-verification/model/
  useRunApplicantPartyVerificationMutation.ts

src/features/employee-request/applicant-verification/ui/
  ApplicantPartyVerificationPanel.tsx
  RunApplicantPartyVerificationButton.tsx
  applicantPartyVerification.css
  applicantPartyVerificationConst.ts

src/entities/employee-request/ui/
  ApplicantVerificationStatusBadge.tsx

src/entities/employee-request/model/
  employeeRequestTypes.ts
  employeeRequestQueryKeys.ts

src/entities/employee-request/ui/
  EmployeeRequestDashboardRow.tsx
  EmployeeRequestDetailsView.tsx

src/pages/employee/requests/dashboard/
  EmployeeDashboardPage.tsx

src/pages/employee/requests/details/
  EmployeeRequestDetailsPage.tsx

Do not add:

src/shared/api/applicantVerificationApi.ts
src/entities/applicant-party/api/runVerification.ts
ApplicantParty create/edit verification button
17. CSS Ownership

Entity CSS owns:

ApplicantVerificationStatusBadge read-only display
dashboard row placement of verification status

Feature CSS owns:

Run button
panel layout
pending/error/success feedback

Page CSS owns:

where the panel appears on details page only if needed
spacing between page sections

Do not:

- do not use broad global selectors;
- do not make page CSS reach into feature internals;
- do not add decorative styling pass;
- do not imitate public portal screenshots in this slice.
18. Implementation Checklist
[ ] confirm SL-APPL-VER-001 backend endpoint exists
[ ] confirm Employee request dashboard/details DTOs expose applicantVerification
[ ] confirm generated response DTO name
[ ] confirm generated verification summary DTO name
[ ] add generated aliases near feature/entity
[ ] add runApplicantPartyVerification API wrapper
[ ] send requestId only
[ ] send no request body
[ ] add useRunApplicantPartyVerificationMutation
[ ] invalidate employeeRequestQueryKeys.details(requestId) on success/error
[ ] invalidate employeeRequestQueryKeys.all on success/error
[ ] add ApplicantVerificationStatusBadge in entity UI
[ ] render badge in EmployeeRequestDashboardRow when DTO exists
[ ] add RunApplicantPartyVerificationButton
[ ] add ApplicantPartyVerificationPanel
[ ] add renderApplicantVerification slot to EmployeeRequestDetailsView
[ ] wire panel in EmployeeRequestDetailsPage
[ ] wire run action in EmployeeDashboardPage renderRowActions
[ ] do not wire into ApplicantParty standalone forms
[ ] do not disable ApproveReview based on verification
[ ] show NotRequired when read DTO says required=false
[ ] show Unverified/Verified states
[ ] show pending/error/success feedback
[ ] add component tests
[ ] add API/model tests
[ ] add E2E when backend/test setup is ready
19. Guardrail Summary
This is an Employee request/review client sidecar.

It displays ApplicantParty verification state from read DTOs.

It runs mock verification by requestId.

It does not send applicantPartyId.

It does not send employeeId.

It does not send target status.

It does not guess state if applicantVerification is missing.

It does not verify from standalone ApplicantParty create/edit.

It does not change ApproveReview behavior.

It does not block approval.

It waits for server success/refetch before showing Verified.

It keeps read-only badge in employee-request entity UI.

It keeps command button/panel in feature UI.

It keeps CSS minimal and owner-scoped.

It uses feature-owned API wrapper, not shared/api business wrapper.

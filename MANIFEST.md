# MANIFEST — L1 Backend Slice Documentation Status Reconciliation

Archive: `enman-l1-backend-slice-docs-status-reconciliation.zip`  
Repository: `AlexPastukhh/enman`  
Branch target: `my-changes`  
Scope: documentation-only replacement package for implemented backend L1 slice docs.

## Replace

| Path | Purpose |
|---|---|
| `planning/slices/SL-ACC-001-register-client-account.md` | Reconciles register-client-account slice with current backend API/DTO/handler/test evidence; corrects current request DTO to email + password only; keeps UI/password confirmation/PendingActivation/DB uniqueness as out of scope. |
| `planning/slices/SL-APPL-001-create-individual-applicant-party.md` | Reconciles applicant-party slice with current protected endpoint, server-derived account context, domain state and integration/domain tests; keeps UI/replacement/verification/future applicant types separate. |
| `planning/slices/SL-REQ-001-create-connection-request.md` | Reconciles request-creation slice with current endpoint, server-selected applicant context, no required response body and test coverage; keeps client UI, My Requests read slice and CSRF rollout separate. |

## Add

None.

## Delete

None.

## Deliberately Not Included

```text
- backend production code changes;
- runtime behavior changes;
- generated artifacts;
- client sidecars;
- My Requests page/read UI;
- request creation UI;
- CSRF implementation;
- DB uniqueness constraints;
- GitHub branch/commit/PR files.
```

## Evidence Checked

```text
planning/README.md
planning/planning-agent-protocol.md
planning/planning-doc-responsibility-map.md
planning/slices/README.md
planning/slices/draft-driven-discovery-principles.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/slices/examples/README.md
planning/slices/examples/L1-CONNECTION-REQUEST-CREATE-early-short-draft-example.md
planning/slices/examples/SL-ACC-001-register-client-account-full-slice-example.md
planning/client/cross-cutting/CL-COMMAND-001-command-success-without-required-response-body.md
planning/documentation/documentation-update-workflow.md
planning/documentation/status-reconciliation-workflow.md
planning/replacement-file-generation-guide.md
EnergyManagement.Server/L1/Controllers/L1Controller.cs
EnergyManagement.Server/L1/Api/L1Dtos.cs
EnergyManagement.Server/L1/Application/Commands/L1Commands.cs
EnergyManagement.Server/L1/Application/Commands/L1RegisterClientAccountHandler.cs
EnergyManagement.Server/L1/Application/Commands/L1CreateIndividualApplicantPartyHandler.cs
EnergyManagement.Server/L1/Application/Commands/L1CreateConnectionRequestHandler.cs
Domain.EnergyManagement/L1/Accounts/Account.cs
Domain.EnergyManagement/L1/Accounts/ClientAccount.cs
Domain.EnergyManagement/L1/Applicants/ApplicantParty.cs
Domain.EnergyManagement/L1/Applicants/IndividualApplicantParty.cs
Domain.EnergyManagement/L1/Requests/ClientRequest.cs
Domain.EnergyManagement/L1/Requests/ConnectionRequest.cs
Tests.EnergyManagement/Integration/L1/L1SliceIntegrationTests.cs
Tests.EnergyManagement/Domain/Accounts/ClientAccountTests.cs
Tests.EnergyManagement/Domain/Applicants/IndividualApplicantPartyTests.cs
Tests.EnergyManagement/Domain/Requests/ConnectionRequestCreationTests.cs
```

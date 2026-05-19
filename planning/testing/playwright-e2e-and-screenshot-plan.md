# Playwright E2E and VKR Screenshot Plan

Status: planned / implementation support added

## Purpose

Playwright is used in two separate modes:

1. Regression E2E tests for cross-layer user flows.
2. VKR/thesis screenshot runner for reproducible PNG assets.

The screenshot runner is not a visual regression baseline and does not use `toHaveScreenshot`.

## Current E2E Coverage

Existing E2E coverage includes:

- client registration and login;
- account ApplicantParty creation/read/default selection;
- connection request creation;
- client request list/filter/details.

This pass adds test support for:

- seeded Employee account and deterministic demo requests;
- Employee request dashboard/details review flow;
- ApplicantParty mock verification from Employee details;
- Agreement Exchange start from Employee flow;
- Client Agreement Exchange list/details;
- document download through Playwright download event;
- Client counter-proposal upload/send.

## Demo Data

The command below seeds deterministic E2E data after the test database has been reset:

```powershell
dotnet run --project EnergyManagement.Tools -- seed-e2e-demo-data
```

Seeded accounts:

```text
Employee: e2e.employee@example.com / ValidPassword111!
Client:   e2e.client@example.com / ValidPassword111!
```

Seeded requests:

```text
9004 — InReview request for Employee review scenario
9005 — Approved request for Agreement Exchange scenario
```

This command is test tooling only. It does not change production runtime behavior.

## VKR Screenshot Runner

Screenshots are written to:

```text
planning/thesis/assets/screenshots/
```

Run:

```powershell
npm.cmd run screenshots:vkr
```

Generated screenshot filenames:

```text
01-register-client.png
02-login-client.png
03-account-applicant.png
04-create-request.png
05-my-requests-list.png
06-my-request-details.png
07-employee-request-dashboard.png
08-employee-request-details-review-actions.png
09-agreement-exchange-list.png
```

Agreement Exchange details screenshots can be added after the full agreement exchange E2E flow is stable in local/CI.

## Stability Rules

- Use deterministic demo data only.
- Do not use real personal data.
- Use fixed Playwright viewport from `playwright.config.ts`.
- Wait for stable headings or successful API responses before screenshots.
- Keep screenshots separate from visual regression snapshots.
- Do not use arbitrary sleeps/timeouts.


## Screenshot runner isolation

Normal E2E scripts exclude tests tagged with `@screenshots` using `--grep-invert @screenshots`. The VKR screenshot script runs only the screenshot suite with `--grep @screenshots`, so documentation PNG generation does not run as part of the normal regression suite.

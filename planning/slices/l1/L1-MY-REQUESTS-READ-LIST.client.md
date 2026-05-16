# L1-MY-REQUESTS-READ-LIST.client — My Requests List Client Sidecar

Status: implemented-by-replacement-archive draft  
Slice type: client read sidecar  
Scope: authenticated client opens My Requests page and sees own request list  
Backend source: `GET /api/l1/requests`  
Out of scope: status filtering UI, request details page, request creation UI

## Behavior

```text
Authenticated client opens My Requests page
        ↓
Client UI fetches own requests
        ↓
System derives current account from L1 auth session
        ↓
System returns requests owned by this account
        ↓
Client sees list, empty state, loading state, or error state
```

Each request summary shows:

```text
- status;
- request type;
- created date;
- summary;
- object address.
```

## Implementation Notes

```text
Page owns route state.
Entity owns data loading.
Feature UI owns list/card rendering.
```

Status filtering is intentionally not implemented in this slice. Future filter slice should keep URL query state on the page and pass it into the entity query.

## Files

```text
src/shared/api/l1RequestApi.ts
src/entities/request/**
src/features/request/my-requests-list/**
src/pages/requests/my/MyRequestsPage.tsx
```

## Verification

```text
npm.cmd --prefix energymanagement.client run build
npm.cmd --prefix energymanagement.client run test
npm.cmd run check:api
npm.cmd run test:e2e -- --list
npm.cmd run test:e2e
```

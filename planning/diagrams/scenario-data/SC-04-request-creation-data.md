# SC-04 — Client Request Creation DATA

## DATA Blocks

### SC-04-DATA-01 — Request creation DATA

Type: Input DATA / Visible DATA / Reference DATA  
Actor: Client  
Used by: Request creation form and accepted submit outcome

Input DATA:

```text
- requested service / request subject information;
- request details/description;
- object address.
```

Reference / Visible DATA during request creation:

```text
- current active ApplicantParty summary for the account;
- applicant type;
- applicant display name;
- applicant contact summary.
```

Visible DATA after accepted submit:

```text
- request status = InReview;
- request appears in My Requests;
- request appears in employee review queue.
```

Target / Future DATA for connection/agreement realism:

```text
[VAR:EXPAND]
- requested maximum power, kW;
- data prefilled from rejected request feedback;
- document references if document flow becomes part of request creation.
```

### SC-04-DATA-02 — Applicant DATA use in request creation

Type: Reference DATA / Visible DATA  
Actor: Client  
Used by: applicant section/summary of request creation form

Reference DATA:

```text
- current active ApplicantParty for the account.
```

Visible DATA:

```text
- applicant type;
- applicant display name;
- applicant contact summary;
- link/action to provide or update applicant data before submit, if needed.
```

Future DATA / UX:

```text
[VAR:EXPAND]
- inline shortcut to update applicant data before submit;
- return-to-request flow after accepted applicant data update;
- applicant snapshot indicator if historical request snapshot policy is introduced.
```

Notes:

```text
ObjectAddress is already part of the current request model.
Request object location means object address.
RequestedPowerKw is agreement/connection-relevant, but currently planned as expansion rather than current implemented L1.
Request creation references current active applicant data instead of defining separate request-local applicant identity.
```

Open questions:

```text
Q: Is requested service type a fixed list or free description?
Q: When should RequestedPowerKw become core scenario DATA?
Q: Should historical requests store applicant snapshot later?
```

Accepted direction:

```text
Request creation uses the account's current active ApplicantParty.
Applicant data changes belong to SC-10 Applicant Data / future replacement flow.
```

Scenario spec references:

```text
planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md
```

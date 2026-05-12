# SC-04 — Client Request Creation DATA

## DATA Blocks

### SC-04-DATA-01 — Request creation DATA

Type: Input DATA / Visible DATA  
Actor: Client  
Used by: Request creation form and accepted submit outcome

Input DATA:

```text
- requested service / request subject information;
- request details/description;
- object address;
- applicant DATA copied/prefilled from previously provided matching applicant DATA, or entered inline.
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

Type: Input DATA / Reference DATA  
Actor: Client  
Used by: applicant section of request creation form

Reference DATA:

```text
- previously saved applicant DATA, if applicant type matches request needs.
```

Input DATA:

```text
- inline applicant DATA when no matching saved applicant DATA exists;
- edited applicant DATA when client wants different data for this request.
```

Future DATA / UX:

```text
[VAR:EXPAND]
- clear prefilled applicant DATA action;
- restore prefilled applicant DATA action.
```

Notes:

```text
ObjectAddress is already part of the current request model.
Request object location means object address.
RequestedPowerKw is agreement/connection-relevant, but currently planned as expansion rather than current implemented L1.
Clearing or editing prefilled applicant DATA in request creation does not delete saved applicant DATA.
```

Open questions:

```text
Q: Is requested service type a fixed list or free description?
Q: Is applicant DATA always required before submit?
Q: When should RequestedPowerKw become core scenario DATA?
```

Scenario spec references:

```text
planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md
```

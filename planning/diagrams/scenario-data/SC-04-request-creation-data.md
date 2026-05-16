# SC-04 — Client Request Creation DATA

Status: current DATA spec / synchronized with applicant-data-in-request-journey UI direction  
Scope: data entered/seen/referenced during request creation

DATA means what the actor enters, sees, selects, filters, attaches or references.

Validation, invariants, access rules and implementation mechanics do not belong in this file.

## 1. DATA Blocks

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

Visible DATA during request creation:

```text
- applicant data section/fields;
- current active ApplicantParty data, when it exists;
- request form state and validation feedback;
- request submit availability.
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

### SC-04-DATA-02 — Applicant DATA in request creation journey

Type: Input DATA / Visible DATA / Reference DATA  
Actor: Client  
Used by: applicant section of request creation form before request submit

Reference DATA:

```text
- current active ApplicantParty for the account, if it exists.
```

Visible / Input DATA:

```text
- applicant type, when visible/applicable;
- applicant display/full name;
- applicant contact email;
- applicant contact phone;
- clear/reset action for prefilled applicant data;
- applicant data entered after clearing or when no current applicant exists.
```

Current narrow implemented L1 applicant fields:

```text
- full name;
- email;
- phone number.
```

Prefill / clear behavior:

```text
- if current active applicant data exists, applicant fields are prefilled from it;
- client can clear the fields and enter new applicant data;
- accepted new applicant data becomes the account-level current active ApplicantParty through SC-10 / applicant replacement behavior;
- request submission uses the current active ApplicantParty at submit time.
```

Future DATA / UX:

```text
[VAR:EXPAND]
- inline shortcut to full applicant edit/replacement flow;
- return-to-request flow after accepted applicant data update;
- applicant snapshot indicator if historical request snapshot policy is introduced;
- richer applicant type-specific data for entrepreneur/legal entity.
```

## 2. Notes

```text
ObjectAddress is already part of the current request model.
Request object location means object address.
RequestedPowerKw is agreement/connection-relevant, but currently planned as expansion rather than current implemented L1.
Request creation references current active applicant data instead of defining separate request-local applicant identity.
```

## 3. Questions

```text
Q: Is requested service type a fixed list or free description?
Q: When should RequestedPowerKw become core scenario DATA?
Q: Should historical requests store applicant snapshot later?
Q: Does inline applicant data replacement reuse SC-10 save behavior or need a dedicated replacement endpoint?
```

## 4. Accepted Direction

```text
Request creation uses the account's current active ApplicantParty.
Applicant data fields may be visible/editable in the request journey.
Prefilled applicant data can be cleared and replaced before submit.
Accepted applicant data changes belong to SC-10 Applicant Data / future replacement flow, not hidden request-local mutation.
```

## 5. Scenario Spec References

```text
planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md
planning/diagrams/scenario-ui-specs/SC-04-request-creation-ui.md
planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md
```

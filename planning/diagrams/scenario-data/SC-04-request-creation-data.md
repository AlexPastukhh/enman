# SC-04 — Client Request Creation DATA

Status: current DATA spec / synchronized with per-type ApplicantParty template direction  
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
- request form state and validation feedback;
- request submit availability;
- applicant data section/fields;
- current/default ApplicantParty template for selected applicant type, when it exists;
- future selectable saved ApplicantParty list/dropdown.
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

Type: Input DATA / Visible DATA / Selection DATA / Reference DATA  
Actor: Client  
Used by: applicant section of request creation form before request submit

Reference DATA:

```text
- current/default ApplicantParty template for selected applicant type, if it exists;
- saved ApplicantParty selected for this request;
- future all-saved ApplicantParty dropdown/list.
```

Visible / Input DATA:

```text
- applicant type;
- applicant display/full name;
- applicant contact email;
- applicant contact phone;
- clear/reset action for prefilled applicant data;
- applicant data entered after clearing or when no current/default template exists;
- offer/choice to make newly created ApplicantParty current/default template for its type.
```

Current narrow implemented L1 applicant fields:

```text
- full name;
- email;
- phone number.
```

Prefill / clear / missing behavior:

```text
- if current/default applicant template exists for selected applicant type, applicant fields are prefilled from it;
- client can keep prefilled data and use that existing ApplicantParty for the request;
- client can clear fields and enter new applicant data;
- if current/default applicant template is missing, fields are empty and no clear action is needed;
- accepted new applicant data creates a new ApplicantParty and uses it for the request;
- after creating a new ApplicantParty, UI offers to make it current/default template for that type;
- creating new ApplicantParty does not overwrite old ApplicantParties.
```

Future DATA / UX:

```text
[VAR:EXPAND]
- dropdown/list of all saved ApplicantParties;
- inline shortcut to full applicant edit/manage flow;
- return-to-request flow after applicant management;
- applicant snapshot/version indicator if historical request snapshot policy is introduced;
- richer applicant type-specific data for entrepreneur/legal entity.
```

## 2. Notes

```text
ObjectAddress is already part of the current request model.
Request object location means object address.
RequestedPowerKw is agreement/connection-relevant, but currently planned as expansion rather than current implemented L1.
Request creation uses one selected/new ApplicantParty context.
Current/default ApplicantParty is a prefill/template concept for future request creation.
```

## 3. Questions

```text
Q: Is requested service type a fixed list or free description?
Q: When should RequestedPowerKw become core scenario DATA?
Q: Should historical requests store applicant snapshot later?
Q: Should new ApplicantParty become current/default automatically when no current/default exists for its type?
Q: What exact future dropdown/list behavior should be used for selecting from all ApplicantParties?
```

## 4. Accepted Direction

```text
Request creation uses one account-owned ApplicantParty context.
Current/default ApplicantParty per type provides initial prefill/default selection.
If no current/default ApplicantParty exists, applicant fields start empty.
If user clears prefilled fields or enters new applicant data, accepted data creates a new ApplicantParty.
The newly created ApplicantParty is used for the request and offered as current/default template for its type.
Existing ApplicantParties remain stored and unchanged.
```

## 5. Scenario Spec References

```text
planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md
planning/diagrams/scenario-ui-specs/SC-04-request-creation-ui.md
planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md
```

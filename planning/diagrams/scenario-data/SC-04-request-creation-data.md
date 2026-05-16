# SC-04 — Client Request Creation DATA

Status: target DATA spec / synchronized with ApplicantParty template-per-type model  
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

Visible DATA after accepted submit:

```text
- request status = InReview;
- request appears in My Requests;
- request appears in employee review queue.
```

Target / Future DATA:

```text
[VAR:EXPAND]
- requested maximum power, kW;
- data prefilled from rejected request feedback;
- document references if document flow becomes part of request creation.
```

### SC-04-DATA-02 — Applicant DATA in request creation journey

Type: Input DATA / Visible DATA / Reference DATA / Selection DATA  
Actor: Client  
Used by: applicant section of request creation form before request submit

Reference DATA:

```text
- current/default ApplicantParty for selected applicant type, if it exists;
- selected existing ApplicantParty, when future saved-applicant dropdown/list is introduced;
- newly created ApplicantParty from entered applicant data.
```

Visible / Input DATA:

```text
- applicant type;
- current/default prefilled applicant fields, when available;
- applicant display/full name;
- applicant contact email;
- applicant contact phone;
- clear/reset action for prefilled applicant fields;
- new applicant data entered after clearing or when no default exists;
- optional prompt/action to make newly created ApplicantParty current/default for future requests when a default already exists.
```

Current narrow implemented L1 applicant fields:

```text
- full name;
- email;
- phone number.
```

Prefill / clear behavior:

```text
- if current/default ApplicantParty exists for selected type, applicant fields are prefilled from it;
- client can keep prefilled data;
- client can clear the fields and enter new applicant data;
- if current/default is missing, fields are empty and ready for new applicant data;
- accepted new applicant data creates a new ApplicantParty and uses it for this request;
- first ApplicantParty of the type may initialize the current/default template;
- additional ApplicantParty of the same type does not silently replace current/default.
```

Future DATA / UX:

```text
[VAR:EXPAND]
- dropdown/list to choose from all saved ApplicantParties;
- current/default template remains initial prefill/default selection;
- return-to-request flow after explicit default-template selection;
- applicant snapshot indicator if historical request snapshot policy is introduced;
- richer applicant type-specific data for entrepreneur/legal entity.
```

## 2. Notes

```text
ObjectAddress is already part of the current request model.
Request object location means object address.
RequestedPowerKw is agreement/connection-relevant, but currently planned as expansion rather than current implemented L1.
Request creation uses one accepted applicant context.
Do not model new applicant data as hidden replacement of an existing ApplicantParty.
```

## 3. Questions

```text
Q: Is requested service type a fixed list or free description?
Q: When should RequestedPowerKw become core scenario DATA?
Q: Should historical requests store applicant snapshot, ApplicantParty reference, or both?
Q: What exact applicantContextType shape should the target request creation API use?
Q: When should saved ApplicantParty dropdown/list be introduced?
```

## 4. Accepted Direction

```text
Request creation can prefill applicant data from current/default ApplicantParty for selected type.
Missing default and cleared prefill both lead to entering new applicant data.
New applicant data creates a new ApplicantParty and uses it for the request.
Creating new ApplicantParty does not overwrite existing ApplicantParties.
Request creation with new applicant data should be atomic in one server call.
```

## 5. Scenario Spec References

```text
planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md
planning/diagrams/scenario-ui-specs/SC-04-request-creation-ui.md
planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md
```

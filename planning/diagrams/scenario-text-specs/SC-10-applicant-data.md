# SC-10 — Applicant Data

## Status

Corrected scenario specification draft / applicant template per type policy synchronized.

## Purpose

Client provides applicant data that can be used as a saved ApplicantParty profile and later used in request creation.

Target scenario direction:

```text
A client account may store multiple ApplicantParty profiles over time.

For request creation convenience, the account may have one current/default ApplicantParty template per applicant type.

Adding a new ApplicantParty does not overwrite, delete or deactivate older ApplicantParties.

Changing the current/default template affects future prefill behavior only.
Existing requests keep the applicant context they were created with.
```

Current implementation note:

```text
Current L1 implementation is narrower than this target scenario direction.
It currently supports individual applicant creation/current read behavior.
Do not overclaim that multi-type templates, all applicant management or request-time applicant creation are already implemented.
```

## Actor / Screen

Actor: Client  
Screen: Account page / Applicant Data section  
Future screen: My Applicant Parties management area  
Goal: Provide, view or maintain applicant data used by request creation

## Entry Points

Entry A: Client opens Account page applicant section.

Entry B: Client provides applicant data while preparing a request.

Entry C [future]: Client opens My Applicant Parties to view, add, edit, delete/archive or set current/default ApplicantParty profiles.

Entry D [future]: Registration may collect part of applicant data.

## Preconditions

- Client is signed in.
- Applicant data section or request creation applicant section is reachable.

## DATA

Applicant type selection DATA  
`SC-10-DATA-01`

Selection DATA:

```text
- physical person;
- individual entrepreneur;
- legal entity.
```

Physical person applicant DATA  
`SC-10-DATA-02`

Input DATA:

```text
- ФИО;
- СНИЛС;
- паспортные данные;
- phone;
- email.
```

Extension / Future DATA:

```text
[VAR:EXPAND]
- actual/residential address.
```

Individual entrepreneur applicant DATA  
`SC-10-DATA-03`

Input DATA:

```text
- ФИО ИП;
- ИНН;
- ОГРНИП;
- phone;
- email.
```

Extension / Future DATA:

```text
[VAR:EXPAND]
- registration address;
- additional ЕГРИП record details if later needed.
```

Legal entity applicant DATA  
`SC-10-DATA-04`

Input DATA:

```text
- organization name;
- ИНН;
- ОГРН;
- phone;
- email.
```

Extension / Future DATA:

```text
[VAR:EXPAND]
- КПП;
- legal address;
- representative / signer;
- representative authority basis.
```

Saved applicant visible DATA  
`SC-10-DATA-05`

Visible DATA:

```text
- applicant type;
- applicant display name;
- applicant contact summary;
- applicant identifiers relevant for selected type;
- current/default marker for its applicant type, when set;
- verification state, when available.
```

## Main Flow — Account Applicant Template

1. Client opens Account page applicant section.
2. System shows current/default ApplicantParty template per supported applicant type, when one exists.
3. If a current/default template for a type is missing, the UI can show an empty form or an add action for that type.
4. Client enters applicant data for a selected applicant type.
5. Client saves applicant data.
6. System creates a new ApplicantParty profile.
7. New ApplicantParty starts as `NotVerified`.
8. UI offers to make the new ApplicantParty the current/default template for that applicant type.
9. Existing ApplicantParties remain stored and are not overwritten.

## Branches

### Physical person applicant

-> applicant type = physical person  
-> client enters physical person applicant data  
-> saved applicant display uses ФИО  
-> profile may become current/default physical person template

### Individual entrepreneur applicant

-> applicant type = individual entrepreneur  
-> client enters ИП applicant data  
-> saved applicant display uses ФИО ИП  
-> profile may become current/default individual entrepreneur template

### Legal entity applicant

-> applicant type = legal entity  
-> client enters legal entity applicant data  
-> saved applicant display uses organization name  
-> profile may become current/default legal entity template

### Applicant data invalid

-> validation errors are visible  
-> client corrects applicant data  
-> invalid applicant data is not saved

### Applicant data saved

-> applicant data accepted  
-> new ApplicantParty profile is created  
-> status = NotVerified  
-> profile is visible as saved applicant data  
-> UI can offer to make it the current/default template for its applicant type

### Current/default template changed

-> client chooses or confirms a saved ApplicantParty as current/default for its type  
-> future request creation prefill uses the selected current/default template  
-> older ApplicantParties remain stored  
-> existing requests keep their submitted applicant context

### Future My Applicant Parties management

-> client opens My Applicant Parties  
-> client can view list/details of saved ApplicantParties  
-> client can add ApplicantParty of any supported type  
-> client can edit or delete/archive when allowed by safety rules  
-> client can choose current/default template per applicant type

## Invariants

Invalid applicant data is not saved.

Adding a new ApplicantParty does not overwrite existing ApplicantParties.

At most one ApplicantParty per applicant type can be current/default for future prefill.

Current/default status is a template/prefill concept for future requests, not a historical rewrite of previous requests.

A request must preserve the applicant context used at submission time through stable reference or future snapshot/version policy.

Standalone applicant data creation does not verify ApplicantParty.

Verification happens in request/review context.

## Step Postconditions

- Applicant data is saved only after accepted applicant data.
- A saved ApplicantParty starts as `NotVerified`.
- A saved ApplicantParty can be used by request creation.
- A saved ApplicantParty may become current/default for its type when the user confirms or when target policy says it should be selected.
- Existing ApplicantParties remain available for history/future management unless explicit delete/archive rules apply.

## Outcomes

- Client can provide applicant data for physical person, individual entrepreneur or legal entity.
- Client can have saved ApplicantParty profiles over time.
- Client can have a current/default template per applicant type.
- Request creation can prefill applicant data from the relevant current/default template.
- Request creation can create a new ApplicantParty when new applicant data is entered.
- Applicant verification remains tied to request/review context.

## ADR / Policy Candidates

ADR?: Request should preserve submitted applicant context through immutable ApplicantParty version, snapshot, or stable reference policy.

ADR?: Physical person passport data and address are richer than current implemented L1. Current code may keep a narrower first implementation while specs describe target scenario DATA.

ADR?: Verification/check is available only in request/review context, not from standalone applicant editing.

ADR?: ApplicantParty deletion may be archive/hide rather than hard delete when requests or verification history exist.

## Questions / Decisions

Accepted direction:

```text
Decision:
An account can have multiple saved ApplicantParty profiles.

Reason:
Requests must preserve the applicant context used at submission time, and users may later enter different applicant data without destroying historical context.
```

```text
Decision:
Current/default ApplicantParty is scoped by applicant type.

Reason:
Physical person, individual entrepreneur and legal entity are different applicant data shapes.
Each type may have its own prefill template for future request creation.
```

```text
Decision:
Adding new ApplicantParty data does not replace/delete existing ApplicantParties.

Reason:
Existing requests and verification history can depend on older applicant data.
```

```text
Decision:
New ApplicantParty starts as NotVerified.

Reason:
Applicant verification happens during request/review context, not during standalone applicant data entry.
```

Open / future-review questions:

```text
Q: Should new ApplicantParty become current/default automatically when there is no current/default template for that type, or should the UI still ask?
Q: Should editing an ApplicantParty mutate it in place or create a new version when it has already been used by requests?
Q: Should deleting ApplicantParty mean hard delete, archive, hide, or deactivate?
Q: What warning should be shown when deleting an ApplicantParty that is used by requests or approved requests?
Q: Should request details show ApplicantParty snapshot, current stored profile data, or both?
Q: When should physical person actual/residential address become current target scenario DATA?
```

Superseded direction:

```text
Superseded:
One current active ApplicantParty per account.

Superseded:
Future applicant replacement makes the newly accepted ApplicantParty current active and the previous ApplicantParty non-current globally.
```

## Diagram Notes

- Do not label this only as Create ApplicantParty.
- Use user-facing wording: Provide applicant data / Applicant templates / Saved applicant profiles.
- Show applicant type as a selection/branch.
- Show current/default as one per applicant type, not one global account profile.
- Show older ApplicantParties as stored/history-preserving, not overwritten.
- Use DATA side blocks for type-specific fields.

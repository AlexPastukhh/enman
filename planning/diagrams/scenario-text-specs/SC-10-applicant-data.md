# SC-10 — Applicant Data

Status: target scenario specification / synchronized with ApplicantParty template-per-type model  
Source family: scenario text + DATA + UI scenario + behavior items  
Related scenarios: `SC-04 Client Request Creation`, `SC-10B My Applicant Parties`

## 1. Purpose

Client provides applicant data and creates saved ApplicantParty profiles for the account.

Target model:

```text
A client account may store many ApplicantParty profiles over time.

For convenience, the account may have one current/default ApplicantParty template per applicant type:
- physical person;
- individual entrepreneur;
- legal entity.

Current/default template means initial prefill/default selection for future request creation.

Creating a new ApplicantParty does not delete, overwrite, deactivate or replace existing ApplicantParties.

Changing current/default affects future prefill behavior only.
Existing requests keep their submitted applicant context.
```

Current implementation note:

```text
Current implemented L1 is narrower than the target scenario model.
It supports individual applicant creation and still has older current-active implementation concepts.
Do not describe the target multi-profile/template-per-type direction as already fully implemented.
```

## 2. Actor / Screen

Actor: Client  
Screen: Account page / Applicant Parties section  
Goal: Add and view applicant data used for future request creation

## 3. Entry Points

Entry A: Client opens Account page and uses Applicant Parties section.

Entry B: Client starts request creation and needs applicant data.

Entry C: Client enters new applicant data during request creation; the created ApplicantParty is used for that request.

Entry D [future]: Client opens a dedicated My Applicant Parties management area.

## 4. Preconditions

- Client is signed in.
- Applicant Parties section is reachable from account context.

## 5. DATA

Applicant type selection DATA: `SC-10-DATA-01`.

Physical person applicant DATA: `SC-10-DATA-02`.

Individual entrepreneur applicant DATA: `SC-10-DATA-03`.

Legal entity applicant DATA: `SC-10-DATA-04`.

Saved applicant visible DATA and template/default DATA: `SC-10-DATA-05`.

See:

```text
planning/diagrams/scenario-data/SC-10-applicant-data.md
planning/diagrams/scenario-data/SC-10B-my-applicant-parties-data.md
```

## 6. Main Flow — Add ApplicantParty From Account Page

1. Client opens Account page.
2. System shows Applicant Parties section.
3. Top area shows current/default templates grouped by applicant type when available.
4. Lower area shows saved ApplicantParties, including non-default ApplicantParties.
5. Client chooses to add applicant data.
6. Client selects applicant type.
7. Client enters applicant data for the selected type.
8. Client-side validation runs for visible fields.
9. Client corrects applicant data if validation fails.
10. Client submits applicant data.
11. System accepts or rejects applicant data.
12. If accepted, system creates a new saved ApplicantParty linked to the account.
13. New ApplicantParty starts as `NotVerified`.
14. Existing ApplicantParties remain stored and unchanged.
15. If there is no current/default ApplicantParty for that applicant type, the newly created ApplicantParty becomes the initial current/default template for that type.
16. If a current/default ApplicantParty already exists for that type, the existing current/default template remains unchanged.
17. UI shows the new ApplicantParty in the Applicant Parties section.
18. UI highlights current/default templates when present.

## 7. Branches

### First ApplicantParty of a type

```text
-> no current/default ApplicantParty exists for selected type
-> client enters applicant data
-> applicant data is accepted
-> new ApplicantParty is created
-> new ApplicantParty starts NotVerified
-> new ApplicantParty becomes initial current/default template for that type
-> Account page shows it as default/current template and in saved list
```

### Additional ApplicantParty of the same type

```text
-> current/default ApplicantParty already exists for selected type
-> client enters applicant data
-> applicant data is accepted
-> new ApplicantParty is created
-> old ApplicantParty remains stored and unchanged
-> existing current/default template remains selected
-> new ApplicantParty appears in saved list
-> explicit future action can make the new ApplicantParty current/default
```

### Applicant data invalid

```text
-> validation errors are visible
-> invalid ApplicantParty is not created
-> existing ApplicantParties remain unchanged
-> client can correct input and resubmit
```

### Request creation creates ApplicantParty

```text
-> request creation uses new applicant data
-> system creates a new ApplicantParty
-> new ApplicantParty is used for that request
-> if no current/default exists for that type, it becomes initial current/default
-> if current/default already exists, UI may offer to make the new ApplicantParty current/default for future requests
-> existing ApplicantParties remain unchanged
```

## 8. Invariants

Invalid applicant data is not saved.

A client account may have many saved ApplicantParties.

One current/default ApplicantParty may exist per applicant type.

Creating a new ApplicantParty does not automatically replace an existing current/default ApplicantParty of the same type.

Changing current/default when a default already exists is a separate explicit behavior.

ApplicantParty verification is separate from standalone applicant creation.

New ApplicantParty starts as `NotVerified`.

Request review may verify the ApplicantParty used by the request.

Existing requests keep their submitted applicant context when later ApplicantParties are added or default templates change.

## 9. Outcomes

- Client can add applicant data for physical person, individual entrepreneur or legal entity.
- Client can see saved ApplicantParties on Account page.
- Client can see which ApplicantParty is current/default template per type when available.
- Adding ApplicantParty does not remove or overwrite older ApplicantParties.
- First ApplicantParty of a type can initialize the current/default template for that type.
- Additional ApplicantParties of the same type remain saved but do not silently change current/default.
- ApplicantParty can later be used by request creation.

## 10. Questions / Decisions

Accepted direction:

```text
Decision:
A client account may store many ApplicantParty profiles.

Reason:
Older requests and future review/verification history may need older applicant data to remain available.

Consequence:
Adding applicant data is not replacement by default.
```

```text
Decision:
Current/default ApplicantParty is per applicant type.

Reason:
Physical person, individual entrepreneur and legal entity are different applicant contexts.
A single global current applicant does not fit the target model.

Consequence:
Future request creation should prefill from the current/default template for the selected applicant type.
```

```text
Decision:
Creating the first ApplicantParty of a type may initialize the current/default template for that type.

Reason:
If there is no default yet, the first saved ApplicantParty is the natural future prefill.
```

```text
Decision:
Creating an additional ApplicantParty of the same type does not silently change current/default.

Reason:
Changing the user's default template affects future request creation and should be explicit.

Consequence:
A future Select Current/Default ApplicantParty slice owns explicit default changes.
```

Future review questions:

```text
Q: Should delete be hard delete, archive, deactivate or hide?
Q: Can a used ApplicantParty be edited in place, or does edit create a new version/profile?
Q: Should request store ApplicantParty reference, applicant snapshot, or both?
Q: When should a dedicated My Applicant Parties page be introduced instead of Account page inline cards?
```

## 11. Diagram Notes

- Use user-facing wording: Add applicant data / Applicant Parties.
- Do not draw adding ApplicantParty as replacement/deactivation.
- Show current/default as a template marker per applicant type, not as the only saved ApplicantParty.
- Show existing saved ApplicantParties staying visible and unchanged.
- Mark verification as future/review-context behavior, not standalone create behavior.

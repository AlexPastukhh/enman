# Scenario Domain Validation Principles

Status: draft / source-of-truth rule addendum  
Scope: scenario text specs, domain design input, value object discovery, aggregate method discovery

## 1. Purpose

Scenario work must distinguish visible UX validation from server-side/domain validation.

The important domain-design rule is:

```text
Domain models and value objects are the source of truth for data validity and state-transition validity.
```

Client-side validation may improve UX, but it is not authoritative.

Server-side validation should validate input by constructing or applying domain/value-object/domain-service behavior and converting validation results into user-facing error responses.

## 2. Validation Types

### Client-side validation

Purpose:

```text
- early visible feedback;
- avoid unnecessary submit;
- guide user correction;
- keep forms understandable.
```

Examples:

```text
- required field visible error;
- invalid email format visible before submit;
- password and confirmation mismatch;
- missing attachment message;
- wrong applicant type form field message.
```

Client-side validation must not be trusted as final validation.

### Server-side application validation

Purpose:

```text
- protect use case boundary;
- reject invalid commands/requests even if UI validation is bypassed;
- map domain/value-object validation results to response errors;
- coordinate state-transition checks.
```

Examples:

```text
- request creation rejected if ObjectAddress cannot be created;
- applicant data save rejected if Snils/Inn/Ogrn value object cannot be created;
- proposal submission rejected if required document/comment is missing;
- review command rejected if request is not InReview.
```

### Domain / value-object validation

Purpose:

```text
- prevent invalid domain objects;
- prevent invalid value objects;
- prevent invalid state transitions;
- express business invariants near the model that owns them.
```

Examples:

```text
- EmailAddress cannot be created from invalid email.
- ObjectAddress cannot be created from empty/invalid address.
- Snils / Inn / Ogrn / Ogrnip must satisfy their accepted format/rule.
- Request can be approved only from InReview.
- AgreementProposal can be accepted only when AwaitingClientConfirmation.
- Employee replacement turns previous client-sent proposal into Rejected.
```

## 3. Single Source Of Truth Rule

The validation source of truth should be:

```text
Value objects + domain model methods + domain state-transition methods.
```

Client-side validation may mirror these rules for UX, but must not define a separate business truth.

Server-side validation should not duplicate domain rules manually when a value object/domain method can express them.

Recommended implementation direction:

```text
input DTO / form payload
-> application use case
-> create value objects / call domain factory or method
-> collect validation/result errors
-> return server validation response
```

A domain object should not be created in an invalid state.

A domain method should not silently perform an invalid state transition.

## 4. DATA Files Rule

DATA files do not contain validation sections.

DATA files say:

```text
what actor enters / sees / selects / filters by / attaches
```

Scenario text specs and domain design input say:

```text
which of those data items require client-side validation,
server-side validation, value-object construction,
domain invariants, or state-transition checks.
```

## 5. Scenario Text Specs Rule

Scenario text specs should include validation where relevant:

```text
## Client-side validation

## Server-side / Domain validation
```

Use these sections when a scenario:

```text
- saves data;
- changes status;
- creates domain object;
- attaches document/file;
- sends agreement proposal;
- accepts/rejects/approves something;
- starts verification;
- creates recovery context;
- updates credentials;
- submits anonymous contact/request data.
```

## 6. Domain Design Input Rule

Before aggregate design, create a table that extracts:

```text
- value object candidates;
- state/status values;
- domain invariants;
- state-transition methods;
- aggregate owner candidates;
- validation error sources.
```

This table should be derived from:

```text
scenario text specs
scenario DATA files
scenario validation addendum
```

and should not invent fields or rules not supported by scenarios.

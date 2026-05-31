# Domain Value Object Draft — ObjectAddress

Status: draft / extracted with ConnectionRequest aggregate  
Doc version: v0.1.0  
Scope: request object address value integrity

## 1. Purpose

`ObjectAddress` / current implementation `Address` protects the required address value used by a connection request.

It ensures a request cannot be created with a missing or structurally invalid object address.

## 2. Source Inputs

Scenario sources:

```text
planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md
planning/diagrams/scenario-data/SC-04-request-creation-data.md
```

Behavior items:

```text
REQ-IBS-003 — Request has object address
REQ-VI-001 — Object address value integrity
SC-04-BI-002 — Client provides request details and object address
SC-04-BI-012 — Invalid request/applicant data produces feedback and no partial write
```

Existing implementation sources:

```text
Domain.EnergyManagement/DocumentManaging/Address.cs
Domain.EnergyManagement/Requests/ClientRequest.cs
Domain.EnergyManagement/Requests/ConnectionRequest.cs
Tests.EnergyManagement/Domain/Requests/ConnectionRequestCreationTests.cs
Tests.EnergyManagement/Integration/App/Requests/CreateConnectionRequestIntegrationTests.cs
```

Not checked:

```text
Full address formatting/localization/persistence requirements are not audited in this pass.
```

## 3. Used By

Aggregates:

```text
ConnectionRequest
```

Application/API references:

```text
CreateConnectionRequest DTO/application command maps address input into Address before request creation.
```

## 4. Shape / Fields

Current implementation shape:

```text
PostalCode
Region
City
Street
House
Building?
Apartment?
```

Required fields:

```text
PostalCode
Region
City
Street
House
```

Optional fields:

```text
Building
Apartment
```

## 5. Invariants

| Invariant | Source | Failure/error |
|---|---|---|
| Address object is required for request creation. | REQ-IBS-003 / REQ-VI-001 | `RequestObjectAddressIsRequired` |
| Required address fields must not be blank. | SC-04 DATA + current Address implementation | address validation errors |
| Address fields must fit allowed lengths/format. | current Address implementation | address validation errors |

## 6. Creation / Normalization Rules

Creation:

```text
Address is created before ConnectionRequest.Create accepts it.
```

Normalization:

```text
No canonical normalization policy was extracted in this pass.
```

Rejected values:

```text
missing Address object;
blank required fields;
fields exceeding current implementation limits;
invalid postal code according to current Address validation.
```

## 7. Equality Rule

Current equality rule was not audited in this pass.

Target direction:

```text
Two object addresses should compare by value when/if address equality becomes domain-significant.
```

## 8. Validation Boundary

Belongs in value object:

```text
structural address field integrity;
required address components;
field length/format rules.
```

Belongs in DTO/input validation:

```text
transport nullability;
branch-specific request form errors;
localized field messages.
```

Belongs in aggregate/application:

```text
whether an address can be used for this request/application flow;
request creation transaction/no-write guarantees.
```

## 9. Persistence / Serialization Notes

```text
Current implementation uses Domain.EnergyManagement.DocumentManaging.Address.
The domain concept is object address for ConnectionRequest; future cleanup may rename or move the implementation type.
```

## 10. Invalid Examples

| Invalid value/state | Why invalid | Source |
|---|---|---|
| null object address | Request without object address must not exist. | REQ-IBS-003 |
| blank required address field | Missing structural address data. | REQ-VI-001 / current Address validation |
| too-long field | Violates current implementation field limits. | current Address validation |

## 11. Questions / Decisions

Open:

```text
- Should the domain value object be renamed from Address to ObjectAddress to match request language?
- Should address equality/normalization rules be documented after implementation cleanup?
```

Accepted:

```text
- Request creation requires an object address.
- Address integrity belongs to a value object, not just DTO validation.
```

## 12. Source Delta / Change Log

```text
- Extracted as value object draft during ConnectionRequest aggregate extraction.
```

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
- applicant DATA reference or inline applicant DATA, if needed.
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

Notes:

```text
ObjectAddress is already part of the current request model.
RequestedPowerKw is agreement/connection-relevant, but currently planned as expansion rather than current implemented L1.
```

Open questions:

```text
Q: Exact request object/location fields need domain confirmation.
Q: Is requested service type a fixed list or free description?
Q: Is applicant DATA always required before submit?
Q: When should RequestedPowerKw become core scenario DATA?
```

Scenario spec references:

```text
planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md
```

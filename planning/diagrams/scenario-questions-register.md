# Scenario Questions Register

Status: active / applicant-template-per-type synchronized

| ID | Source | Question | Current preference | Status |
|---|---|---|---|---|
| `Q-SC-04-001` | SC-04 / SC-10 | Which applicant context does request creation use? | Explicit Existing selected ApplicantPartyId or New applicant data. | accepted |
| `Q-SC-04-002` | SC-04 | Can Existing branch use non-default ApplicantParty? | Yes. Current/default is only initial prefill/default. | accepted |
| `Q-SC-04-003` | SC-04 | Should New applicant + request be atomic? | Yes. One server command. | accepted |
| `Q-SC-10-001` | SC-10 | Is default global or per type? | Per applicant type. | accepted |
| `Q-SC-10-002` | SC-10 | What happens on create? | First of type may initialize default; additional same-type does not switch. | accepted |

Superseded:

```text
one current active ApplicantParty per account;
request creation target uses server-selected single current active applicant;
ApplicantParty creation is replacement.
```

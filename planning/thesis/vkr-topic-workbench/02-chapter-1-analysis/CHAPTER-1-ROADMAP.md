# Chapter 1 roadmap

## 1. Desired outcome of Chapter 1

After Chapter 1, the reader should understand:

- what process is being automated;
- what problems appear in manual or fragmented handling;
- what automation options exist;
- in what situations different options can be selected;
- what trade-offs own development has;
- why, within the given VKR topic, the following chapters consider development of a custom web application.

Chapter 1 must not prove that custom development is always better than ready-made systems.

## 2. What Chapter 1 must not become

- a technical architecture chapter;
- a backend/frontend implementation chapter;
- a database/API description;
- a claim that own application is universally superior;
- a full ECM/EDO justification;
- a chapter about mock-check implementation.

## 3. Chapter points and roles

| Point | Desired outcome | Role in chapter |
|---|---|---|
| 1.1 Request process | Show the process and request as a linking object | Gives base for all later arguments |
| 1.2 Documents and feedback | Show how approved request leads to document exchange | Connects request to document stage |
| 1.3 Manual/fragmented problems | Show why unmanaged process is risky | Grounds automation need |
| 1.4 Automation options | Compare conditions and trade-offs of different options | Prevents fake "own app is always best" argument |
| 1.5 Direction and requirements | State what requirements will be implemented in the VKR web app | Bridges to design |

## 4. Cross-cutting semantic lines

| Line | Where used | Notes |
|---|---|---|
| Request as linking business-process object | 1.1, 1.3 | It links user, applicant, state, decision and document stage |
| State and allowed actions | 1.1, 1.3 | Use simple language: "rules of allowed actions", not heavy DDD terms |
| Consistency of process data | 1.3, 1.5 | Manual process requires people to track many dependent rules |
| Trade-offs of custom app | 1.4, 1.5 | Own app has control/adaptation but requires development/support |
| Mock-check transfer | Chapter 3 | Not a Chapter 1 semantic point |

## 5. Current decisions

| Decision | Reason | Affects |
|---|---|---|
| 1.1 remains first | Need to introduce automated process before problems/options | Chapter order |
| Chapter 1 may start with short organization context but must quickly move to request process | Prevents generic organization description | 1.1 |
| Mock-check not mentioned in 1.1 | It is implementation/demo | Chapter 3 |
| "Client" is clarified through "user of web app" and "applicant" | Avoids mixing account and person data | 1.1, Chapter 2 |
| Request is described as linking object | Better explains process logic | 1.1, 1.3 |
| Own app analysis is framed through conditions/trade-offs | Avoids artificial proof | 1.4/1.5 |
| Conditions for choosing automation options need research | Avoids weak claims | 1.4/1.5 |

## 6. Transfer notes

| Note | Target | Status |
|---|---|---|
| Request state controls allowed actions | 1.1 / 1.3 / Chapter 2 | captured |
| Manual process problem includes consistency, not only extra actions | 1.3 | captured |
| Ready solutions vs own development should be conditions/trade-offs | 1.4 | captured |
| Need research on when custom development vs ready systems is chosen | 1.4/1.5 | research needed |
| License/subscription/implementation costs may matter when choosing ready solutions | 1.4/1.5 | research needed |
| Own web app is used within VKR topic and can model specific process logic | 1.5 | captured |
| Architecture supports rules, consistency and maintainability | Chapter 2 | captured |
| Mock-check as demonstration/extension point | Chapter 3 | decided |
| Desired-result coverage belongs inside semantic point cards | all topic drafts | workflow update |

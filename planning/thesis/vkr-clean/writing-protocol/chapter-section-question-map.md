# Chapter Section Question Map

Status: draft  
Scope: project-centered questions for future VKR subsections

This file helps keep VKR sections from becoming generic.

## 1. Introduction

| Possible subsection | Project question |
|---|---|
| Relevance | Why is automation of client requests and document flow needed in the ООО «ЗСК» scenario? |
| Goal and tasks | What concrete system must be developed and which project tasks lead to it? |
| Object and subject | Which business process is studied and which software design decisions are the subject? |
| Practical significance | What practical improvement does the application provide for request handling and document preparation? |

## 2. Chapter 1 — Analysis

| Possible subsection | Project question |
|---|---|
| Subject domain | What happens to a client request from submission to decision and document preparation? |
| Problems of manual handling | Which risks appear without a unified system: lost data, unclear status, repeated input, document mismatch? |
| Existing solutions | Which external systems solve similar problems and why are they not a direct replacement for the focused VKR system? |
| Functional scope formation | Which capabilities must be included in the developed application based on the analysis? |
| Justification of custom development | Why is a custom ASP.NET Core + React/TypeScript application justified for a VKR? |

## 3. Chapter 2 — Design

| Possible subsection | Project question |
|---|---|
| Actors and roles | Which actors participate in request handling and which actions must be separated? |
| Functional specification | What are the main user scenarios and their preconditions/postconditions? |
| Request lifecycle | Which states and transitions are needed to control a request? |
| Document lifecycle | When does a document draft appear and why after approval? |
| Domain model | Which domain entities represent the process: account, applicant, request, decision, document, notification? |
| Architecture | How should frontend, backend, application logic, domain and database be separated? |
| API contract | How to prevent backend/frontend contract drift? |
| UI design | Which screens are needed to support client and employee scenarios? |
| Database design | Which data must be stored and how should relationships be represented? |

## 4. Chapter 3 — Implementation And Testing

| Possible subsection | Project question |
|---|---|
| Backend implementation | How are request-handling operations exposed through API and coordinated through handlers? |
| Domain implementation | Which business rules are represented in domain entities and value objects? |
| Persistence implementation | How are accounts, applicants and requests stored? |
| Frontend implementation | Which client flows are implemented and how do they consume API wrappers? |
| API contract artifacts | How are OpenAPI and generated constants produced and used? |
| UI screenshots | Which implemented screens demonstrate the main flows? |
| Testing | Which test level confirms which part of behavior? |
| Limitations | Which planned features are outside the current cut and why? |

## 5. Chapter 4 / Future Work

| Possible subsection | Project question |
|---|---|
| Deployment and operation | How can the application be prepared for deployment and use? |
| Security development | Which security improvements are required for production use? |
| Document workflow expansion | How can the document/agreement scenario be extended? |
| Employee workspace expansion | Which employee functions should be developed next? |
| Notifications | How should email or other notification channels be integrated? |

## 6. Rule

A subsection may be included only if its project question is useful for the VKR goal.

If the question cannot be connected to the ООО «ЗСК» application, the subsection should be removed or rewritten.

# Page Fragment Checklist

Status: draft  
Scope: quick checklist before adding a paragraph/table/figure to VKR text

## 1. Before Adding A Paragraph

Ask:

```text
1. Which project question does this paragraph answer?
2. Does it describe our project, an external fact, or an author conclusion?
3. Is a citation needed?
4. Does it need a diagram/table/screenshot instead of more prose?
5. Is it too generic?
6. Does it overclaim implementation status?
7. Does it repeat a source structure or wording?
```

## 2. Paragraph Labels During Drafting

Use these temporary labels in drafts:

```text
PROJECT:
external-independent project description.

SOURCE:
external fact requiring citation.

ANALYSIS:
author comparison or conclusion.

IMPLEMENTATION:
repo-backed implementation description.

INSERT:
visual/table/code/test material must be inserted.

CHECK REPO:
implementation status must be checked before final text.

TODO SOURCE:
source must be attached before final text.
```

Remove labels from final VKR text.

## 3. Example

Draft:

```text
PROJECT: In the developed system, a client request is the central object connecting the client, applicant, review decision and document draft.

SOURCE: Existing document-management and service-desk systems show that request status and role-based processing are common mechanisms for controlling workflows. TODO SOURCE: add official product/documentation references.

ANALYSIS: For the VKR scope, these mechanisms are implemented in a narrower form, focused on the client request lifecycle rather than a full enterprise-wide workflow platform.

INSERT: request lifecycle diagram.
```

Final direction:

```text
В разрабатываемой системе заявка является центральным объектом, связывающим клиента, заявителя, результат рассмотрения и последующую подготовку документа. При анализе аналогов были выделены общие механизмы контроля обращений: фиксация статуса, разграничение ролей и сохранение результата обработки. В данной ВКР эти механизмы применяются в более узком виде — для управления жизненным циклом клиентской заявки.
```

## 4. Risk Levels

| Risk | Signs | Mitigation |
|---|---|---|
| Low | Project-specific implementation, screenshots, own diagrams | Keep evidence accurate |
| Medium | Comparison of external systems | Use tables, cite facts, write conclusions yourself |
| High | General theory, product descriptions, translated source text | Shorten, cite, rewrite from project question |

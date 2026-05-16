# Repo-Grounded GitHub Line Links Workflow

Status: active workflow rule  
Scope: user-facing answers, implementation/status explanations, code/doc evidence links, prompt generation for other chats

## 1. Purpose

When a chat explains current repo implementation, identifies a problem, reviews a concrete file, or the user asks for a link, the answer should provide GitHub links to the exact relevant line or line range.

A whole-file link is not enough when the answer is about a specific code path, DTO, route, test, workflow rule, status statement or documented decision.

## 2. Default Repository Context

Default repository and branch for this project:

```text
Repository: https://github.com/AlexPastukhh/enman
Branch: my-changes
```

Prefer immutable links with a commit SHA:

```text
https://github.com/AlexPastukhh/enman/blob/<commit-sha>/<path>#L10-L25
```

Branch links are allowed only as a fallback when the current commit SHA is not available:

```text
https://github.com/AlexPastukhh/enman/blob/my-changes/<path>#L10-L25
```

If using a branch link in an evidence-sensitive answer, say that the link may move if the branch changes.

## 3. When Line Links Are Required

Provide Markdown GitHub line links when:

```text
- the user says: дай ссылку / link / show me where / где это;
- the answer describes current code or implementation behavior;
- the answer describes tests/checks that prove behavior;
- the answer identifies a docs/code consistency problem;
- the answer explains why a status is implemented / planned / stale;
- the answer points to a specific workflow rule or responsibility;
- the answer reviews a specific file, slice, scenario, controller, handler, DTO, generated artifact or test.
```

Line links should be used as often as previous whole-file links were used, but the target must be a precise line or range.

## 4. Link Format

Use clickable Markdown text, not a bare URL.

Single line:

```markdown
[planning/README.md, line 61](https://github.com/AlexPastukhh/enman/blob/<commit-sha>/planning/README.md#L61)
```

Line range:

```markdown
[L1Controller.cs, lines 120-143](https://github.com/AlexPastukhh/enman/blob/<commit-sha>/EnergyManagement.Server/L1/Controllers/L1Controller.cs#L120-L143)
```

Preferred label format:

```text
<short path>, line <N>
<short path>, lines <N>-<M>
```

## 5. How To Build A Line Link

Steps:

```text
1. Read the current file from GitHub/repo tooling.
2. Locate the exact line or smallest useful line range.
3. Determine the current branch commit SHA when possible.
4. Build:
   https://github.com/OWNER/REPO/blob/COMMIT_SHA/PATH#LSTART-LEND
5. Wrap it in Markdown.
```

Example:

```markdown
[planning/README.md, lines 59-61](https://github.com/AlexPastukhh/enman/blob/bdeb93c7546ef44df2b19d99e0ce598690fdce54/planning/README.md#L59-L61)
```

## 6. Commit SHA Rule

Preferred source for `<commit-sha>`:

```text
- current checked-out repo: git rev-parse HEAD;
- GitHub branch/ref metadata;
- GitHub commit metadata returned by repository tooling;
- compare/commit API result when it exposes the branch head SHA.
```

If the chat cannot obtain a commit SHA through available tools, use `blob/my-changes` and explicitly treat it as a moving branch link.

Do not invent a commit SHA.

## 7. Precision Rule

Use the smallest line range that supports the statement.

Good:

```text
route mapping line/range
DTO definition line/range
handler branch line/range
test assertion line/range
workflow rule line/range
```

Avoid linking a whole file unless the answer is only file navigation.

If multiple lines are far apart, provide multiple links instead of one huge range.

## 8. Relationship To Tool Citations

Tool/file citations and internal connector citations are useful, but they do not replace user-visible GitHub line links when the user asks for links or when a code/implementation explanation needs repo-grounded evidence.

User-facing answers may include both:

```text
- normal assistant citations, if the platform renders them;
- explicit Markdown GitHub line links for exact repo locations.
```

## 9. Prompt Generation Rule

When creating a prompt for another chat or implementation agent, include this evidence-link rule:

```text
When explaining current repo code/docs or reporting problems, provide GitHub Markdown links to exact lines/ranges. Prefer commit SHA links. If commit SHA is unavailable, use branch links and say they may drift.
```

Do not tell another chat to rely only on whole-file links.

## 10. What Not To Do

```text
- Do not work from memory when providing a repo line link.
- Do not link to a whole file when discussing a specific line/range.
- Do not use a long bare URL in the answer.
- Do not invent line numbers.
- Do not invent commit SHAs.
- Do not use stale links from older discussions without rechecking the current file.
- Do not treat a generated artifact link as proof of source implementation without checking source files when implementation behavior matters.
```

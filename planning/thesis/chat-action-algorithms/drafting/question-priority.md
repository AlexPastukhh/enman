# Algorithm: question priority for drafting

Questions are a production mechanism, not decoration. A chat should ask enough questions, but the questions must be prioritised by usefulness at the current moment.

## Priority classes

### 1. Blocking questions

Without an answer, writing would be unsafe, false or impossible.

Examples:

```text
Is this feature implemented or only planned?
Does email actually work?
Does the agreement stage start automatically or by employee action?
```

### 2. Strong questions

Not blocking, but they significantly improve precision, project specificity and non-generic writing.

Examples:

```text
What is the best example of an approved request leading to document exchange?
Which part of ООО «ЗСК» process should be emphasised?
```

### 3. Research questions

They ask what external source support is needed.

```text
Which research result supports the claim that document exchange should be tied to workflow?
```

### 4. Repo/evidence questions

They prevent overclaiming.

```text
Is this in code, slice draft, test, UI screenshot, or only design?
```

### 5. Visual questions

They decide how a thesis block should be supported visually.

```text
Does this block need a process scheme, table, screenshot or nothing?
```

### 6. Style questions

Useful later during editing. Do not block topic drafting unless style creates misunderstanding.

## Required format

For each important question state:

```text
question;
priority;
why it matters;
what happens if unanswered;
default answer;
alternatives;
where the answer will go in section draft.
```

## Rule

When there are many possible questions, ask first:

```text
blocking → strong → research/repo/visual → style
```

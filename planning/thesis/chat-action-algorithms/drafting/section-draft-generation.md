# Algorithm: section draft generation

Run when the user asks for a section draft.

## Required behavior

1. Find topic draft(s) that feed the section.
2. Extract section draft blocks from topic-draft semantic blocks / semantic points.
3. Check if there are existing chapter drafts that can be used as a resource or section draft candidate.
4. If existing text exists:
   - do not copy it as final;
   - do not paste it into topic draft;
   - treat it as current section draft candidate;
   - reverse-engineer it into semantic points;
   - map it to section blocks;
   - mark what can be reused;
   - send it through topic-draft questions, source support and overclaim checks.
5. For each block check:
   - meaning;
   - purpose in disclosure;
   - order: after what / before what;
   - questions and priorities;
   - research support;
   - repo/evidence need;
   - visual support;
   - overclaim risks;
   - whether current candidate wording should be kept, compressed, expanded, moved or rewritten.
6. First create or update a section skeleton:
   - headings;
   - purpose of each block;
   - questions;
   - plan of disclosure;
   - current text candidate, if available.
7. Then gradually generate/edit text inside blocks.
8. Mark the draft as iterative, not final.
9. Do not write a full section from memory or as one uncontrolled wall of text.

## Section draft role

Section draft is the accumulation place for candidate VKR text. No mandatory fragment bank.

When existing chapters already exist, they can temporarily be the starting section draft candidate. The topic draft then explains and controls how that candidate should be rewritten.

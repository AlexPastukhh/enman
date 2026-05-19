# Algorithm: visual material review

Run when the user sends diagrams, figures, screenshots or visual PDFs.

## Required behavior

1. Determine which VKR subsection the visual belongs to.
2. Check whether the visual is misplaced.
3. Classify purpose:
   - main text;
   - appendix;
   - presentation;
   - chapter 3 implementation screenshot;
   - internal planning only.
4. Check overclaim risks:
   - mock shown as real integration;
   - document reference shown as full ECM;
   - email shown as fully implemented without repo-check;
   - future work shown as current system.
5. Remove/flag internal editor blocks inside final diagrams:
   - “Смысл рисунка”;
   - “Как использовать”;
   - “Для ПЗ лучше таблица”.
6. Recommend: keep / simplify / move to presentation / convert to table / remove from VKR.
7. Provide exact instructions for visual chat if needed.

## Questions every visual should answer

```text
Which section block does it support?
What should the reader understand?
What thesis does it prove?
What must not be shown?
What is the caption?
What text should follow after the figure?
```

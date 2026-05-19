# Algorithm: archive generation and navigation update

Run when the user asks to update docs/planning files by archive.

## Required behavior

1. Check current repo/archive state. Do not work only from memory.
2. Identify add/replace/deprecate/delete files.
3. Perform navigation impact check.
4. Create complete replacement files.
5. Save originals for replacement files in `_archive-review/.../original-files/`.
6. Create MANIFEST and APPLY.
7. Create DELETIONS if files should be removed.
8. Create MERGE-RISK-REPORT and PRE-DELIVERY-CHECK.

## Pre-delivery check

Before giving the archive link, the chat must:

1. Open created zip.
2. Check file list.
3. Check MANIFEST and APPLY exist.
4. Check DELETIONS exists if deletions are needed.
5. Check new files are inside.
6. Check replacement files have originals in `_archive-review`.
7. Check navigation impact check was performed.
8. Check new folders have README or are reflected in navigation.
9. Check legacy/support was not deleted accidentally.
10. In final response list:
    - added files;
    - updated files;
    - deprecated/deleted files;
    - apply command;
    - git diff checks;
    - git add/commit command.

# Algorithm: archive generation and navigation update

Run this when the user asks to create an archive that adds/replaces/removes planning or VKR files.

## Required steps before creating archive

1. Check current repo/archive when available. Do not work from memory only.
2. Determine added files, replacement files, deprecations and deletions.
3. Run navigation impact check.
4. Update source of truth / README / indexes as needed.
5. For replacement files, include originals in `_archive-review/<slug>/original-files/` when available.
6. Create `MANIFEST` and `APPLY`.
7. If files should be removed, create `DELETIONS` and include explicit PowerShell removal commands.

## Pre-delivery check

Before giving the archive link, the chat must:

1. Open the created zip.
2. Check file list.
3. Check that MANIFEST and APPLY are present.
4. Check that new files are really inside.
5. Check that replacement files have originals in `_archive-review` when available.
6. Check that navigation impact check was performed.
7. Check that new folders have README or are listed in navigation.
8. Check that legacy/support folders were not accidentally removed.
9. In the final answer, list:
   - added files;
   - updated files;
   - deprecated/deleted files;
   - application command;
   - git diff check commands;
   - git add/commit commands.

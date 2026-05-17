# APPLY — Domain Draft 02 L2

From repo root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\domain-draft-02-l2-full.zip" -DestinationPath "." -Force
```

Verify file exists:

```powershell
Test-Path .\planning\tables\domain-drafts\domain-draft-02.md
```

Open it:

```powershell
code .\planning\tables\domain-drafts\domain-draft-02.md
```

This archive adds `domain-draft-02.md` and does not replace `domain-draft-01.md`.

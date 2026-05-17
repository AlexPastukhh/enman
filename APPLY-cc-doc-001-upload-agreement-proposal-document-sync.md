# Apply CC-DOC-001 Upload Agreement Proposal Document Sync

From the repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\cc-doc-001-upload-agreement-proposal-document-sync.zip" -DestinationPath . -Force
.\APPLY-cc-doc-001-upload-agreement-proposal-document-sync.ps1
git status
git diff -- planning
```

If the diff is correct:

```powershell
git add planning APPLY-cc-doc-001-upload-agreement-proposal-document-sync.ps1 APPLY-cc-doc-001-upload-agreement-proposal-document-sync.md MANIFEST-cc-doc-001-upload-agreement-proposal-document-sync.md
git status
```

This is docs-only. It does not modify runtime code, tests or generated artifacts.

# L1 Applicant Party Read Current Slice Docs Archive

This archive contains planning slice documentation for the current applicant party read flow.

Files:

```text
planning/slices/l1/L1-APPLICANT-PARTY-READ-CURRENT.md
planning/slices/l1/L1-APPLICANT-PARTY-READ-CURRENT.client.md
```

`L1-APPLICANT-PARTY-READ-CURRENT.md` is the full backend/API slice file.

`L1-APPLICANT-PARTY-READ-CURRENT.client.md` is the client sidecar for Account page integration after the backend endpoint exists.

Apply from the repository root:

```powershell
Expand-Archive "C:\Users\alexa\Downloads\enman-l1-applicant-party-read-current-slice-docs.zip" -DestinationPath . -Force
powershell -ExecutionPolicy Bypass -File .\apply-l1-applicant-party-read-current-slice.ps1
```

Or manually copy the files into the same paths under the repository root.

This archive changes documentation only.

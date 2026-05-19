# Apply playwright-locator-cleanup-v1.4

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\playwright-locator-cleanup-v1.4.zip" -DestinationPath "." -Force
```

This archive replaces E2E/test-tooling files and also writes review material to:

```text
_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/
```

Original versions of replaced files are preserved under:

```text
_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/original-files/<same-project-path>
```

After applying, run the validation commands listed in `README.playwright-locator-cleanup-v1.4.md`.

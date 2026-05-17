# Apply L2 Scenario / Slice Follow-up Cleanup

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-scenario-slice-followup-cleanup-sync.zip" -DestinationPath . -Force
.\APPLY-l2-scenario-slice-followup-cleanup.ps1
git status
git diff -- planning
```

Then run the verification greps printed by the script.

If the diff is correct:

```powershell
git add planning
git status
```

No runtime code, no tests and no generated artifacts are included.

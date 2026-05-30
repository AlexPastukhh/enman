# APPLY — ConnectionRequest Domain Aggregate Extraction

Status: replacement package apply instructions  
Scope: add the second aggregate extraction and update domain discovery indexes

## Files

This package adds:

```text
planning/domain/aggregates/connection-request.md
planning/domain/value-objects/object-address.md
planning/domain/value-objects/rejection-feedback.md
```

This package replaces:

```text
planning/domain/scenario-to-aggregate-map.md
planning/domain/domain-notes-register.md
planning/domain/aggregates/README.md
planning/domain/value-objects/README.md
```

## Apply from repo root

```powershell
git fetch origin
git checkout my-changes
git pull --ff-only origin my-changes

$archive = "C:\Users\alexa\Downloads\enman-domain-connection-request-extraction-package.zip"
$tmp = Join-Path $env:TEMP "enman-domain-connection-request-extraction-package-apply"
Remove-Item $tmp -Recurse -Force -ErrorAction SilentlyContinue
New-Item -ItemType Directory -Path $tmp | Out-Null
Expand-Archive -Path $archive -DestinationPath $tmp -Force
Copy-Item -Path (Join-Path $tmp "replacement-files\*") -Destination . -Recurse -Force

git status
git diff --name-only -- `
  planning/domain/aggregates/connection-request.md `
  planning/domain/value-objects/object-address.md `
  planning/domain/value-objects/rejection-feedback.md `
  planning/domain/scenario-to-aggregate-map.md `
  planning/domain/domain-notes-register.md `
  planning/domain/aggregates/README.md `
  planning/domain/value-objects/README.md
```

## Commit

```powershell
git add `
  planning/domain/aggregates/connection-request.md `
  planning/domain/value-objects/object-address.md `
  planning/domain/value-objects/rejection-feedback.md `
  planning/domain/scenario-to-aggregate-map.md `
  planning/domain/domain-notes-register.md `
  planning/domain/aggregates/README.md `
  planning/domain/value-objects/README.md

git commit -m "docs: extract connection request domain aggregate"
git push origin my-changes
```

Do not use `git add .` if the working tree has unrelated local changes.

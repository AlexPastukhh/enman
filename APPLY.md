# APPLY: enman-vkr-chapter1-topic-drafts-update-v1

## Simple apply

From repository root:

```powershell
cd "C:\enman\enman"

Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-chapter1-topic-drafts-update-v1.zip" -DestinationPath . -Force

git status
```

## Check diffs

```powershell
git diff -- planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/01-domain-process/01-client-request-process.topic.md
git diff -- planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/04-automation-options/01-existing-solutions-and-custom-development.topic.md
```

## Add and commit

```powershell
git add planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/01-domain-process/01-client-request-process.topic.md `
        planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/04-automation-options/01-existing-solutions-and-custom-development.topic.md `
        MANIFEST.md `
        APPLY.md

git commit -m "Update VKR Chapter 1 topic drafts"
```

## Safer apply without overwriting root MANIFEST/APPLY names

```powershell
cd "C:\enman\enman"

$tmp = "$env:TEMP\vkr-chapter1-topic-drafts-update-v1"
Remove-Item $tmp -Recurse -Force -ErrorAction SilentlyContinue

Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-chapter1-topic-drafts-update-v1.zip" -DestinationPath $tmp -Force

Copy-Item "$tmp\planning" "." -Recurse -Force
Copy-Item "$tmp\MANIFEST.md" ".\MANIFEST.vkr-chapter1-topic-drafts-update-v1.md" -Force
Copy-Item "$tmp\APPLY.md" ".\APPLY.vkr-chapter1-topic-drafts-update-v1.md" -Force

git status
```

Then:

```powershell
git add planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/01-domain-process/01-client-request-process.topic.md `
        planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/04-automation-options/01-existing-solutions-and-custom-development.topic.md `
        MANIFEST.vkr-chapter1-topic-drafts-update-v1.md `
        APPLY.vkr-chapter1-topic-drafts-update-v1.md

git commit -m "Update VKR Chapter 1 topic drafts"
```

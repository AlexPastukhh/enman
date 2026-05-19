# APPLY — vkr-chapter2-intermediate-result-after-harvest-v1

## PowerShell apply command

```powershell
cd "C:\enman\enman"

$zip = "C:\Users\alexa\Downloads\vkr-chapter2-intermediate-result-after-harvest-v1.zip"
$tmp = "$env:TEMP\vkr-chapter2-intermediate-result-after-harvest-v1"

Remove-Item $tmp -Recurse -Force -ErrorAction SilentlyContinue
Expand-Archive -Path $zip -DestinationPath $tmp -Force

Copy-Item "$tmp\planning" "." -Recurse -Force
Copy-Item "$tmp\_archive-review" "." -Recurse -Force
Copy-Item "$tmp\MANIFEST.vkr-chapter2-intermediate-result-after-harvest-v1.md" ".\MANIFEST.vkr-chapter2-intermediate-result-after-harvest-v1.md" -Force
Copy-Item "$tmp\APPLY.vkr-chapter2-intermediate-result-after-harvest-v1.md" ".\APPLY.vkr-chapter2-intermediate-result-after-harvest-v1.md" -Force

git status
```

## Check

```powershell
git diff -- planning/thesis/vkr-topic-workbench/03-chapter-2-design/_chapter2-planning-context-harvest-v1
git status
```

## Commit

```powershell
git add planning/thesis/vkr-topic-workbench/03-chapter-2-design/_chapter2-planning-context-harvest-v1/chapter-2-skeleton-v1.md `
        planning/thesis/vkr-topic-workbench/03-chapter-2-design/_chapter2-planning-context-harvest-v1/06a-visual-chat-figure-briefs.md `
        planning/thesis/vkr-topic-workbench/03-chapter-2-design/_chapter2-planning-context-harvest-v1/10-intermediate-result-and-next-steps.md `
        _archive-review/vkr-chapter2-intermediate-result-after-harvest-v1/ `
        MANIFEST.vkr-chapter2-intermediate-result-after-harvest-v1.md `
        APPLY.vkr-chapter2-intermediate-result-after-harvest-v1.md

git commit -m "Save VKR Chapter 2 intermediate planning result"
```

# APPLY — enman-vkr-topic-workbench-v1

This archive is add-only.

## Apply

```powershell
cd "C:\enman\enman"
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-topic-workbench-v1.zip" -DestinationPath . -Force
git status
```

## Inspect

```powershell
git diff -- planning/thesis/vkr-topic-workbench
```

## Add to git

```powershell
git add planning/thesis/vkr-topic-workbench MANIFEST.md APPLY.md
```

If you do not want to commit archive helper files:

```powershell
git add planning/thesis/vkr-topic-workbench
```

## Commit suggestion

```powershell
git commit -m "Add VKR topic workbench"
```

## Safety note

The archive does not contain replacement copies of existing repository files. It should not overwrite the fresh commit except if a same path was created independently after archive generation.

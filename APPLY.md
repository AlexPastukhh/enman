# Apply — enman-vkr-section-reviewer-workflow-docs-v1.zip

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-section-reviewer-workflow-docs-v1.zip" -DestinationPath . -Force
git status
```

Suggested review:

```powershell
git diff -- vkr-clean/README.md
git diff -- vkr-clean/vkr-materials-index.md
git diff -- vkr-clean/section-drafts/README.md
git diff -- vkr-clean/section-drafts/vkr-section-drafting-workflow.md
git diff -- vkr-clean/section-drafts/reviewer-workflow.md
git diff -- vkr-clean/section-drafts/reviewer-prompts.md
git diff -- vkr-clean/section-drafts/fragment-bank.md
git diff -- vkr-clean/section-drafts/section-draft-register.md
```

Suggested add:

```powershell
git add vkr-clean/README.md vkr-clean/vkr-materials-index.md vkr-clean/section-drafts/README.md vkr-clean/section-drafts/vkr-section-drafting-workflow.md vkr-clean/section-drafts/reviewer-workflow.md vkr-clean/section-drafts/reviewer-prompts.md vkr-clean/section-drafts/fragment-bank.md vkr-clean/section-drafts/section-draft-register.md
```

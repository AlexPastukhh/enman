$ErrorActionPreference = "Stop"

$old = "planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md"
$deprecatedDir = "planning/diagrams/scenario-text-specs/deprecated"
$deprecated = "$deprecatedDir/scenario-server-domain-validation-addendum.deprecated.md"

if (Test-Path $old) {
    New-Item -ItemType Directory -Force -Path $deprecatedDir | Out-Null
    $content = Get-Content -Raw -Path $old
    $prefix = @"
# Deprecated — Scenario Server Domain Validation Addendum

Status: deprecated / archived from active L2 source-of-truth path

This file is retained only for historical context.

Do not use it as active L2 guidance when it conflicts with:
- scenario-local validation sections;
- scenario clarifications;
- concrete slice docs;
- current L2 domain direction.

See:
- planning/diagrams/scenario-clarifications/L2-validation-and-agreement-exchange-source-cleanup.md

---

"@
    Set-Content -Path $deprecated -Value ($prefix + $content) -Encoding UTF8
    Remove-Item -Path $old
}

Write-Host "L2 validation cleanup sync applied."

$ErrorActionPreference = "Stop"

function Replace-InFile {
    param(
        [Parameter(Mandatory = $true)][string]$Path,
        [Parameter(Mandatory = $true)][string]$Old,
        [Parameter(Mandatory = $true)][string]$New
    )

    if (-not (Test-Path $Path)) {
        return
    }

    $content = Get-Content -LiteralPath $Path -Raw
    if ($content.Contains($Old)) {
        $content = $content.Replace($Old, $New)
        Set-Content -LiteralPath $Path -Value $content -NoNewline
    }
}

$oldDashboardDir = "energymanagement.client/src/pages/employee/dashboard"
$newDashboardDir = "energymanagement.client/src/pages/employee/requests/dashboard"

New-Item -ItemType Directory -Force -Path $newDashboardDir | Out-Null

if (Test-Path $oldDashboardDir) {
    Get-ChildItem -LiteralPath $oldDashboardDir -Force | ForEach-Object {
        Move-Item -LiteralPath $_.FullName -Destination $newDashboardDir -Force
    }
    Remove-Item -LiteralPath $oldDashboardDir -Recurse -Force
}

# Runtime import path sync after moving one level deeper.
Replace-InFile `
    -Path "energymanagement.client/src/app/router/router.tsx" `
    -Old '../../pages/employee/dashboard/EmployeeDashboardPage' `
    -New '../../pages/employee/requests/dashboard/EmployeeDashboardPage'

Replace-InFile `
    -Path "$newDashboardDir/EmployeeDashboardPage.tsx" `
    -Old '"../../../entities/' `
    -New '"../../../../entities/'
Replace-InFile `
    -Path "$newDashboardDir/EmployeeDashboardPage.tsx" `
    -Old '"../../../shared/' `
    -New '"../../../../shared/'

Replace-InFile `
    -Path "$newDashboardDir/EmployeeRequestDashboardFilters.tsx" `
    -Old '"../../../entities/' `
    -New '"../../../../entities/'

Replace-InFile `
    -Path "$newDashboardDir/model/employeeDashboardUrlFilters.ts" `
    -Old '"../../../../entities/' `
    -New '"../../../../../entities/'

# Docs sync for the new request-area page grouping.
Replace-InFile `
    -Path "planning/slices/l2/L2-EMP-DASH-001-employee-request-dashboard.client.md" `
    -Old 'pages/employee/dashboard/EmployeeDashboardPage.tsx' `
    -New 'pages/employee/requests/dashboard/EmployeeDashboardPage.tsx'

$dashDoc = "planning/slices/l2/L2-EMP-DASH-001-employee-request-dashboard.client.md"
if (Test-Path $dashDoc) {
    $content = Get-Content -LiteralPath $dashDoc -Raw
    if (-not $content.Contains("Page placement:")) {
        $content = $content.Replace(
@'
Employee account identity:
  Target L2 model uses Employee : Account.
  ClaimTypes.NameIdentifier stores Account.Id, which is Employee.Id for Employee sessions.
  Client must never submit employeeId; server derives Employee actor from session.
'@,
@'
Employee account identity:
  Target L2 model uses Employee : Account.
  ClaimTypes.NameIdentifier stores Account.Id, which is Employee.Id for Employee sessions.
  Client must never submit employeeId; server derives Employee actor from session.

Page placement:
  Employee request-area pages are grouped under pages/employee/requests/*.
  Dashboard page lives in pages/employee/requests/dashboard.
  Details page lives in pages/employee/requests/details.
  Do not place Employee request dashboard under pages/employee/dashboard.
'@)
        Set-Content -LiteralPath $dashDoc -Value $content -NoNewline
    }
}

$l2Readme = "planning/slices/l2/README.md"
if (Test-Path $l2Readme) {
    $content = Get-Content -LiteralPath $l2Readme -Raw
    if (-not $content.Contains("## 3. Employee Request Page Placement Rule")) {
        $insert = @'
## 3. Employee Request Page Placement Rule

Employee request-area pages are grouped under:

```text
pages/employee/requests/*
```

Target placement:

```text
pages/employee/requests/dashboard
pages/employee/requests/details
```

Do not use a separate page area such as:

```text
pages/employee/dashboard
```

'@
        $content = $content.Replace("Examples:`r`n", $insert + "Examples:`r`n")
        $content = $content.Replace("## 3. Current L2 Drafted Slices", "## 4. Current L2 Drafted Slices")
        $content = $content.Replace("## 4. Employee Details Client Placement Note", "## 5. Employee Details Client Placement Note")
        $content = $content.Replace("## 5. Current Guardrails", "## 6. Current Guardrails")
        Set-Content -LiteralPath $l2Readme -Value $content -NoNewline
    }
}

$implNotes = "planning/slices/slice-implementation-notes-register.md"
if (Test-Path $implNotes) {
    $content = Get-Content -LiteralPath $implNotes -Raw
    if (-not $content.Contains("IMPL-L2-EMP-PAGE-PLACEMENT-001")) {
        $content = $content.TrimEnd() + "`r`n| ``IMPL-L2-EMP-PAGE-PLACEMENT-001`` | Employee request pages | Employee request-area pages live under ``pages/employee/requests/*``: dashboard in ``pages/employee/requests/dashboard``, details in ``pages/employee/requests/details``. Do not place dashboard under ``pages/employee/dashboard``. | accepted |`r`n"
        Set-Content -LiteralPath $implNotes -Value $content -NoNewline
    }
}

Write-Host "L2 Employee request page placement sync applied."

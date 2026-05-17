# MANIFEST

Archive: enman-l1commands-full-fix.zip

Purpose: restore full EnergyManagement.Server/L1/Application/Commands/L1Commands.cs after reject archive accidentally removed approve command definitions.

Included file:
- EnergyManagement.Server/L1/Application/Commands/L1Commands.cs

The file includes:
- existing L1 auth/applicant/request commands
- EmployeeStartRequestReviewCommand block
- EmployeeApproveRequestReviewCommand block restored from previous commit
- EmployeeRejectRequestReviewCommand block from reject implementation

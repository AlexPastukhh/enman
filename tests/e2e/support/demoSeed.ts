import { execFileSync } from "node:child_process";

export const demoCredentials = {
  employeeEmail: "e2e.employee@example.com",
  clientEmail: "e2e.client@example.com",
  password: "ValidPassword111!",
} as const;

export const demoRequestIds = {
  review: 9004,
  agreement: 9005,
} as const;

export const demoText = {
  reviewRequestDetails: "E2E employee review request.",
  agreementRequestDetails: "E2E agreement exchange request.",
} as const;

export function seedE2eDemoData() {
  execFileSync(
    "dotnet",
    ["run", "--project", "EnergyManagement.Tools", "--", "seed-e2e-demo-data"],
    {
      cwd: process.cwd(),
      stdio: "inherit",
      env: process.env,
    },
  );
}

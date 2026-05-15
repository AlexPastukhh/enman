import { defineConfig, devices } from "@playwright/test";

const isCI = !!process.env.CI;
// Keep in sync with TestDatabaseDefaults.LocalDbConnectionString.
const testDatabaseConnectionString =
  "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TestEnergyManagement;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

export default defineConfig({
  testDir: "./tests/e2e",
  timeout: 30_000,

  workers: 1,

  forbidOnly: isCI,
  retries: isCI ? 1 : 0,

  reporter: [
    ["list"],
    ["html", { open: "never" }],
  ],

  use: {
    baseURL: "https://localhost:5173",
    headless: true,
    ignoreHTTPSErrors: true,
    viewport: { width: 1280, height: 720 },
    trace: "on-first-retry",
    screenshot: "only-on-failure",
  },

  projects: [
    {
      name: "chromium",
      use: { ...devices["Desktop Chrome"] },
    },
  ],

  webServer: [
    {
      name: "backend",
      command:
        "dotnet run --project EnergyManagement.Server/EnergyManagement.Server.csproj --no-launch-profile -- --urls https://localhost:7250",
      url: "https://localhost:7250/swagger/index.html",
      reuseExistingServer: !isCI,
      timeout: 120_000,
      ignoreHTTPSErrors: true,
      stdout: "pipe",
      stderr: "pipe",
      env: {
        ASPNETCORE_ENVIRONMENT: "Development",
        ConnectionStrings__ManagementDb: testDatabaseConnectionString,
      },
    },
    {
      name: "frontend",
      command: "npm --prefix energymanagement.client run dev -- --host 127.0.0.1",
      url: "https://127.0.0.1:5173",
      reuseExistingServer: !isCI,
      timeout: 120_000,
      ignoreHTTPSErrors: true,
      stdout: "pipe",
      stderr: "pipe",
    },
  ],
});
